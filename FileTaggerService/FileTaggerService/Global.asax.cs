using FileTaggerRepository.Repositories.Abstract;
using FileTaggerRepository.Repositories.Impl;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace FileTaggerService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register<IFileRepository, FileRepository>(Lifestyle.Scoped);
            container.Register<ITagRepository, TagRepository>(Lifestyle.Scoped);
            container.Register<ITagTypeRepository, TagTypeRepository>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
