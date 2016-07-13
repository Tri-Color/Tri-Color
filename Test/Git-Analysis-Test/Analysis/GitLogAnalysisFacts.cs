using Git_Analysis.Analysis;
using Xunit;

namespace Git_Analysis_Test.Analysis
{
    public class GitLogAnalysisFacts
    {
        [Theory]
        [InlineData("hash:5016e80|addTime:2016-07-08|commitTime:2016-07-08| " +
            "comment:[wangjian & shengqi & jijie] #1919 Change comments length " +
            "from 255 to 500 characters for Upload teq feature, and fix some ie issue for text-area.",
            "[wangjian & shengqi & jijie] #1919 Change comments length from 255 to 500 characters for Upload teq feature, and fix some ie issue for text-area.")]
        [InlineData("","")]
        [InlineData("hash:870b57c|addTime:2016-07-08|commitTime:2016-07-08| " +
            "comment:[naijia] Handle the case of no publications nor subscriptions" +
            " in Tiger and MyData database when dropping existing publications and subscriptions",
            "[naijia] Handle the case of no publications nor subscriptions in Tiger and MyData " +
                "database when dropping existing publications and subscriptions")]
        public void should_get_comment_info_from_commmit(string commitInfo,string comment)
        {
            GitLogAnalysis analysis = new GitLogAnalysis();
            Assert.Equal(comment,analysis.GetCommentFromCommit(commitInfo));
        }
    }
}