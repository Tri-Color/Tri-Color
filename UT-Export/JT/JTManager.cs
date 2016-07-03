using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UTExport.JT
{
    public class JTManager
    {
        public List<UTInfo> Export(string fileFullName)
        {
            var result = new List<UTInfo>();

            var streamReader = new StreamReader(fileFullName);
            UTInfo currentUtInfo = null;

            while (!streamReader.EndOfStream)
            {
                string currentLine = streamReader.ReadLine();
                if (currentLine.IsDescribe())
                {
                    currentUtInfo = CreateNewUtInfo(fileFullName, result);
                    currentUtInfo.Description = currentLine.ToDescribeDescription();
                }

                if (currentLine.IsIt())
                {
                    Debug.Assert(currentUtInfo != null, "currentUtInfo != null");
                    currentUtInfo.ThenList.Add(currentLine.ToItDescription());
                }
            }

            return result;
        }

        private static UTInfo CreateNewUtInfo(string fileFullName, List<UTInfo> result)
        {
            var utInfo = new UTInfo
            {
                FileName = GetFileName(fileFullName)
            };
            result.Add(utInfo);
            return utInfo;
        }

        private static string GetFileName(string fileFullName)
        {
            var fileInfo = new FileInfo(fileFullName);
            return fileInfo.Name;
        }
    }
}