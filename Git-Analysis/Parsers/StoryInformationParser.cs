using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Git_Analysis.Parsers
{
    public class StoryInformationParser : Parser
    {
        const String pattern = @"#(\d|\/|\w)*";
        readonly Regex regex;

        public StoryInformationParser()
        {
            regex = new Regex(pattern);
        }

        public object parse(string str)
        {
            string storyNumber = null;
            var matches = regex.Matches(str);
            foreach (var match in matches)
            {
                storyNumber = poundParse(new[] { match.ToString() })[0];
            }
            return storyNumber;
        }

        public string[] poundParse(string[] strArr)
        {
            return ParserUtil.transArrayToString(strArr).Split('#').Where(name => name != "").Select(name => name.Trim()).ToArray();
        }
    }
}