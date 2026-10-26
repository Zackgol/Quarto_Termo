using System.Windows;
using System.Windows.Input;
using CineDiario.Services;

namespace CineDiario.Views;

public partial class BuscaWindow : Window
{
    private const string Todos = "Todos";
    private readonly FilmeService servico = new();

    public BuscaWindow()
    {
        InitializeComponent();

        try
        {
            var generos = new List<string> { Todos };
            generos.AddRange(servico.Generos());
            cbGenero.ItemsSource = generos;
            cbGenero.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao carregar os gêneros.\n\n" + ex.Message, "CineDiário", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        Pesquisar();
    }

    private void Pesquisar_Click(object sender, RoutedEventArgs e) => Pesquisar();

    private void Campo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) Pesquisar();
    }

    private void Pesquisar()
    {
        try
        {
            var genero = cbGenero.SelectedItem as string;
            bool? assistido = cbSituacao.SelectedIndex switch
            {
                1 => true,
                2 => false,
                _ => null
            };

            var resultado = servico.Pesquisar(
                txtTexto.Text,
                genero == Todos ? null : genero,
                Ano(txtAnoDe.Text),
                Ano(txtAnoAte.Text),
                assistido);

            grade.ItemsSource = resultado;
            lblResultado.Text = resultado.Count switch
            {
                0 => "Nenhum filme encontrado com esses filtros.",
                1 => "1 filme encontrado.",
                _ => $"{resultado.Count} filmes encontrados, somando {resultado.Sum(f => f.DuracaoMin) / 60}h de filme."
            };
        }
        catch (RegraException ex)
        {
            lblResultado.Text = ex.Message;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro na pesquisa.\n\n" + ex.Message, "CineDiário", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private static int? Ano(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return null;
        if (int.TryParse(texto.Trim(), out var ano)) return ano;
        throw new RegraException("O ano precisa ser um número, tipo 2010.");
    }
}
