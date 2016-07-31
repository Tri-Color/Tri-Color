using System.Collections.Generic;
using System.Configuration;
using UTExport;

namespace UTCli
{
    class Program
    {
        static void Main(string[] args)
        {
            ExportUtils.ExportProjectTestsToFile("MyMobility", new List<string>
            {
                ConfigurationManager.AppSettings["MyMobilityFolder"]
            });

            ExportUtils.ExportProjectTestsToFile("Tiger", new List<string>
            {
                ConfigurationManager.AppSettings["TigerFolder"]                
            });

            ExportUtils.ExportProjectTestsToFile("MyMobility Apps", new List<string>
            {
                ConfigurationManager.AppSettings["MyMobilityAppsFolder"]
            });
        }
    }
}
