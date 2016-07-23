using System;
using System.Collections.Generic;
using System.Linq;

namespace Git_Analysis.Domain
{
    public class CommitInformation
    {
        public string Hash { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime CommitTime { get; set; }
        public List<string> Devs { get; set; }
        public string StoryNumber { get; set; }
        public string Comment { get; set; }
        public ISet<string> TestFileList { get; set; }

        public override string ToString()
        {
            return "\nHash:" + Hash + "\nAddTime:"+AddTime+"\nCommitTime:"+CommitTime+"\n" +
                "Devs:"+string.Join(",",Devs)+"\nStoryNumber:"+StoryNumber+"\nComment:"+Comment+"\n" +
                "TestFileList:\n\t"+string.Join("\n\t",TestFileList.ToArray());
        }
    }
}