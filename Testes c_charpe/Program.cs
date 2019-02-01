using Sistema_Advocacia.gerador;
using Sistema_Advocacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Testes_c_charpe
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Regex regex = new Regex(@"[\w]*");
            string PrimeiroNome = regex.Match("ADAOstrogildo-da Silva Sauro").Value;
            Console.WriteLine(PrimeiroNome);
            Console.ReadKey();
            */
            //PrimeiroNome();
            //RegexAnexo();
            //AprenderIndexOf();
            //TestarPeticaoRespondidade();
            //TestarRegexTitulo();
            //TestarRegexSubTitulo();
            TestarRegexNegrito();




        }

        static void PrimeiroNome()
        {
            Regex regex = new Regex(@"[\w]*");
            var PrimeiroNome = regex.Match("AdãoAnacredo").Value;
            Console.WriteLine(PrimeiroNome);
            Console.ReadKey();

        }

        static void RegexAnexo()
        {
            string Padrao = @"doc_[0-9]*";

            Regex regex = new Regex(Padrao);

            string Texto = "{jogador gogue doc_35 e que pode ir pra csa doc_37, na sua casa}";
            var resultado = regex.Matches(Texto);
        }

        static void AprenderIndexOf()
        {
            string anexo = "doc_125456812548";
            var anexoId = anexo.Substring(4);
            Console.WriteLine("anexo: " + anexoId);
            Console.ReadKey();
        }

        static void TestarPeticaoRespondidade()
        {
            List<Questionario> questionarios = new List<Questionario>() {
                new Questionario {TituloTrecho = "titulo1", ProcessoPeticaoId = 58, Pergunta = "1.1 pergunta aqui", Resposta = "R1" },
                new Questionario {TituloTrecho = "titulo2",  ProcessoPeticaoId = 58, Pergunta = "1.2 - pergunta aqui", Resposta = "R2" },                
                new Questionario {TituloTrecho = "titulo3",  ProcessoPeticaoId = 58, Pergunta = "P3"},
                new Questionario {TituloTrecho = "titulo4",  ProcessoPeticaoId = 58, Pergunta = "P4"}
            };
            string peticao = "{titulo1& [1.1 pergunta aqui] abc[1.2 - pergunta aqui] abc[1.3 - pergunta aqui] doc_1 que está bem ali }" +
    "{titulo2& [2.1 pergunta aqui] abcabc doc_2 [2.2 - pergunta aqui] abc[2.3 - pergunta aqui]}" +
    "{titulo3& [3.1 pergunta aqui] abcabc doc_3 [3.2 - pergunta aqui] abc[3.3 - pergunta aqui]}";           


            GerarQuestionario gerarQuestionario = new GerarQuestionario();
            var peticaoRespondida = gerarQuestionario.MontarPeticao(peticao, questionarios);
            Console.WriteLine(peticaoRespondida);
            Console.ReadKey();



        }

        static void TestarRegexTitulo()
        {
            string txt = "corpo de texto\nTitulo1-de jogardores\n---\nCorpo de texto\nSubTitulo-de jogardores\n---\n---\nCorpo de texto";
            //string txt = "Jogador de basebol\nTitulo1-de jogardores\n---\nCorpo de texto";
            //string txt = "\nTitulo1------\nCorpo de texto";
            string padraoTitulo = @"(.*)\n-{3,}";
            Regex regex = new Regex(padraoTitulo);
            string resultado = regex.Match(txt).Groups[1].Value;


          //  Console.WriteLine(txt);
           // Console.WriteLine();
            Console.WriteLine(resultado);
            Console.ReadKey();
        }


        static void TestarRegexSubTitulo()
        {
            string txt = "corpo de texto\nTitulo1-de jogardores\n---\nCorpo de texto\nSubTitulo-de jogardores\n---\n---\nCorpo de texto";
            //string txt = "\nTitulo1------\n------\nCorpo de texto";
            string padraoTitulo = @"(.*)\n-{3,}\n-{3,}";
            Regex regex = new Regex(padraoTitulo);
            string resultado = regex.Match(txt).Groups[1].Value;


            //  Console.WriteLine(txt);
            // Console.WriteLine();            Console.WriteLine(resultado);
            Console.ReadKey();
        }

        static void TestarRegexNegrito()
        {
            //string txt = "corpo de texto\nTitulo1-de \n---\nCorpo de**negrito aqui. E seguindo, __**sublinhado aqui**__texto, e _texto italico_ aqui\nSubTitulo-de jogardores\n---\n---\nCorpo de texto";
            string txt = "corpo de texto\nTitulo1-de \n---\nCorpo de**negrito aqui. E seguindo, , e _texto italico_ aqui\nSubTitulo-de jogardores\n---\n---\nCorpo de texto";

            string padraoNegrito = @"\*{2}(.*)\*{2}";
            string padraoSublinhado = @"_{2}(.*)_{2}";  //MAIS CORRETO
            //string padraoItalico = @"_(.*)[^_][^\w]";
            //string padraoItalico = @"_([^_]*)";
            //string PadraoPergunta = @"_([^_]*)_";

            //string padraoSublinhado = @"_{2}\*{0,}(.*)_{2}";  //CORRETO
            //string padraoSublinhado = @"_{2}\*{2}([^/*]*)_{2}";



            //string padraoSublinhado = @"_{2}\*{0,}([.*])_{2}";
            Regex regex = new Regex(padraoNegrito);
            //Regex regex = new Regex(padraoSublinhado);
            //Regex regex = new Regex(padraoItalico);

            string resultado = regex.Match(txt).Groups[1].Value;


            //  Console.WriteLine(txt);
            // Console.WriteLine();
            Console.WriteLine(resultado);
            Console.ReadKey();
        }

    }



}
