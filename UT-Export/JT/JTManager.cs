using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UTExport.JT
{
    public class JTManager : IUTManager
    {
        public static bool IsJtFile(FileInfo f)
        {
            string fullName = f.FullName;
            return fullName.EndsWith("-spec.es6") || fullName.EndsWith("-spec.js");
        }

        public List<UTInfo> Export(string fileFullName)
        {
            var rootUtInfo = new UTInfo(fileFullName);

            using (var streamReader = new StreamReader(fileFullName))
            {
                while (!streamReader.EndOfStream)
                {
                    string currentLine = streamReader.ReadLine();

                    if (currentLine.IsComment() || !currentLine.IsDescribeOrIt())
                    {
                        continue;
                    }

                    int currentLevel = currentLine.GetLevel();
                    if (currentLine.IsDescribe())
                    {
                        UTInfo parentUtInfo = GetDescribeParent(rootUtInfo, currentLevel);
                        UTInfo currentUtInfo = new UTInfo(fileFullName)
                        {
                            Description = currentLine.ToDescribeDescription(),
                            Parent = parentUtInfo
                        };
                        parentUtInfo.Children.Add(currentUtInfo);
                    }

                    if (currentLine.IsIt())
                    {
                        UTInfo parentUTInfo = GetItParent(rootUtInfo, currentLevel);
                        parentUTInfo.ThenList.Add(currentLine.ToItDescription());
                    }
                }
            }

            rootUtInfo.ClearEmptyChildren();

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
    }
}