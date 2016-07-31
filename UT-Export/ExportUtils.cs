using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UTExport.JT;
using UTExport.MSpec;
using UTExport.XUnit;

namespace UTExport
{
    public static class ExportUtils
    {
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

        public static void ExportProjectTestsToFile(string projectName, List<string> folders)
        {
            var projectUtInfo = new ProjectUtInfo
            {
                ProjectName = projectName,
                ApiTests = ExportUtInfosFromFolders(folders, new MSpecManager(), Utils.IsCsFile),
                UnitTests = ExportUtInfosFromFolders(folders, new XUnitManager(), Utils.IsCsFile),
                JavaScriptTests = ExportUtInfosFromFolders(folders, new JTManager(), JTManager.IsJtFile)
            };

            var fileInfo = new FileInfo(Assembly.GetCallingAssembly().FullName);
            string fileName = String.Format("{0}\\..\\..\\..\\UT-Web\\App_Data\\{1}.json", fileInfo.Directory.FullName, projectName);
            UtJsonWriter.WriteToJson(projectUtInfo, fileName);
        }

        public static List<UTInfo> ExportUtInfosFromFolders(
            List<string> folders, IUTManager utManager,  Func<FileInfo, bool> isUtFile)
        {
            return folders
                .SelectMany(folder => ExportAllUTs(folder, utManager.Export, isUtFile))
                .ToList();
        }
    }
}