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
            string tigerFolderName = ConfigurationManager.AppSettings["TigerFolder"];

            var jtManager = new JTManager();
            List<UTInfo> exportAllJTs = Utils.ExportAllUTs(tigerFolderName, 
                f => jtManager.Export(f),
                JTManager.IsJtFile);

            UtTxtWriter.WriteToTxt(exportAllJTs, "c:\\users\\administrator\\desktop\\jt.txt");

            var mSpecManager = new MSpecManager();
            List<UTInfo> exportAllMspecs = Utils.ExportAllUTs(tigerFolderName,
                f => mSpecManager.Export(f),
                IsCsFile);

            UtTxtWriter.WriteToTxt(exportAllMspecs, "c:\\users\\administrator\\desktop\\mspec1.txt");
            
            var xUnitManager = new XUnitManager();
            List<UTInfo> exportAllXUnits = Utils.ExportAllUTs(tigerFolderName,
                f => xUnitManager.Export(f),
                IsCsFile);

            UtTxtWriter.WriteToTxt(exportAllXUnits, "c:\\users\\administrator\\desktop\\xunit1.txt");

        }

        private static bool IsCsFile(FileInfo arg)
        {
            return arg.Extension == ".cs";
        }
    }
}
