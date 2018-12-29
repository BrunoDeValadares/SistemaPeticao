using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Gerador
{
    public class PerguntaPeticao
    {
        public int PerguntaPeticaoId { get; set; }

        public string Trecho{ get; set; }

        public string Pergunta { get; set; }

        public string Resposta { get; set; }
    }


}