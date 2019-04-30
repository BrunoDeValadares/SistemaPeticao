namespace Testes_c_charpe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PeticaoModelo")]
    public partial class PeticaoModelo
    {
        public int PeticaoModeloId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public int NaturezaAcaoId { get; set; }

        public string Comentario { get; set; }

        [Required]
        public string PeticaoOriginal { get; set; }

        public string PeticaoModificada { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeAcao { get; set; }

        public string Direito { get; set; }

        public string Pedido { get; set; }
    }
}
