using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;

namespace Git_Analysis.Parsers
{
    public class StoryInformationParser : Parser
    {
        const String pattern = @"#(\d|\/|\w)*";
        Regex regex;

        public string StoryNum { get; set; }

        public StoryInformationParser()
        {
            regex = new Regex(pattern);
        }

        public void parse(string str)
        {
            var matches = regex.Matches(str);
            StoryNum = poundParse(new[] {matches[0].ToString()})[0];
        }

        public string[] poundParse(string[] strArr)
        {
            return ParserUtil.transArrayToString(strArr).Split('#').Where(name => name != "").Select(name => name.Trim()).ToArray();
        }
    }
}