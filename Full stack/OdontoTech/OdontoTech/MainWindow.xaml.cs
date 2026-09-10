using OdontoTech.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OdontoTech
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var db = new AppDbContext())
            {
                db.CriarBancoESeed();
            }
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            string email = txtemail.Text.Trim();
            string senha = txtsenha.Password.Trim();

            try
            {
                using (var db = new AppDbContext())
                {
                    var usuario = db.Usuarios
                    .FirstOrDefault(u => u.Email == email && u.Senha == senha);
                    if (usuario != null)
                    {
                        DashboardWindow dashboard = new DashboardWindow(usuario.Nome);
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {

                        MessageBox.Show("E-mail ou senha incorretos. Tente novamente!",
                            "Acesso Negado",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);

                        txtsenha.Clear();
                        txtsenha.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar no SQL Server LocalDB:\n{ex.Message}",
                    "Falha no Banco",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

            }
        }
    }
}
