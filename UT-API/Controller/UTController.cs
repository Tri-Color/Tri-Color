using System.Collections.Generic;
using System.IO;
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
            List<UTInfo> mspecInfos = GetUtInfos("mspecFileName");
            List<UTInfo> xunitInfos = GetUtInfos("xunitFileName");
            List<UTInfo> jtInfos = GetUtInfos("jtFileName");

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                mspecInfos,
                xunitInfos,
                jtInfos
            }, new JsonMediaTypeFormatter());
        }

        private static List<UTInfo> GetUtInfos(string utFileKey)
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
    }
}