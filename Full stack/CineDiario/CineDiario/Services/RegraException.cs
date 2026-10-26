namespace CineDiario.Services;

public class RegraException : Exception
{
    public RegraException(string mensagem) : base(mensagem) { }

    public RegraException(string mensagem, Exception interna) : base(mensagem, interna) { }
}
