using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace AuthServiceIN6BM.Application.Services;


public static class UuidGenerator
{
    private static readonly string Alphabet = "123456789ABCDEFGHJKMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz";

    public static string GenerateShorUUID()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[12];
        rng.GetBytes(bytes);

        /*cadena*/
        var result = new StringBuilder(12);

        for (int i = 0; i < 12; i++)
        {
            /*devuelve una letra aleatoria del abecedario*/
            result.Append(Alphabet[bytes[i] % Alphabet.Length]);
        }

        return result.ToString();
    }

    /*Agregamos nuesto identificador*/
    public static string GenerateUserId()
    {
        return $"usr_{GenerateShorUUID()}";
    }

    /*agregamosnuestro rol aleatorio*/
    public static string GenerateRoleId()
    {
        return $"rol_{GenerateShorUUID()}";

    }

    public static bool IsValidUserId(string? id)
    {
        if (string.IsNullOrEmpty(id))
            return false;

        if (id.Length != 16 || !id.StartsWith("usr_"))
            return false;
        /*obtiene la partre despues del usr_*/
        var idPart = id[4..];
        return idPart.All(c => Alphabet.Contains(c));
    }



}