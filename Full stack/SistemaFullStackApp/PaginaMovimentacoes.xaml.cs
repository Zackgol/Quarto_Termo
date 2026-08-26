using MySql.Data.MySqlClient;
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
    public partial class PaginaMovimentacoes : Page
    {
        public PaginaMovimentacoes()
        {
            InitializeComponent();
            ListarMovimentacoes();
        }

        private void ListarMovimentacoes()
        {
            MySqlConnection conn = ConexaoMySQL.ObterConexao();
            conn.Open();

            string sql = @"SELECT m.id, p.nome AS produto, m.tipo, m.quantidade, m.data_movimentacao 
                           FROM movimentacoes m
                           JOIN produtos p ON m.id_produto = p.id
                           ORDER BY m.data_movimentacao DESC";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Movimentacao> lista = new List<Movimentacao>();

            while (reader.Read())
            {
                Movimentacao mov = new Movimentacao();
                mov.Id = Convert.ToInt32(reader["id"]);
                mov.Produto = reader["produto"].ToString();
                mov.Tipo = reader["tipo"].ToString();
                mov.Quantidade = Convert.ToInt32(reader["quantidade"]);
                mov.Data = Convert.ToDateTime(reader["data_movimentacao"]);

                lista.Add(mov);
            }

            conn.Close();

            dgMovimentacoes.ItemsSource = lista;
        }
    }
}
