using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Git_Analysis.Parsers
{
    public class TestFilesParser : Parser
    {
        const string pattern = @"\/(\/|\w|-)+[\w,-]*(spec|when|Facts)+\w*.(js|es6|cs)";
        Regex regex;

        public TestFilesParser()
        {
            regex = new Regex(pattern);
            TestFiles = new HashSet<string>();
        }

        public void parse(string str)
        {
            var matches = regex.Matches(str);
            foreach (var match in matches)
            {
                TestFiles.Add(match.ToString());
            }
        }
        public ISet<string> TestFiles { get; set; }
    }
}