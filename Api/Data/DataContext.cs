using System;
using System.Collections.Generic;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {

        }

        public DbSet<Cliente> Clientes {get; set;}

        private void ConfiguraCliente(ModelBuilder builder)
        {
            builder.Entity<Cliente>( etd => 
            {
                etd.ToTable("cliente");
                etd.HasKey(c => c.Id).HasName("id");
                etd.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
                etd.Property(c => c.NomeFantasia).HasColumnName("nome_fantasia").HasMaxLength(250);
                etd.Property(c => c.RazaoSocial).HasColumnName("razao_social").HasMaxLength(250);
                etd.Property(c => c.Cnpj).HasColumnName("cnpj").HasMaxLength(18);
                etd.Property(c => c.Email).HasColumnName("email").HasMaxLength(30);
                etd.Property(c => c.Telefone).HasColumnName("telefone").HasMaxLength(16);
                etd.Property(c => c.DataCadastro).HasColumnName("data_cadastro");
            });
        }

    //     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Hotsite");
    // }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseIdentityColumns();
            builder.HasDefaultSchema("hotsite");
            
            ConfiguraCliente(builder);
        }
    }
}