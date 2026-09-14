using System;
using System.Collections.Generic;
using System.Linq;
using Mercado.Data;
using Mercado.Exceptions;
using Mercado.Models;
using Microsoft.EntityFrameworkCore;

namespace Mercado.Repositories
{
    /// <summary>
    /// Camada de acesso a dados. Todo método captura falhas do EF/SQLite e as
    /// traduz para OperacaoBancoException, para a UI nunca ver exceção "crua".
    /// </summary>
    public class ProdutoRepository
    {
        private readonly MercadoContext _context;

        public ProdutoRepository(MercadoContext context)
        {
            _context = context;
        }

        public List<Produto> ListarTodos()
        {
            try
            {
                return _context.Produtos.OrderBy(p => p.Categoria).ThenBy(p => p.Nome).ToList();
            }
            catch (Exception ex)
            {
                throw new OperacaoBancoException("listar produtos", ex);
            }
        }

        /// <summary>
        /// Pesquisa parametrizada: nome (LIKE) e categoria são passados como
        /// parâmetros da expressão LINQ — o EF Core traduz para SQL parametrizado,
        /// protegido contra SQL Injection.
        /// </summary>
        public List<Produto> Buscar(string? termoNome, string? categoria)
        {
            try
            {
                var query = _context.Produtos.AsQueryable();

                if (!string.IsNullOrWhiteSpace(termoNome))
                {
                    var termo = termoNome.Trim();
                    query = query.Where(p => EF.Functions.Like(p.Nome, $"%{termo}%"));
                }

                if (!string.IsNullOrWhiteSpace(categoria) && categoria != "Todas")
                    query = query.Where(p => p.Categoria == categoria);

                return query.OrderBy(p => p.Nome).ToList();
            }
            catch (Exception ex)
            {
                throw new OperacaoBancoException("pesquisar produtos", ex);
            }
        }

        public Produto Adicionar(Produto produto)
        {
            try
            {
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return produto;
            }
            catch (Exception ex)
            {
                throw new OperacaoBancoException("adicionar produto", ex);
            }
        }

        public void Atualizar(Produto produto)
        {
            try
            {
                var existente = _context.Produtos.FirstOrDefault(p => p.Id == produto.Id)
                    ?? throw new RegistroNaoEncontradoException(produto.Id);

                existente.Nome = produto.Nome;
                existente.Categoria = produto.Categoria;
                existente.Preco = produto.Preco;
                existente.Estoque = produto.Estoque;

                _context.SaveChanges();
            }
            catch (MercadoException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new OperacaoBancoException("atualizar produto", ex);
            }
        }

        public void Remover(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.Id == id)
                    ?? throw new RegistroNaoEncontradoException(id);

                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }
            catch (MercadoException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new OperacaoBancoException("remover produto", ex);
            }
        }
    }
}
