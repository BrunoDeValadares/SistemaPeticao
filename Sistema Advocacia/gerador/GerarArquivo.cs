using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.gerador
{
    public class GerarArquivo
    {
        public void Gerar() { 

        var application = new Microsoft.Office.Interop.Word.Application();
        var document = new Microsoft.Office.Interop.Word.Document();
            //var document2 = new Microsoft.Office.Interop.Word.Document();

            //string templatePath = @"~\Templates\PeticaoModelo.docx";
            //var templatePath = Server.MapPath("~/Templates/report.xlsx");



            //var templatePath = System.Web.HttpContext.Current.Server.MapPath("~/Templates/PeticaoModelo.docx");
            var templatePath = @"C:\GitHub\PeticaoModelo.docx";




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
                        "jogou bola em toda adoslescencia depois casou divorciou e morreu. mas antes disso foi da escola sçao jose" 
                        );
                }


            }
        }

    }
}