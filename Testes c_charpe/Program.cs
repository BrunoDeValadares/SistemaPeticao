using Sistema_Advocacia.Context;
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
        public class Pergunta
        {
            public string Topico { get; set; }
            public string Pergunt { get; set; }
        }
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
            //TestarRegexNegrito();
            //TestarRegexPedido();
            //ExtrairPerguntas();
            //testarRegexNegrito2();
            //testarStringBuilder();
            //testarRegexPerguntas();
            //TestarValidarRegexPerguntas();
            //TestarRegexAnexo();
            //TestarRegexTituloReplace();
            //Assinatura();
            RegexComecandoDoFim();
        }

        static void TestarRegexSubTitulo()
        {
            string txt = "corpo de texto\nTitulo1-de jogardores\n---\nCorpo de texto\nSubTitulo1-de jogardores\n---\n---\nCorpo de texto\nCorpo de texto\nSubTitulo2-de jogardores\n---\n---\nCorpo de texto";
            //string txt = "\nTitulo1------\n------\nCorpo de texto";
            //string padraoTitulo = @"(.*)\n-{3,}\n-{3,}";
            string padraoTitulo = @".*\n-{3,}\n-{3,}";

            Regex regex = new Regex(padraoTitulo);
            //string resultado = regex.Match(txt).Groups[1].Value;
            string resultado = regex.Match(txt).Value;

            //var result = new Regex(@"(.*)\n-{3,}\n-{3,}").Match(txt).Groups[1].Value;
            var result = new Regex(@"(.*)\n-{3,}\n-{3,}");
            txt = Regex.Replace(txt, padraoTitulo, "[Ttulo2]$0");

            Console.WriteLine(txt);
            Console.ReadKey();
        }


        static List<Pergunta> ExtrairPerguntas()
        {
            List<Pergunta> perguntas = new List<Pergunta>();
            string txt = "SubTitulo1\n----\ntexto texto [pergunta01+1 aqui neste trecho] texto [pergunta01.2 aqui neste trecho] texto \nSubTitulo2\n---- texto [pergunta02.1 aqui neste trecho] texto [pergunta02.2 aqui neste trecho]";

            //add o marcador "{{{" aos topicos
            string titulosMarcado = Regex.Replace(txt, @".*\n-{2,}", "{{{$0");

            //divide o texto em topicos
            var topicos = titulosMarcado.Split(new string[] { "{{{" }, System.StringSplitOptions.None);

            foreach (var topico in topicos)
            {
                //extraia apenas o  nome do topico
                var nomeTopico = new Regex(@".*").Match(topico).Value;  
                
                //estraia todas perguntas do topico
                var perguntasExtraidas = new Regex(@"\[.*?\]").Matches(topico);

                //coloque as perguntas extraidas em uma LIst<pergunta>
                foreach (var pergunta in perguntasExtraidas)
                {
                    perguntas.Add(new Pergunta { Topico = nomeTopico, Pergunt = pergunta.ToString() });
                }
            }
            return perguntas;
        }

        static void ExtrairPerguntas2()
        {
            List<Pergunta> perguntas = new List<Pergunta>();
            string txt = "SubTitulo1\n----\ntexto texto [pergunta01+1 aqui neste trecho] texto [pergunta01.2 aqui neste trecho] texto \nSubTitulo2\n---- texto [pergunta02.1 aqui neste trecho] texto [pergunta02.2 aqui neste trecho]";

            //var resultTitulo = new Regex(@".*-{2,}").Matches(txt);
            string titulosMarcado = Regex.Replace(txt, @".*\n-{2,}", "{{{$0");

            Console.WriteLine(titulosMarcado);
           
            var topicos = titulosMarcado.Split(new string[] { "{{{" }, System.StringSplitOptions.None);

#region consoles.writeline

            /*
            Console.WriteLine("Todos************************");
            Console.WriteLine(topicos[0] + topicos[1]  + topicos[2]);

            Console.WriteLine("[0]************************");
            Console.WriteLine(topicos[0]);

            Console.WriteLine("[1]************************");
            Console.WriteLine(topicos[1] );

            Console.WriteLine("[2]************************");
            Console.WriteLine(topicos[2]);
            */
#endregion


            foreach (var topico in topicos)
            {
                var nomeTopico = new Regex(@".*").Match(topico).Value;                

                Console.WriteLine(nomeTopico);

                var perguntasExtraidas = new Regex(@"\[.*?\]").Matches(topico);
                foreach (var pergunta in perguntasExtraidas)
                {
                    perguntas.Add(new Pergunta { Topico = nomeTopico, Pergunt = pergunta.ToString() });
                    
                }
                
            }
            Console.WriteLine("Topico" + " - " + "Pergunta");
            foreach (Pergunta pergunta in perguntas)
            {

                Console.WriteLine(pergunta.Topico + " - " + pergunta.Pergunt);
            }

            //Console.Write(titulosMarcado);
            var resultTitulo = new Regex(@".*-{2,}").Matches(txt);

            foreach (Match item in resultTitulo)
            {
                //Console.WriteLine(item.Value);
            }
            
            var resultado = new Regex(@"\[(.*?)\]").Matches(txt);
            foreach (Match item in resultado)
            {
                //Console.WriteLine(item.Groups[1].Value);
            }
            //Console.WriteLine(resultado);
            Console.ReadKey();
        }

        static void TestarRegexSubTitulo3()
        {
            string txt = "corpo de texto\nTitulo1-de jogardores\n---\nCorpo de texto\nSubTitulo-de jogardores\n---\n---\nCorpo de texto";
            //string txt = "\nTitulo1------\n------\nCorpo de texto";
            //string padraoTitulo = @"(.*)\n-{3,}\n-{3,}";
            string padraoTitulo = @".*\n-{3,}\n-{3,}";

            Regex regex = new Regex(padraoTitulo);
            //string resultado = regex.Match(txt).Groups[1].Value;
            string resultado = regex.Match(txt).Value;

            //var result = new Regex(@"(.*)\n-{3,}\n-{3,}").Match(txt).Groups[1].Value;
            var result = new Regex(@"(.*)\n-{3,}\n-{3,}");
            txt = Regex.Replace(txt, padraoTitulo, "<jogue>$0</futebol>");

            Console.WriteLine(resultado);
            Console.ReadKey();
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
        static void TestarRegexSubTitulo2()
        {
            string txt = "corpo de texto\nTitulo1-de jogardores\n---\nCorpo de texto\nSubTitulo-de jogardores\n---\n---\nCorpo de texto";
            //string txt = "\nTitulo1------\n------\nCorpo de texto";
            string padraoTitulo = @"(.*)\n-{3,}\n-{3,}";
            Regex regex = new Regex(padraoTitulo);
            //string resultado = regex.Match(txt).Groups[1].Value;
            string resultado = regex.Match(txt).Groups[1].Value;

            


            //  Console.WriteLine(txt);
            // Console.WriteLine();            Console.WriteLine(resultado);
            Console.ReadKey();
        }

        static void TestarRegexNegrito()
        {
            //string txt = "corpo de texto\nTitulo1-de \n---\nCorpo de**negrito aqui. E seguindo, __**sublinhado aqui**__texto, e _texto italico_ aqui\nSubTitulo-de jogardores\n---\n---\nCorpo de texto";
            string txt = "corpo de texto\nTitulo1-de \n---\nCorpo de __**negrito aqui e sublinhado aqui**__. E seguindo **negrito ali**, , e _texto italico_ aqui\nSubTitulo-de jogardores\n---\n---\nCorpo de texto";

            //string padraoNegrito = @"\*{2}(.*)\*{2}";
            ///string padraoNegrito = @"\*{2}(.*)\*{2}"; ;
            //string padraoNegrito = @"?<=\*(.*?)?=\*";
            string padraoNegrito = @"\*{2}.*?\*{2}";
            //string padraoSublinhado = @"_{2}.*?_{2}";  

            //string padraoSublinhado = @"_{2}(.*)_{2}";  //MAIS CORRETO


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
            var resultados = regex.Matches(txt);

            //  Console.WriteLine(txt);
            // Console.WriteLine();
            foreach (Match result in regex.Matches(txt))            
                Console.WriteLine(result.Groups[0].Value);
            
            
            Console.ReadKey();
        }
        static void TestarRegexPedido()
        {
            //string txt = "aqui existe um pedido que foi feito";
            string txt = "aqui existe um MORTE que foi feito";
            string padraoTitulo = @"MOR.*";
            Regex regex = new Regex(padraoTitulo);
            string resultado = regex.Match(txt).Value;


             Console.WriteLine(resultado);
            // Console.WriteLine();            Console.WriteLine(resultado);
            Console.ReadKey();
        }

        static void testarRegexNegrito2()
        {
            string txt = "corpo de texto\nTitulo1-de \n---\nCorpo de __**negrito aqui e sublinhado aqui**__. E seguindo **negrito ali**, , e _texto italico_ aqui\nSubTitulo-de jogardores\n---\n---\nCorpo de texto";
            var negritos = new Regex(@"\*\*.*?\*\*").Matches(txt);
            foreach (Match item in negritos)
            {
                Console.WriteLine("valor: " + item.Value);
            }

            Console.ReadKey();

        }

        static void testarStringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("mesma linha")
                .Append("2 mesma linha")
                .Append("3 mesma linha")
                .AppendLine().AppendLine().Append("30 linha abaixo")
            ;
            Console.WriteLine(sb.ToString());
            Console.ReadKey();

        }

        static void testarRegexPerguntas()
        {
            string txt = "SubTitulo1\n----\ntexto texto [pergunta01+1 aqui neste trecho] texto [pergunta01.2 aqui neste trecho] texto \nSubTitulo2\n---- texto [pergunta02.1 aqui neste trecho] texto [pergunta02.2 aqui neste trecho]";
            var perguntas = new Regex(@"\[.*?\]").Matches(txt);
            int i = 1;
            foreach (Match item in perguntas)
            {
                
                Console.WriteLine(i + ":   " + item.Value);
                i ++;
            }
            Console.WriteLine(perguntas.Count);

            Console.ReadKey();
        }
        static void TestarValidarRegexPerguntas()
        {
            //string txt = "SubTitulo1\n----\ntexto texto [pergunta01+1 aqui neste trecho] [pergunta01+1 aqui neste trecho] texto [pergunta01.2 aqui neste trecho] texto \nSubTitulo2\n---- texto [pergunta02.1 aqui neste trecho] texto [pergunta02.2 aqui neste trecho]";
            string txt = "SubTitulo1\n----\ntexto texto [pergunta01+1 aqui neste trecho] [pergunta01+1 aqui neste trecho] ";

            var perguntas = new Regex(@"\[.*?\]").Matches(txt);
            var perguntasUnicas = perguntas.OfType<Match>().Select(m => m.Groups[0].Value).Distinct();

            /*
            if (perguntas.Count > perguntasUnicas.Count())
                Console.WriteLine("sim");
            else
                Console.WriteLine("não");
            */

            Console.WriteLine("perguntas");
            foreach (Match item in perguntas)
            {
                Console.WriteLine(item.Value);                
            }

            Console.WriteLine("perguntas unicas");            
            foreach (var item in perguntasUnicas)
            {
                Console.WriteLine(item);
            }

            /*
            Console.WriteLine(perguntas.Count);
            Console.WriteLine(perguntasUnicas.Count());
            */

            Console.ReadKey();

        }

        static void TestarRegexAnexo()
        {
            string Texto = "{jogador gogue doc_35 e que pode ir pra csa doc_37, na sua casa}";
            var anexosDoQuestionario = new Regex(@"doc_(\d{1,3})").Matches(Texto);
            foreach (Match item in anexosDoQuestionario)            
                Console.WriteLine(item.Groups[1].Value);
            Console.ReadKey();            
        }

        static void TestarRegexTituloReplace()
        {
            #region
            /*
            string txt = "   Títulão1\n----\n-----\nSubTitulo1\n----\ntexto texto [pergunta01+1 aqui neste trecho] texto [pergunta01.2 aqui neste trecho] texto \nSubTitulo2\n---- texto [pergunta02.1 aqui neste trecho] texto [pergunta02.2 aqui neste trecho]";
            string txt1 = "Topico1";            
            var modeloPeticao = dB.PeticaoModeloes.Find(1);
            var txt3 = modeloPeticao.PeticaoModificada;
            var peticaoComTitulos = Regex.Replace(txt4, @".*\n-{3,}\n-{3,}", "[Ttulo1]$0");
            var peticaoComTitulos = Regex.Replace(txt4, @".*-{3,}", "[Ttulo2]$0");
            var peticaoComTitulos = Regex.Replace(documento, @"(.*)\r\n-{3,}\r\n-{3,}", "[Ttulo1]$0");
            var peticaoComTitulos1 = Regex.Replace(txt, @"\e", "[s]");
            var peticaoComTitulos2 = Regex.Replace(txt4, @"", "[wanderlei]");
            Console.WriteLine(peticaoComTitulos);            
            Console.WriteLine(peticaoComTitulos1);

            System.Diagnostics.Debug.WriteLine("[---------------------]" + peticaoComTitulos1 + "[[]]");
            //Console.WriteLine(txt4);

            */
            #endregion

            DBContext dB = new DBContext();

            BD db = new BD();

            var txt4 =  db.PeticaoModelo.Find(1).PeticaoModificada;
            txt4 = txt4.Substring(0, 300);


            var peticaoComTitulos1 = Regex.Replace(txt4, @".*\n-{3,}\r\n-{3,}", "[Ttulo1]$0");            
            Console.WriteLine(peticaoComTitulos1);

            /*
            Console.WriteLine("\n\n\n");
            peticaoComTitulos1 = Regex.Replace(peticaoComTitulos1, @"\r\n-{3,}\r\n-{3,}", "");
            Console.WriteLine(peticaoComTitulos1);
            
            Console.WriteLine("\n\n\n");
            peticaoComTitulos1 = Regex.Replace(peticaoComTitulos1, @".*\r\n-{3,}", "[Ttulo2]$0");            
            peticaoComTitulos1 = Regex.Replace(peticaoComTitulos1, @"\r\n-{3,}", "");
            Console.WriteLine(peticaoComTitulos1);
            */

            Console.ReadKey();

        }

        static void Assinatura()
        {
            var data = DateTime.Today;
            //var dataExtenso = "Goiânia, " + data.ToString("dd") + " de " + data.ToString("MMMM") + " de " + data.ToString("yyyy") + ".";

            var dataExtenso = "Goiânia, " +  DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy");

            Console.WriteLine(dataExtenso);
            Console.ReadKey();
        }
        static void RegexComecandoDoFim()
        {


            //var txt = new Regex(@"[^/]*").Match("c/fonte1/fonte2/arquivos/modelo.dox").Groups[0].Value;
            //var txt = new Regex(@"arquivos/(.*)").Match("c/fonte1/fonte2/arquivos/modelo.dox").Groups[1].Value;
            var txt = new Regex(@"[^/]*$").Match("c/fonte1/fonte2/arquivos/modelo.dox").Value;




            Console.WriteLine("resp" + txt);
            Console.ReadKey();



        }
    }





}

