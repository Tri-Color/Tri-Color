using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using UTExport;
using UTExport.JT;
using UTExport.MSpec;
using UTExport.XUnit;

namespace UTCli
{
    class Program
    {
        static void Main(string[] args)
        {
//            ExportToFiles(UtTxtWriter.WriteToTxt);
            ExportToFiles(UtJsonWriter.WriteToJson);
        }

        private static void ExportToFiles(Action<List<UTInfo>, string> writeToFileAction)
        {
            ExportToFile(new JTManager(), JTManager.IsJtFile,
                "C:\\Tri-Color\\UT-API\\App_Data\\jt.json", writeToFileAction);

            ExportToFile(new MSpecManager(), Utils.IsCsFile,
                "C:\\Tri-Color\\UT-API\\App_Data\\mspec.json", writeToFileAction);

            ExportToFile(new XUnitManager(), Utils.IsCsFile,
                "C:\\Tri-Color\\UT-API\\App_Data\\xunit.json", writeToFileAction);
        }

        private static void ExportToFile(IUTManager utManager,
            Func<FileInfo, bool> isUtFile, string outputFileName, Action<List<UTInfo>, string> writeAction)
        {
            List<string> folderList = GetFolders();

            List<UTInfo> utInfos = folderList
                .SelectMany(folder => Utils.ExportAllUTs(
                    folder,
                    utManager.Export,
                    isUtFile))
                .ToList();

            writeAction(utInfos, outputFileName);
        }

        private static List<string> GetFolders()
        {
            return new List<string>
            {
                ConfigurationManager.AppSettings["TigerFolder"],
                ConfigurationManager.AppSettings["MyMobilityFolder"],
                ConfigurationManager.AppSettings["MyMobilityAppsFolder"]
            };
        }
    }
}
