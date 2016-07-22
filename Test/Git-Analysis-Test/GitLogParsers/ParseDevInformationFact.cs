using System;
using System.Collections.Generic;
using Git_Analysis.Parsers;
using Xunit;

namespace Git_Analysis_Test.GitLogParsers
{
    public class ParseDevInformationFact
    {

        [Theory]
        [InlineData("[Ruizhi & hcheng] #n/a use year as the filter", "Ruizhi,hcheng")]
        [InlineData("[bojian & wande] #n/a ajust wording in kpi overview and kpi detail page.", "bojian,wande")]
        [InlineData("[loic] #1881 Update the message when unsubscribe CoC tile. Move messages in i18n.", "loic")]
        [InlineData("wangdi/yongguang #N/A re-arrange tiles.", "wangdi,yongguang")]
        [InlineData("wangdi/yongguang/liuxia #N/A Extract all overview strategies.", "wangdi,yongguang,liuxia")]
        [InlineData("zhifang & liuxia & yongguang #7876 Fix some jt when ut review", "zhifang,liuxia,yongguang")]
        [InlineData("liuxia #7686 Use same convension between overview models.", "liuxia")]
        public void should_parse_dev_from_comments(String comment,String devs)
        {
            DevInformationParser devParser = new DevInformationParser();
             List<string> devNames = devParser.parse(comment) as List<string>;
            foreach (var dev in devs.Split(','))
            {
                Assert.True(devNames.Contains(dev));
            }
            Assert.Equal(devNames.Count, devs.Split(',').Length);
        }
    }
}
