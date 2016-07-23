using System;
using System.Collections.Generic;
using System.IO;
using Git_Analysis.Domain;
using Git_Analysis.Utils;
using Xunit;

namespace Git_Analysis_Test.Utils
{
    public class FileReaderFact
    {
        readonly string file_path = Path.GetFullPath(Environment.CurrentDirectory+ @"..\..\..\Utils\testFiles\git.log");
        [Fact]
        public void should_recognize_commit_information()
        {
            string oneline;
            using (FileReader reader = new FileReader(file_path))
            {
                reader.Open();
                oneline = reader.ReadLine();
                reader.Close();
            }
            Assert.StartsWith("hash:", oneline);
        }

        [Fact]
        public void should_recognize_comment_information()
        {
            using (FileReader reader = new FileReader(file_path))
            {
                reader.Open();
                var comment = reader.ReadLine();
                reader.Close();
                Assert.Equal(comment, "hash:5016e80|addTime:2016-07-08|commitTime:2016-07-08| comment:[wangjian & shengqi & jijie] #1919 Change comments length from 255 to 500 characters for Upload teq feature, and fix some ie issue for text-area.");
            }
        }

        [Fact]
        public void should_return_commit_count()
        {
            using (FileReader reader = new FileReader(file_path))
            {
                reader.Open();
                var count = reader.GetCommitCount();
                reader.Close();
                Assert.Equal(17,count);
            }
        }

        [Fact]
        public void should_get_commit_by_block()
        {
            using (FileReader reader = new FileReader(file_path))
            {
                reader.Open();
                var commit = reader.GetOneCommit();
                reader.Close();
                Assert.Equal(true, commit.CommitInfo.StartsWith("hash:5016e80|addTime:2016-07-08|commitTime:2016-07-08| " +
                    "comment:[wangjian & shengqi & jijie] #1919 Change comments length from" +
                    " 255 to 500 characters for Upload teq feature, and fix some ie issue for text-area."));
                Assert.Equal(true, commit.ParseInfo.EndsWith(" 7 files changed, 35 insertions(+), 4 deletions(-)\n"));
            }
        }

        [Fact]
        public void should_return_true_when_there_are_more_commit_block()
        {
            using (FileReader reader = new FileReader(file_path))
            {
                reader.Open();
                var hasMore = reader.HasMoreCommitInfo();
                reader.Close();
                Assert.Equal(true,hasMore);
            }

        }

        [Fact]
        public void should_get_correct_commit_block()
        {
            List<CommitBlockInfo> commitInfos = new List<CommitBlockInfo>();
            using (FileReader reader = new FileReader(file_path))
            {
                reader.Open();
                commitInfos = reader.GetAllCommits();
                reader.Close();
            }
            long count = 0;
            using (FileReader reader = new FileReader(file_path))
            {
                reader.Open();
                count = reader.GetCommitCount();
                reader.Close();
            }
            Assert.Equal(commitInfos.Count,count);
        }

    }
}