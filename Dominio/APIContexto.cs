﻿using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace apitreino
{
    public class APIContexto : DbContext
    {
        public APIContexto(DbContextOptions<APIContexto> options) : base(options)
        {
        }

        public DbSet<Pessoas> Pessoass { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoas>()
             .HasOne(p => p.Endereco)
             .WithMany()
             .HasForeignKey(p => p.EnderecoId);
        }

    }
}
