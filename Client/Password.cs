using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Exceptions;

namespace Client
{
    public class Password
    {
        string _password;

        public Password(string password)
        {
            _password = SHA256Hash(password);
        }

        public string _Password { get => _password; set => _password = value; }

        public static bool isValid(string password)
        {
            if (password.Length < 7)
                return false;
            if (!password.Any(char.IsUpper))
                return false;
            if (!password.Any(char.IsLower))
                return false;
            if (!password.Any(char.IsDigit))
                return false;
            if (!(password.IndexOfAny(new char[] { '*', '&', '#', '!', '@', '%' }) != -1))
                return false;
            if (password.Any(char.IsWhiteSpace))
                return false;
            
            return true;
        }

        private string SHA256Hash(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            SHA256Managed sha256hash = new SHA256Managed();
            byte[] hash = sha256hash.ComputeHash(bytes);

            string s = ByteArrayToHexString(hash);
            return ByteArrayToHexString(hash);
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
