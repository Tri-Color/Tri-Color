using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
            string tigerFolderName = ConfigurationManager.AppSettings["TigerFolder"];

            List<UTInfo> exportAllJTs = Utils.ExportAllUTs(tigerFolderName,
                utManager.Export,
                isUtFile);

            writeAction(exportAllJTs, outputFileName);
        }
    }
}
