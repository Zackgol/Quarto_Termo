using CineDiario.Data;
using CineDiario.Models;
using Microsoft.EntityFrameworkCore;

namespace CineDiario.Repositories;

public class UsuarioRepository
{
    public Usuario? BuscarPorLogin(string login)
    {
        using var db = new CineContext();
        return db.Usuarios.AsNoTracking().FirstOrDefault(u => u.Login == login);
    }

    public bool Existe(string login)
    {
        using var db = new CineContext();
        return db.Usuarios.Any(u => u.Login == login);
    }

    public void Adicionar(Usuario usuario)
    {
        using var db = new CineContext();
        db.Usuarios.Add(usuario);
        db.SaveChanges();
    }
}
