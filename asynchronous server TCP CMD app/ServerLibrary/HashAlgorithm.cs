using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ServerLibrary
{
    public class HashAlgorithm
    {
        public static string getHash( string source)
        {
            var sBuilder = new StringBuilder();
            using (SHA256 hashAlgorithm = SHA256.Create())
            {
                byte[] hashArray = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(source));

                for (int i = 0; i < hashArray.Length; i++)
                {
                    sBuilder.Append(hashArray[i].ToString("x2"));
                }
            }
            return sBuilder.ToString();
        }

    }
}
