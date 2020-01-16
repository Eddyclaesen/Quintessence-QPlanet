using System.Web.Mvc;

namespace Quintessence.QPlanet.Webshell.Areas.SimulationSet
{
    public class SimulationSetAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SimulationSet";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SimulationSet_default",
                "SimulationSet/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
