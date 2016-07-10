using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace UT_API
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            HttpConfiguration httpConfiguration = GlobalConfiguration.Configuration;
            httpConfiguration.Routes.MapHttpRoute(
                "GetAllUT",
                "UnitTests",
                new
                {
                    controller = "UT",
                    action = "Get"
                },
                new
                {
                    httpMethod = new HttpMethodConstraint(HttpMethod.Get)
                });
        }
    }
}