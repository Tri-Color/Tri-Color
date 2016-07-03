using System.Text.RegularExpressions;

namespace UTExport.JT
{
    public static class JtParseExtension
    {
        public static bool IsDescribe(this string currentLine)
        {
            return !currentLine.IsComment() && GetDescribeMatch(currentLine).Success;
        }

        public static string ToDescribeDescription(this string currentLine)
        {
            return GetDescribeMatch(currentLine).Groups[2].Value;
        }
        
        public static bool IsIt(this string currentLine)
        {
            return !currentLine.IsComment() && GetItMatch(currentLine).Success;
        }

        public static bool IsDescribeOrIt(this string line)
        {
            return line.IsDescribe() || line.IsIt();
        }

        public static string ToItDescription(this string currentLine)
        {
            return GetItMatch(currentLine).Groups[2].Value;
        }

        private static Match GetDescribeMatch(string currentLine)
        {
            return new Regex("(\\bdescribe\\([', \"])(.*)([', \"],)").Match(currentLine);
        }

        private static Match GetItMatch(string currentLine)
        {
            return new Regex("(\\bit\\([', \"])(.*)([', \"],)").Match(currentLine);
        }
    }
}