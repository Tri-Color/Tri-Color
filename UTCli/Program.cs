using System.Collections.Generic;
using System.Configuration;
using UTExport;
using UTExport.JT;
using UTExport.MSpec;

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
                Utils.IsCsFile);

            UtTxtWriter.WriteToTxt(exportAllMspecs, "c:\\users\\administrator\\desktop\\mspec1.txt");

        }
    }
}
