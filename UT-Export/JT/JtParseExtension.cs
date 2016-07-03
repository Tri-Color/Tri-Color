using System.Text.RegularExpressions;

namespace UTExport.JT
{
    public static class JtParseExtension
    {
        public static bool IsDescribe(this string currentLine)
        {
            return GetDescribeMatch(currentLine).Success;
        }

        public static string ToDescribeDescription(this string currentLine)
        {
            return GetDescribeMatch(currentLine).Groups[2].Value;
        }
        
        public static bool IsIt(this string currentLine)
        {
            return GetItMatch(currentLine).Success;
        }

        public static string ToItDescription(this string currentLine)
        {
            return GetItMatch(currentLine).Groups[2].Value;
        }

        private static Match GetDescribeMatch(string currentLine)
        {
            return GetMatch(currentLine, "(\\bdescribe\\([', \"])(.*)([', \"],)");
        }

        private static Match GetMatch(string currentLine, string pattern)
        {
            return new Regex(pattern).Match(currentLine);
        }

        private static Match GetItMatch(string currentLine)
        {
            return GetMatch(currentLine, "(\\bit\\([', \"])(.*)([', \"],)");
        }
    }
}