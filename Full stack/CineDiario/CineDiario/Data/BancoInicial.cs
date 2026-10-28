using CineDiario.Models;
using CineDiario.Services;
using Microsoft.EntityFrameworkCore;

namespace CineDiario.Data;

public static class BancoInicial
{
    public static void Preparar()
    {
        using var db = new CineContext();
        db.Database.Migrate();

        if (!db.Usuarios.Any())
        {
            var (hash, salt) = Senha.Gerar("cinema123");
            db.Usuarios.Add(new Usuario { Login = "admin", SenhaHash = hash, Salt = salt });
        }

        if (!db.Filmes.Any())
        {
            db.Filmes.AddRange(
                new Filme { Titulo = "Cidade de Deus", Diretor = "Fernando Meirelles", Genero = "Drama", Ano = 2002, DuracaoMin = 130, Nota = 5, Assistido = true },
                new Filme { Titulo = "O Auto da Compadecida", Diretor = "Guel Arraes", Genero = "Comédia", Ano = 2000, DuracaoMin = 104, Nota = 5, Assistido = true },
                new Filme { Titulo = "Interestelar", Diretor = "Christopher Nolan", Genero = "Ficção", Ano = 2014, DuracaoMin = 169, Nota = 5, Assistido = true },
                new Filme { Titulo = "Tropa de Elite", Diretor = "José Padilha", Genero = "Ação", Ano = 2007, DuracaoMin = 115, Nota = 4, Assistido = true },
                new Filme { Titulo = "Parasita", Diretor = "Bong Joon-ho", Genero = "Suspense", Ano = 2019, DuracaoMin = 132, Nota = 4, Assistido = true },
                new Filme { Titulo = "De Volta para o Futuro", Diretor = "Robert Zemeckis", Genero = "Ficção", Ano = 1985, DuracaoMin = 116, Nota = 4, Assistido = true },
                new Filme { Titulo = "Toy Story", Diretor = "John Lasseter", Genero = "Animação", Ano = 1995, DuracaoMin = 81, Nota = 3, Assistido = true },
                new Filme { Titulo = "Corra!", Diretor = "Jordan Peele", Genero = "Terror", Ano = 2017, DuracaoMin = 104, Nota = 4, Assistido = true },
                new Filme { Titulo = "Central do Brasil", Diretor = "Walter Salles", Genero = "Drama", Ano = 1998, DuracaoMin = 113 },
                new Filme { Titulo = "O Poderoso Chefão", Diretor = "Francis Ford Coppola", Genero = "Drama", Ano = 1972, DuracaoMin = 175 },
                new Filme { Titulo = "Bacurau", Diretor = "Kleber Mendonça Filho", Genero = "Suspense", Ano = 2019, DuracaoMin = 131 },
                new Filme { Titulo = "A Viagem de Chihiro", Diretor = "Hayao Miyazaki", Genero = "Animação", Ano = 2001, DuracaoMin = 125 }
            );
        }

        db.SaveChanges();
    }
}
