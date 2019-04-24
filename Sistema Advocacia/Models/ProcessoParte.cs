using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public enum Papel
    {
        autor = 1,
        réu = 2
    };

    public class ProcessoParte
    {
        [key]
        public int ProcessoParteId { get; set; }

        [ScaffoldColumn(false)]
        public int ProcessoId { get; set; }
        
        public int PessoaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione um valor")]           
        public Papel Papel { get; set; }
        
        public virtual Processo Processo { get; set; }

        public virtual Pessoa Pessoa{ get; set; }

    }
}