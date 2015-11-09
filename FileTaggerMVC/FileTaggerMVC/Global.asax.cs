using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FileTaggerRepository.Helpers;
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

            DbCreator.CreateDatabase();
            AutoMapperConfiguration.Configure();

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
