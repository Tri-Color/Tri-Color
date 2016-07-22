using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Git_Analysis.Parsers
{
    public class TestFilesParser : Parser
    {
        const string pattern = @"\/(\/|\w|-)+[\w,-]*(spec|when|Facts)+\w*.(js|es6|cs)";
        readonly Regex regex;

        public TestFilesParser()
        {
            regex = new Regex(pattern);
        }

        public object parse(string str)
        {
            var matches = regex.Matches(str);
            HashSet<string> TestFiles = new HashSet<string>();
            foreach (var match in matches)
            {
                TestFiles.Add(match.ToString());
            }
            return TestFiles;
        }
    }
}