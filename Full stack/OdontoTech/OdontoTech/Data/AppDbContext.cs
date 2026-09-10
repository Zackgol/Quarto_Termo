using System.Data.Entity;
using System.Linq;
using OdontoTech.Models;
namespace OdontoTech.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        : base(@"Server=(localdb)\mssqllocaldb;Database=OdontoTechDB;Trusted_Connection=True;")
        {
            Database.CreateIfNotExists();
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public void CriarBancoESeed()
        {
            Database.CreateIfNotExists();
            // Insere usuário padrão se a tabela estiver vazia
            if (!Usuarios.Any())
            {
                Usuarios.Add(new Usuario
                {
                    Nome = "Dr. Hermilo",
                    Email = "admin@odonto.com",
                    Senha = "123"
                });
                SaveChanges();
            }
        }
    }
}