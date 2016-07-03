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
            return GetClassMatch(line).Groups[2].Value;
        }
    }
}