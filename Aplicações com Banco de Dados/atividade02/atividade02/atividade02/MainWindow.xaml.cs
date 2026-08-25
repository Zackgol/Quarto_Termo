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
using MySql.Data.MySqlClient;

namespace Atividade02
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ListarProdutos();
        }

        // MÉTODO SELECT
        private void ListarProdutos()
        {
            MySqlConnection conn = Conexao.GetConnection();
            conn.Open();

            string sql = "SELECT * FROM produtos";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Produto> listaProdutos = new List<Produto>();

            while (reader.Read())
            {
                Produto p = new Produto();
                p.Id = Convert.ToInt32(reader["id"]);
                p.Nome = reader["nome"].ToString();
                p.Categoria = reader["categoria"].ToString();
                p.Preco = Convert.ToDecimal(reader["preco"]);
                p.Estoque = Convert.ToInt32(reader["estoque"]);

                listaProdutos.Add(p);
            }

            conn.Close();

            dgProdutos.ItemsSource = listaProdutos;
        }

        // MÉTODO INSERT (protegido com Parameters)
        private void InserirProduto(string nome, string categoria, decimal preco, int estoque)
        {
            MySqlConnection conn = Conexao.GetConnection();
            conn.Open();

            string sql = "INSERT INTO produtos (nome, categoria, preco, estoque) VALUES (@nome, @categoria, @preco, @estoque)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@categoria", categoria);
            cmd.Parameters.AddWithValue("@preco", preco);
            cmd.Parameters.AddWithValue("@estoque", estoque);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void BtnInserir_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNome.Text;
            string categoria = txtCategoria.Text;
            decimal preco = Convert.ToDecimal(txtPreco.Text);
            int estoque = Convert.ToInt32(txtEstoque.Text);

            InserirProduto(nome, categoria, preco, estoque);
            ListarProdutos();

            txtNome.Text = "";
            txtCategoria.Text = "";
            txtPreco.Text = "";
            txtEstoque.Text = "";
        }

        private void BtnListar_Click(object sender, RoutedEventArgs e)
        {
            ListarProdutos();
        }
    }
}