using System.Windows;
using CineDiario.Models;
using CineDiario.Services;

namespace CineDiario.Views;

public partial class MainWindow : Window
{
    private readonly FilmeService servico = new();

    public MainWindow(Usuario usuario)
    {
        InitializeComponent();
        txtSaudacao.Text = $"{Saudacao()}, {usuario.Login}!";
        Atualizar();
    }

    private static string Saudacao()
    {
        var hora = DateTime.Now.Hour;
        if (hora < 12) return "Bom dia";
        if (hora < 18) return "Boa tarde";
        return "Boa noite";
    }

    private void Atualizar()
    {
        try
        {
            var r = servico.Resumo();
            txtTotal.Text = r.Total.ToString();
            txtGeneros.Text = r.Generos.ToString();
            txtMedia.Text = r.Media.HasValue ? $"★ {r.Media:0.0}" : "—";
            txtFila.Text = r.NaFila.ToString();
            txtRodape.Text = $"Você já passou {r.MinutosVistos / 60}h {r.MinutosVistos % 60}min vendo filmes por aqui.";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Não foi possível carregar o painel.\n\n" + ex.Message, "CineDiário", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Abrir(Window tela)
    {
        tela.Owner = this;
        tela.ShowDialog();
        Atualizar();
    }

    private void Estante_Click(object sender, RoutedEventArgs e) => Abrir(new EstanteWindow());

    private void Busca_Click(object sender, RoutedEventArgs e) => Abrir(new BuscaWindow());

    private void Podio_Click(object sender, RoutedEventArgs e) => Abrir(new PodioWindow());

    private void Roleta_Click(object sender, RoutedEventArgs e) => Abrir(new RoletaWindow());

    private void Sair_Click(object sender, RoutedEventArgs e)
    {
        var login = new LoginWindow();
        Application.Current.MainWindow = login;
        login.Show();
        Close();
    }
}
