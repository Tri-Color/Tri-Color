using System;
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
            }
        }
    }
}