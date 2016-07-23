using System;
using System.Collections.Generic;
using System.IO;
using Git_Analysis.Domain;
using Git_Analysis.Utils;

namespace Git_Analysis.Analysis
{
    public class AnalysisRunner
    {
        string inputPath = "";
        FileReader reader;
        GitLogAnalysis analysis;

        public AnalysisRunner(string inputPath)
        {
            this.inputPath = inputPath;
            CommitInfos = new List<CommitInformation>();
        }

        public List<CommitInformation> CommitInfos { get; set; }

        public void init()
        {
            reader = new FileReader(inputPath);
            analysis = new GitLogAnalysis();
        }

        public void run()
        {
            init();
            using (reader)
            {
                reader.Open();
                do
                {
                    CommitInfos.Add(analysis.GetParseCommitInformation(reader.GetOneCommit()));
                } while (reader.HasMoreCommitInfo());
                reader.Close();
            }
         }

        public void Write()
        {
            foreach (var commitInfo in CommitInfos)
            {
                Console.Write(commitInfo.ToString());
            }
        }

        public static void Main(string[] args)
        {

            string input_path = Path.GetFullPath(Environment.CurrentDirectory + @"\git.log");

            AnalysisRunner runner = new AnalysisRunner(input_path);
            runner.run();
            runner.Write();
        }
    }
}