using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Quintessence.QPlanet.Webshell.App_Start.RazorGeneratorMvcStart), "Start")]

namespace Quintessence.QPlanet.Webshell.App_Start {
    public static class RazorGeneratorMvcStart {
        public static void Start()
        {
            var engine = new PrecompiledMvcEngine(typeof (RazorGeneratorMvcStart).Assembly);

#if DEBUG
            engine.UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal;
#endif

            ViewEngines.Engines.Insert(0, engine);

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }
    }
}
