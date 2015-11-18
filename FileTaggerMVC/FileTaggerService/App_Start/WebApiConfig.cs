using FileTaggerRepository.Repositories.Abstract;
using FileTaggerRepository.Repositories.Impl;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace FileTaggerService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "FileTaggerServiceApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
