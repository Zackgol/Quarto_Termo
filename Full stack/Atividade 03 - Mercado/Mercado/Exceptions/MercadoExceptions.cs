using System;

namespace Mercado.Exceptions
{
    public abstract class MercadoException : Exception
    {
        protected MercadoException(string message) : base(message) { }
        protected MercadoException(string message, Exception inner) : base(message, inner) { }
    }

    public class ValidacaoException : MercadoException
    {
        public ValidacaoException(string message) : base(message) { }
    }

    public class RegistroNaoEncontradoException : MercadoException
    {
        public RegistroNaoEncontradoException(int id) : base($"Produto com Id {id} não foi encontrado.") { }
    }

    public class OperacaoBancoException : MercadoException
    {
        public OperacaoBancoException(string operacao, Exception inner)
            : base($"Falha ao {operacao} no banco de dados: {inner.Message}", inner) { }
    }
}
