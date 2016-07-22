using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Git_Analysis.Domain;
using Git_Analysis.Parsers;
using Git_Analysis.Utils;

namespace Git_Analysis.Analysis
{
    public class GitLogAnalysis
    {
        readonly DevInformationParser devInformationParser;
        readonly StoryInformationParser storyInformationParser;
        readonly CommentInformationParser commentInformationParser;
        readonly TestFilesParser testFilesParser;

        string commit;
        string parse;
        public GitLogAnalysis()
        {
            devInformationParser = new DevInformationParser();
            storyInformationParser = new StoryInformationParser();
            commentInformationParser = new CommentInformationParser();
            testFilesParser = new TestFilesParser();
        }

        public string GetCommentFromCommit()
        {
            var infoArr = commit.Split('|');
            if (infoArr.Count() < 4) return string.Empty;
            return infoArr.LastOrDefault().Contains("comment:") ? infoArr.LastOrDefault().Trim().Substring(8).Trim() :string.Empty;
        }

        public List<string> GetDevs()
        {
            return devInformationParser.parse(GetCommentFromCommit()) as List<string>;
        }

        public string GetStoryNum()
        {
            return storyInformationParser.parse(GetCommentFromCommit()) as string;
        }

        public string GetComment()
        {
            return commentInformationParser.parse(GetCommentFromCommit()) as string ?? string.Empty;
        }

        public string GetHash()
        {
            var infoArr = commit.Split('|');
            if (infoArr.Count() < 4) return string.Empty;
            return infoArr[0].Split(':')[1];
        }

        public DateTime GetAddTime()
        {
            var infoArr = commit.Split('|');
            if (infoArr.Count() < 4) return DateTime.UtcNow;
            return DateTime.Parse(infoArr[1].Split(':')[1]);
        }

        public DateTime GetCommitTime()
        {
            var infoArr = commit.Split('|');
            if (infoArr.Count() < 4) return DateTime.UtcNow;
            return DateTime.Parse(infoArr[2].Split(':')[1]);
        }

        public ISet<string> GetTestFilesList()
        {
            return testFilesParser.parse(parse) as ISet<string>;
        }

        public CommitInformation GetParseCommitInformation(CommitBlockInfo commitBlockInfo)
        {
            commit = commitBlockInfo.CommitInfo;
            parse = commitBlockInfo.ParseInfo;
            return new CommitInformation
            {
                Hash = GetHash(),
                AddTime = GetAddTime(),
                CommitTime = GetCommitTime(),
                Devs = GetDevs(),
                Comment = GetComment(),
                StoryNumber = GetStoryNum(),
                TestFileList = GetTestFilesList()
            };
        }
    }
}