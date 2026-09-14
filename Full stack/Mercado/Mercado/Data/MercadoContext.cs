using System.Linq;
using Mercado.Models;
using Microsoft.EntityFrameworkCore;

namespace Mercado.Data
{
    public class MercadoContext : DbContext
    {
        public DbSet<Produto> Produtos => Set<Produto>();
       
        private const string ConnectionString =
            "Server=localhost;Port=3306;Database=mercado;User=root;Password=vidaloka1;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().Property(p => p.Preco).HasPrecision(10, 2);
        }

        
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
