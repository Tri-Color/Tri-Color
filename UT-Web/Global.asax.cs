using System;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using UT_API.Repository;

namespace UT_API
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            HttpConfiguration httpConfiguration = GlobalConfiguration.Configuration;
            RegisterRoutes(httpConfiguration);
            RegisterDependency(httpConfiguration);
        }

        private void RegisterDependency(HttpConfiguration httpConfiguration)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterType<ProjectUtInfoRepository>();

            IContainer container = containerBuilder.Build();
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterRoutes(HttpConfiguration httpConfiguration)
        {
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

            httpConfiguration.Routes.MapHttpRoute(
                "Search",
                "Search",
                new
                {
                    controller = "UT",
                    action = "Search"
                },
                new
                {
                    httpMethod = new HttpMethodConstraint(HttpMethod.Get)
                });
        }
    }
}