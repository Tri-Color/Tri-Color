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
            ExportToTxt(new JTManager(), JTManager.IsJtFile,
                "c:\\users\\administrator\\desktop\\jt.txt");

            ExportToTxt(new MSpecManager(), Utils.IsCsFile,
                "c:\\users\\administrator\\desktop\\mspec1.txt");

            ExportToTxt(new XUnitManager(), Utils.IsCsFile,
                "c:\\users\\administrator\\desktop\\xunit1.txt");

        }

        private static void ExportToTxt(IUTManager utManager,
            Func<FileInfo, bool> isUtFile, string outputFileName)
        {
            string tigerFolderName = ConfigurationManager.AppSettings["TigerFolder"];

            List<UTInfo> exportAllJTs = Utils.ExportAllUTs(tigerFolderName,
                utManager.Export,
                isUtFile);

            UtTxtWriter.WriteToTxt(exportAllJTs, outputFileName);
        }
    }
}
