using Sistema_Advocacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.ViewModels
{
    public class TodoProcesso
    {
        public Processo Processo { get; set; }

        public List<ProcessoPeticao> ProcessoPeticoes { get; set; }

        public List<ProcessoDocumento> ProcessoDocumentos { get; set; }

        public List<ProcessoTabelaValor> ProcessoTabelaValores { get; set; }

    }
}