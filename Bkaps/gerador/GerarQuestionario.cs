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
    public class GerarQuestionario
    {
        private DBContext db = new DBContext();



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




        public void CriarQuestionario3(int peticaoModeloId)
        {            
            PeticaoModelo peticaoModelo = db.PeticaoModeloes.Find(peticaoModeloId);
            string TextoPeticao = peticaoModelo.PeticaoModificada;

            if (TextoPeticao == null)
                return;

            List<Pergunta> perguntas = ExtrairPerguntas(TextoPeticao);  

            //System.Diagnostics.Debug.WriteLine("******************************peticaoModelo.Nome" + processoPeticao.PeticaoModelo.Nome);

            foreach (var pergunta in perguntas)
            {
                db.Questionarios.Add(new Questionario
                {                    
                    //PeticaoModeloId = peticaoModeloId,
                    TituloTrecho = pergunta.TituloTrecho,
                    Pergunta = pergunta.pergunta,
                    DataModificacao = DateTime.Today
                });


            }
            db.SaveChanges();
        }
        public void CriarQuestionario2(int processoPeticaoId) 
        {
            ProcessoPeticao processoPeticao = db.ProcessoPeticaos.Find(processoPeticaoId);
            string peticaoModelo = processoPeticao.PeticaoModelo.PeticaoModificada;

            if (peticaoModelo == null)
                return; 

            List<Pergunta> perguntas =  ExtrairPerguntas(peticaoModelo);
            //List<Pergunta> perguntas = gerador2.ExtrairPerguntas(peticaoModelo.Peticao); // correto

            //System.Diagnostics.Debug.WriteLine("******************************peticaoModelo.Nome" + processoPeticao.PeticaoModelo.Nome);


            foreach (var pergunta in perguntas)
            {
                db.Questionarios.Add(new Questionario
                {
                    //PeticaoModeloId = processoPeticao.PeticaoModeloId,
                    TituloTrecho = pergunta.TituloTrecho,
                    Pergunta = pergunta.pergunta,
                    DataModificacao = DateTime.Today
                });


            }
            db.SaveChanges();
        }


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



    }
}