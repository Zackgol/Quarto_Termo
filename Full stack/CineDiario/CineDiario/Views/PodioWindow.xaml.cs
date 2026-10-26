using System.Windows;
using System.Windows.Controls;
using CineDiario.Models;
using CineDiario.Services;

namespace CineDiario.Views;

public partial class PodioWindow : Window
{
    private readonly FilmeService servico = new();

    public PodioWindow()
    {
        InitializeComponent();

        try
        {
            var d = servico.Destaques();

            Posicao(d.Podio, 0, txtPrimeiro, txtPrimeiroNota);
            Posicao(d.Podio, 1, txtSegundo, txtSegundoNota);
            Posicao(d.Podio, 2, txtTerceiro, txtTerceiroNota);

            txtGenero.Text = $"🎭  Gênero que você mais assiste: {d.GeneroFavorito}";
            txtDiretor.Text = $"🎬  Diretor com a melhor média: {d.DiretorFavorito}";
            txtLongo.Text = d.MaisLongo == null
                ? "⏱  Você ainda não marcou nenhum filme como assistido."
                : $"⏱  Maior maratona: {d.MaisLongo.Titulo} ({d.MaisLongo.Duracao})";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao montar o pódio.\n\n" + ex.Message, "CineDiário", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private static void Posicao(List<Filme> podio, int indice, TextBlock titulo, TextBlock nota)
    {
        if (indice < podio.Count)
        {
            titulo.Text = podio[indice].Titulo;
            nota.Text = podio[indice].Estrelas;
        }
        else
        {
            titulo.Text = "vago";
            nota.Text = "dê nota a mais filmes";
        }
    }
}
