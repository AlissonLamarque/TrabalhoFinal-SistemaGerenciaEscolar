using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Data
{
    public class PF_GerenciaEscolarDbContext : IdentityDbContext
    {

        public PF_GerenciaEscolarDbContext(DbContextOptions<PF_GerenciaEscolarDbContext> options) : base(options)
        { }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DEVELOPER;initial Catalog=PF_GerenciaEscolar;User ID=PF_GerenciaEscolar;password=senha_PF;Trust Server Certificate=True;language=Portuguese;Trusted_Connection=True;");
            optionsBuilder.UseLazyLoadingProxies();
        }

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