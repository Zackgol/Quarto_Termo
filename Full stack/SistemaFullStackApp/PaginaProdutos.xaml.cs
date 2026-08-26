using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient; // Namespace do MySQL
namespace SistemaFullStackApp
{
    public partial class PaginaProdutos : Page
    {
        public PaginaProdutos()
        {
            InitializeComponent();
            CarregarProdutosDoMySQL();
        }
        private void CarregarProdutosDoMySQL()
        {
            List<Produto> lista = new List<Produto>();

            using (MySqlConnection conn = ConexaoMySQL.ObterConexao())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Id, Nome, Preco, Estoque FROM Produtos order by nome Desc";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Produto
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"].ToString(),
                                    Preco = Convert.ToDecimal(reader["Preco"]),
                                    Estoque = Convert.ToInt32(reader["Estoque"])
                                });
                            }
                        }
                    }
                    dgProdutos.ItemsSource = lista;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro de Conexão MySQL: {ex.Message}");
                }
            }
        }

        // 2. MÉTODOS DE ESCRITA (INSERT com Parâmetros)
        private void SalvarProdutoMySQL(string nome, decimal preco, int estoque)
        {
            string query = "INSERT INTO Produtos (Nome, Preco, Estoque) VALUES (@nome, @preco, @estoque)";

            using (MySqlConnection conn = ConexaoMySQL.ObterConexao())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Parâmetros para evitar SQL Injection
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@preco", preco);
                        cmd.Parameters.AddWithValue("@estoque", estoque);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Produto salvo com sucesso no MySQL!");
                        CarregarProdutosDoMySQL();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar no MySQL: {ex.Message}");
                }
            }
        }
    }
}