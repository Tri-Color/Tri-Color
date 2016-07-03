using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace UTExport
{
    public static class Utils
    {
        public static bool IsComment(this string line)
        {
            return line.Trim().StartsWith("//");
        }

        public static int GetLevel(this string currentLine)
        {
            Match match = GetMatch(currentLine, "^([\t, \x20]*)");
            Debug.Assert(match.Success);
            string whitespace = match.Groups[1].Value;
            string afterReplace4SpacesWithTab = whitespace.Replace("    ", "\t");
            string afterRemoveReduntantSpace = afterReplace4SpacesWithTab.Replace(" ", "");
            return afterRemoveReduntantSpace.Length;
        }

        public static Match GetMatch(this string currentLine, string pattern)
        {
            return new Regex(pattern).Match(currentLine);
        }

        public static List<UTInfo> ExportAllUTs(string path,
            Func<string, IEnumerable<UTInfo>> exportFunc, Func<FileInfo, bool> isUtFile)
        {
            var directoryInfo = new DirectoryInfo(path);

            IEnumerable<UTInfo> utInfosFromFiles = directoryInfo
                .GetFiles()
                .Where(isUtFile)
                .ToList()
                .SelectMany(d => exportFunc(d.FullName));

            IEnumerable<UTInfo> utInfosFromDirectories =
                directoryInfo.GetDirectories()
                    .SelectMany(d => ExportAllUTs(d.FullName, exportFunc, isUtFile));

            IEnumerable<UTInfo> result = utInfosFromDirectories.Union(utInfosFromFiles);
            return result.ToList();
        }

        public static bool IsCsFile(FileInfo arg)
        {
            return arg.Extension == ".cs";
        }

        public static string GetFileName(string fileFullName)
        {
            var fileInfo = new FileInfo(fileFullName);
            return fileInfo.Name;
        }
    }
}