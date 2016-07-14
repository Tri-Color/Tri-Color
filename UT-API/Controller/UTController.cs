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
            return GetUtInfosResponse();
        }

        private HttpResponseMessage GetUtInfosResponse(string query = null)
        {
            List<UTInfo> mspecInfos = GetUtInfos("mspecFileName", query);
            List<UTInfo> xunitInfos = GetUtInfos("xunitFileName", query);
            List<UTInfo> jtInfos = GetUtInfos("jtFileName", query);

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                mspecInfos,
                xunitInfos,
                jtInfos
            }, new JsonMediaTypeFormatter());
        }

        private static List<UTInfo> GetUtInfos(string utFileKey, string searchKeyword = null)
        {
            List<UTInfo> utInfos = LoadUTInfosFromFile(utFileKey);

            if (string.IsNullOrEmpty(searchKeyword))
            {
                return utInfos;
            }

            List<UTInfo> searchResult =
                utInfos.Where(utInfo => utInfo.Contains(searchKeyword)).ToList();

            return searchResult;
        }

        private static List<UTInfo> LoadUTInfosFromFile(string utFileKey)
        {
            List<UTInfo> utInfos;
            string mspecFileName = WebConfigurationManager.AppSettings[utFileKey];
            string mapPath = HttpContext.Current.Server.MapPath("~/App_Data");
            string fileName = string.Format("{0}\\{1}", mapPath, mspecFileName);
            using (var streamReader = new StreamReader(fileName))
            {
                utInfos = JsonConvert.DeserializeObject<List<UTInfo>>(streamReader.ReadToEnd());
            }
            return utInfos;
        }

        [HttpGet]
        public HttpResponseMessage Search(string query)
        {
            return GetUtInfosResponse(query);
        }
    }
}