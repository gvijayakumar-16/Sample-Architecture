using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

namespace Blogging.Web.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterComponents()
        {
            var builder = new ContainerBuilder();
            Services.AutofacConfigServices.RegisterServices(builder);
            Services.AutofacConfigServices.RegisterData(builder);
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}