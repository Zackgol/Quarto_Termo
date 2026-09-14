using System.Collections.Generic;
using Mercado.Exceptions;
using Mercado.Models;
using Mercado.Repositories;

namespace Mercado.Services
{

    public class ProdutoService
    {
        private readonly ProdutoRepository _repository;

        public ProdutoService(ProdutoRepository repository)
        {
            _repository = repository;
        }

        public List<Produto> ListarTodos() => _repository.ListarTodos();

        public List<Produto> Buscar(string? termoNome, string? categoria) => _repository.Buscar(termoNome, categoria);

        public Produto Salvar(Produto produto, bool novo)
        {
            Validar(produto);
            if (novo)
            {
                return _repository.Adicionar(produto);
            }

            _repository.Atualizar(produto);
            return produto;
        }

        public void Remover(int id) => _repository.Remover(id);

        private static void Validar(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new ValidacaoException("O nome do produto é obrigatório.");

            if (produto.Nome.Trim().Length < 2)
                throw new ValidacaoException("O nome do produto deve ter ao menos 2 caracteres.");

            if (string.IsNullOrWhiteSpace(produto.Categoria))
                throw new ValidacaoException("A categoria é obrigatória.");

            if (produto.Preco < 0)
                throw new ValidacaoException("O preço não pode ser negativo.");

            if (produto.Estoque < 0)
                throw new ValidacaoException("O estoque não pode ser negativo.");
        }
    }
}
