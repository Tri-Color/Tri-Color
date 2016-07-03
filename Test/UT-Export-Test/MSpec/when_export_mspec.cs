using System.Collections.Generic;
using System.Linq;
using UTExport.MSpec;
using Xunit;

namespace UTExport.Test.MSpec
{
    public class when_export_mspec
    {
        [Fact]
        public void when_export_standard_simple_mspec()
        {
            var mSpecManager = new MSpecManager();
            List<UTInfo> utInfos =
                mSpecManager.Export(
                    Utils.GetFixtureFileFullName("MSpec\\Fixtures\\StandardSimpleMSpec.txt"));

            Assert.Equal(1, utInfos.Count);
            UTInfo utInfo = utInfos.Single();
            Assert.Equal("StandardSimpleMSpec.txt", utInfo.FileName);
            Assert.Equal(1, utInfo.WhenList.Count);
            Assert.Equal("do_something", utInfo.WhenList.Single());
            Assert.Equal(2, utInfo.ThenList.Count);
            Assert.Equal("should_do_one_thing", utInfo.ThenList.First());
            Assert.Equal("should_do_another_thing", utInfo.ThenList.Last());
        }

        [Fact]
        public void when_export_mspec_with_nested_spec()
        {
            var mSpecManager = new MSpecManager();
            List<UTInfo> utInfos =
                mSpecManager.Export(
                    Utils.GetFixtureFileFullName("MSpec\\Fixtures\\MSpecWithNested.txt"));

            Assert.Equal(1, utInfos.Count);
            UTInfo topLevelUtInfo = utInfos.Single();
            Assert.Equal(0, topLevelUtInfo.WhenList.Count);
            Assert.Equal(0, topLevelUtInfo.ThenList.Count);
            Assert.Equal(2, topLevelUtInfo.Children.Count);

            UTInfo nested1UtInfo = topLevelUtInfo.Children.First();
            Assert.Equal(1, nested1UtInfo.WhenList.Count);
            Assert.Equal(2, nested1UtInfo.ThenList.Count);
            Assert.Equal(0, nested1UtInfo.Children.Count);

            UTInfo nested2UtInfo = topLevelUtInfo.Children.Last();
            Assert.Equal(0, nested2UtInfo.WhenList.Count);
            Assert.Equal(0, nested2UtInfo.ThenList.Count);
            Assert.Equal(2, nested2UtInfo.Children.Count);

            UTInfo nested3UtInfo = nested2UtInfo.Children.First();
            Assert.Equal(2, nested3UtInfo.WhenList.Count);
            Assert.Equal(2, nested3UtInfo.ThenList.Count);
            Assert.Equal(0, nested3UtInfo.Children.Count);

            UTInfo nested4UtInfo = nested2UtInfo.Children.Last();
            Assert.Equal(1, nested4UtInfo.WhenList.Count);
            Assert.Equal(2, nested4UtInfo.ThenList.Count);
            Assert.Equal(0, nested4UtInfo.Children.Count);
        }
    }
}
