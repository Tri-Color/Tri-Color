using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using UT_API.Repository;

namespace UT_API.Controller
{
    public class UTController : ApiController
    {
        private readonly ProjectUtInfoRepository projectUtInfoRepository;

        public UTController(ProjectUtInfoRepository projectUtInfoRepository)
        {
            this.projectUtInfoRepository = projectUtInfoRepository;
        }

        public HttpResponseMessage Get()
        {
            return GetResponse();
        }

        private HttpResponseMessage GetResponse(string query = null)
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                projectUtInfoRepository.GetProjectUtInfos(query),
                new JsonMediaTypeFormatter());
        }

        [HttpGet]
        public HttpResponseMessage Search(string query)
        {
            return GetResponse(query);
        }
    }
}