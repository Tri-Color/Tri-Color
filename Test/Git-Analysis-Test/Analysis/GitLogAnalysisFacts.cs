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
            GitLogAnalysis analysis = new GitLogAnalysis(commitInfo);
            Assert.Equal(comment,analysis.GetCommentFromCommit());
        }

        [Theory]
        [InlineData("hash:5016e80|addTime:2016-07-08|commitTime:2016-07-08| " +
            "comment:[wangjian & shengqi & jijie] #1919 Change comments length " +
            "from 255 to 500 characters for Upload teq feature, and fix some ie issue for text-area.",
            "wangjian,shengqi,jijie","1919", "Change comments length from 255 to 500 characters for Upload teq feature, and fix some ie issue for text-area.")]
        [InlineData("hash:f4b5364|addTime:2016-07-07|commitTime:2016-07-07| " +
            "comment:[wangtao & chaohui] #8041 Type of Service drop down should list all countries", "chaohui,wangtao", "8041", "Type of Service drop down should list all countries")]
        public void should_recogenize_dev_story_comment_from_commit(string commitInfo,string devs,string story,string comment)
        {
            GitLogAnalysis analysis = new GitLogAnalysis(commitInfo);
            foreach (var dev in devs.Split(','))
            {
                Assert.True(analysis.GetDevs().Contains(dev));
            }
            Assert.Equal(story,analysis.GetStoryNum());
            Assert.Equal(comment,analysis.GetComment());
        }
    }
}