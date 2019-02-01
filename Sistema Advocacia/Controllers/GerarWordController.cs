using Sistema_Advocacia.gerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Advocacia.Controllers
{
    public class GerarWordController : Controller
    {
        public ActionResult GerarWord()
        {
            return View();
        }


        // GET: GerarWord
        public ActionResult Index()
        {
            return View();
        }
        public void GerarDoc()
        {
            GerarArquivo gerarArquivo = new GerarArquivo();
            gerarArquivo.Gerar();            
        }
    }
}