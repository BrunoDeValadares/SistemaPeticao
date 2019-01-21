using Sistema_Advocacia.Context;
using Sistema_Advocacia.gerador;
using Sistema_Advocacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes
{
    class Teste
    {
        static void TestarAnexo()
        {
            List<Questionario> questionarios = new List<Questionario>() {
                new Questionario {TituloTrecho = "titulo1", ProcessoPeticaoId = 58, Pergunta = "P1", Resposta = "R1" },
                new Questionario {TituloTrecho = "titulo2",  ProcessoPeticaoId = 58, Pergunta = "P2", Resposta = "R2" },
                //new Questionario {TituloTrecho = "titulo3",  ProcessoPeticaoId = 58, Pergunta = "P3", Resposta = "R3"},
                new Questionario {TituloTrecho = "titulo3",  ProcessoPeticaoId = 58, Pergunta = "P3"},
                new Questionario {TituloTrecho = "titulo4",  ProcessoPeticaoId = 58, Pergunta = "P4"}
            };

            /*
            DBContext db = new DBContext();            
            questionarios = db.Questionarios.Where(q => q.ProcessoPeticaoId == 58).ToList();
            */

            string peticao = "{titulo1& [1.1 pergunta aqui] abc[1.2 - pergunta aqui] abc[1.3 - pergunta aqui] doc_1 que está bem ali }" +
                "{titulo2& [2.1 pergunta aqui] abcabc doc_2 [2.2 - pergunta aqui] abc[2.3 - pergunta aqui]}" +
                "{titulo3& [3.1 pergunta aqui] abcabc doc_3 [3.2 - pergunta aqui] abc[3.3 - pergunta aqui]}";

            GerarQuestionario gerarQuestionario = new GerarQuestionario();

            List<Anexo> anexos = new List<Anexo>();

            //anexos= gerarQuestionario.ExtrairAnexosPeticao(peticao);
            anexos = gerarQuestionario.ExtrairAnexosDoQuestionario(peticao, questionarios);

            Console.WriteLine("Titulo - Anexo");
            foreach (var anexo in anexos)
            {
                Console.WriteLine(anexo.TituloTrecho + " - " + anexo.AnexoId);
            }
            Console.ReadKey();

        }

        static void TestarValidarPeticao()
        {
            string peticao = "{titulo1& [1.1 pergunta aqui] abc[1.2 - pergunta aqui] abc[1.3 - pergunta aqui] doc_1 que está bem ali }" +
    "{titulo2& [2.1 pergunta aqui] abcabc doc_2 [3.2 - pergunta aqui] abc[2.3 - pergunta aqui]}" +
    "{titulo3& [3.1 pergunta aqui] abcabc doc_3 [3.2 - pergunta aqui] abc[3.3 - pergunta aqui]}";

            GerarQuestionario gerarQuestionario = new GerarQuestionario();
            var perguntas = gerarQuestionario.ExtrairPerguntas(peticao);
            var mensagem = gerarQuestionario.ValidarExtrairPeguntas(perguntas);
            Console.WriteLine(mensagem);
            Console.ReadKey();
        }
        static void TestarExemplo()
        {
            GerarQuestionario gerarQuestionario = new GerarQuestionario();
            string peticao = "{titulo1& [1.1 pergunta aqui] abc[1.2 - pergunta aqui] abc[1.3 - pergunta aqui] doc_1 que está bem ali }" +
    "{titulo2& [2.1 pergunta aqui] abcabc doc_2 [2.2 - pergunta aqui] abc[2.3 - pergunta aqui]}" +
    "{titulo3& [3.1 pergunta aqui] abcabc doc_3 [3.2 - pergunta aqui] abc[3.3 - pergunta aqui]}";

            Console.WriteLine(gerarQuestionario.GetExemplo(peticao, "titulo2"));
            Console.ReadKey();           

        }


            static void Main(string[] args)
        {
            //TestarAnexo();
            //TestarValidarPeticao();

            TestarExemplo();

            


            //gerarQuestionario.ExtrairAnexosPeticao

        }
    }
}
