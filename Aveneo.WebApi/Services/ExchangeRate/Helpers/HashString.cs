using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Services.ExchangeRate.Helpers
{
    public static class HashString
    {
        public static String GetHash(this String str)
        {
            String hashed = String.Empty;
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                byte[] stringBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(str));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in stringBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                hashed = sb.ToString();
            }
            return hashed;
        }
    }
}
