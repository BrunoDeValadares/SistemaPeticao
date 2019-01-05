using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public class Questionario
    {
        
        [Key]
        public int QuestionarioId { get; set; }

        public int ProcessoPeticaoId { get; set; }

        //public int PeticaoModeloId { get; set; }


        public string TituloTrecho { get; set; }

        public string Pergunta { get; set; }

        public string Resposta { get; set; }

        public string Exemplo { get; set; }
        
        public DateTime DataModificacao { get; set; }

        public virtual PeticaoModelo PeticaoModelo { get; set; }

        public virtual ProcessoPeticao ProcessoPeticao { get; set; }

    }
}