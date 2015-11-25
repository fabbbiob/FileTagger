using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FileTaggerMVC.AutoMapper;
using FileTaggerMVC.RestSharp.Abstract;
using FileTaggerMVC.App_Start;

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
            SimpleInjectorInitializer.Initialize();

            IDataBaseCreatorRest creator = DependencyResolver.Current.GetService<IDataBaseCreatorRest>();
            creator.Create();
            AutoMapperConfiguration.Configure();
        }
    }
}
