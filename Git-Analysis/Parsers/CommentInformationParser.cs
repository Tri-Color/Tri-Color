using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;

namespace Git_Analysis.Parsers
{
    public class CommentInformationParser : Parser
    {
        const String pattern = @"#(\d|\/|\w)+( )+[a-zA-Z|( )|(.)|(\-)|0-9|(,)]+";
        Regex regex;

        public string Comment { get; set; }

        public CommentInformationParser()
        {
            regex = new Regex(pattern);
        }

        public void parse(string str)
        {
            var matches = regex.Matches(str);
            foreach (var match in matches)
            {
                Comment = commentParse(match.ToString());
            }
        }

        public string commentParse(string comment)
        {
           var words = comment.Split(ParserUtil.slashSpliter).Where(word => !word.Contains('#')).ToArray();
            return ParserUtil.transArrayToString(words).Trim();
        }
    }
}