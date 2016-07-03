using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UTExport.JT;
using Xunit;

namespace UTExport.Test.JT
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class when_export_jt_with_space_and_tab
    {
        [Fact]
        public void should_export_jt_with_correct_levels()
        {
            var fixtureFileFullName = Utils.GetFixtureFileFullName("JT\\Fixtures\\space-and-tab-spec.es6");

            var jtManager = new JTManager();
            List<UTInfo> utInfos = jtManager.Export(fixtureFileFullName);

            Assert.Equal(1, utInfos.Count);

            UTInfo topUtInfo = utInfos.Single();
            Assert.Equal(1, topUtInfo.Children.Count);
            
            UTInfo child = topUtInfo.Children.Single();
            Assert.Equal("describe with 3 spaces and 1 tab", child.Description);
            Assert.Equal(1, child.Children.Count);
            
            UTInfo grandson = child.Children.Single();
            Assert.Equal("describe with 5 spaces and 1 tab", grandson.Description);
        }
    }
}