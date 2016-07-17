using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Newtonsoft.Json;
using UTExport;

namespace UT_API.Repository
{
    public class ProjectUtInfoRepository
    {
        private static readonly List<ProjectUtInfo> allProjectUtInfos;

        static ProjectUtInfoRepository()
        {
            ProjectUtInfo tigerUtInfos = LoadProjectUtInfoFromFile(UtConfigKeys.Tiger);
            ProjectUtInfo myMobilityUtInfos = LoadProjectUtInfoFromFile(UtConfigKeys.MyMobility);

            allProjectUtInfos = new List<ProjectUtInfo> { tigerUtInfos, myMobilityUtInfos };
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
            return tests.FindAll(utInfo => utInfo.Contains(searchKeyword));
        }

        private static ProjectUtInfo LoadProjectUtInfoFromFile(string projectName)
        {
            ProjectUtInfo projectUtInfo;
            string projectFileName = WebConfigurationManager.AppSettings[projectName];
            string mapPath = HttpContext.Current.Server.MapPath("~/App_Data");
            string fileName = string.Format("{0}\\{1}", mapPath, projectFileName);
            using (var streamReader = new StreamReader(fileName))
            {
                projectUtInfo = JsonConvert.DeserializeObject<ProjectUtInfo>(streamReader.ReadToEnd());
            }
            return projectUtInfo;
        }
    }
}