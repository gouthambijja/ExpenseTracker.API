using System.Security.Cryptography;
using System.Text;

namespace ExpenseTracker.WEBAPI
{
    public class Utility
    {
        public static string Encrypt(string password)
        {
            var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
