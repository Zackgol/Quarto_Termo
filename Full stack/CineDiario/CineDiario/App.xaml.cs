using System.Windows;
using CineDiario.Data;
using CineDiario.Views;

namespace CineDiario;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            BancoInicial.Preparar();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Não foi possível preparar o banco de dados.\n\n" + ex.Message, "CineDiário", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
            return;
        }

        new LoginWindow().Show();
    }
}
