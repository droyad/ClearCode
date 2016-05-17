using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ClearCode.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IoCConfig.Build();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewLocationConfig.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
