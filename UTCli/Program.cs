using System.Collections.Generic;
using System.Configuration;
using UTExport;
using UTExport.JT;

namespace UTCli
{
    class Program
    {
        static void Main(string[] args)
        {
            string tigerFolderName = ConfigurationManager.AppSettings["TigerFolder"];
            var jtManager = new JTManager();
            List<UTInfo> exportAllJTs = jtManager.ExportAllJTs(tigerFolderName);
            UtTxtWriter.WriteToTxt(exportAllJTs, "c:\\users\\administrator\\desktop\\jt.txt");
        }
    }
}
