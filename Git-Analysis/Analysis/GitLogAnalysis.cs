using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Git_Analysis.Parsers;

namespace Git_Analysis.Analysis
{
    public class GitLogAnalysis
    {
        DevInformationParser devInformationParser;
        StoryInformationParser storyInformationParser;
        CommentInformationParser commentInformationParser;

        string commit;
        public GitLogAnalysis(string commit)
        {
            devInformationParser = new DevInformationParser();
            storyInformationParser = new StoryInformationParser();
            commentInformationParser = new CommentInformationParser();
            this.commit = commit;
        }

        public string GetCommentFromCommit()
        {
            var infoArr = commit.Split('|');
            return infoArr.LastOrDefault().Contains("comment:") ? infoArr.LastOrDefault().Trim().Substring(8).Trim() :string.Empty;
        }

        public List<string> GetDevs()
        {
            devInformationParser.parse(GetCommentFromCommit());
            return devInformationParser.DevNames;
        }

        public string GetStoryNum()
        {
            storyInformationParser.parse(GetCommentFromCommit());
            return storyInformationParser.StoryNum;
        }

        public string GetComment()
        {
            commentInformationParser.parse(GetCommentFromCommit());
            return commentInformationParser.Comment;
        }
    }
}