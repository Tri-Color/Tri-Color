using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;

namespace Git_Analysis.Parsers
{
    public class DevInformationParser : Parser
    {
        const String pattern = @"^\[.*\]|^\w+/\w+[/\w]*|^\w+\s+&[\s+&\s+\w+]*";
        List<String> devNames;
        Regex regex;
        const char USERSpliter = ' ';

        public DevInformationParser()
        {
            regex = new Regex(pattern);
            devNames = new List<string>();
        }

        public List<string> DevNames
        {
            get { return devNames; }
        }

        public void parse(string str)
        {
            var matches = regex.Matches(str);
            foreach (var match in matches)
            {
                var nameStr = bracketsParse(new []{match.ToString()});
                nameStr = slashParse(nameStr);
//                nameStr = ampersandParse(nameStr);
                devNames.AddRange(getDevs(nameStr));
            }
        }

        public List<string> getDevs(string[] strArr)
        {
            List<string> devs = new List<string>();
            foreach (var str in strArr)
            {
                devs.AddRange(str.Split(USERSpliter));
            }
            return devs;
        }

        public string[] ampersandParse(string[] strArr)
        {
            return transArrayToString(strArr).Split('&').Select(name => name.Trim()).ToArray();
        }

        public string[] bracketsParse(string[] strArr)
        {
            return transArrayToString(strArr).Replace('[', ' ').Replace(']', ' ').Split('&').Select(name => name.Trim()).ToArray();
        }

        public string[] slashParse(string[] strArr)
        {
            return transArrayToString(strArr).Split('/').Select(name => name.Trim()).ToArray();
        }

        public string transArrayToString(string[] strArr)
        {
            var result = "";
            foreach (var str in strArr)
            {
                result += string.Format("{0}{1}",str,USERSpliter);
            }
            return result;
        }
    }
}