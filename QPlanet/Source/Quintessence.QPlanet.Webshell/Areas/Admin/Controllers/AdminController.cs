using Quintessence.QPlanet.Webshell.Infrastructure;
using Quintessence.QPlanet.Webshell.Infrastructure.ActionFilters;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Controllers
{
    [QPlanetAuthenticateController("ADMIN")]
    public abstract class AdminController : QPlanetControllerBase
    {
    }
}