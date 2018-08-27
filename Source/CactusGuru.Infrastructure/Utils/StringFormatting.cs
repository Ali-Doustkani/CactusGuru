using System;
using System.Text.RegularExpressions;

namespace CactusGuru.Infrastructure.Utils
{
    public static class StringFormatting
    {
        public static string Tokenize(string format, Func<string, string> specifier)
        {
            var reg = new Regex(@"{(\w+|\s*,\s*\w+)}");
            var matches = reg.Matches(format);
            var result = format;
            foreach (Match match in matches)
            {
                var token = Regex.Match(match.Value, @"\w+").Value;
                var value = specifier(token);
                if (string.IsNullOrEmpty(value))
                {
                    result = result.Replace(match.Value, string.Empty);
                }
                else
                {
                    var finalValue = RemoveBraces(match.Value.Replace(token, value));
                    result = result.Replace(match.Value, finalValue);
                }
            }
            return result;
        }

        private static string RemoveBraces(string token)
        {
            return token.Replace("{", string.Empty).Replace("}", string.Empty);
        }
    }
}
