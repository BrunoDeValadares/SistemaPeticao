using Sistema_Advocacia.Context;
using Sistema_Advocacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Sistema_Advocacia.gerador
{
    public class Pergunta
    {

        public string TituloTrecho { get; set; }
        public string pergunta { get; set; }

    }
    public class Trecho
    {
        public string trecho { get; set; }
        public string Titulo { get; set; }
    }
    public class Anexo
    {
        public string TituloTrecho { get; set; }
        //public string anexo { get; set; }
        public int AnexoId { get; set; }
    }

    public class GerarQuestionario
    {
        private DBContext db = new DBContext();


        public List<Pergunta> ExtrairPerguntas(string Texto)
        {
            var perguntas = new List<Pergunta>();
            string PadraoTrecho = @"\{([^&]*)[^\}]*\}";
            string PadraoPergunta = @"\[[^\]]*\]";

            Regex RgxTrecho = new Regex(PadraoTrecho);
            var encontrados = RgxTrecho.Matches(Texto);

            foreach (Match trechoEncontrado in encontrados)
            {
                var trecho = trechoEncontrado.Groups[0].Value;
                var titulo = trechoEncontrado.Groups[1].Value;

                var RegexPergunta = new Regex(PadraoPergunta);
                var PerguntasEncontradas = RegexPergunta.Matches(trecho);

                foreach (Match perguntaEncontrada in PerguntasEncontradas)
                {
                    perguntas.Add(new Pergunta { TituloTrecho = titulo, pergunta = perguntaEncontrada.Value });
                }             

            }
            return perguntas;
        }

        public string ValidarExtrairPeguntas(List<Pergunta> perguntas)
        {
            //string mensagemErro = null;
            var perguntasRepetidas = perguntas.Select(p => p.pergunta).Distinct();

            if ((perguntas.Count > 0) && (perguntas.Count() > perguntasRepetidas.Count()))
                // return "Erro: uma pergunta não pode ser identica a outra em uma mesma petição";
                throw new System.Exception("Erro: uma pergunta não pode ser identica a outra em uma mesma petição");

            return "ok";
        }


        public string GetExemplo(string peticao, string titulo)
        {

            string padrao = titulo + @"&([^\}]*)";//[^\}]*


            //string padrao =  @"titulo2&([^\}]*)";//[^\}]*
            Regex regex = new Regex(padrao);
            return regex.Match(peticao).Groups[1].Value;           

        }

        public void CriarQuestionario(int processoPeticaoId)
        {
            ProcessoPeticao processoPeticao = db.ProcessoPeticaos.Find(processoPeticaoId);
            //PeticaoModelo peticaoModelo = db.PeticaoModeloes.Find(peticaoModeloId);
            string TextoPeticao = processoPeticao.PeticaoModelo.PeticaoModificada;

            if (TextoPeticao == null)
                return;

            List<Pergunta> perguntas = ExtrairPerguntas(TextoPeticao);

            //System.Diagnostics.Debug.WriteLine("******************************peticaoModelo.Nome" + processoPeticao.PeticaoModelo.Nome);

            foreach (var pergunta in perguntas)
            {
                db.Questionarios.Add(new Questionario
                {
                    ProcessoPeticaoId = processoPeticao.ProcessoPeticaoId,
                    TituloTrecho = pergunta.TituloTrecho,
                    Pergunta = pergunta.pergunta,
                    DataModificacao = DateTime.Today
                });
            }
            db.SaveChanges();
        }





        public string CriarQuestionario2(int processoPeticaoId)
        {
            ProcessoPeticao processoPeticao = db.ProcessoPeticaos.Find(processoPeticaoId);
            //PeticaoModelo peticaoModelo = db.PeticaoModeloes.Find(peticaoModeloId);
            string TextoPeticao = processoPeticao.PeticaoModelo.PeticaoModificada;

            if (TextoPeticao == null)
                return "Erro: petição nula";

            List<Pergunta> perguntas = ExtrairPerguntas(TextoPeticao);

            //System.Diagnostics.Debug.WriteLine("******************************peticaoModelo.Nome" + processoPeticao.PeticaoModelo.Nome);

            foreach (var pergunta in perguntas)
            {
                db.Questionarios.Add(new Questionario
                {
                    ProcessoPeticaoId = processoPeticao.ProcessoPeticaoId,
                    TituloTrecho = pergunta.TituloTrecho,
                    Pergunta = pergunta.pergunta,
                    DataModificacao = DateTime.Today
                });


            }
            db.SaveChanges();
            return "salvo";
        }



                       //ANEXOS
        //extrai anexos da petição dada como parametro
        public List<Anexo> ExtrairAnexosPeticao(string peticao)
        {
            var anexos = new List<Anexo>();
            string PadraoTrecho = @"\{([^&]*)[^\}]*\}";
            string PadraoAnexo = @"doc_[0-9]*";

            Regex RgxTrecho = new Regex(PadraoTrecho);
            var encontrados = RgxTrecho.Matches(peticao);

            //percorra cada trecho encontrado
            foreach (Match trechoEncontrado in encontrados)
            {
                var trecho = trechoEncontrado.Groups[0].Value;
                var titulo = trechoEncontrado.Groups[1].Value;

                //extraia os anexos contidos no trecho                
                Regex regexAnexo = new Regex(PadraoAnexo);
                var anexosEncontrados = regexAnexo.Matches(trechoEncontrado.Value);
                foreach (Match anexoEncontrado in anexosEncontrados)
                {
                    int anexoId = int.Parse(anexoEncontrado.Value.Substring(4));
                    anexos.Add(new Anexo { TituloTrecho = titulo, AnexoId = anexoId });
                }
            }
            return anexos.Distinct().ToList();
        }

        //Remove da lista de anexos, trecho com questões não respondidas
        public List<Anexo> ExtrairAnexosDoQuestionario(string peticao, List<Questionario> questionariosRespondidos)
        {
            var anexosPeticao = ExtrairAnexosPeticao(peticao);
            var anexosQuestionario = anexosPeticao;

            foreach (var anexoPeticao in anexosPeticao)
            {
                //se o titulo do trecho não tiver nenhuma resposta no questionario, exclua os anexos vinculados ao trecho. 
                if (!questionariosRespondidos.Any(q => q.TituloTrecho == anexoPeticao.TituloTrecho && q.Resposta != null))
                {
                    //anexosQuestionario.RemoveAll(a => a.TituloTrecho == anexoPeticao.TituloTrecho); 
                    anexosQuestionario = anexosQuestionario.Where(aq => aq.TituloTrecho != anexoPeticao.TituloTrecho).ToList();
                }
            }
            return anexosQuestionario.ToList();
        }

        //Inseri anexos da Petição no banco de dados após removido anexos de trechos de questões não respondidas.
        public void GerarAnexosNoBD(string peticao, List<Questionario> questionariosRespondidos)
        {
            var anexosDoQuestionario = ExtrairAnexosDoQuestionario(peticao, questionariosRespondidos);
            var processoId = (int)questionariosRespondidos.First().ProcessoPeticao.ProcessoId;

            foreach (var anexoDoQuestionario in anexosDoQuestionario)
            {
                if (!db.ProcessoDocumentoes.Any(pd => pd.ProcessoId == processoId && pd.DocumentoId == anexoDoQuestionario.AnexoId))
                    db.ProcessoDocumentoes.Add(new ProcessoDocumento { DocumentoId = anexoDoQuestionario.AnexoId, ProcessoId = processoId });
            }

            db.SaveChanges();
        }

        //apagar
        public void GerarAnexosProcesso(List<Questionario> questionariosRespondidos)
        {
            Questionario questionario = questionariosRespondidos.First();
            int processoId = (int)questionario.ProcessoPeticao.ProcessoId;

            string peticao = questionario.ProcessoPeticao.PeticaoModelo.PeticaoModificada;
            var anexosPeticao = ExtrairAnexosPeticao(peticao);

            foreach (var anexoPeticao in anexosPeticao)
            {
                //se o titulo do trecho não tiver nenhuma resposta no questionario, exclua os anexos vinculados ao trecho. 
                if (!questionariosRespondidos.Any(q => q.TituloTrecho == anexoPeticao.TituloTrecho && q.Resposta != null))
                {
                    anexosPeticao.RemoveAll(a => a.TituloTrecho == anexoPeticao.TituloTrecho);
                }
            }
            //inclua no bd se já não estiver incluido
            foreach (var anexoPeticao in anexosPeticao)
            {
                if (!db.ProcessoDocumentoes.Any(pd => pd.ProcessoId == processoId && pd.ProcessoDocumentoId == anexoPeticao.AnexoId))
                    db.ProcessoDocumentoes.Add(new ProcessoDocumento { ProcessoId = processoId, DocumentoId = anexoPeticao.AnexoId });
            }
            db.SaveChanges();
        }




    }
}