using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CineDiario.Services;

namespace CineDiario.Views;

public partial class LoginWindow : Window
{
    private readonly AuthService auth = new();
    private readonly DispatcherTimer cronometro = new() { Interval = TimeSpan.FromSeconds(1) };
    private int erros;
    private int segundosBloqueado;

    public LoginWindow()
    {
        InitializeComponent();
        cronometro.Tick += Cronometro_Tick;
        Loaded += (_, _) => txtLogin.Focus();
    }

    private void Entrar_Click(object sender, RoutedEventArgs e) => Entrar();

    private void Senha_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) Entrar();
    }

    private void Entrar()
    {
        if (!btnEntrar.IsEnabled) return;

        if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrEmpty(txtSenha.Password))
        {
            Avisar("Preencha usuário e senha.", false);
            return;
        }

        try
        {
            var usuario = auth.Entrar(txtLogin.Text, txtSenha.Password);
            var principal = new MainWindow(usuario);
            Application.Current.MainWindow = principal;
            principal.Show();
            Close();
        }
        catch (RegraException ex)
        {
            erros++;
            txtSenha.Clear();

            if (erros >= 3)
                Bloquear();
            else
                Avisar($"{ex.Message} Tentativa {erros} de 3.", false);
        }
        catch (Exception ex)
        {
            Avisar("Erro ao acessar o banco: " + ex.Message, false);
        }
    }

    private void CriarConta_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            auth.CriarConta(txtLogin.Text, txtSenha.Password);
            Avisar("Conta criada! Agora é só clicar em Entrar.", true);
        }
        catch (RegraException ex)
        {
            Avisar(ex.Message, false);
        }
        catch (Exception ex)
        {
            Avisar("Erro ao acessar o banco: " + ex.Message, false);
        }
    }

    private void Bloquear()
    {
        segundosBloqueado = 30;
        btnEntrar.IsEnabled = false;
        Avisar($"Muitas tentativas erradas. Espere {segundosBloqueado}s.", false);
        cronometro.Start();
    }

    private void Cronometro_Tick(object? sender, EventArgs e)
    {
        segundosBloqueado--;

        if (segundosBloqueado > 0)
        {
            Avisar($"Muitas tentativas erradas. Espere {segundosBloqueado}s.", false);
            return;
        }

        cronometro.Stop();
        erros = 0;
        btnEntrar.IsEnabled = true;
        Avisar("Pode tentar de novo.", true);
    }

    private void Avisar(string mensagem, bool deuCerto)
    {
        lblAviso.Foreground = (Brush)FindResource(deuCerto ? "Sucesso" : "Erro");
        lblAviso.Text = mensagem;
    }
}
