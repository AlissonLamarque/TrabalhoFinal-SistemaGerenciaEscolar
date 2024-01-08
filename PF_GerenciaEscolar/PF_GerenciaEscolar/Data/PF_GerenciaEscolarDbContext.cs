using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Data
{
    public class PF_GerenciaEscolarDbContext : IdentityDbContext
    {

        public PF_GerenciaEscolarDbContext(DbContextOptions<PF_GerenciaEscolarDbContext> options) : base(options)
        { }

        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Autenticacao> Autenticadores { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Nota>(entity =>
            {
                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(18, 2)");
            });
        }
        
    }
}
