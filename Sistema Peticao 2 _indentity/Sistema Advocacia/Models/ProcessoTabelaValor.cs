using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public class ProcessoTabelaValor
    {
        [Key]
        public int ProcessoTabelaValorId { get; set; }

        public int ProcessoId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        [Display(Name ="Origem do Crédito")]
        public string OrigemCredito { get; set; }


        [Required]
        [DataType(DataType.Url)]
        [Display(Name ="Link Boleto")]
        public string LinkDoc { get; set; }

        [Required]
        [Display(Name = "Data do Débito")]
        [DataType(DataType.Date)]
        public DateTime DataDebito { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name ="Valor Original")]
        public decimal ValorOriginal{ get; set; }

        [Required]
        [DataType(DataType.Date)]        
        [Display(Name ="Atualizado até")]        
        public DateTime DataAtualizacao { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor Atualizado")]
        public decimal ValorAtualizado { get; set; }


    }
}