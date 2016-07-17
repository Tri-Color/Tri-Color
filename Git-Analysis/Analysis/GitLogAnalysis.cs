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
        DevInformationParser devInformationParser;
        StoryInformationParser storyInformationParser;
        CommentInformationParser commentInformationParser;
        TestFilesParser testFilesParser;

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
            return commentInformationParser.Comment ?? string.Empty;
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
            testFilesParser.parse(parse);
            return testFilesParser.TestFiles;
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