using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public enum RamoDireito
    {        
        Previdenciário = 1,
        Civil = 2,
        Trabalhista = 3
    }
                

    public class NaturezaAcao
    {
        [Key]
        public int NaturezaAcaoID { get; set; }

        [Display(Name = "Ramo do Direito")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um registro!")]        
        public RamoDireito? RamoDireito { get; set; }

        
        [StringLength(50, MinimumLength = 3)]
        [Required]
        [Display(Name = "Natureza da Ação")]        
        public string Nome { get; set; }


        [Display(Name = "Comentário")]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }




    }
}