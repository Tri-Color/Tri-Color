using System.Text;
using System.Text.RegularExpressions;

namespace UTExport
{
    public static class CsParseExtension
    {
        public static bool IsClass(this string line)
        {
            return GetClassMatch(line).Success;
        }

        private static Match GetClassMatch(string currentLine)
        {
            return currentLine.GetMatch("(\\bclass\\x20)(\\b\\w*\\b)");
        }

        public static string ToClassName(this string line)
        {
            return GetClassMatch(line).Groups[2].Value.SplitIntoWords();
        }

        public static string SplitIntoWords(this string line)
        {
            return line.Replace('_', ' ').SplitCamelToWords();
        }

        private static string SplitCamelToWords(this string line)
        {
            Match match = new Regex(@"
\d+                                 #number is a word
| 
([A-Z]+((?=[A-Z_][0-9a-z])|\b))     #abbreviation is a word
| 
(\b|[A-Z]|(?<=_)) [a-z]+ ((?=[A-Z0-9\s_])|\b)   #camel word", RegexOptions.IgnorePatternWhitespace).Match(line);

            if (!match.Success)
            {
                return line;
            }

            var stringBuilder = new StringBuilder();
            while (match.Success)
            {
                string matchValue = match.Value;
                stringBuilder.AppendFormat("{0} ",
                    matchValue.IsAbbreviation() ? matchValue : matchValue.ToLower());

                match = match.NextMatch();
            }

            return stringBuilder.ToString().Trim();
        }

        private static bool IsAbbreviation(this string captureValue)
        {
            return captureValue.Length > 1 && captureValue == captureValue.ToUpper();
        }
    }
}