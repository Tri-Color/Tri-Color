using System.Diagnostics;
using System.Text.RegularExpressions;

namespace UTExport
{
    public static class Utils
    {
        public static int GetLevel(this string currentLine)
        {
            Match match = GetMatch(currentLine, "^([\t, \x20]*)");
            Debug.Assert(match.Success);
            string whitespace = match.Groups[1].Value;
            string afterReplace4SpacesWithTab = whitespace.Replace("    ", "\t");
            string afterRemoveReduntantSpace = afterReplace4SpacesWithTab.Replace(" ", "");
            return afterRemoveReduntantSpace.Length;
        }

        public static Match GetMatch(this string currentLine, string pattern)
        {
            return new Regex(pattern).Match(currentLine);
        }
    }
}