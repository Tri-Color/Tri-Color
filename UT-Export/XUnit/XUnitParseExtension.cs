using System.Text.RegularExpressions;

namespace UTExport.XUnit
{
    public static class XUnitParseExtension
    {
        public static bool IsUsefulXUnitStatement(this string line)
        {
            return line.IsClass() || line.IsFact() || line.IsTheory() || line.IsMethod();
        }

        public static bool IsFact(this string line)
        {
            return line.GetMatch("\\[Fact\\]").Success;
        }

        public static bool IsTheory(this string line)
        {
            return line.GetMatch("\\[Theory\\]").Success;
        }

        public static bool IsInlineData(this string line)
        {
            return line.GetMatch("\\[InlineData").Success;
        }

        public static bool IsMethod(this string line)
        {
            return GetMethodMatch(line).Success;
        }

        public static string ToMethodName(this string line)
        {
            return GetMethodMatch(line).Groups[2].Value;
        }

        private static Match GetMethodMatch(string line)
        {
            return line.GetMatch("(\\bvoid\\b)\\s+(\\w+)\\s*\\(");
        }
    }
}