using System.Windows;
namespace OdontoTech
{
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
        }

        public DashboardWindow(string nomeUsuario) : this()
        {
            txtBoasVindas.Content = $"Bem-vindo, {nomeUsuario}";
        }
    }
}
