using Git_Analysis.Parsers;
using Xunit;

namespace Git_Analysis_Test.GitLogParsers
{
    public class ParseStoryInformationFact
    {
        [Theory]
        [InlineData("liuxia #7686 Use same convension between overview models.", "7686")]
        [InlineData("comment:[xueting] #7906 Fix timezone issue of service expire date.", "7906")]
        [InlineData("#n/a Update test description", "n/a")]
        public void should_parse_story_information_from_comments(string comment,string storyinfo)
        {
            StoryInformationParser parser = new StoryInformationParser();
            parser.parse(comment);
            Assert.Equal(parser.parse(comment), storyinfo);
        }

    }
}