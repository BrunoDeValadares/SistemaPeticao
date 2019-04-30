using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public class DocumentoModelo
    {
        [key]
        public int DocumentoModeloId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comentarios { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataCriacao { get; set; }

        [ScaffoldColumn(false)]
        public string EnderecoArquivo { get; set; }

        [NotMapped]
        public string NomeArquivo
        {
            get { return new Regex(@"[^\\]*$").Match(EnderecoArquivo).Value; }
        }

        /*
        if (EnderecoArquivo != null)
            return new Regex(@"[^\\]*$").Match(EnderecoArquivo).Value;
        return null;
        */



    }

}
