using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UTExport.MSpec
{
    public class MSpecManager : IUTManager
    {
        public List<UTInfo> Export(string fullFileName)
        {
            var rootUtInfo = new UTInfo(fullFileName);

            using (var streamReader = new StreamReader(fullFileName))
            {
                while (!streamReader.EndOfStream)
                {
                    string currentLine = streamReader.ReadLine();

                    if (currentLine.IsComment() || !currentLine.IsUsefulMSpecStatement()) continue;

                    int currentLevel = currentLine.GetLevel();
                    if (currentLine.IsClass())
                    {
                        AddClass(fullFileName, rootUtInfo, currentLevel, currentLine);
                    }

                    if (currentLine.IsBecause())
                    {
                        AddBecause(rootUtInfo, currentLevel, currentLine);
                    }

                    if (currentLine.IsIt())
                    {
                        AddIt(rootUtInfo, currentLevel, currentLine);
                    }
                }
            }

            rootUtInfo.ClearEmptyChildren();

            return rootUtInfo.Children;
        }

        private void AddIt(UTInfo rootUtInfo, int currentLevel, string currentLine)
        {
            UTInfo parentUTInfo = GetFieldParentClass(rootUtInfo, currentLevel);
            parentUTInfo.ThenList.Add(new ThenInfo {Description = currentLine.ToIt()});
        }

        private void AddBecause(UTInfo rootUtInfo, int currentLevel, string currentLine)
        {
            UTInfo parentClass = GetFieldParentClass(rootUtInfo, currentLevel);
            parentClass.WhenList.Add(currentLine.ToBecause());
        }

        private void AddClass(string fullFileName, UTInfo rootUtInfo, int currentLevel, string currentLine)
        {
            UTInfo parentUtInfo = GetClassParent(rootUtInfo, currentLevel);
            UTInfo currentUtInfo = new UTInfo(fullFileName)
            {
                Description = currentLine.ToClassName(),
                Parent = parentUtInfo
            };
            parentUtInfo.Children.Add(currentUtInfo);
        }

        private UTInfo GetClassParent(UTInfo utInfo, int currentLevel)
        {
            if (currentLevel == 1)
            {
                return utInfo;
            }

            if (!utInfo.Children.Any())
            {
                return utInfo;
            }

            return GetClassParent(utInfo.Children.Last(), currentLevel - 1);
        }

        private UTInfo GetFieldParentClass(UTInfo utInfo, int currentLevel)
        {
            return GetClassParent(utInfo, currentLevel);
        }
    }
}