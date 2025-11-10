using FlorecaStore.Models;
using FlorecaStore.Models.Auxiliares;
using Microsoft.EntityFrameworkCore;

namespace FlorecaStore.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Nome)
                .IsUnique();
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(20);

            modelBuilder.Entity<Entrada>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Produtos>()
                .HasMany(p => p.Entradas)
                .WithOne(e => e.Produto)
                .HasForeignKey(e => e.ProdutoId);

            modelBuilder.Entity<Item>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Sale>()
                .HasMany(s => s.Items)
                .WithOne(i => i.Sale)
                .HasForeignKey(i => i.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
