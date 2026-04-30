using System.Security.Cryptography;
using System.Text;

namespace SalaoDeBelezaLeila.Application.Helpers;

public static class PasswordHelper
{
    public static string Hash(string senha)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(senha);
        var hash = sha.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }

    public static bool Verify(string senha, string hash)
    {
        return Hash(senha) == hash;
    }
}