using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public class ClienteDocumento
    {
        [Key]
        public int ClienteDocumentoId { get; set; }

        
        [Required]
        public int ClienteId { get; set; }

        [Required]
        [Display(Name ="Documento")]
        public int DocumentoId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        [Display(Name ="Entregue?")]
        public bool Entregue { get; set; }

        [Display(Name = "Link")]
        [DataType(DataType.Url)]
        public string Link { get; set; }

        public virtual Cliente Cliente{ get; set; }

        public virtual Documento Documento { get; set; }
    }
}