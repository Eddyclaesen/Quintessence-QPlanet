using System.Linq;
using System.Web.Mvc;
using Quintessence.Infrastructure.Validation;

namespace Quintessence.QPlanet.Webshell.Infrastructure.ActionFilters
{
    public class QPlanetHandleTempdataErrorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var tempData = filterContext.Controller.TempData;
            var modelState = ((Controller)filterContext.Controller).ModelState;

            var errorKey = typeof(AuthenticationFaultEntry).Name;

            if (tempData.ContainsKey(errorKey))
            {
                modelState.AddModelError(string.Empty, ((AuthenticationFaultEntry)tempData[errorKey]).Message);
            }

            errorKey = typeof(FaultDetail).Name;

            if (tempData.ContainsKey(errorKey))
            {
                var faultDetail = (FaultDetail)tempData[errorKey];

                modelState.AddModelError(string.Empty, faultDetail.FaultEntries.Last().Message);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}