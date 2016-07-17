using System;
using System.Collections.Generic;
using System.Linq;
using Git_Analysis.Domain;
using Xunit;

namespace Git_Analysis_Test.Domain
{
    public class CommitInformationFacts
    {
        [Fact]
        public void should_to_string_valid_format()
        {
            CommitInformation commitInformation = new CommitInformation
            {
                Hash = "HelloWorld",
                AddTime = DateTime.UtcNow,
                CommitTime = DateTime.UtcNow,
                Comment = "Fix Jt",
                StoryNumber = "8888",
                Devs = new List<string> { "zhangsan","lisi"},
                TestFileList = new HashSet<string>
                {
                    "/Test/Tiger-Core-Test/ResourceTests/MyMobilitySpec/ImmigrationMissingInfo/when_get_immigration_missing_info.cs",
                    "/Test/Tiger-Contract-Test-By-Consumer/myMobilityApps/ImmigrationMissingInformationFacts.cs"
                }
            };
            string result = "Hash:" + commitInformation.Hash + "\nAddTime:" + commitInformation.AddTime + "\nCommitTime:" + commitInformation.CommitTime + "\n" +
                "Devs:" + string.Join(",", commitInformation.Devs.ToArray()) + "\nStoryNumber:" + commitInformation.StoryNumber + "\nComment:" + commitInformation.Comment + "\n" +
                "TestFileList:\n\t" + string.Join("\n\t",commitInformation.TestFileList.ToArray());
            Assert.Equal(result,commitInformation.ToString());
        }
    }
}