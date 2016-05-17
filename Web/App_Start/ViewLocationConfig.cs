using System.Linq;
using System.Web.Mvc;

namespace ClearCode.Web
{
    public class ViewLocationConfig
    {

        public static void Configure()
        {
            ViewEngines.Engines.Clear();
            var viewEngine = new CustomViewLocationRazorViewEngine();
            ViewEngines.Engines.Add(viewEngine);
        }

        public class CustomViewLocationRazorViewEngine : RazorViewEngine
        {
            public CustomViewLocationRazorViewEngine()
            {
                ViewLocationFormats = (new[] { "~/Features/{1}/{0}.cshtml", "~/Features/{1}/Views/{0}.cshtml" }).Concat(base.ViewLocationFormats).ToArray();
            }
        }
    }
}