using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public class PeticaoModelo
    {
        [Key]
        public int PedicaoModeloId { get; set; }

        [Required(ErrorMessage ="Digite um valor para o campo {0}!")]
        public string Nome { get; set; }        

        [Display(Name ="Natureza da ação")]
        [Required(ErrorMessage = "Digite um valor para o campo {0}!")]
        public int NaturezaAcaoId { get; set; }

        [Display(Name = "Petição")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Digite um valor para o campo {0}!")]
        public string Peticao { get; set; }        

        [Display(Name = "Comentários")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Digite um valor para o campo {0}!")]
        public string Comentario { get; set; }

        public virtual NaturezaAcao NaturezaAcao { get; set; }
    }
}