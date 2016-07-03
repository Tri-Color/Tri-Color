using System.Collections.Generic;
using System.IO;

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
            while (!streamReader.EndOfStream)
            {
                string currentLine = streamReader.ReadLine();

                if (currentLine.IsDescribe())
                {
                    utInfo.Description = currentLine.ToDescribeDescription();
                }

                if (currentLine.IsIt())
                {
                    utInfo.ThenList.Add(currentLine.ToItDescription());
                }
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