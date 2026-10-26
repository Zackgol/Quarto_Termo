using CineDiario.Models;
using CineDiario.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CineDiario.Services;

public class AuthService
{
    private readonly UsuarioRepository repositorio = new();

    public Usuario Entrar(string login, string senha)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrEmpty(senha))
            throw new RegraException("Preencha usuário e senha.");

        var usuario = repositorio.BuscarPorLogin(login.Trim().ToLower());

        if (usuario == null || !Senha.Conferir(senha, usuario.SenhaHash, usuario.Salt))
            throw new RegraException("Usuário ou senha inválidos.");

        return usuario;
    }

    public void CriarConta(string login, string senha)
    {
        login = (login ?? "").Trim().ToLower();

        if (login.Length < 3)
            throw new RegraException("O usuário precisa ter pelo menos 3 letras.");

        if (login.Contains(' '))
            throw new RegraException("O usuário não pode ter espaço.");

        if (senha.Length < 6 || !senha.Any(char.IsDigit))
            throw new RegraException("A senha precisa ter 6 caracteres ou mais e pelo menos um número.");

        if (repositorio.Existe(login))
            throw new RegraException("Esse usuário já existe.");

        var (hash, salt) = Senha.Gerar(senha);

        try
        {
            repositorio.Adicionar(new Usuario { Login = login, SenhaHash = hash, Salt = salt });
        }
        catch (DbUpdateException ex)
        {
            throw new RegraException("Não foi possível criar a conta agora.", ex);
        }
    }
}
