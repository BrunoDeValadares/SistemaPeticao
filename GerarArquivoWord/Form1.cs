using Sistema_Advocacia.Context;
using Sistema_Advocacia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Words.NET;

namespace GerarArquivoWord
{
    public partial class Form1 : Form
    {
        Cliente cliente1 = new Cliente
        {
            ClienteId = 1,
            Nome = "Bruno Valadares do Nascimento",
            CPF = "011.916.601-17",
            RG = "2798",
            Nacionalidade = "brasileiro",
            EstadoCivil = EstadoCivil.solteiro,
            Sexo = Sexo.masculino,
            Logradouro = "Rua 59-A",
            Numero = 547,
            Setor = "Aeroporto",
            Cidade = "Goinaia",
            Estado = "GO"
        };
        Processo processo1 = new Processo
        {
            enderecamento = "Excelentíssimo Senhor Doutor da Comarca de Goiânia-GO",
            NumeroProcesso = "1.0.564"
        };
        PeticaoModelo peticaoModelo1 = new PeticaoModelo
        {
            NomeAcao = "AÇÃO ORDINÁRIA DE PENSÃO POR MORTE"
        };

        List < Cliente > clientes = new List<Cliente>
        {
            new Cliente
            {

                Nome = "Bruno Valadares do Nascimento",
                CPF = "011.916.601-17",
                RG = "2798",
                Nacionalidade = "brasileiro",
                EstadoCivil = EstadoCivil.solteiro,
                Sexo = Sexo.masculino,
                Logradouro = "Rua 59-A",
                Numero = 547,
                Setor = "Aeroporto",
                Cidade = "Goinaia",
                Estado = "GO"
            },
            new Cliente
            {
                Nome = "João Valadares do Nascimento",
                CPF = "011.916.601-17",
                RG = "2798",
                Nacionalidade = "brasileiro",
                EstadoCivil = EstadoCivil.solteiro,
                Sexo = Sexo.masculino,
                Logradouro = "Rua 59-A",
                Numero = 547,
                Setor = "Aeroporto",
                Cidade = "Goinaia",
                Estado = "GO"
            },
            new Cliente
            {
                Nome = "José Valadares do Nascimento",
                CPF = "011.916.601-17",
                RG = "2798",
                Nacionalidade = "brasileiro",
                EstadoCivil = EstadoCivil.solteiro,
                Sexo = Sexo.masculino,
                Logradouro = "Rua 59-A",
                Numero = 547,
                Setor = "Aeroporto",
                Cidade = "Goinaia",
                Estado = "GO"
            },


    };

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGerarDocX4_Click(object sender, EventArgs e)
        {

            //document.InsertParagraph("**" + cliente1.Nome + "**").Append(cliente1.QualificacaoComNomeVazio_).StyleName = "CorpoDeTexto" ;                    
            //string peticao = "[endereçamento]\n[qualificação], vêm a digna presença de vossa excelecência propor:\n[nome_peça]\nFatos\nIsso é um fato\nIsso também é um fato";
            StringBuilder peticao = new StringBuilder();
            peticao.AppendLine("[endereçamento]");
            peticao.AppendLine("[qualificação], vêm a digna presença de vossa excelecência propor:");
            peticao.AppendLine("[peça]");
            peticao.AppendLine("[fatos]");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            peticao.AppendLine("[direito]");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            peticao.AppendLine("[pedido]");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");

            //identificar titulo e inserir no rimeiro ragrafo


            var docLines = peticao.ToString().Split(new string[] { "\r\n" }, System.StringSplitOptions.None);
            string filename = @"C:\GitHub\Test2.docx";
            using (DocX document = DocX.Create(@"C:\GitHub\Test2.docx"))
            {
                document.ApplyTemplate(@"C:\GitHub\PeticaoModelo3.docx");
                //lavras chave
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

                    else
                        document.InsertParagraph(line).StyleName = "CorpoDeTexto0";
                }

                //substituir palavras chave
                string qualificacao = string.Concat("**", cliente1.Nome, "**", " ", cliente1.QualificacaoComNomeVazio_);
                string enderecamento = "Excelentíssimo Senhor Doutor Juiz de Direito da Comarca de Goiania-GO";
                string nomePeca = "Ação Ordinária de Pensão por MORTE pedido";

                document.ReplaceText("[endereçamento]", enderecamento.ToUpper());
                document.ReplaceText("[qualificação]", qualificacao);
                document.ReplaceText("[peça]", nomePeca.ToUpper());
                document.ReplaceText("[fatos]", "Fatos");
                document.ReplaceText("[direito]", "Direito");
                document.ReplaceText("[pedido]", "Pedidos");

                var fPadrao = new Formatting();
                var fPadrao1 = new Formatting() { Size = 14 };
                //fPadrao.Size = 14;
                //fPadrao.UnderlineStyle = UnderlineStyle.singleLine;




                //document.ReplaceText("(MOR).*", "ABC",false, RegexOptions.IgnoreCase, null, fPadrao);
                //string s = document.Text;
                //string[] array = new string[];
                string padraoRegex = "/*/*.*/*/*";
                var negritos = new List<string>();

                document.ReplaceText("morte", "ABC", false, RegexOptions.IgnoreCase, new Formatting() { Size = 10, Bold = true });




                foreach (var paragrafo in document.Paragraphs)
                {
                    document.InsertParagraph(paragrafo.StyleName);
                }


                document.Save();
                Process.Start("WINWORD.EXE", filename);
            }




        }
        private void btnGerarDocX3_Click(object sender, EventArgs e)
        {
            string fontPadrao = "Verdana";
            int sizePadrao = 12;
            var firstLinePadrao = 6.0f;
            string enderecarPara = "EXCELENTÍSSIMO(A) SENHOR(A) JUIZ(A) FEDERAL DA VARA DO JUIZADO ESPECIAL FEDERAL DA SEÇÃO JUDICIÁRIA DO estado de Goiás".ToUpper();
            //string quaflicação =

            string filename = @"C:\GitHub\Test2.docx";
            using (DocX document = DocX.Create(filename))
            {
                document.ApplyTemplate(@"C:\GitHub\PeticaoModelo3.docx");


                //endereçamento           
                document.InsertParagraph(enderecarPara).SpacingAfter(150).Font(fontPadrao).FontSize(sizePadrao);
                Paragraph p = document.InsertParagraph(enderecarPara.ToUpper());
                p.SpacingAfter(150).Alignment = Alignment.left;


                //p.StyleName = "Normal";


                p.IndentationFirstLine = 0.0f;



                //qualificação                
                document.InsertParagraph("**" + cliente1.Nome + "**").Append(cliente1.QualificacaoComNomeVazio_).StyleName = "normal";
                //Paragraph pQUalificacao = document.InsertParagraph("**" + cliente1.Nome + "**").Append(cliente1.QualificacaoComNomeVazio_).StyleName = "normal";
                //pQUalificacao.Append(cliente1.QualificacaoComNomeVazio_).StyleName = "normal";



                //Parte principal
                string txt = ", por seu advogado que esta subscreve, vem à presença de Vossa Excelência, com fulcro na Legislação Pátria, propor a presente:";
                //pQUalificacao.Append(txt);

                //ação
                txt = "ação de execução de título extrajudicial".ToUpper();
                document.InsertParagraph(txt).SpacingBefore(30).SpacingAfter(30).Font(fontPadrao).FontSize(sizePadrao);
                //pAcao.StyleName = "normal";

                //FATOS
                //Cabeçalho                
                //document.InsertParagraph("DOS FATOS").FontSize(sizePadrao).Font(fontPadrao).Bold().SpacingAfter(0).Alignment = Alignment.left;
                document.InsertParagraph("DOS FATOS").StyleName = "Ttulo1";
                txt = "Linha1-ação de execução de título extrajudicial de quem apenas gosta de jogar bola e não quer saber de nada que a banda toca. \nLinha2- mas se vc sabe onde estou então estamos bem.\nLinha3 aqui.";

                var docLines = txt.Split(new string[] { "\n" }, System.StringSplitOptions.None);
                foreach (var line in docLines)
                    document.InsertParagraph(line).StyleName = "Normal";

                //CONFIGURAR titulo 1 Ttulo2
                /*
                foreach (var paragrafo in document.Paragraphs)
                {

                    if (paragrafo.StyleName == "Ttulo1")
                    {
                        paragrafo.Font("Verdana").FontSize(18).IndentationFirstLine = firstLinePadrao;
                        paragrafo.Alignment = Alignment.both;
                        paragrafo.SetLineSpacing(LineSpacingType.Line, 1.5f);
                    }
                }
                */



                //CONFIGURAR normal
                foreach (var paragrafo in document.Paragraphs)
                {
                    if (paragrafo.StyleName == "normalização")
                    {
                        paragrafo.Font("Verdana").FontSize(12).IndentationFirstLine = firstLinePadrao;
                        paragrafo.Alignment = Alignment.both;
                        paragrafo.SetLineSpacing(LineSpacingType.Line, 1.5f);
                    }
                }

                foreach (var paragrafo in document.Paragraphs)
                {
                    document.InsertParagraph(paragrafo.StyleName);
                }

                document.Save();
                Process.Start("WINWORD.EXE", filename);
            }
        }
        private void btnGerar_Click(object sender, EventArgs e)
        {
            var application = new Microsoft.Office.Interop.Word.Application();
            var document = new Microsoft.Office.Interop.Word.Document();
            //var document2 = new Microsoft.Office.Interop.Word.Document();

            string templatePath = @"~\Templates\PeticaoModelo.docx";
            //var templatePath = Server.MapPath("~/Templates/report.xlsx");
            //var templatePath = System.Web.HttpContext.Current.Server.MapPath("~/Templates/report.xlsx");

            document = application.Documents.Add(Template: templatePath);
            //document = application.Documents.Add(Template: @"C:\GitHub\PeticaoModelo.docx");
            // document2 = application.Documents.Add(Template: document);

            application.Visible = true;

            foreach (Microsoft.Office.Interop.Word.Field field in document.Fields)
            {
                field.Select();

                if (field.Code.Text.Contains("Qualificação"))
                {
                    field.Select();
                    application.Selection.TypeText("José da Silva, brasileiro casado, morador do Parque Way ~e não deve brasileiro");
                }
                else if (field.Code.Text.Contains("Fatos"))
                {
                    field.Select();
                    application.Selection.TypeText("jogou bola em toda adoslescencia depois casou divorciou e morreu. mas antes disso foi da escola sçao jose" +
                        "Depois da escola voltou a sorri. Jogou ooutros esportes e depois começou a jogar video game. Trabalho na Feira do Senho Amiltom e depois" +
                        "como não gostava muito da função e vendas fundou uma rede de teve para parar de trabalhar." +
                        "jogou bola em toda adoslescencia depois casou divorciou e morreu. mas antes disso foi da escola sçao jose" +
                        "Depois da escola voltou a sorri. Jogou ooutros esportes e depois começou a jogar video game. Trabalho na Feira do Senho Amiltom e depois" +
                        "como não gostava muito da função e vendas fundou uma rede de teve para parar de trabalhar." +
                        "jogou bola em toda adoslescencia depois casou divorciou e morreu. mas antes disso foi da escola sçao jose" +
                        "Depois da escola voltou a sorri. Jogou ooutros esportes e depois começou a jogar video game. Trabalho na Feira do Senho Amiltom e depois" +
                        "como não gostava muito da função e vendas fundou uma rede de teve para parar de trabalhar." +
                        "jogou bola em toda adoslescencia depois casou divorciou e morreu. mas antes disso foi da escola sçao jose" +
                        "Depois da escola voltou a sorri. Jogou ooutros esportes e depois começou a jogar video game. Trabalho na Feira do Senho Amiltom e depois" +
                        "como não gostava muito da função e vendas fundou uma rede de teve para parar de trabalhar."
                        );
                }


            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //using (DocX document = DocX.Create(@"C:\GitHub\Test.docx"))

            //{

            //var doc

            /*
            foreach (var field in document.CustomProperties)
            {
                if (field.Value.Name.Contains("Fatos"))
                {
                    document.InsertParagraph("sim sim sim");
                }

            }
            */


            var filename = @"C:\GitHub\Test.docx";
            DocX document = DocX.Create(filename);
            document.ApplyTemplate(@"C:\GitHub\PeticaoModelo.docx");
            Paragraph p = document.InsertParagraph("alo Hordes");
            if (p.Text.Contains("ordes"))
            {
                Paragraph i = document.InsertParagraph("n n");
            }
            document.InsertParagraph("sim sim");


            document.Save();
            Process.Start("WINWORD.EXE", filename);
            //document.Tex/t.Select();




            // }
        }
        private void btnGerarDocx2_Click(object sender, EventArgs e)
        {

            var cliente1 = new Cliente
            {
                Nome = "Bruno Valadares do Nascimento",
                CPF = "011.916.601-17",
                RG = "2798",
                Nacionalidade = "brasileiro",
                EstadoCivil = EstadoCivil.solteiro,
                Sexo = Sexo.masculino,
                Logradouro = "Rua 59-A",
                Numero = 547,
                Setor = "Aeroporto",
                Cidade = "Goinaia",
                Estado = "GO"
            };

            string fontPadrao = "Courier New";
            int sizePadrao = 12;
            var firstLinePadrao = 6.0f;

            var cliente2 = new Cliente
            {
                Nome = "Mario do Carmo",
                CPF = "011.916.601-17",
                RG = "2798",
                Nacionalidade = "brasileiro",
                EstadoCivil = EstadoCivil.solteiro,
                Sexo = Sexo.masculino,
                Logradouro = "Rua 59-A",
                Numero = 547,
                Setor = "Aeroporto",
                Cidade = "Goinaia",
                Estado = "GO"
            };

            clientes.Add(cliente1);
            clientes.Add(cliente2);
            clientes.Add(cliente2);
            clientes.Add(cliente2);

            var fPadrao = new Formatting();
            fPadrao.Size = 12;
            fPadrao.FontFamily = new Xceed.Words.NET.Font("Arial");
            ;

            var fPadraoNegrito = new Formatting();
            fPadraoNegrito.Size = 12;
            fPadraoNegrito.FontFamily = new Xceed.Words.NET.Font("Arial");


            var filename = @"C:\GitHub\Test2.docx";
            using (DocX document = DocX.Create(filename))
            {

                document.ApplyTemplate(@"C:\GitHub\PeticaoModelo3.docx");
                Paragraph p1 = document.InsertParagraph();
                //document.ReplaceText()

                //endereçamento
                var padrao = new Formatting();
                padrao.FontFamily = new Xceed.Words.NET.Font("Arial");
                padrao.Size = 12;



                var padraoNegrito = new Formatting();
                padraoNegrito.FontFamily = new Xceed.Words.NET.Font("Arial");
                padraoNegrito.Size = 12;
                padraoNegrito.Bold = true;// Xceed.Words.NET.Formatting.;




                //headLineFormat.Position = 12;




                string enderecarPara = "JUIZ(A) FEDERAL DA VARA DO JUIZADO ESPECIAL FEDERAL DA SEÇÃO JUDICIÁRIA DO estado de Goiás";
                string enderecamento = ("EXCELENTÍSSIMO(A) SENHOR(A) " + enderecarPara).ToUpper();
                //p1.Append(enderecamento).Font(fontPadrao).FontSize(tamanhoPadrao).Bold().SpacingAfter(150);7                
                document.InsertParagraph(enderecamento, false, padraoNegrito).SpacingAfter(150);
                //document.InsertParagraph(enderecamento).FontSize(12).Font("Arial").Bold().SpacingAfter(150).Alignment = Alignment.right;

                if (p1.StyleName.Equals("jk"))
                {
                    p1.StyleName = "";
                    var estilo = p1.StyleName;
                }






                //qualificação
                string qualificacao = cliente1.Qualificacao_;
                Paragraph pQUalificacao = document.InsertParagraph();
                pQUalificacao.Alignment = Alignment.both;
                //pQUalificacao.InsertTabStopPosition(Alignment.left, 160f, TabStopPositionLeader.none);

                pQUalificacao.IndentationFirstLine = firstLinePadrao;
                //pQUalificacao.InsertTabStopPosition(Alignment.right, 432f, TabStopPositionLeader.none);
                //pQUalificacao.Append("\t");
                pQUalificacao.Append(cliente1.Nome).Font(fontPadrao).FontSize(sizePadrao).Bold();
                pQUalificacao.Append(cliente1.QualificacaoComNomeVazio_).Font(fontPadrao).FontSize(sizePadrao);

                //Parte principal
                string txt = ", por seu advogado que esta subscreve, vem à presença de Vossa Excelência, com fulcro na Legislação Pátria, propor a presente:";
                pQUalificacao.Append(txt).Font(fontPadrao).FontSize(sizePadrao);
                pQUalificacao.SetLineSpacing(LineSpacingType.Line, 1.5f);


                //ação
                txt = "ação de execução de título extrajudicial".ToUpper();
                Paragraph pAcao = document.InsertParagraph();
                pAcao.Append(txt).Font(fontPadrao).FontSize(sizePadrao).Bold().SpacingBefore(30).SpacingAfter(30);


                //em face de
                //var p = document.InsertParagraph();
                //p.Append("em face de: ").Font(fontPadrao).FontSize(tamanhoPadrao).Bold();

                //parte aAdversa                
                Paragraph pParteAdversa = document.InsertParagraph();
                pParteAdversa.Alignment = Alignment.both;

                //insira quantas partes houverem
                var p = document.InsertParagraph();
                for (int i = 0; i < clientes.Count; ++i)
                {
                    pParteAdversa = document.InsertParagraph();
                    pParteAdversa.IndentationFirstLine = firstLinePadrao;
                    pParteAdversa.Alignment = Alignment.both;

                    if (i == 0) pParteAdversa.Append("em face de: ").Font(fontPadrao).FontSize(sizePadrao).Bold();

                    pParteAdversa.Append(clientes[i].Nome).Font(fontPadrao).FontSize(sizePadrao).Bold();
                    pParteAdversa.Append(clientes[i].QualificacaoComNomeVazio_).Font(fontPadrao).FontSize(sizePadrao);

                    var pontuacao = (clientes.Count - i > 1) ? ";" : ".";

                    pParteAdversa.SetLineSpacing(LineSpacingType.Line, 1.5f);
                    pParteAdversa.Append(pontuacao);
                }





                //FATOS
                //Cabeçalho                
                document.InsertParagraph("DOS FATOS").FontSize(sizePadrao).Font(fontPadrao).Bold().SpacingAfter(0).Alignment = Alignment.left;

                txt = "Linha1-ação de execução de título extrajudicial de quem apenas gosta de jogar bola e não quer saber de nada que a banda toca. \nLinha2- mas se vc sabe onde estou então estamos bem.\nLinha3 aqui.";

                var docLines = txt.Split(new string[] { "\n" }, System.StringSplitOptions.None);

                var pFatos = document.InsertParagraph();
                foreach (var line in docLines)
                {
                    pFatos = document.InsertParagraph();
                    pFatos.Alignment = Alignment.both;
                    pFatos.Append(line).Font(fontPadrao).FontSize(sizePadrao).IndentationFirstLine = firstLinePadrao;
                    pFatos.SetLineSpacing(LineSpacingType.Line, 1.5f);

                }


                //PEDIDOS
                //Cabeçalho                
                document.InsertParagraph("DOS PEDIDOS").FontSize(sizePadrao).Font(fontPadrao).Bold().SpacingAfter(0).Alignment = Alignment.left;

                txt = "A) Este é o pedido 1\nB) Este é o pedido 2\nC) Este é o pedido 3.";

                docLines = txt.Split(new string[] { "\n" }, System.StringSplitOptions.None);

                foreach (var line in docLines)
                {
                    var pPedido = document.InsertParagraph();
                    pPedido.Alignment = Alignment.both;
                    pPedido.Append(line).Font(fontPadrao).FontSize(sizePadrao).IndentationFirstLine = firstLinePadrao;
                    pPedido.SetLineSpacing(LineSpacingType.Line, 1.5f);
                }

                //DATA E ASSINATURA
                //Cabeçalho 

                //data
                var data = string.Concat("Goiânia, ", DateTime.Today.ToString("dd"), " de ", DateTime.Today.ToString("MMMM"), " de ", DateTime.Today.ToString("yyyy"), ".");
                document.InsertParagraph(data).FontSize(sizePadrao).Font(fontPadrao).SpacingBefore(30).Alignment = Alignment.center;
                // document.InsertParagraph(data).FontSize(sizePadrao).Font(fontPadrao).SpacingBefore(30).Alignment = Alignment.center;

                //assinatura
                document.InsertParagraph("Paulo do Nascimento").FontSize(sizePadrao).Font(fontPadrao).SpacingBefore(15).SpacingAfter(1).Alignment = Alignment.center;
                document.InsertParagraph("OAB/GO 33863").FontSize(sizePadrao).Font(fontPadrao).SpacingBefore(1).Alignment = Alignment.center;

                //remover paragrafos vazios
                var emptyLines = document.Paragraphs.Where(o => string.IsNullOrEmpty(o.Text));
                foreach (var paragraph in emptyLines)
                {
                    paragraph.Remove(false);
                }





                document.Save();
                Process.Start("WINWORD.EXE", filename);
            }
        }

        public void gerarDocumento(Processo processo)
        {
            #region
            StringBuilder peticao = new StringBuilder();
            peticao.AppendLine("[endereçamento]");
            peticao.AppendLine("[qualificação], vêm a digna presença de vossa excelecência propor:");
            peticao.AppendLine("[peça]");
            peticao.AppendLine("[fatos]");
            peticao.AppendLine("SubTitulo1\n------");
            peticao.AppendLine("Isso é um fato, **isso é um negrito**, isso é um fato, isso é um fato, __isso é um sublinhado__, isso é um fato, isso é um fato, isso é um fato, isso é um fato");

            peticao.AppendLine("SubTitulo1\n------");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
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
                string qualificacao = string.Concat("**", cliente1.Nome, "**", " ", cliente1.QualificacaoComNomeVazio_);
                string enderecamento = "Excelentíssimo Senhor Doutor Juiz de Direito da Comarca de Goiania-GO";
                string nomePeca = "Ação Ordinária de Pensão por MORTE pedido";

                document.ReplaceText("[endereçamento]", enderecamento.ToUpper());
                document.ReplaceText("[qualificação]", qualificacao);
                document.ReplaceText("[peça]", nomePeca.ToUpper());
                document.ReplaceText("[fatos]", "Fatos");
                document.ReplaceText("[direito]", "Direito");
                document.ReplaceText("[pedido]", "Pedidos");
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
        private void btnGerarDocX5_Click(object sender, EventArgs e)
        {
            #region
            StringBuilder peticao = new StringBuilder();
            peticao.AppendLine("[endereçamento]");
            peticao.AppendLine("[qualificação], vêm a digna presença de vossa excelecência propor:");
            peticao.AppendLine("[peça]");
            peticao.AppendLine("[fatos]");
            peticao.AppendLine("SubTitulo1\n------");
            peticao.AppendLine("Isso é um fato, **isso é um negrito**, isso é um fato, isso é um fato, __isso é um sublinhado__, isso é um fato, isso é um fato, isso é um fato, isso é um fato");

            peticao.AppendLine("SubTitulo1\n------");
            peticao.AppendLine("Isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato, isso é um fato");
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
                string qualificacao = string.Concat("**", cliente1.Nome, "**", " ", cliente1.QualificacaoComNomeVazio_);
                string enderecamento = "Excelentíssimo Senhor Doutor Juiz de Direito da Comarca de Goiania-GO";
                string nomePeca = "Ação Ordinária de Pensão por MORTE pedido";

                document.ReplaceText("[endereçamento]", enderecamento.ToUpper());
                document.ReplaceText("[qualificação]", qualificacao);
                document.ReplaceText("[peça]", nomePeca.ToUpper());
                document.ReplaceText("[fatos]", "Fatos");
                document.ReplaceText("[direito]", "Direito");
                document.ReplaceText("[pedido]", "Pedidos");
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
        private void btnGerarDocx6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void advocaciaDataSetBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}

