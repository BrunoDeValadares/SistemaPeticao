using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public class ProcessoPeticao
    {
        [Key]
        public int ProcessoPeticaoId { get; set; }

        [Required]
        [Display(Name = "Processo")]
        public int ProcessoId { get; set; }

        [Required]
        [Display(Name = "Peticao")]
        public int PeticaoModeloId { get; set; }        

        [Display(Name = "Link Questionário")]
        [DataType(DataType.Url)]
        public string LinkQuestionario { get; set; }

        [Display(Name = "Comentário")]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set;}

        [Display(Name = "Link Petição")]
        [DataType(DataType.Url)]
        public string LinkPeticao { get; set; }

        [Display(Name = "Finalizada?")]
        public bool Finalizada { get; set; }

        public virtual PeticaoModelo PeticaoModelo { get; set; }
    }
}