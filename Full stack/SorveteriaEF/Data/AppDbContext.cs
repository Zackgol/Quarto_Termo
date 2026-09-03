using System.Data.Entity;
using SorveteriaEF.Models;

namespace SorveteriaEF.Data
{
    public class AppDbContext : DbContext
    {
        // O nome passado em base("name=...") referencia a chave no App.config
        public AppDbContext() : base("name=ConexaoSQLServer")
        {
        }

        public DbSet<Sorvete> Sorvetes { get; set; }
    }
}