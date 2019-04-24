using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public class Processo
    {
        [Key]
        public int ProcessoId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int NaturezaAcaoId  { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        [Required]
        [Display(Name ="Resumo do Caso")]
        [DataType(DataType.MultilineText)]
        public string ResumoDoCaso { get; set; }
        
        [Display(Name ="Numero do Processo")]
        public string NumeroProcesso { get; set; }
        

        [Display(Name ="Link Processo")]
        [DataType(DataType.Url)]
        public string LinkProcesso { get; set; }

        public string Vara { get; set; }

        [Display(Name = "Endereçamento")]
        public string enderecamento { get; set; }

        /*
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }
        */

        public virtual Cliente Cliente{ get; set; }

        public virtual NaturezaAcao NaturezaAcao { get; set; }

        public virtual ICollection<ProcessoDocumento> ProcessoDocumentos { get; set; }
    }
}