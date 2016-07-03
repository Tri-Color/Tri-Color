using System.Text.RegularExpressions;

namespace UTExport.MSpec
{
    public static class CsParseExtension
    {
        public static bool IsComment(this string line)
        {
            return line.Trim().StartsWith("//");
        }

        public static bool IsUsefulMSpecStatement(this string line)
        {
            return line.IsClass() || line.IsBecause() || line.IsIt();
        }

        public static bool IsClass(this string line)
        {
            return GetClassMatch(line).Success;
        }

        private static Match GetClassMatch(string currentLine)
        {
            return currentLine.GetMatch("(\\bclass\\x20)(\\b\\w*\\b)");
        }

        public static bool IsBecause(this string line)
        {
            return GetBecauseMatch(line).Success;
        }

        private static Match GetBecauseMatch(string currentLine)
        {
            return currentLine.GetMatch("(\\bBecause\\x20)(\\b\\w*\\b)");
        }

        public static bool IsIt(this string line)
        {
            return GetItMatch(line).Success;
        }

        private static Match GetItMatch(string currentLine)
        {
            return currentLine.GetMatch("(\\bIt\\x20)(\\b\\w*\\b)");
        }

        public static string ToClassName(this string line)
        {
            return GetClassMatch(line).Groups[2].Value;
        }

        public static string ToBecause(this string line)
        {
            return GetBecauseMatch(line).Groups[2].Value;
        }

        public static string ToIt(this string line)
        {
            return GetItMatch(line).Groups[2].Value;
        }
    }
}