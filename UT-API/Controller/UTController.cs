using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Newtonsoft.Json;
using UTExport;

namespace UT_API.Controller
{
    public class UTController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return GetResponse();
        }

        private HttpResponseMessage GetResponse(string query = null)
        {
            ProjectUtInfo tigerUtInfos = GetProjectUtInfo(UtConfigKeys.Tiger, query);
            ProjectUtInfo myMobilityUtInfos = GetProjectUtInfo(UtConfigKeys.MyMobility, query);

            return Request.CreateResponse(HttpStatusCode.OK,
                new List<ProjectUtInfo> {tigerUtInfos, myMobilityUtInfos},
                new JsonMediaTypeFormatter());
        }

        private static ProjectUtInfo GetProjectUtInfo(string projectName, string searchKeyword = null)
        {
            ProjectUtInfo projectUtInfo = LoadProjectUtInfoFromFile(projectName);

            if (string.IsNullOrEmpty(searchKeyword))
            {
                return projectUtInfo;
            }

            FilterByKeyword(projectUtInfo, searchKeyword);

            return projectUtInfo;
        }

        private static void FilterByKeyword(ProjectUtInfo projectUtInfo, string searchKeyword)
        {
            projectUtInfo.ApiTests = FindAll(projectUtInfo.ApiTests, searchKeyword);
            projectUtInfo.UnitTests = FindAll(projectUtInfo.UnitTests, searchKeyword);
            projectUtInfo.JavaScriptTests = FindAll(projectUtInfo.JavaScriptTests, searchKeyword);
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

        [HttpGet]
        public HttpResponseMessage Search(string query)
        {
            return GetResponse(query);
        }
    }
}