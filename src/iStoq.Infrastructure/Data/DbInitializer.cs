using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using iStoq.Domain.Entities;

namespace iStoq.Infrastructure.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider, IHostEnvironment env)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Applies pending migrations
            context.Database.Migrate();

            // Just seed the database if environment is development
            if (env.IsDevelopment())
            {
                SeedDevData(context);
            }
        }

        // Initial data example (avoiding duplicate data)
        private static void SeedDevData(AppDbContext context)
        {
            // Evita duplicações
            if (context.Categories.Any() || context.Suppliers.Any() || context.Products.Any())
                return;

            // GUIDs fixos
            var categoriaId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var fornecedorId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var produtoId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            // CATEGORIA
            var categoria = new Category("Utilidades", "Produtos diversos");
            categoria.Id = categoriaId;
            context.Categories.Add(categoria);

            // FORNECEDOR
            var fornecedor = new Supplier("Fornecedor Teste", "11122233344455", "dev@teste.com", "11988887777");
            fornecedor.Id = fornecedorId;
            context.Suppliers.Add(fornecedor);

            // PRODUTO
            var produto = new Product("Caneca", "Caneca de cerâmica personalizada", 25.90m, produtoId, 100);
            produto.CategoryId = categoriaId;
            produto.SupplierId = fornecedorId;
            context.Products.Add(produto);

            // MOVIMENTAÇÃO DE ESTOQUE
            var entrada = new StockMovement(produtoId, 100, "IN", "Entrada inicial", DateTime.UtcNow)
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444")
            };
            context.StockMovements.Add(entrada);

            context.SaveChanges();
        }
    }
}
