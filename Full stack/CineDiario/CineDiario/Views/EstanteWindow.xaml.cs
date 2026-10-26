using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CineDiario.Models;
using CineDiario.Services;

namespace CineDiario.Views;

public partial class EstanteWindow : Window
{
    private static readonly string[] GenerosPadrao = { "Ação", "Animação", "Comédia", "Documentário", "Drama", "Ficção", "Romance", "Suspense", "Terror" };

    private readonly FilmeService servico = new();
    private Filme? selecionado;

    public EstanteWindow()
    {
        InitializeComponent();
        Carregar();
        Limpar();
    }

    private void Carregar()
    {
        try
        {
            grade.ItemsSource = servico.Listar();
            cbGenero.ItemsSource = GenerosPadrao.Union(servico.Generos()).OrderBy(g => g).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao carregar a estante.\n\n" + ex.Message, "CineDiário", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Grade_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (grade.SelectedItem is not Filme filme) return;

        selecionado = filme;
        lblModo.Text = "Editando filme";
        txtTitulo.Text = filme.Titulo;
        txtDiretor.Text = filme.Diretor;
        cbGenero.Text = filme.Genero;
        txtAno.Text = filme.Ano.ToString();
        txtDuracao.Text = filme.DuracaoMin.ToString();
        chkAssistido.IsChecked = filme.Assistido;
        cbNota.SelectedIndex = filme.Nota ?? 0;
        btnExcluir.IsEnabled = true;
        lblAviso.Text = "";
    }

    private void Assistido_Mudou(object sender, RoutedEventArgs e)
    {
        cbNota.IsEnabled = chkAssistido.IsChecked == true;
        if (!cbNota.IsEnabled) cbNota.SelectedIndex = 0;
    }

    private void Salvar_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var filme = new Filme
            {
                Id = selecionado?.Id ?? 0,
                Titulo = txtTitulo.Text,
                Diretor = txtDiretor.Text,
                Genero = cbGenero.Text,
                Ano = Numero(txtAno, "Ano"),
                DuracaoMin = Numero(txtDuracao, "Duração"),
                Assistido = chkAssistido.IsChecked == true,
                Nota = cbNota.SelectedIndex > 0 ? cbNota.SelectedIndex : null
            };

            var novo = filme.Id == 0;
            servico.Salvar(filme);
            Carregar();
            Limpar();
            Avisar(novo ? $"\"{filme.Titulo}\" entrou na estante." : $"\"{filme.Titulo}\" foi atualizado.", true);
        }
        catch (RegraException ex)
        {
            Avisar(ex.Message, false);
        }
        catch (Exception ex)
        {
            Avisar("Erro inesperado: " + ex.Message, false);
        }
    }

    private void Excluir_Click(object sender, RoutedEventArgs e)
    {
        if (selecionado == null) return;

        var resposta = MessageBox.Show($"Tirar \"{selecionado.Titulo}\" da estante?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resposta != MessageBoxResult.Yes) return;

        try
        {
            var titulo = selecionado.Titulo;
            servico.Excluir(selecionado.Id);
            Carregar();
            Limpar();
            Avisar($"\"{titulo}\" saiu da estante.", true);
        }
        catch (RegraException ex)
        {
            Avisar(ex.Message, false);
        }
        catch (Exception ex)
        {
            Avisar("Erro inesperado: " + ex.Message, false);
        }
    }

    private void Novo_Click(object sender, RoutedEventArgs e) => Limpar();

    private void Limpar()
    {
        selecionado = null;
        grade.SelectedItem = null;
        lblModo.Text = "Novo filme";
        txtTitulo.Clear();
        txtDiretor.Clear();
        cbGenero.Text = "";
        txtAno.Clear();
        txtDuracao.Clear();
        chkAssistido.IsChecked = false;
        cbNota.SelectedIndex = 0;
        cbNota.IsEnabled = false;
        btnExcluir.IsEnabled = false;
        lblAviso.Text = "";
        txtTitulo.Focus();
    }

    private static int Numero(TextBox campo, string nome)
    {
        if (!int.TryParse(campo.Text.Trim(), out var valor))
            throw new RegraException($"{nome} precisa ser um número.");
        return valor;
    }

    private void Avisar(string mensagem, bool deuCerto)
    {
        lblAviso.Foreground = (Brush)FindResource(deuCerto ? "Sucesso" : "Erro");
        lblAviso.Text = mensagem;
    }
}
