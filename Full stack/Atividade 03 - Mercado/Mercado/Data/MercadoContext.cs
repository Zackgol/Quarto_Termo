using System.Linq;
using Mercado.Models;
using Microsoft.EntityFrameworkCore;

namespace Mercado.Data
{
    public class MercadoContext : DbContext
    {
        public DbSet<Produto> Produtos => Set<Produto>();

        // Banco em arquivo único (SQLite), não precisa instalar servidor nenhum.
        private const string ConnectionString = "Data Source=mercado.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);

            // Para usar SQL Server em vez de SQLite, troque a linha acima por:
            // optionsBuilder.UseSqlServer(
            //     @"Server=(localdb)\mssqllocaldb;Database=Mercado;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().Property(p => p.Preco).HasPrecision(10, 2);
        }

        /// <summary>Garante que o banco existe e insere alguns produtos de exemplo na primeira vez.</summary>
        public void GarantirBancoCriadoESeed()
        {
            Database.EnsureCreated();

            if (Produtos.Any())
                return;

            Produtos.AddRange(
                new Produto { Nome = "Arroz 5kg", Categoria = "Mercearia", Preco = 24.90m, Estoque = 40 },
                new Produto { Nome = "Feijão 1kg", Categoria = "Mercearia", Preco = 8.50m, Estoque = 60 },
                new Produto { Nome = "Leite Integral 1L", Categoria = "Laticínios", Preco = 5.30m, Estoque = 80 },
                new Produto { Nome = "Queijo Mussarela 500g", Categoria = "Laticínios", Preco = 22.00m, Estoque = 15 },
                new Produto { Nome = "Sabão em Pó 1kg", Categoria = "Limpeza", Preco = 14.90m, Estoque = 25 },
                new Produto { Nome = "Detergente 500ml", Categoria = "Limpeza", Preco = 2.80m, Estoque = 50 },
                new Produto { Nome = "Refrigerante 2L", Categoria = "Bebidas", Preco = 9.00m, Estoque = 30 },
                new Produto { Nome = "Banana Prata (kg)", Categoria = "Hortifruti", Preco = 6.20m, Estoque = 45 }
            );

            SaveChanges();
        }
    }
}
