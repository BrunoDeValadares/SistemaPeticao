using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public class Documento
    {
        [Key]
        public int DocumentoId { get; set; }

        [Display(Name = "Documento")]
        [Required]
        public string Nome { get; set; }

        [Display(Name = "Comentários")]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        [Display(Name = "Civil?")]
        public bool Civil { get; set; }

        [Display(Name ="Previdenciário?")]
        public bool Previdenciario { get; set; }

        [Display(Name = "Direito do Trabalho?")]
        public bool DireitoTrabalho{ get; set; }







    }
}