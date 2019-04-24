using Sistema_Advocacia.Context;
using Sistema_Advocacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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






        //Perguntas 
        //0-estrai todas perguntas de uma Petição Anotada
        public List<Pergunta> ExtrairPerguntas(string Texto)
        {
            var perguntas = new List<Pergunta>();
            string PadraoTrecho = @"\{([^&]*)[^\}]*\}";
            string PadraoPergunta = @"\[([^\]]*)\]";

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
                    //perguntas.Add(new Pergunta { TituloTrecho = titulo, pergunta = perguntaEncontrada.Value });
                    perguntas.Add(new Pergunta { TituloTrecho = titulo, pergunta = perguntaEncontrada.Groups[1].Value});
                }

            }
            return perguntas;
        }

        //1-Verifica se há perguntas iguais ou se há titulos iguais
        public string ValidarExtrairPeguntas(List<Pergunta> perguntas)
        {
            string mensagemErro = null;
            var perguntasRepetidas = perguntas.Select(p => p.pergunta).Distinct();

            if ((perguntas.Count > 0) && (perguntas.Count() > perguntasRepetidas.Count()))
            {
                mensagemErro = "Erro: uma pergunta não pode ser identica a outra em uma mesma petição";
                return mensagemErro;
            }

            return mensagemErro;
        }

        //2-Retorna o trecho inteiro de uma determinada pergunta pra 
        public string GetExemplo(string peticao, string titulo)
        {

            string padrao = titulo + @"&([^\}]*)";//[^\}]*
            //string padrao =  @"titulo2&([^\}]*)";//[^\}]*
            Regex regex = new Regex(padrao);
            return regex.Match(peticao).Groups[1].Value;

        }

        //3-Gere o questionário
        public void GerarQuestionarioNoBD(int processoPeticaoId)
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

        //04-Montar petição
        public string MontarPeticao(string peticao, List<Questionario> questionarios)
        {            
            StringBuilder peticaoFinalizada = new StringBuilder();
            peticaoFinalizada.Append(peticao) ;
            if (peticao == null || questionarios == null)
                return null;

            //substitua perguntas por respostas
            foreach (var questionario in questionarios)
            {
                peticaoFinalizada.Replace(questionario.Pergunta, questionario.Resposta);                
            }

            //remova as anotações feitas petição
            //string padrao = @"[&\{\}\[\]]";
            string padrao = @"[&\{\}]";
            var peticaoSemMarcadores =Regex.Replace(peticaoFinalizada.ToString(), padrao, "");

            return peticaoSemMarcadores;
            //var texto = peticaoFinalizada.ToString();
            //var texto = peticaoFinalizada.ToString();

            /*            
            peticaoFinalizada.Replace("{", "");
            peticaoFinalizada.Replace("}", "");
            peticaoFinalizada.Replace("&", "");
            peticaoFinalizada.Replace("[", "");
            peticaoFinalizada.Replace("]", "");

            
            */

            //return peticaoFinalizada.ToString();

        }

        //ANEXOS
        //0-extrai anexos da petição dada como parametro
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

        //1-Remove da lista de anexos, trecho com questões não respondidas
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

        //2-Inseri anexos da Petição no banco de dados após removido anexos de trechos de questões não respondidas.
        public void GerarAnexosNoBD(string peticao, List<Questionario> questionariosRespondidos)
        {
            //var templatePath = System.Web.HttpContext.Current.Server.MapPath("~/Templates/report.xlsx");

            var anexosDoQuestionario = ExtrairAnexosDoQuestionario(peticao, questionariosRespondidos);
            var processoId = (int)questionariosRespondidos.First().ProcessoPeticao.ProcessoId;

            foreach (var anexoDoQuestionario in anexosDoQuestionario)
            {
                if (!db.ProcessoDocumentoes.Any(pd => pd.ProcessoId == processoId && pd.DocumentoId == anexoDoQuestionario.AnexoId))
                    db.ProcessoDocumentoes.Add(new ProcessoDocumento { DocumentoId = anexoDoQuestionario.AnexoId, ProcessoId = processoId });
            }

            db.SaveChanges();
        }


        //Gerar Questionário
        public void GerarQuestionarioNoBD2(int processoPeticaoId)
        {
            ProcessoPeticao processoPeticao = db.ProcessoPeticaos.Find(processoPeticaoId);            
            string TextoPeticao = processoPeticao.PeticaoModelo.PeticaoModificada;

            if (TextoPeticao == null)
                return;

            var perguntasEncontradas = new Regex(@"\[.*?\]").Matches(TextoPeticao);

            //System.Diagnostics.Debug.WriteLine("******************************peticaoModelo.Nome" + processoPeticao.PeticaoModelo.Nome);

            foreach (Match pergunta in perguntasEncontradas)
            {                
                db.Questionarios.Add(new Questionario
                {
                    ProcessoPeticaoId = processoPeticao.ProcessoPeticaoId,                    
                    Pergunta = pergunta.Value,
                    DataModificacao = DateTime.Today
                });
            }
            db.SaveChanges();
        }



        /*
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

        */

    }
}