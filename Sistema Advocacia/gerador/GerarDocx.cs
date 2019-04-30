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
    public class GerarDocx : Controller
    {
        private DBContext db = new DBContext();
        
        public string construirPeticao(ProcessoPeticao processoPeticao)
        {
            var nomePeca = processoPeticao.PeticaoModelo.NomeAcao.ToUpper();
            var enderecamento = processoPeticao.Processo.enderecamento.ToUpper();
            var corpoPeca = processoPeticao.PeticaoRespondida;

            var autores = db.ProcessoPartes.Include(p => p.Pessoa).Where(p => p.ProcessoId == processoPeticao.ProcessoId && p.Papel == Papel.autor).ToList();

            var reus = db.ProcessoPartes.Include(p => p.Pessoa).Where(p => p.ProcessoId == processoPeticao.ProcessoId && p.Papel == Papel.réu).ToList();

            StringBuilder qualificacaoAutores = new StringBuilder();
            StringBuilder qualificacaoReus = new StringBuilder();

            for (int i = 0; i < autores.Count; i++)
            {
                qualificacaoAutores.Append("**" + autores[i].Pessoa.Nome + "** " + autores[i].Pessoa.QualificacaoPJComNomeVazio_);

                if (i < autores.Count - 1)
                    qualificacaoAutores.Append(";").AppendLine();
           
            }

            for (int i = 0; i < reus.Count; i++)
            {
                qualificacaoReus.Append("**" + reus[i].Pessoa.Nome + "** " + reus[i].Pessoa.QualificacaoPJComNomeVazio_);

                if (i < reus.Count - 1)
                    qualificacaoReus.Append(";").AppendLine();            
            }

            StringBuilder peticao = new StringBuilder();
            peticao.AppendLine("[endereçamento]" + enderecamento)
            //.AppendLine(qualificacaoAutor + "[qualificação], vêm a digna presença de vossa excelecência propor:")
            .AppendLine(qualificacaoAutores.ToString() + "[qualificação], ").AppendLine("vêm a digna presença de vossa excelecência propor:")
            .AppendLine("[peça]" + nomePeca)
            .Append("em face de " + qualificacaoReus)
            .AppendLine(", em decorrência das justificativas de ordem fática e de direito abaixo delineadas:")
            .AppendLine(corpoPeca);

            #region apagar
            //.AppendLine("[fatos]").AppendLine(fatos);
            /*
            .AppendLine("[direito]")
            .AppendLine("Isso é um direito, isso é um direito, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato")
            .AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato")
            .AppendLine("[pedido]")
            .AppendLine("Isso é um pedido, isso é um pedido, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato")
            .AppendLine("Isso é um pedido, isso é um pedido, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            */
            #endregion


            return peticao.ToString();
        }

        public void gerarDocumento(string documento)
        {

            //identificar titulos
            #region
            //identificar titulo1 e marcar como [Ttulo1]            
            var peticaoComTitulos = Regex.Replace(documento, @".*\r\n-{3,}\r\n-{3,}", "[Ttulo1]$0");
            //excluir marcador de titulo1
            peticaoComTitulos = Regex.Replace(peticaoComTitulos, @"\r\n-{3,}\r\n-{3,}", "");       

            //identificar titulo2--- e marcar como [Ttulo2]            
            peticaoComTitulos = Regex.Replace(peticaoComTitulos, @".*\r\n-{3,}", "[Ttulo2]$0");
            //excluir marcador de Titulo2
            peticaoComTitulos = Regex.Replace(peticaoComTitulos, @"\r\n-{3,}", "");

            

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

                    else if (line.Contains("[Ttulo1]") || line.Contains("[fatos]") || line.Contains("[direito]") || line.Contains("[pedido]"))
                        document.InsertParagraph(line).StyleName = "Ttulo1";

                    else if (line.Contains("[Ttulo2]"))
                        document.InsertParagraph(line).StyleName = "Ttulo2";

                    else if (line.Contains("[citação]"))
                        document.InsertParagraph(line).StyleName = "Citacao";

                    else if (line.Contains("[data]"))
                        document.InsertParagraph(DateTime.Today.ToString("'Goiânia, 'dd 'de' MMMM 'de' yyyy")).StyleName = "data"; 

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

                //inserir data e assinatura

                var dataExtenso = DateTime.Today.ToString("'Goiânia, 'dd 'de' MMMM 'de' yyyy");




                //apagar marcações
                #region                
                document.ReplaceText("[Ttulo1]", "");
                document.ReplaceText("[Ttulo2]", "");
                document.ReplaceText("**", "");
                document.ReplaceText("__", "");
                document.ReplaceText("[endereçamento]", "");
                document.ReplaceText("[qualificação]", "");
                document.ReplaceText("[peça]", "");
                document.ReplaceText("[fatos]", "Fatos");
                document.ReplaceText("[direito]", "Direito");
                document.ReplaceText("[pedido]", "Pedido");
                document.ReplaceText("[citação]", "");
                #endregion


                foreach (var paragraph in document.Paragraphs)
                {
                    document.InsertParagraph(paragraph.StyleName);
                }

                document.Save();
                Process.Start("WINWORD.EXE", filename);
            }

        }
    }
}
