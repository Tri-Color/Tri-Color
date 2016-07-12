using System.Text.RegularExpressions;

namespace UTExport.MSpec
{
    public static class MSpecParseExtension
    {
        public static bool IsUsefulMSpecStatement(this string line)
        {
            return line.IsClass() || line.IsBecause() || line.IsIt();
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

        public static string ToBecause(this string line)
        {
            return GetBecauseMatch(line).Groups[2].Value.SplitIntoWords();
        }

        public static string ToIt(this string line)
        {
            return GetItMatch(line).Groups[2].Value.SplitIntoWords();
        }
    }
}