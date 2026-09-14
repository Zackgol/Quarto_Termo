using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Mercado.Data;
using Mercado.Exceptions;
using Mercado.Models;
using Mercado.Repositories;
using Mercado.Services;

namespace Mercado
{
    public partial class MainWindow : Window
    {
        private readonly MercadoContext _context;
        private readonly ProdutoService _produtoService;

        private Produto? _selecionado;

        public MainWindow()
        {
            InitializeComponent();

            _context = new MercadoContext();

            try
            {
                _context.GarantirBancoCriadoESeed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Não foi possível preparar o banco de dados:\n" + ex.Message,
                    "Erro de inicialização", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _produtoService = new ProdutoService(new ProdutoRepository(_context));

            CarregarProdutos();
            CarregarCategoriasNaBusca();
        }

        // ---------------------------------------------------------------
        // LISTAGEM E BUSCA
        // ---------------------------------------------------------------

        private void CarregarProdutos()
        {
            try
            {
                GridProdutos.ItemsSource = _produtoService.ListarTodos();
            }
            catch (MercadoException ex)
            {
                MessageBox.Show(this, ex.Message, "Erro ao carregar produtos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CarregarCategoriasNaBusca()
        {
            var categorias = _produtoService.ListarTodos()
                .Select(p => p.Categoria)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            CmbBuscaCategoria.Items.Clear();
            CmbBuscaCategoria.Items.Add("Todas");
            foreach (var categoria in categorias)
                CmbBuscaCategoria.Items.Add(categoria);
            CmbBuscaCategoria.SelectedIndex = 0;
        }

        private void BtnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var categoria = CmbBuscaCategoria.SelectedItem as string;
                GridProdutos.ItemsSource = _produtoService.Buscar(TxtBuscaNome.Text, categoria);
            }
            catch (MercadoException ex)
            {
                MessageBox.Show(this, ex.Message, "Erro na pesquisa", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLimparBusca_Click(object sender, RoutedEventArgs e)
        {
            TxtBuscaNome.Clear();
            CmbBuscaCategoria.SelectedIndex = 0;
            CarregarProdutos();
        }

        // ---------------------------------------------------------------
        // FORMULÁRIO (ADD / EDIT)
        // ---------------------------------------------------------------

        private void GridProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridProdutos.SelectedItem is not Produto produto)
                return;

            _selecionado = produto;
            TxtNome.Text = produto.Nome;
            TxtCategoria.Text = produto.Categoria;
            TxtPreco.Text = produto.Preco.ToString(CultureInfo.InvariantCulture);
            TxtEstoque.Text = produto.Estoque.ToString();
            EsconderErro();
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var produto = LerFormulario(new Produto());
                _produtoService.Salvar(produto, novo: true);

                LimparFormulario();
                CarregarProdutos();
                CarregarCategoriasNaBusca();
            }
            catch (MercadoException ex)
            {
                MostrarErro(ex.Message);
            }
        }

        private void BtnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            if (_selecionado is null)
            {
                MostrarErro("Selecione um produto na lista para atualizar.");
                return;
            }

            try
            {
                var produto = LerFormulario(new Produto { Id = _selecionado.Id });
                _produtoService.Salvar(produto, novo: false);

                LimparFormulario();
                CarregarProdutos();
                CarregarCategoriasNaBusca();
            }
            catch (MercadoException ex)
            {
                MostrarErro(ex.Message);
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (_selecionado is null)
            {
                MostrarErro("Selecione um produto na lista para excluir.");
                return;
            }

            var confirmar = MessageBox.Show(this, $"Excluir o produto \"{_selecionado.Nome}\"?",
                "Confirmar exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmar != MessageBoxResult.Yes) return;

            try
            {
                _produtoService.Remover(_selecionado.Id);
                LimparFormulario();
                CarregarProdutos();
                CarregarCategoriasNaBusca();
            }
            catch (MercadoException ex)
            {
                MostrarErro(ex.Message);
            }
        }

        private void BtnLimparFormulario_Click(object sender, RoutedEventArgs e) => LimparFormulario();

        private Produto LerFormulario(Produto produto)
        {
            produto.Nome = TxtNome.Text.Trim();
            produto.Categoria = TxtCategoria.Text.Trim();

            if (!decimal.TryParse(TxtPreco.Text.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out var preco))
                throw new ValidacaoException("Preço inválido. Use um número, ex.: 12.50");
            produto.Preco = preco;

            if (!int.TryParse(TxtEstoque.Text, out var estoque))
                throw new ValidacaoException("Estoque inválido. Use um número inteiro.");
            produto.Estoque = estoque;

            return produto;
        }

        private void LimparFormulario()
        {
            _selecionado = null;
            GridProdutos.SelectedItem = null;
            TxtNome.Clear();
            TxtCategoria.Clear();
            TxtPreco.Clear();
            TxtEstoque.Clear();
            EsconderErro();
        }

        private void MostrarErro(string mensagem)
        {
            TxtErro.Text = mensagem;
            TxtErro.Visibility = Visibility.Visible;
        }

        private void EsconderErro() => TxtErro.Visibility = Visibility.Collapsed;

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _context.Dispose();
        }
    }
}
