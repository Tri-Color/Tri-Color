using System.Collections.Generic;
using System.Runtime.InteropServices;
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
                var commit = reader.GetOneCommit();
                CommitInfos.Add(analysis.GetParseCommitInformation(commit));
                reader.Close();
            }
         }
    }
}