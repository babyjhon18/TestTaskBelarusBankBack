using System.Security.Cryptography;
using System.Text;

namespace StoreApplication.Utils
{
    public static class SysUtils
    {
        public static String GetHash(String Name, String Password)
        {
            String result = "";

            SHA1 algorithm = SHA1.Create();

            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(String.Concat(Password, Name)));

            for (int i = 0; i < hash.Length; i++)
                result += hash[i].ToString("x2").ToUpperInvariant();

            return result;
        }
    }
}
