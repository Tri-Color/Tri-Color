﻿using System;
using System.IO;
using Git_Analysis.Utils;
using Xunit;

namespace Git_Analysis_Test.Utils
{
    public class FileReaderFact
    {
        string file_path = Path.GetFullPath(Environment.CurrentDirectory+ @"..\..\..\Utils\testFiles\git.log");
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
                Assert.Equal(true, commit.StartsWith("hash:5016e80|addTime:2016-07-08|commitTime:2016-07-08| " +
                    "comment:[wangjian & shengqi & jijie] #1919 Change comments length from" +
                    " 255 to 500 characters for Upload teq feature, and fix some ie issue for text-area."));
                Assert.Equal(true, commit.EndsWith(" 7 files changed, 35 insertions(+), 4 deletions(-)"));
            }
        }

    }
}