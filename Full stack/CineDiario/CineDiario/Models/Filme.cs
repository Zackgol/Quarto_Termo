using System.ComponentModel.DataAnnotations.Schema;

namespace CineDiario.Models;

public class Filme
{
    public int Id { get; set; }
    public string Titulo { get; set; } = "";
    public string Diretor { get; set; } = "";
    public string Genero { get; set; } = "";
    public int Ano { get; set; }
    public int DuracaoMin { get; set; }
    public int? Nota { get; set; }
    public bool Assistido { get; set; }

    [NotMapped]
    public string Estrelas => Nota.HasValue ? new string('★', Nota.Value) + new string('☆', 5 - Nota.Value) : "—";

    [NotMapped]
    public string Duracao => $"{DuracaoMin / 60}h {DuracaoMin % 60:00}min";

    [NotMapped]
    public string Situacao => Assistido ? "Assistido" : "Na fila";
}
