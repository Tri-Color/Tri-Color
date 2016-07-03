using System.Diagnostics;
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
        
        public static bool IsComment(this string line)
        {
            return line.Trim().StartsWith("//");
        }

        public static string ToItDescription(this string currentLine)
        {
            return GetItMatch(currentLine).Groups[2].Value;
        }

        public static int GetLevel(this string currentLine)
        {
            Match match = GetMatch(currentLine, "([\t, \x20]*)(\\bdescribe\\b\\(|\\bit\\b\\()");
            Debug.Assert(match.Success);
            string whitespace = match.Groups[1].Value;
            string afterReplace4SpacesWithTab = whitespace.Replace("    ", "\t");
            string afterRemoveReduntantSpace = afterReplace4SpacesWithTab.Replace(" ", "");
            return afterRemoveReduntantSpace.Length;
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