using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using CSResults.DAL;
using CSResults.Models;

namespace CSResults
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            //Register all MVC controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //Register my Generic Repository classes
            builder.RegisterType<GenericRepository<Result>>().As<IGenericRepository<Result>>();
            builder.RegisterType<GenericRepository<Models.Module>>().As<IGenericRepository<Models.Module>>();


            builder.RegisterFilterProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}
