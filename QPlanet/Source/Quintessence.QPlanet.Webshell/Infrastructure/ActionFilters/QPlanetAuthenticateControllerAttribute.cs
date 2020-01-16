using System;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Infrastructure.Security;
using Quintessence.QPlanet.Infrastructure.Services;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QPlanet.Webshell.Infrastructure.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class QPlanetAuthenticateControllerAttribute : AuthorizeAttribute
    {
        private readonly string _roleCode;

        public QPlanetAuthenticateControllerAttribute(string roleCode = null)
        {
            _roleCode = roleCode;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                var identity = IdentityHelper.RetrieveIdentity(filterContext.HttpContext.ApplicationInstance.Context);

                if (identity == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult("Unable to retrieve identity information");
                    return;
                }

                AuthenticationTokenView token = null;

                var serviceInvoker = new ServiceInvoker<IAuthenticationQueryService>();
                token = serviceInvoker.Execute(filterContext.HttpContext.ApplicationInstance.Context,
                                               service =>
                                               service.RetrieveAuthenticationTokenDetail(
                                                   new Guid(identity.Ticket.UserData)));

                if (token == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult("Unable to retrieve authentication token");
                    return;
                }

                if (_roleCode != null && token.User.RoleCode != _roleCode)
                {
                    filterContext.Result = new HttpUnauthorizedResult(string.Format("You have not enough rights for this domain (Required role code: '{0}')", _roleCode));
                    return;
                }
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                filterContext.Result = new HttpUnauthorizedResult("Unable to retrieve authentication token");
            }
        }
    }
}