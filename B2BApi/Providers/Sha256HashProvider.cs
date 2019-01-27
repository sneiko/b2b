using System.Security.Cryptography;
using System.Text;
using B2BApi.Intrefaces;

namespace B2BApi.Providers
{
    public class Sha256HashProvider : IHashProvider
    {
        public string GetHash(string value)
        {
            using (var algorithm = SHA256.Create())
            {
                var hash = algorithm.ComputeHash(Encoding.Unicode.GetBytes(value));
                var hashString = Encoding.Unicode.GetString(hash);
                return hashString;
            }
        }
    }
}