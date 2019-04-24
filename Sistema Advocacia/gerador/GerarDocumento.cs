using Sistema_Advocacia.Context;
using Sistema_Advocacia.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Xceed.Words.NET;
using System.Data.Entity;
namespace Sistema_Advocacia.gerador
{
    public class GerarDocumento : Controller
    {
        private DBContext db = new DBContext();

        //public string construirPeticao(ProcessoPeticao processoPeticao)


        public List<Pergunta> ExtrairPerguntas(string Texto)
        {
            var perguntas = new List<Pergunta>();
            var PerguntasEncontradas = new Regex(@"\[([^\]]*)\]").Matches(Texto);            

            foreach (Match perguntaEncontrada in PerguntasEncontradas)
                perguntas.Add(new Pergunta { pergunta = perguntaEncontrada.Value});

            return perguntas;
        }




        public string construirPeticao(ProcessoPeticao processoPeticao)
        {
            var nomePeca = processoPeticao.PeticaoModelo.NomeAcao.ToUpper();
            var enderecamento = processoPeticao.Processo.enderecamento.ToUpper();
            var fatos = processoPeticao.PeticaoRespondida;

            var autores = db.ProcessoPartes.Include(p => p.Pessoa).Where(p => p.ProcessoId == processoPeticao.ProcessoId && p.Papel == Papel.autor).ToList();

            var reus = db.ProcessoPartes.Include(p => p.Pessoa).Where(p => p.ProcessoId == processoPeticao.ProcessoId && p.Papel == Papel.réu).ToList();

            StringBuilder qualificacaoAutores = new StringBuilder();
            StringBuilder qualificacaoReus = new StringBuilder();

            for (int i = 0; i < autores.Count; i++)
            {
                qualificacaoAutores.Append("**" + autores[i].Pessoa.Nome + "** " + autores[i].Pessoa.QualificacaoPJComNomeVazio_);

                if (i < autores.Count - 1)
                    qualificacaoAutores.Append(";").AppendLine();

                //else
                //   qualificacaoAutores.Append(".");                
            }

            for (int i = 0; i < reus.Count; i++)
            {
                qualificacaoReus.Append("**" + reus[i].Pessoa.Nome + "** " + reus[i].Pessoa.QualificacaoPJComNomeVazio_);

                if (i < reus.Count - 1)
                    qualificacaoReus.Append(";").AppendLine();
                //else
                //  qualificacaoReus.Append(".");                
            }
            /*
            for (int i = 0; i < reus.Count; i++)
            {
                var qualificacaoReus = "**" + reus[i].Pessoa.Nome + "** " + reus[i].Pessoa.QualificacaoComNomeVazio_;

                if (i < autores.Count - 1)
                    qualificacaoReus += ";\n";
                else
                    qualificacaoReus += ".\n";
            }
            */

            StringBuilder peticao = new StringBuilder();
            peticao.AppendLine("[endereçamento]" + enderecamento)
            //.AppendLine(qualificacaoAutor + "[qualificação], vêm a digna presença de vossa excelecência propor:")
            .AppendLine(qualificacaoAutores.ToString() + "[qualificação], ").AppendLine("vêm a digna presença de vossa excelecência propor:")
            .AppendLine("[peça]" + nomePeca)
            .Append("em face de " + qualificacaoReus)
            .AppendLine(", em decorrência das justificativas de ordem fática e de direito abaixo delineadas:")
            .AppendLine("[fatos]").AppendLine(fatos)
            .AppendLine("[direito]")
            .AppendLine("Isso é um direito, isso é um direito, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato")
            .AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato")
            .AppendLine("[pedido]")
            .AppendLine("Isso é um pedido, isso é um pedido, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato")
            .AppendLine("Isso é um pedido, isso é um pedido, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");


            return peticao.ToString();
        }

        public string construirPeticao2(ProcessoPeticao processoPeticao)
        {
            //if (processoPeticao != null) { };

            var cliente = processoPeticao.Processo.Cliente;
            /*
            foreach (var item in collection)
            {
            var qualificacaoAutor = "**" + cliente.Nome + "** " + cliente.QualificacaoComNomeVazio_;
            }
            */

            //var qualificacaoReu
            var nomePeca = processoPeticao.PeticaoModelo.NomeAcao.ToUpper();
            var enderecamento = processoPeticao.Processo.enderecamento.ToUpper();
            var fatos = processoPeticao.PeticaoRespondida;
            //var direito = processoPeticao.


            StringBuilder peticao = new StringBuilder();
            peticao.AppendLine("[endereçamento]" + enderecamento)
            //.AppendLine(qualificacaoAutor + "[qualificação], vêm a digna presença de vossa excelecência propor:")
            .AppendLine("[peça]" + nomePeca)
            .AppendLine("[fatos]").AppendLine(fatos)
            .AppendLine("[direito]")
            .AppendLine("Isso é um direito, isso é um direito, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato")
            .AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato")
            .AppendLine("[pedido]")
            .AppendLine("Isso é um pedido, isso é um pedido, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato")
            .AppendLine("Isso é um pedido, isso é um pedido, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");


            return peticao.ToString();
        }


