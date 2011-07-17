using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AowEmailWrapper.Helpers
{
    public class CryptographyHelper
    {
        public static string Encrypt(string input)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(input))
            {
                byte[] textBytes = Encoding.UTF8.GetBytes(input);
                returnVal = StringHelper.ReverseString(Convert.ToBase64String(textBytes));
            }
            return returnVal;
        }

        public static string Decrypt(string input)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(input))
            {
                UTF8Encoding encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode = Convert.FromBase64String(StringHelper.ReverseString(input));
                int charCount = utf8Decode.GetCharCount(todecode, 0, todecode.Length);
                char[] decodedc = new char[charCount];
                utf8Decode.GetChars(todecode, 0, todecode.Length, decodedc, 0);
                returnVal = new String(decodedc);
            }

            return returnVal;
        }

        public static byte[] DecodeBase64String(string input)
        {
            return Convert.FromBase64String(input.Replace("\r\n", string.Empty).Trim());
        }
    }
}
