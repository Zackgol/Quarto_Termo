namespace CineDiario.Models;

public record Resumo(int Total, int Generos, double? Media, int NaFila, int MinutosVistos);

public record Destaques(List<Filme> Podio, string GeneroFavorito, string DiretorFavorito, Filme? MaisLongo);
