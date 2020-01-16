using System;
using Quintessence.QPlanet.Infrastructure.Services;
using Quintessence.QPlanet.Webshell.Infrastructure;
using Quintessence.QPlanet.Webshell.Infrastructure.ActionFilters;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QPlanet.Webshell.Areas.Dictionary.Controllers
{
    //[QPlanetAuthenticateController("SEC")]
    public abstract class DictionaryController : QPlanetControllerBase
    {
        protected TResult Execute<TResult>(Func<IDictionaryManagementQueryService, TResult> action)
        {
            var serviceInvoker = new ServiceInvoker<IDictionaryManagementQueryService>();

            return serviceInvoker.Execute(ControllerContext.HttpContext.ApplicationInstance.Context, action);
        }
    }
}