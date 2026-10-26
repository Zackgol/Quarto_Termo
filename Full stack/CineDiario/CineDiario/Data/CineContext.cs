using CineDiario.Models;
using Microsoft.EntityFrameworkCore;

namespace CineDiario.Data;

public class CineContext : DbContext
{
    public DbSet<Filme> Filmes => Set<Filme>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    private const string Conexao = "server=localhost;port=3306;database=cinediario;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseMySql(Conexao, new MySqlServerVersion(new Version(8, 0, 36)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filme>(e =>
        {
            e.Property(f => f.Titulo).IsRequired().HasMaxLength(120);
            e.Property(f => f.Diretor).HasMaxLength(80);
            e.Property(f => f.Genero).HasMaxLength(30);
        });

        modelBuilder.Entity<Usuario>(e =>
        {
            e.Property(u => u.Login).IsRequired().HasMaxLength(40);
            e.HasIndex(u => u.Login).IsUnique();
        });
    }
}
