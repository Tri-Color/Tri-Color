using System.Linq;

namespace Git_Analysis.Parsers
{
    public class ParserUtil
    {
        public const char slashSpliter = ' ';
        public static string transArrayToString(string[] strArr)
        {
            return strArr.Aggregate("", (current, str) => current + string.Format("{0}{1}", str, slashSpliter));
        }
    }
}