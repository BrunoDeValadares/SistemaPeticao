namespace Testes_c_charpe
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BD : DbContext
    {
        public BD()
            : base("name=BD")
        {
        }

        public virtual DbSet<PeticaoModelo> PeticaoModelo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
