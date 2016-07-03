using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UTExport.JT;
using Xunit;

namespace UTExport.Test.JT
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class when_export_nested_jt
    {
        [Fact]
        public void should_export_jt_with_nested_jts()
        {
            var fixtureFileFullName = Utils.GetFixtureFileFullName("JT\\Fixtures\\nested-spec.es6");

            var jtManager = new JTManager();
            List<UTInfo> utInfos = jtManager.Export(fixtureFileFullName);

            Assert.Equal(2, utInfos.Count);

            UTInfo topUtInfo1 = utInfos.First();
            Assert.Equal("top describe 1", topUtInfo1.Description);
            Assert.Equal("top it 1.1", topUtInfo1.ThenList[0]);
            Assert.Equal("top it 1.2", topUtInfo1.ThenList[1]);
            
            UTInfo child1 = topUtInfo1.Children.First();
            Assert.Equal("describe 1", child1.Description);
            Assert.Equal("it 1.1", child1.ThenList[0]);
            Assert.Equal("it 1.2", child1.ThenList[1]);

            Assert.Equal(1, child1.Children.Count);
            UTInfo grandson = child1.Children.First();
            Assert.Equal("describe 1.3", grandson.Description);
            Assert.Equal("it 1.3.1", grandson.ThenList[0]);
            Assert.Equal("it 1.3.2", grandson.ThenList[1]);

            UTInfo child2 = topUtInfo1.Children.Last();
            Assert.Equal("describe 2", child2.Description);
            Assert.Equal("it 2.1", child2.ThenList[0]);
            Assert.Equal("it 2.2", child2.ThenList[1]);

            UTInfo topUtInfo2 = utInfos.Last();
            Assert.Equal("top describe 2", topUtInfo2.Description);
            Assert.Equal("top it 2.1", topUtInfo2.ThenList[0]);
            Assert.Equal("top it 2.2", topUtInfo2.ThenList[1]);
        }
    }
}