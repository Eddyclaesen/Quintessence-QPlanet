using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using Quintessence.QCandidate.Configuration;

namespace Quintessence.QCandidate.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AzureAdB2CSettings _adB2CSettings;

        public AccountsController(IOptionsMonitor<AzureAdB2CSettings> adB2CSettings)
        {
            _adB2CSettings = adB2CSettings.CurrentValue;
        }

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

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var scheme = AzureADB2CDefaults.AuthenticationScheme;
            var authenticated = await HttpContext.AuthenticateAsync(scheme);
            
            if(!authenticated.Succeeded)
            {
                return Challenge(scheme);
            }

            await HttpContext.SignOutAsync(AzureADB2CDefaults.AuthenticationScheme);

            //Specify the protocol, so an absolute url is generated, which is needed by azure ad b2c
            var rootUrl = HttpUtility.UrlEncode(Url.Action("Get", "Assessments", null, Request.Scheme));
            var redirectUrl = $"{_adB2CSettings.Tenant}{_adB2CSettings.Domain}/oauth2/v2.0/logout?p={_adB2CSettings.SignUpSignInPolicyId}&post_logout_redirect_uri={rootUrl}";
            
            return Redirect(redirectUrl);
        }
    }
}