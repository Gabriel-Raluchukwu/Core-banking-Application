using System;
using System.Text;

namespace BankTwo.Application.Logic.Utilities
{
    public class Encrypt
    {
        public static string Encode(string encode)
        {
            byte[] encoded = Encoding.UTF8.GetBytes(encode);
            return Convert.ToBase64String(encoded);
        }
        public static string Encode(int encode)
        {
            byte[] encoded = Encoding.UTF8.GetBytes(encode.ToString());
            return Convert.ToBase64String(encoded);
        }
        public static string Decode(string decodeString)
        {
            byte[] decoded = Convert.FromBase64String(decodeString);
            return Encoding.UTF8.GetString(decoded);
        }
    }
}
