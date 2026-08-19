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
using System.Windows.Shapes;

namespace SistemaFullStackApp
{
 
    public partial class JanelaNova : Window
    {
        public JanelaNova()
        {
            InitializeComponent();
        }

        private void btnAbrirNovaJanela_Click(object sender, RoutedEventArgs e)
        {
            JanelaNova telaModal = new JanelaNova();

            // .ShowDialog() abre como modal (bloqueia a janela de trás até fechar)
            telaModal.ShowDialog();
            // Se preferir abrir sem bloquear a janela de trás, use:
            // telaModal.Show();
        }
    }
}
