using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FileTaggerMVC.AutoMapper;

namespace FileTaggerMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // TODO call web api
            //DbCreator.CreateDatabase();
            AutoMapperConfiguration.Configure();
        }
    }
}
