using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Regex_Extrair_Perguntas_Teste_isolado
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



    class Program
    {
        public static List<Pergunta> ExtrairPerguntas(string Texto)
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

        static void Main(string[] args)
            {
            var perguntas = new List<Pergunta>();
            var Texto = "{titulo 1& [1.1 texto aqui] abc [1.2-texto2 aqui] abc [1.3]}{ titulo 2 & [2.1 texto aqui] abc[abc[2.2 - texto2 aqui] abc[2.3 - texto aqui]}";
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

        //Program teste = new Program();

        //var perguntas = Program.ExtrairPerguntas("{titulo 1& [1.1 texto aqui] abc [1.2-texto2 aqui] abc [1.3]{ titulo 2 & [2.1 texto aqui] abc[abc[2.2 - texto2 aqui] abc[2.3 - texto aqui]").ToList();





        Console.WriteLine(perguntas.Count);
            foreach (Pergunta pergunta in perguntas)
            {
                Console.WriteLine(pergunta.TituloTrecho + " - " +  pergunta.pergunta);
            }
            Console.ReadKey();

            //Console.WriteLine(Extra)
        }






    }
}
