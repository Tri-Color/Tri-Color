using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Text.RegularExpressions;

namespace UTExport.JT
{
    public class JTManager
    {
        public List<UTInfo> Export(string fileFullName)
        {
            var utInfo = new UTInfo
            {
                FileName = GetFileName(fileFullName)
            };
            var streamReader = new StreamReader(fileFullName);
            var currentLine = streamReader.ReadLine();
            var regex = new Regex("(\\bdescribe\\([', \"])(.*)([', \"],)");
            Match match = regex.Match(currentLine);
            if (match.Success)
            {
                utInfo.Description = match.Groups[2].Value;                
            }
            return new List<UTInfo>
            {
                utInfo
            };
        }

        private static string GetFileName(string fileFullName)
        {
            var fileInfo = new FileInfo(fileFullName);
            return fileInfo.Name;
        }
    }
}