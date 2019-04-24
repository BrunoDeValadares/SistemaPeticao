using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{

    /* repensar para apagar
    public enum RamoDireito
    {
        Previdenciário = 1,
        Civil = 2,
        Trabalhista = 3
    }
    */


    public class PeticaoModelo
    {
        
        [Key]
        public int PeticaoModeloId { get; set; }

        /* repensar para apagar
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um valor para Ramo do Direito!")]
        [Display(Name ="Ramo do Direito")]
        public RamoDireito RamoDireito{ get; set; }
        */

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Nome { get; set; }

        [Display(Name ="Nome da Ação")]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string NomeAcao { get; set; }

        [Required]
        [Display(Name ="Natureza da ação")]
        //[Required(ErrorMessage = "Digite um valor para o campo {0}!")]
        public int NaturezaAcaoId { get; set; }

        [Required]
        [Display(Name = "Petição Original")]
        [DataType(DataType.MultilineText)]
        public string PeticaoOriginal { get; set; }

              
        [Display(Name = "Petição Modificada")]
        [DataType(DataType.MultilineText)]       
        public string PeticaoModificada { get; set; }

        [Display(Name = "Direito")]
        public string Direito { get; set; }

        public string Pedido { get; set; }

        //public string Peticao { get; set; }

        [Display(Name = "Comentários")]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        public virtual NaturezaAcao NaturezaAcao { get; set; }        
    }
}