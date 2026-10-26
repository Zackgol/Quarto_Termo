using System.Windows;
using System.Windows.Threading;
using CineDiario.Models;
using CineDiario.Services;

namespace CineDiario.Views;

public partial class RoletaWindow : Window
{
    private const string Qualquer = "Qualquer um";
    private readonly FilmeService servico = new();
    private readonly DispatcherTimer giro = new() { Interval = TimeSpan.FromMilliseconds(90) };
    private List<Filme> candidatos = new();
    private Filme? sorteado;
    private int voltas;

    public RoletaWindow()
    {
        InitializeComponent();
        giro.Tick += Giro_Tick;
        AtualizarTempo();

        try
        {
            var generos = new List<string> { Qualquer };
            generos.AddRange(servico.Generos());
            cbGenero.ItemsSource = generos;
            cbGenero.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao carregar os gêneros.\n\n" + ex.Message, "CineDiário", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Tempo_Mudou(object sender, RoutedPropertyChangedEventArgs<double> e) => AtualizarTempo();

    private void AtualizarTempo()
    {
        if (lblTempo == null) return;
        var minutos = (int)sldTempo.Value;
        lblTempo.Text = $"{minutos / 60}h {minutos % 60:00}min";
    }

    private void Girar_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var genero = cbGenero.SelectedItem as string;
            candidatos = servico.CandidatosRoleta((int)sldTempo.Value, genero == Qualquer ? null : genero);
        }
        catch (RegraException ex)
        {
            txtFilme.Text = "Nada por aqui";
            txtDetalhe.Text = ex.Message;
            btnAssistido.Visibility = Visibility.Collapsed;
            return;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao girar a roleta.\n\n" + ex.Message, "CineDiário", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        sorteado = candidatos[Random.Shared.Next(candidatos.Count)];
        voltas = 0;
        btnGirar.IsEnabled = false;
        btnAssistido.Visibility = Visibility.Collapsed;
        txtDetalhe.Text = "girando...";
        giro.Start();
    }

    private void Giro_Tick(object? sender, EventArgs e)
    {
        voltas++;

        if (voltas < 18)
        {
            txtFilme.Text = candidatos[Random.Shared.Next(candidatos.Count)].Titulo;
            return;
        }

        giro.Stop();
        btnGirar.IsEnabled = true;

        if (sorteado == null) return;

        txtFilme.Text = sorteado.Titulo;
        var sobra = (int)sldTempo.Value - sorteado.DuracaoMin;
        txtDetalhe.Text = $"{sorteado.Genero} • {sorteado.Ano} • {sorteado.Diretor}\n{sorteado.Duracao} de filme, ainda sobram {sobra} min pra pipoca.";
        btnAssistido.Visibility = Visibility.Visible;
    }

    private void Assistido_Click(object sender, RoutedEventArgs e)
    {
        if (sorteado == null) return;

        try
        {
            servico.MarcarAssistido(sorteado.Id, null);
            txtDetalhe.Text = $"\"{sorteado.Titulo}\" saiu da fila. Lembre de dar a nota na Estante!";
            btnAssistido.Visibility = Visibility.Collapsed;
            sorteado = null;
        }
        catch (RegraException ex)
        {
            txtDetalhe.Text = ex.Message;
        }
    }
}
