using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using UTExport;

namespace UT_API.Repository
{
    public class ProjectUtInfoRepository
    {
        private static readonly List<ProjectUtInfo> allProjectUtInfos;

        static ProjectUtInfoRepository()
        {
            allProjectUtInfos = LoadProjectUtInfoFromFile();
        }

        public List<ProjectUtInfo> GetProjectUtInfos(string query)
        {
            return allProjectUtInfos
                .Select(projectUtInfo => new ProjectUtInfo
                {
                    ProjectName = projectUtInfo.ProjectName,
                    ApiTests = FindAll(projectUtInfo.ApiTests, query),
                    UnitTests = FindAll(projectUtInfo.UnitTests, query),
                    JavaScriptTests = FindAll(projectUtInfo.JavaScriptTests, query)
                })
                .ToList();
        }

        private static List<UTInfo> FindAll(List<UTInfo> tests, string searchKeyword)
        {
            if (string.IsNullOrEmpty(searchKeyword))
            {
                return tests;
            }
            return tests.FindAll(utInfo => utInfo.Contains(searchKeyword));
        }

        private static List<ProjectUtInfo> LoadProjectUtInfoFromFile()
        {
            string mapPath = HttpContext.Current.Server.MapPath("~/App_Data");
            var directoryInfo = new DirectoryInfo(mapPath);

            return directoryInfo.GetFiles()
                .Select(fileInfo =>
                {
                    using (var streamReader = new StreamReader(fileInfo.FullName))
                    {
                        return JsonConvert.DeserializeObject<ProjectUtInfo>(streamReader.ReadToEnd());
                    }
                })
                .ToList();
        }
    }
}