using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AowEmailWrapper.Helpers
{
    public class StringHelper
    {
        public const string DOT = ".";
        public const string BACKSLASH = "\\";
        public const string CrLf = "\r\n";
        
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static string ExtractQuotes(string input)
        {
            Regex re = new Regex("\"([^\"]+)\"", RegexOptions.Multiline);

            Match match = re.Match(input);

            string returnVal = null;

            if (match != null)
            {
                returnVal = match.ToString().Replace("\"", string.Empty);
            }

            return returnVal;
        }

        public static string ExtractLetters(string input)
        {
            Regex re = new Regex("[A-Za-z]+$", RegexOptions.Multiline);

            Match match = re.Match(input);

            string returnVal = null;

            if (match != null)
            {
                returnVal = match.ToString();
            }

            return returnVal;
        }

        //Tidy this up
        public static string DecodeQuotedPrintable(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                input = input.Replace("=\r\n", string.Empty);
                Regex hexRegex = new Regex(@"(\=(?:\"")?([0-9A-F][0-9A-F])(?:\"")?)", RegexOptions.IgnoreCase);
                input = hexRegex.Replace(input, new MatchEvaluator(HexMatchEvaluator));
                input = input.Replace('_', ' ');
            }
            return input;
        }

        private static string HexMatchEvaluator(Match m)
        {
            int dec = Convert.ToInt32(m.Groups[2].Value, 16);
            char character = Convert.ToChar(dec);
            return character.ToString();
        }

        public static string ExtractLeftOf(string input, char theChar)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(input) && input.Contains(theChar))
            {
                int index = input.IndexOf(theChar) + 1;
                if (index > 0)
                {
                    returnVal = input.Substring(index, input.Length - index);
                }
            }
            
            return returnVal.Replace("\"", string.Empty);
        }

        public static string RemoveInvalidPathChars(string input)
        {
            string returnVal = null;

            if (!string.IsNullOrEmpty(input))
            {
                returnVal = RemoveChars(input, System.IO.Path.GetInvalidPathChars());
                returnVal = RemoveChars(returnVal, System.IO.Path.GetInvalidFileNameChars());
            }

            return returnVal.Trim();
        }

        private static string RemoveChars(string input, char[] toRemove)
        {
            string returnVal = input;

            foreach (char chr in toRemove)
            {
                returnVal = returnVal.Replace(chr.ToString(), string.Empty);
            }

            return returnVal;
        }
    }
}
