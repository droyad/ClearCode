using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace ClearCode.Web
{
    public class IoCConfig
    {
        public static void Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterByAttributes(
                typeof (IoCConfig).Assembly
                );
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}