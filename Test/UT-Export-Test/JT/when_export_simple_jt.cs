﻿using System.Collections.Generic;
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
        public void should_export_describe_and_it()
        {
            var fixtureFileFullName = Utils.GetFixtureFileFullName("JT\\Fixtures\\simple-spec.js");

            var jtManager = new JTManager();
            List<UTInfo> utInfos = jtManager.Export(fixtureFileFullName);

            Assert.Equal(1, utInfos.Count);

            UTInfo utInfo = utInfos.Single();
            Assert.Equal("simple-spec.js", utInfo.FileName);
            Assert.Equal("describe", utInfo.Description);
            Assert.Equal("it 1", utInfo.ThenList[0]);
            Assert.Equal("it 2", utInfo.ThenList[1]);
        }
    }
}