using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Quintessence.QCandidate.Controllers
{
    public class AccountsController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                // making cookie valid for the actual app root path (which is not necessarily "/" e.g. if we're behind a reverse proxy)
                new CookieOptions { Path = Url.Content("~/") });

            return Redirect(returnUrl);
        }
    }
}