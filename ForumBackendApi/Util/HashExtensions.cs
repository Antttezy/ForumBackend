using System.Security.Cryptography;
using System.Text;

namespace ForumBackendApi.Util;

public static class HashExtensions
{
    public static string GetHash(this HashAlgorithm hashAlgorithm, string input)
    {
        var data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder builder = new();

        foreach (var b in data)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }

    public static bool VerifyHash(this HashAlgorithm hashAlgorithm, string input, string hash)
    {
        var expectedHash = hashAlgorithm.GetHash(input);
        return expectedHash == hash;
    }
}