using System.Collections.Generic;
using System.Linq;
using UTExport.XUnit;
using Xunit;

namespace UTExport.Test.XUnit
{
    public class when_export_xuint
    {
        [Fact]
        public void when_export_xunit_test_with_fact_attribute()
        {
            var xUnitManager = new XUnitManager();
            List<UTInfo> utInfos =
                xUnitManager.Export(
                    TestUtils.GetFixtureFileFullName("XUnit\\Fixtures\\GeneralXUnit.txt"));

            Assert.Equal(1, utInfos.Count);
            UTInfo utInfo = utInfos.Single();
            Assert.Equal("GeneralXUnit.txt", utInfo.FileName);
            Assert.Equal(0, utInfo.WhenList.Count);
            Assert.Equal("GeneralXUnit", utInfo.Description);
            Assert.Equal(4, utInfo.ThenList.Count);
            Assert.Equal("FactMethodName", utInfo.ThenList[0]);
            Assert.Equal("FactMethodName2", utInfo.ThenList[1]);
            Assert.Equal("TheoryMethodName", utInfo.ThenList[2]);
            Assert.Equal("TheoryMethodName2", utInfo.ThenList[3]);
        }
    }
}