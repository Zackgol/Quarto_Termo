using CineDiario.Models;
using CineDiario.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CineDiario.Services;

public class FilmeService
{
    private readonly FilmeRepository repositorio = new();

    public List<Filme> Listar() => repositorio.Listar();

    public List<string> Generos() => repositorio.Generos();

    public void Salvar(Filme filme)
    {
        Validar(filme);

        try
        {
            if (filme.Id == 0)
                repositorio.Adicionar(filme);
            else
                repositorio.Atualizar(filme);
        }
        catch (DbUpdateException ex)
        {
            throw new RegraException("Não foi possível salvar o filme no banco.", ex);
        }
    }

    public void Excluir(int id)
    {
        try
        {
            if (!repositorio.Remover(id))
                throw new RegraException("Esse filme não existe mais na estante.");
        }
        catch (DbUpdateException ex)
        {
            throw new RegraException("Não foi possível excluir o filme.", ex);
        }
    }

    public List<Filme> Pesquisar(string? texto, string? genero, int? anoDe, int? anoAte, bool? assistido)
    {
        if (anoDe.HasValue && anoAte.HasValue && anoDe > anoAte)
            throw new RegraException("O ano inicial não pode ser maior que o final.");

        return repositorio.Pesquisar(texto, genero, anoDe, anoAte, assistido);
    }

    public List<Filme> CandidatosRoleta(int minutos, string? genero)
    {
        var candidatos = repositorio.NaFilaAte(minutos, genero);

        if (candidatos.Count == 0)
            throw new RegraException("Nenhum filme da fila cabe nesse tempo. Tenta aumentar o tempo ou trocar o gênero.");

        return candidatos;
    }

    public void MarcarAssistido(int id, int? nota)
    {
        var filme = repositorio.BuscarPorId(id) ?? throw new RegraException("Filme não encontrado.");
        filme.Assistido = true;
        filme.Nota = nota;
        Salvar(filme);
    }

    public Resumo Resumo()
    {
        var filmes = repositorio.Listar();
        var vistos = filmes.Where(f => f.Assistido).ToList();
        var comNota = vistos.Where(f => f.Nota.HasValue).ToList();

        return new Resumo(
            filmes.Count,
            filmes.Select(f => f.Genero).Distinct().Count(),
            comNota.Count > 0 ? comNota.Average(f => f.Nota!.Value) : null,
            filmes.Count(f => !f.Assistido),
            vistos.Sum(f => f.DuracaoMin));
    }

    public Destaques Destaques()
    {
        var vistos = repositorio.Listar().Where(f => f.Assistido).ToList();

        var podio = vistos
            .Where(f => f.Nota.HasValue)
            .OrderByDescending(f => f.Nota)
            .ThenByDescending(f => f.Ano)
            .Take(3)
            .ToList();

        var genero = vistos
            .GroupBy(f => f.Genero)
            .OrderByDescending(g => g.Count())
            .Select(g => $"{g.Key} ({g.Count()} filmes)")
            .FirstOrDefault() ?? "—";

        var diretor = vistos
            .GroupBy(f => f.Diretor)
            .Select(g => new { Nome = g.Key, Media = g.Average(f => f.Nota ?? 0) })
            .OrderByDescending(d => d.Media)
            .Select(d => $"{d.Nome} (média {d.Media:0.0})")
            .FirstOrDefault() ?? "—";

        var maisLongo = vistos.OrderByDescending(f => f.DuracaoMin).FirstOrDefault();

        return new Destaques(podio, genero, diretor, maisLongo);
    }

    private static void Validar(Filme filme)
    {
        if (string.IsNullOrWhiteSpace(filme.Titulo))
            throw new RegraException("Informe o título do filme.");

        if (filme.Ano < 1888 || filme.Ano > DateTime.Now.Year + 2)
            throw new RegraException($"O ano precisa estar entre 1888 e {DateTime.Now.Year + 2}.");

        if (filme.DuracaoMin < 1 || filme.DuracaoMin > 600)
            throw new RegraException("A duração precisa ser entre 1 e 600 minutos.");

        if (!filme.Assistido && filme.Nota.HasValue)
            throw new RegraException("Só dá pra dar nota depois de assistir. Marque como assistido.");

        if (filme.Nota is < 1 or > 5)
            throw new RegraException("A nota vai de 1 a 5 estrelas.");

        filme.Titulo = filme.Titulo.Trim();
        filme.Diretor = (filme.Diretor ?? "").Trim();
        filme.Genero = string.IsNullOrWhiteSpace(filme.Genero) ? "Outros" : filme.Genero.Trim();
    }
}
