using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UTExport.JT;
using Xunit;

namespace UTExport.Test.JT
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class when_export_simple_jt
    {
        [Fact]
        public void should_export_describe_and_it_with_double_quotes()
        {
            var fixtureFileFullName = TestUtils.GetFixtureFileFullName("JT\\Fixtures\\simple-spec.js");

            var jtManager = new JTManager();
            List<UTInfo> utInfos = jtManager.Export(fixtureFileFullName);

            Assert.Equal(1, utInfos.Count);

            UTInfo utInfo = utInfos.Single();
            Assert.Equal("simple-spec.js", utInfo.FileName);
            Assert.Equal("describe", utInfo.Description);
            Assert.Equal("it 1", utInfo.ThenList[0].Description);
            Assert.Equal("it 2", utInfo.ThenList[1].Description);
        }
        
        [Fact]
        public void should_export_describe_and_it_with_single_quotes()
        {
            var fixtureFileFullName = TestUtils.GetFixtureFileFullName("JT\\Fixtures\\single-quotes-spec.js");

            var jtManager = new JTManager();
            List<UTInfo> utInfos = jtManager.Export(fixtureFileFullName);

            Assert.Equal(1, utInfos.Count);

            UTInfo utInfo = utInfos.Single();
            Assert.Equal("describe", utInfo.Description);
            Assert.Equal("it 1", utInfo.ThenList[0].Description);
            Assert.Equal("it 2", utInfo.ThenList[1].Description);
        }

        [Fact]
        public void should_export_no_jt_if_line_is_comment()
        {
            var fixtureFileFullName = TestUtils.GetFixtureFileFullName("JT\\Fixtures\\comment-spec.js");

            var jtManager = new JTManager();
            List<UTInfo> utInfos = jtManager.Export(fixtureFileFullName);

            Assert.Equal(0, utInfos.Count);
        }

        [Fact]
        public void should_export_jt_with_irregular_indent()
        {
            var fixtureFileFullName = TestUtils.GetFixtureFileFullName("JT\\Fixtures\\irregular-indent-spec.js");

            var jtManager = new JTManager();
            List<UTInfo> utInfos = jtManager.Export(fixtureFileFullName);

            Assert.Equal(1, utInfos.Count);

            UTInfo utInfo = utInfos.Single();
            Assert.Equal(1, utInfo.ThenList.Count);
        }
    }
}