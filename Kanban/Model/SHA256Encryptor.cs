using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Model
{
    internal class SHA256Encryptor : IEncryptor
    {
        public string Encrypt(string text)
        {
            var sha256 = SHA256.Create();
            var textAsByteArray = Encoding.Default.GetBytes(text);
            var hashedByteArray = sha256.ComputeHash(textAsByteArray);
            var hashedText = Convert.ToBase64String(hashedByteArray);

            return hashedText;
        }
    }
}
