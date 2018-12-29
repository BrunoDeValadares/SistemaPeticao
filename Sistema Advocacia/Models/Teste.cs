using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public class Teste
    {
        [Key]
        public int TesteCod { get; set; }

        
        public string nome { get; set; }
    }
}