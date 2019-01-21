using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Sistema_Advocacia.Gerador
{
    public class Gerador2
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