using System.Text;

using System.Security.Cryptography;
using System.Text;

namespace Projekt_PasswortManager
{
    public static class HashHelper
    {
        public static string ComputeSha256Hash(string raw)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
                var sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}