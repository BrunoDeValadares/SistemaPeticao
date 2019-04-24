using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Context
{
    public class DBContext: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.Colaborador> Colaboradors { get; set; }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.NaturezaAcao> NaturezaAcaos { get; set; }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.Processo> Processoes { get; set; }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.PeticaoModelo> PeticaoModeloes { get; set; }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.ClienteDocumento> ClienteDocumentoes { get; set; }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.Documento> Documentoes { get; set; }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.Cliente> Clientes { get; set; }


        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.ProcessoDocumento> ProcessoDocumentoes { get; set; }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.ProcessoPeticao> ProcessoPeticaos { get; set; }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.ProcessoTabelaValor> ProcessoTabelaValors { get; set; }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.Questionario> Questionarios { get; set; }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.Pessoa> Pessoas { get; set; }

        public System.Data.Entity.DbSet<Sistema_Advocacia.Models.ProcessoParte> ProcessoPartes { get; set; }
    }
}