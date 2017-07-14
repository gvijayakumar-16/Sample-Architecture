using Blogging.Web.App_Start;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blogging.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutofacConfig.RegisterComponents();
            AutomapperConfig.Init();
        }
    }
}