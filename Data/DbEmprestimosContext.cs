using Microsoft.EntityFrameworkCore;
using EmprestimosAPI.Models;
using EmprestimosAPI.Token;

namespace EmprestimosAPI.Data
{
    public class DbEmprestimosContext : DbContext
    {
        public DbEmprestimosContext(DbContextOptions<DbEmprestimosContext> options)
            : base (options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Associacao> Associacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Local>()
                .HasMany(l => l.Equipamentos)
                .WithOne(e => e.Local)
                .HasForeignKey(e => e.IdLocal);

            modelBuilder.Entity<Associacao>(entity =>
            {
                entity.ToTable("Associacoes");

                entity.Property(e => e.IdAssociacao).HasColumnName("idassociacao"); // Nome atual da coluna
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");

                entity.Property(e => e.IdUsuario).HasColumnName("id");
                // Outras propriedades...
                entity.Property(e => e.IdAssociacao).HasColumnName("idassociacao"); // Novo nome da coluna
                                                                                    // Configure outras propriedades e relações aqui conforme necessário
            });

            modelBuilder.Entity<Equipamento>()
            .HasOne(e => e.Categoria) // Define a relação
            .WithMany() // Define o lado inverso da relação, se aplicável
            .HasForeignKey(e => e.IdCategoria); // Chave estrangeira na tabela Equipamentos

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.DataNascimento).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<Emprestimo>(entity =>
            {
                entity.Property(e => e.DataEmprestimo)
                      .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                      .HasColumnType("timestamp with time zone");

                entity.Property(e => e.DataDevolucaoEmprestimo)
                      .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                      .HasColumnType("timestamp with time zone");
            });

            modelBuilder.Entity<Emprestimo>()
            .HasOne(e => e.Pessoa)
            .WithMany()
            .HasForeignKey(e => e.IdPessoa)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Emprestimo>()
                .HasOne(e => e.Equipamento)
                .WithMany()
                .HasForeignKey(e => e.IdEquipamento)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }
}
