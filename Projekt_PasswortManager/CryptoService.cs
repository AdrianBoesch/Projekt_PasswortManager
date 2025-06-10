using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class CryptoService
{
    private static readonly string _key = "1234567890123456"; // 16 Zeichen = 128 Bit Schlüssel (nur Beispiel!)
    private static readonly string _iv = "6543210987654321";  // 16 Zeichen IV

    public static string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_key);
        aes.IV = Encoding.UTF8.GetBytes(_iv);

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string encryptedText)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_key);
        aes.IV = Encoding.UTF8.GetBytes(_iv);

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream(Convert.FromBase64String(encryptedText));
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}


    