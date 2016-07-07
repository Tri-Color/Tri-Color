using Git_Analysis.Parsers;
using Xunit;

namespace Git_Analysis_Test.GitLogParsers
{
    public class ParseCommentInformationFact
    {
        [Theory]
        [InlineData("liuxia #7686 Use same convension between overview models.", "Use same convension between overview models.")]
        [InlineData("comment:[xueting] #7906 Fix timezone issue of service expire date.", "Fix timezone issue of service expire date.")]
        [InlineData("#n/a Update test description", "Update test description")]
        [InlineData("[jiaoming & jijie] #7856 add secure flag to serverError and requestTimeout cookie in Tiger-UI", "add secure flag to serverError and requestTimeout cookie in Tiger-UI")]
        [InlineData("#1883 Send notification email even when immigration application is rejected. Return comments in the DTO. " +
            "Set the variable approvedOrRejected before sending the email to notificationService", "Send notification email even when immigration application is rejected. Return comments in the DTO. Set the variable approvedOrRejected before sending the email to notificationService")]
        public void should_parse_comment_information_from_comments(string comment,string commentInfo)
        {
            CommentInformationParser parser = new CommentInformationParser();
            parser.parse(comment);
            Assert.Equal(parser.Comment,commentInfo);
        }

    }
}