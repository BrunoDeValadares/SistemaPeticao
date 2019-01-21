using Sistema_Advocacia.gerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public class Questionario
    {
        
        [Key]
        public int QuestionarioId { get; set; }

        //[Display(AutoGenerateField = false)]        
        //[ScaffoldColumn(false)]
        public int ProcessoPeticaoId { get; set; }

        //public int PeticaoModeloId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name ="Trecho")]            
        public string TituloTrecho { get; set; }

        public string Pergunta { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Resposta { get; set; }

        public string Exemplo { get; set; }
        
        [Display(Name = "Data Modificação")]
        public DateTime DataModificacao { get; set; }

        //public virtual PeticaoModelo PeticaoModelo { get; set; }

        public virtual ProcessoPeticao ProcessoPeticao { get; set; }


        
        [NotMapped]
        //[DataType(DataType.MultilineText)]
        [Display(Name = "Exemplo")]
        public string _GetExemplo
        {
            get
            {
                GerarQuestionario gerarQuestionario = new GerarQuestionario();
                return gerarQuestionario.GetExemplo(ProcessoPeticao.PeticaoModelo.PeticaoOriginal, TituloTrecho);
            }
            
        }



    }
}