        public void gerarDocumento2(string documento)
        {
            #region

            StringBuilder peticao = new StringBuilder();


            peticao.AppendLine("[endereçamento]");
            peticao.AppendLine("[qualificação], vêm a digna presença de vossa excelecência propor:");
            peticao.AppendLine("[peça]");
            peticao.AppendLine("[fatos]");
            peticao.AppendLine("SubTitulo1\n------");
            peticao.AppendLine("Isso é um fatorado, **isso é um negrito**, isso é um fato, isso é um fato, __isso é um sublinhado__, isso é um fato, isso é um fato, isso é um fato, isso é um fato");

            peticao.AppendLine("SubTitulo1\n------");
            peticao.AppendLine("Isso é um fatorado---------------------, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            peticao.AppendLine("SubTitulo2\n-------");
            peticao.AppendLine("Isso é um fato, **isso é um negrito**, isso é um fato, isso é um fato, __isso é um sublinhado__, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            peticao.AppendLine("[direito]");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            peticao.AppendLine("[pedido]");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            #endregion

            //identificar titulos
            #region
            //identificar titulo1--- e marcar como [Ttulo1]            
            var peticaoComTitulos = Regex.Replace(peticao.ToString(), @"(.*)\n-{3,}\n-{3,}", "[Ttulo1]$0");

            //identificar titulo2--- e marcar como [Ttulo2]            
            peticaoComTitulos = Regex.Replace(peticao.ToString(), @".*\n-{3,}", "[Ttulo2]$0");

            //remover marcação
            peticaoComTitulos = Regex.Replace(peticaoComTitulos, @"\n-{3,}", "");
            #endregion

            // split petição por parágrafos
            #region 
            var docLines = peticaoComTitulos.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);
            string filename = @"C:\GitHub\Test2.docx";
            #endregion

            //criação do documento
            //using (DocX document = DocX.Create(@"C:\GitHub\Test2.docx"))
            using (DocX document = DocX.Create(@"C:\GitHub\Test2.docx"))
            {
                document.ApplyTemplate(@"C:\GitHub\PeticaoModelo4.docx");

                //remover configurações do tamplate no documento
                foreach (var paragraph in document.Paragraphs)
                {
                    paragraph.Remove(false);
                }

                //atribuir estilo de acordo com palavras chaves
                foreach (var line in docLines)
                {
                    if (line.Contains("[endereçamento]"))
                        document.InsertParagraph(line).StyleName = "Endereamento";

                    else if (line.IndexOf("[qualificação]") > -1)
                        document.InsertParagraph(line).StyleName = "CorpoDeTexto0";

                    else if (line.Contains("[peça]"))
                        document.InsertParagraph(line).StyleName = "NomeDaPea";

                    else if (line.Contains("[fatos]") || line.Contains("[direito]") || line.Contains("[pedido]"))
                        document.InsertParagraph(line).StyleName = "Ttulo1";
                    else if (line.Contains("[Ttulo2]"))
                        document.InsertParagraph(line).StyleName = "Ttulo2";

                    else
                        document.InsertParagraph(line).StyleName = "CorpoDeTexto0";
                }

                //substituir palavras chave
                #region 
                /*
                if (processoPeticao != null)
                {
                    var cliente = processoPeticao.Processo.Cliente;
                    var nomePeca = processoPeticao.PeticaoModelo.NomeAcao;
                    var enderecamento = processoPeticao.Processo.enderecamento;

                    string qualificacao = string.Concat("**", cliente.Nome, "**", " ", cliente.QualificacaoComNomeVazio_);

                    document.ReplaceText("[endereçamento]", enderecamento.ToUpper());
                    document.ReplaceText("[qualificação]", qualificacao);
                    document.ReplaceText("[peça]", nomePeca.ToUpper());
                    document.ReplaceText("[fatos]", "Fatos");
                    document.ReplaceText("[direito]", "Direito");
                    document.ReplaceText("[pedido]", "Pedidos");
                }
                //string enderecamento = "Excelentíssimo Senhor Doutor Juiz de Direito da Comarca de Goiania-GO";
                //string nomePeca = "Ação Ordinária de Pensão por MORTE pedido";
                */
                #endregion

                //inserir negritos e sublinhados
                #region
                var negritos = new Regex(@"\*\*.*?\*\*").Matches(document.Text);
                foreach (Match negrito in negritos)
                    document.ReplaceText(negrito.Value, negrito.Value, false, RegexOptions.IgnoreCase, new Formatting() { Bold = true });

                var sublinhados = new Regex(@"__.*?__").Matches(document.Text);
                foreach (Match sublinhado in sublinhados)
                    document.ReplaceText(sublinhado.Value, sublinhado.Value, false, RegexOptions.IgnoreCase, new Formatting() { UnderlineStyle = UnderlineStyle.singleLine });
                #endregion

                //apagar marcações
                #region                
                document.ReplaceText("[Ttulo2]", "");
                document.ReplaceText("**", "");
                document.ReplaceText("__", "");
                #endregion

                document.Save();
                Process.Start("WINWORD.EXE", filename);
            }
        }


        public void gerarDocumento(string documento)
        {


            //identificar titulos
            #region
            //identificar titulo1--- e marcar como [Ttulo1]            
            var peticaoComTitulos = Regex.Replace(documento, @"(.*)\n-{3,}\n-{3,}", "[Ttulo1]$0");

            //identificar titulo2--- e marcar como [Ttulo2]            
            peticaoComTitulos = Regex.Replace(documento, @".*\n-{3,}", "[Ttulo2]$0");

            //remover marcação
            peticaoComTitulos = Regex.Replace(peticaoComTitulos, @"\n-{3,}", "");
            #endregion

            // split petição por parágrafos
            #region 
            var docLines = peticaoComTitulos.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);
            string filename = @"C:\GitHub\Test2.docx";
            #endregion

            //criação do documento
            //using (DocX document = DocX.Create(@"C:\GitHub\Test2.docx"))
            using (DocX document = DocX.Create(@"C:\GitHub\Test2.docx"))
            {
                document.ApplyTemplate(@"C:\GitHub\PeticaoModelo4.docx");

                //remover configurações do tamplate no documento
                foreach (var paragraph in document.Paragraphs)
                {
                    paragraph.Remove(false);
                }

                //atribuir estilo de acordo com palavras chaves
                foreach (var line in docLines)
                {
                    if (line.Contains("[endereçamento]"))
                        document.InsertParagraph(line).StyleName = "Endereamento";

                    else if (line.IndexOf("[qualificação]") > -1)
                        document.InsertParagraph(line).StyleName = "CorpoDeTexto0";

                    else if (line.Contains("[peça]"))
                        document.InsertParagraph(line).StyleName = "NomeDaPea";

                    else if (line.Contains("[fatos]") || line.Contains("[direito]") || line.Contains("[pedido]"))
                        document.InsertParagraph(line).StyleName = "Ttulo1";
                    else if (line.Contains("[Ttulo2]"))
                        document.InsertParagraph(line).StyleName = "Ttulo2";

                    else
                        document.InsertParagraph(line).StyleName = "CorpoDeTexto0";
                }

                //inserir negritos e sublinhados
                #region
                var negritos = new Regex(@"\*\*.*?\*\*").Matches(document.Text);
                foreach (Match negrito in negritos)
                    document.ReplaceText(negrito.Value, negrito.Value, false, RegexOptions.IgnoreCase, new Formatting() { Bold = true });

                var sublinhados = new Regex(@"__.*?__").Matches(document.Text);
                foreach (Match sublinhado in sublinhados)
                    document.ReplaceText(sublinhado.Value, sublinhado.Value, false, RegexOptions.IgnoreCase, new Formatting() { UnderlineStyle = UnderlineStyle.singleLine });
                #endregion

                //apagar marcações
                #region                
                document.ReplaceText("[Ttulo2]", "");
                document.ReplaceText("**", "");
                document.ReplaceText("__", "");
                document.ReplaceText("[endereçamento]", "");
                document.ReplaceText("[qualificação]", "");
                document.ReplaceText("[peça]", "");
                document.ReplaceText("[fatos]", "Fatos");
                document.ReplaceText("[direito]", "Direito");
                document.ReplaceText("[pedido]", "Pedido");
                #endregion

                document.Save();
                Process.Start("WINWORD.EXE", filename);
            }

        }
    }
}
