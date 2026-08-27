using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
namespace SistemaFullStackApp
{
    public partial class PaginaProdutos : Page
    {
        public PaginaProdutos()
        {
            InitializeComponent();
            CarregarProdutosMySQL();
        }
        private void CarregarProdutosMySQL()
        {
            List<Produto> lista = new List<Produto>();
            using (MySqlConnection conn = ConexaoMySQL.ObterConexao())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Id, Nome, Preco, Estoque FROM Produtos ORDER BY Id DESC";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
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
                    dgProdutos.ItemsSource = lista;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro MySQL ao carregar dados: {ex.Message}", "Erro de Banco", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                !decimal.TryParse(txtPreco.Text, out decimal preco) ||
                !int.TryParse(txtEstoque.Text, out int estoque))
            {
                MessageBox.Show("Preencha todos os campos corretamente com valores válidos!",
                            "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            using (MySqlConnection conn = ConexaoMySQL.ObterConexao())
            {
                try
                {
                    conn.Open();
                    bool ehEdicao = !string.IsNullOrEmpty(txtId.Text);
                    string query;
                    if (ehEdicao)
                    {
                        query = "UPDATE Produtos SET Nome = @nome, Preco = @preco, Estoque = @estoque WHERE Id = @id";
                    }
                    else
                    {
                        query = "INSERT INTO Produtos (Nome, Preco, Estoque) VALUES (@nome, @preco,@estoque)";
                    }
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", txtNome.Text.Trim());
                        cmd.Parameters.AddWithValue("@preco", preco);
                        cmd.Parameters.AddWithValue("@estoque", estoque);
                        if (ehEdicao)
                        {
                            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                        }
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(ehEdicao ? "Produto atualizado com sucesso no MySQL!" : "Produto cadastrado com sucesso no MySQL!", 
                                        "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        LimparCampos();
                        CarregarProdutosMySQL();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar no MySQL: {ex.Message}", "Erro de Execução",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Selecione um registro na tabela para poder excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirmacao = MessageBox.Show($"Deseja realmente remover o produto '{txtNome.Text}' ? ",
                                              "Confirmar Exclusão",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);
            if (confirmacao == MessageBoxResult.Yes)
            {
                using (MySqlConnection conn = ConexaoMySQL.ObterConexao())
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Produtos WHERE Id = @id";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                            cmd.ExecuteNonQuery();
                           

                            MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                           

                            LimparCampos();
                            CarregarProdutosMySQL();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir no MySQL: {ex.Message}", "Erro",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void dgProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProdutos.SelectedItem is Produto prod)
            {
                txtId.Text = prod.Id.ToString();
                txtNome.Text = prod.Nome;
                txtPreco.Text = prod.Preco.ToString("F2");
                txtEstoque.Text = prod.Estoque.ToString();
            }
        }
        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            LimparCampos();
        }
        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtPreco.Clear();
            txtEstoque.Clear();
            dgProdutos.UnselectAll();
            txtNome.Focus();
        }
    }
}
