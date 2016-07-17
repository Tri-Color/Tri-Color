using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UTExport
{
    public class UtTxtWriter
    {
        public static void WriteToTxt(List<UTInfo> utInfos, string fileName)
        {
            using (var streamWriter = new StreamWriter(fileName))
            {
                OutputTo(utInfos, streamWriter);
            }
        }

        private static void OutputTo(IList<UTInfo> utInfos, TextWriter textWriter)
        {
            textWriter.WriteLine("UT count is {0}", utInfos.Count);

            var becauseCount = utInfos.Sum(utInfo => GetElementCount(utInfo, i => i.WhenList));
            if (becauseCount != 0)
            {
                textWriter.WriteLine("Because count is {0}", becauseCount);                
            }

            var itCount = utInfos.Sum(utInfo => GetElementCount(utInfo, i => i.ThenList));
            textWriter.WriteLine("It count is {0}", itCount);
            textWriter.WriteLine();

            foreach (var utInfo in utInfos)
            {
                WriteUtInfo(textWriter, utInfo, 0);
                textWriter.WriteLine();
            }
        }

        private static void WriteUtInfo(TextWriter textWriter, UTInfo utInfo, int tabCount)
        {
            textWriter.WriteLine("{0}{1}", new string('\t', tabCount), utInfo.Description);

            var space = new string('\t', tabCount + 1);
            if (utInfo.WhenList.Any())
            {
                utInfo.WhenList.ForEach(b => textWriter.WriteLine("{0}{1}", space, b));
            }
            utInfo.ThenList.ForEach(b => textWriter.WriteLine("{0}{1}", space, b));

            utInfo.Children.ForEach(
                s =>
                {
                    WriteUtInfo(textWriter, s, tabCount + 1);
                    textWriter.WriteLine();
                });

            if (utInfo.Children.Count == 0)
            {
                textWriter.WriteLine();
            }
        }

        private static int GetElementCount<T>(UTInfo utInfo, Func<UTInfo, List<T>> func)
        {
            return func(utInfo).Count + utInfo.Children.Sum(i => GetElementCount(i, func));
        }
    }
}