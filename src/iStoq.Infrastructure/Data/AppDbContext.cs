using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using iStoq.Domain.Entities;

namespace iStoq.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<StockMovement> StockMovements => Set<StockMovement>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Suprime o aviso de modelo dinâmico
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definição de precisão de preço
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // +---------------+
            // | Relacionamentos
            // +---------------+
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.Product)
                .WithMany()
                .HasForeignKey(sm => sm.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // SEED DE DADOS
            // -----------------------------
            var bebidasId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var fornecedorId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var produtoId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            modelBuilder.Entity<Category>().HasData(new
            {
                Id = bebidasId,
                Name = "Bebidas",
                Description = "Líquidos em geral"
            });

            modelBuilder.Entity<Supplier>().HasData(new
            {
                Id = fornecedorId,
                Name = "ACME Ltda",
                CNPJ = "12345678000190",
                Email = "contato@acme.com",
                Phone = "11999990000"
            });

            modelBuilder.Entity<Product>().HasData(new
            {
                Id = produtoId,
                Name = "Coca-Cola",
                Description = "Refrigerante de cola",
                Price = 6.5m,
                Stock = 100,
                CreatedAt = new DateTime(2024, 01, 01), // valor fixo para evitar o warning
                CategoryId = bebidasId,
                SupplierId = fornecedorId
            });
        }
    }
}
