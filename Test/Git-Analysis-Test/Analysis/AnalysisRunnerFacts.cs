using System;
using System.Collections.Generic;
using System.IO;
using Git_Analysis.Analysis;
using Xunit;

namespace Git_Analysis_Test.Analysis
{
    public class AnalysisRunnerFacts
    {
        string input_path = Path.GetFullPath(Environment.CurrentDirectory + @"..\..\..\Utils\testFiles\git.log");
        [Fact]
        public void should_parse_commit_information_from_file_correctly()
        {
            AnalysisRunner runner = new AnalysisRunner(input_path);

            runner.run();
            Assert.Equal(runner.CommitInfos.Count,2);
            Assert.Equal(runner.CommitInfos[0].Hash, "5016e80");
            Assert.Equal(runner.CommitInfos[0].AddTime, DateTime.Parse("2016-07-08"));
            Assert.Equal(runner.CommitInfos[0].CommitTime, DateTime.Parse("2016-07-08"));
            Assert.Equal(new List<string> { "wangjian", "shengqi", "jijie" },runner.CommitInfos[0].Devs);
            Assert.Equal(runner.CommitInfos[0].StoryNumber, "1919");
            Assert.Equal(runner.CommitInfos[0].Comment, "Change comments length from 255 to 500 characters for Upload teq feature, and fix some ie issue for text-area.");
            Assert.Equal(runner.CommitInfos[0].TestFileList, new HashSet<string>());
        }

    }
}