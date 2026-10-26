using CineDiario.Data;
using CineDiario.Models;
using Microsoft.EntityFrameworkCore;

namespace CineDiario.Repositories;

public class FilmeRepository
{
    public List<Filme> Listar()
    {
        using var db = new CineContext();
        return db.Filmes.AsNoTracking().OrderBy(f => f.Titulo).ToList();
    }

    public Filme? BuscarPorId(int id)
    {
        using var db = new CineContext();
        return db.Filmes.AsNoTracking().FirstOrDefault(f => f.Id == id);
    }

    public void Adicionar(Filme filme)
    {
        using var db = new CineContext();
        db.Filmes.Add(filme);
        db.SaveChanges();
    }

    public void Atualizar(Filme filme)
    {
        using var db = new CineContext();
        db.Filmes.Update(filme);
        db.SaveChanges();
    }

    public bool Remover(int id)
    {
        using var db = new CineContext();
        var filme = db.Filmes.Find(id);
        if (filme == null) return false;
        db.Filmes.Remove(filme);
        db.SaveChanges();
        return true;
    }

    public List<Filme> Pesquisar(string? texto, string? genero, int? anoDe, int? anoAte, bool? assistido)
    {
        using var db = new CineContext();
        var consulta = db.Filmes.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(texto))
        {
            var termo = $"%{texto.Trim()}%";
            consulta = consulta.Where(f => EF.Functions.Like(f.Titulo, termo) || EF.Functions.Like(f.Diretor, termo));
        }

        if (!string.IsNullOrWhiteSpace(genero))
            consulta = consulta.Where(f => f.Genero == genero);

        if (anoDe.HasValue)
            consulta = consulta.Where(f => f.Ano >= anoDe.Value);

        if (anoAte.HasValue)
            consulta = consulta.Where(f => f.Ano <= anoAte.Value);

        if (assistido.HasValue)
            consulta = consulta.Where(f => f.Assistido == assistido.Value);

        return consulta.OrderBy(f => f.Ano).ThenBy(f => f.Titulo).ToList();
    }

    public List<Filme> NaFilaAte(int minutos, string? genero)
    {
        using var db = new CineContext();
        var consulta = db.Filmes.AsNoTracking().Where(f => !f.Assistido && f.DuracaoMin <= minutos);

        if (!string.IsNullOrWhiteSpace(genero))
            consulta = consulta.Where(f => f.Genero == genero);

        return consulta.ToList();
    }

    public List<string> Generos()
    {
        using var db = new CineContext();
        return db.Filmes.Select(f => f.Genero).Distinct().OrderBy(g => g).ToList();
    }
}
