using System.Web.Mvc;

namespace Quintessence.QPlanet.Webshell.Areas.SimulationSet.Controllers
{
    public class SimulationSetHomeController : SimulationSetController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
