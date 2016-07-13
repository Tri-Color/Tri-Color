using System.Linq;
using System.Net.NetworkInformation;

namespace Git_Analysis.Analysis
{
    public class GitLogAnalysis
    {
        public string GetCommentFromCommit(string comment)
        {
            var infoArr = comment.Split('|');
            return infoArr.LastOrDefault().Contains("comment:") ? infoArr.LastOrDefault().Trim().Substring(8).Trim() :string.Empty;
        }
    }
}