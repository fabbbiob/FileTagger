[assembly: WebActivator.PostApplicationStartMethod(typeof(FileTaggerMVC.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace FileTaggerMVC.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using RestSharp.Abstract;
    using RestSharp.Impl;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IFileRest, FileRestSharp>(Lifestyle.Scoped);
            container.Register<IProcessRest, ProcessRestSharp>(Lifestyle.Scoped);
            container.Register<ISearchRest, SearchRestSharp>(Lifestyle.Scoped);
            container.Register<ITagRest, TagRestSharp>(Lifestyle.Scoped);
            container.Register<ITagTypeRest, TagTypeRestSharp>(Lifestyle.Scoped);
            container.Register<IDataBaseCreatorRest, DataBaseCreatorRestSharp>(Lifestyle.Scoped);
            container.Register<IValidateFilePathControllerRest, ValidateFilePathControllerRestSharp>(Lifestyle.Scoped);
        }
    }
}