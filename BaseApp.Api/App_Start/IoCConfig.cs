using BaseApp.Data;
using BaseApp.Data.Contracts;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace BaseApp.Api.App_Start
{
    public static class IoCConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // Register types here
            //container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<RepositoryFactories>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRepositoryProvider, RepositoryProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IUoW, BaseAppUow>(new HierarchicalLifetimeManager());
            // TODO create an IServiceProvider

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}