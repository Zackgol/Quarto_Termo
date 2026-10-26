using System.Security.Cryptography;

namespace CineDiario.Services;

public static class Senha
{
    private const int Iteracoes = 100_000;

    public static (string Hash, string Salt) Gerar(string senha)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(senha, salt, Iteracoes, HashAlgorithmName.SHA256, 32);
        return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }

    public static bool Conferir(string senha, string hashSalvo, string saltSalvo)
    {
        var salt = Convert.FromBase64String(saltSalvo);
        var hash = Rfc2898DeriveBytes.Pbkdf2(senha, salt, Iteracoes, HashAlgorithmName.SHA256, 32);
        return CryptographicOperations.FixedTimeEquals(hash, Convert.FromBase64String(hashSalvo));
    }
}
