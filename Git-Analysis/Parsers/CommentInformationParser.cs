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
        readonly Regex regex;

        public CommentInformationParser()
        {
            regex = new Regex(pattern);
        }

        public object parse(string str)
        {
            var matches = regex.Matches(str);
            string comment = null;
            foreach (var match in matches)
            {
                comment = commentParse(match.ToString());
            }
            return comment;
        }

        public string commentParse(string comment)
        {
           var words = comment.Split(ParserUtil.slashSpliter).Where(word => !word.Contains('#')).ToArray();
            return ParserUtil.transArrayToString(words).Trim();
        }
    }
}