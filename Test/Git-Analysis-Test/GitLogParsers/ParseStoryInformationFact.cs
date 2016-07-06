using Git_Analysis.Parsers;
using Xunit;

namespace Git_Analysis_Test.GitLogParsers
{
    public class ParseStoryInformationFact
    {
        [Theory]
        [InlineData("liuxia #7686 Use same convension between overview models.", "7686")]
        public void should_parse_story_information_from_comments(string comment,string storyinfo)
        {
            StoryInformationParser parser = new StoryInformationParser();
            parser.parse(comment);
            Assert.Equal(parser.StoryNum,storyinfo);
        }

    }
}