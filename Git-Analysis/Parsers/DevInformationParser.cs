using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;

namespace Git_Analysis.Parsers
{
    public class DevInformationParser : Parser
    {
        const string pattern = @"^\[.*\]|^\w+/\w+[/\w]*|^\w+\s+&[\s+&\s+\w+]*|^\w+\s#";
        Regex regex;

        public DevInformationParser()
        {
            regex = new Regex(pattern);
            DevNames = new List<string>();
        }

        public List<string> DevNames { get; set; }

        public void parse(string str)
        {
            var matches = regex.Matches(str);
            foreach (var match in matches)
            {
                var nameStr = bracketsParse(new []{match.ToString()});
                nameStr = slashParse(nameStr);
                nameStr = poundParse(nameStr);
                DevNames.AddRange(getDevs(nameStr));
            }
        }

        public List<string> getDevs(string[] strArr)
        {
            List<string> devs = new List<string>();
            foreach (var str in strArr)
            {
                devs.AddRange(str.Split(ParserUtil.slashSpliter));
            }
            return devs;
        }

        public string[] poundParse(string[] strArr)
        {
            return ParserUtil.transArrayToString(strArr).Trim().Split('#').Where(name => name != "").Select(name => name.Trim()).ToArray();
        }

        public string[] bracketsParse(string[] strArr)
        {
            return ParserUtil.transArrayToString(strArr).Replace('[', ' ').Replace(']', ' ').Split('&').Select(name => name.Trim()).ToArray();
        }

        public string[] slashParse(string[] strArr)
        {
            return ParserUtil.transArrayToString(strArr).Split('/').Select(name => name.Trim()).ToArray();
        }
    }
}