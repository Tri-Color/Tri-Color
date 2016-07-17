using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UTExport.JT;
using Xunit;

namespace UTExport.Test.JT
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class when_export_jt_with_2_describes_in_same_level
    {
        [Fact]
        public void should_export_2_utInfos()
        {
            var fixtureFileFullName = TestUtils.GetFixtureFileFullName("JT\\Fixtures\\two-describe-in-same-level.js");

            var jtManager = new JTManager();
            List<UTInfo> utInfos = jtManager.Export(fixtureFileFullName);

            Assert.Equal(2, utInfos.Count);

            UTInfo utInfo1 = utInfos.First();
            Assert.Equal("describe 1", utInfo1.Description);
            Assert.Equal("it 1.1", utInfo1.ThenList[0].Description);
            Assert.Equal("it 1.2", utInfo1.ThenList[1].Description);
            
            UTInfo utInfo2 = utInfos.Last();
            Assert.Equal("describe 2", utInfo2.Description);
            Assert.Equal("it 2.1", utInfo2.ThenList[0].Description);
            Assert.Equal("it 2.2", utInfo2.ThenList[1].Description);
        }
    }
}