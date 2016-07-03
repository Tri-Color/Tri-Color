using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UTExport.JT
{
    public class JTManager
    {
        public List<UTInfo> ExportAllJTs(string path)
        {
            var directoryInfo = new DirectoryInfo(path);

            IEnumerable<UTInfo> utInfosFromFiles = directoryInfo
                .GetFiles()
                .Where(IsJtFile)
                .ToList()
                .SelectMany(f => Export(f.FullName));

            IEnumerable<UTInfo> utInfosFromDirectories =
                directoryInfo.GetDirectories().SelectMany(d => ExportAllJTs(d.FullName));

            IEnumerable<UTInfo> result = utInfosFromDirectories.Union(utInfosFromFiles);
            return result.ToList();
        }

        private static bool IsJtFile(FileInfo f)
        {
            string fullName = f.FullName;
            return fullName.EndsWith("-spec.es6") || fullName.EndsWith("-spec.js");
        }

        public List<UTInfo> Export(string fileFullName)
        {
            var rootUtInfo = new UTInfo();

            var streamReader = new StreamReader(fileFullName);
            while (!streamReader.EndOfStream)
            {
                string currentLine = streamReader.ReadLine();

                if (currentLine.IsComment() || !currentLine.IsDescribeOrIt()) continue;

                int currentLevel = currentLine.GetLevel();
                if (currentLine.IsDescribe())
                {
                    UTInfo parentUtInfo = GetDescribeParent(rootUtInfo, currentLevel);
                    UTInfo currentUtInfo = CreateNewUtInfo(fileFullName);
                    currentUtInfo.Description = currentLine.ToDescribeDescription();
                    currentUtInfo.Parent = parentUtInfo;
                    parentUtInfo.Children.Add(currentUtInfo);
                }

                if (currentLine.IsIt())
                {
                    UTInfo parentUTInfo = GetItParent(rootUtInfo, currentLevel);
                    parentUTInfo.ThenList.Add(currentLine.ToItDescription());
                }
            }

            return rootUtInfo.Children;
        }

        private UTInfo GetDescribeParent(UTInfo utInfo, int currentLevel)
        {
            if (currentLevel == 0)
            {
                return utInfo;
            }

            if (!utInfo.Children.Any())
            {
                return utInfo;
            }

            return GetDescribeParent(utInfo.Children.Last(), currentLevel - 1);
        }

        private UTInfo GetItParent(UTInfo utInfo, int currentLevel)
        {
            return GetDescribeParent(utInfo, currentLevel);
        }

        private static UTInfo CreateNewUtInfo(string fileFullName)
        {
            var utInfo = new UTInfo
            {
                FileName = GetFileName(fileFullName)
            };
            return utInfo;
        }

        private static string GetFileName(string fileFullName)
        {
            var fileInfo = new FileInfo(fileFullName);
            return fileInfo.Name;
        }
    }
}