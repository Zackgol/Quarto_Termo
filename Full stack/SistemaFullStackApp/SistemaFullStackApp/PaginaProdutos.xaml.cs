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


namespace SistemaFullStackApp
{
    public partial class PaginaProdutos : Page
    {
        public PaginaProdutos()
        {
            InitializeComponent();
            CarregarProdutos();
        }
        private void CarregarProdutos()
        {
          
            List<Produto> listaProdutos = new List<Produto>
 {
 new Produto { Id = 1, Nome = "Teclado Mecânico", Preco = 250.00m,
Estoque = 15 },
 new Produto { Id = 2, Nome = "Mouse Gamer", Preco = 120.50m, Estoque
= 30 },
 new Produto { Id = 3, Nome = "Monitor 24' IPS", Preco = 899.90m,
Estoque = 8 }
 };
        
            dgProdutos.ItemsSource = listaProdutos;
        }

        private void btnAbrirNovaJanela_Click(object sender, RoutedEventArgs e)
        {
            JanelaNova telaModal = new JanelaNova();
       
            telaModal.ShowDialog();
           
        }
    }
}
