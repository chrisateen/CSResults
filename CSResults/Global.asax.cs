﻿using System.Data.Entity;
using System.Reflection;
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
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            
            var dataAccess = Assembly.GetExecutingAssembly();

            //Register the context class
            builder.RegisterAssemblyTypes(dataAccess)
               .Where(t => t.Name.EndsWith("Context"))
               .As<DbContext>();

            //Register my ViewModel classes
            builder.RegisterAssemblyTypes(dataAccess)
               .Where(t => t.Name.EndsWith("ViewModel"));

            builder.RegisterFilterProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}
