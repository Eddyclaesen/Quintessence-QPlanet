using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Security;
using Quintessence.QPlanet.Webshell.Models.Profile;

namespace Quintessence.QPlanet.Webshell.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            var identity = IdentityHelper.RetrieveIdentity(ControllerContext.HttpContext);

            if (identity != null)
            {
                var profileInfo = new ProfileInfo
                {
                    IsLoggedIn = true,
                    UserName = identity.Name
                };
                return PartialView(profileInfo);
            }
            return PartialView(new ProfileInfo { IsLoggedIn = false });
        }

        [Authorize]
        public ActionResult Detail()
        {
            return View();
        }
    }
}
