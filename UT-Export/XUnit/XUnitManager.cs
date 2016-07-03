using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UTExport.XUnit
{
    public class XUnitManager : IUTManager
    {
        public List<UTInfo> Export(string fullFileName)
        {
            var rootUtInfo = new UTInfo(fullFileName);
                
            using (var streamReader = new StreamReader(fullFileName))
            {
                bool isFact = false;

                while (!streamReader.EndOfStream)
                {
                    string currentLine = streamReader.ReadLine();

                    if (currentLine.IsComment() || !currentLine.IsUsefulXUnitStatement()) continue;

                    int currentLevel = currentLine.GetLevel();
                    if (currentLine.IsClass())
                    {
                        UTInfo parentUtInfo = GetClassParent(rootUtInfo, currentLevel);
                        UTInfo currentUtInfo = new UTInfo(fullFileName)
                        {
                            Description = currentLine.ToClassName(),
                            Parent = parentUtInfo
                        };
                        parentUtInfo.Children.Add(currentUtInfo);
                    }
                    else if (currentLine.IsFact() || currentLine.IsTheory() ||
                             currentLine.IsInlineData())
                    {
                        isFact = true;
                    }
                    else if (isFact && currentLine.IsMethod())
                    {
                        UTInfo parentUTInfo = GetFieldParentClass(rootUtInfo, currentLevel);
                        parentUTInfo.ThenList.Add(currentLine.ToMethodName());
                        isFact = false;
                    }
                }
            }

            rootUtInfo.ClearEmptyChildren();

            return rootUtInfo.Children;
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