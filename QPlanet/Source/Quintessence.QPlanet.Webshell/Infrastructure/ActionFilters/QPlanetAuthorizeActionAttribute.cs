using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Security;
using Quintessence.QPlanet.Infrastructure.Services;
using Quintessence.QService.QPlanetService.Contracts.SecurityContracts;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QPlanet.Webshell.Infrastructure.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class QPlanetAuthorizeActionAttribute : AuthorizeAttribute
    {
        private readonly OperationType _operation;

        public QPlanetAuthorizeActionAttribute(OperationType operation)
        {
            _operation = operation;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var identity = IdentityHelper.RetrieveIdentity(filterContext.HttpContext.ApplicationInstance.Context);

            if (identity == null)
            {
                filterContext.Result = new HttpUnauthorizedResult("Unable to retrieve identity information");
                return;
            }

            var token = System.Web.HttpContext.Current.Items["AuthenticationToken"] as AuthenticationTokenView;

            if (token == null)
            {
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

                System.Web.HttpContext.Current.Items["AuthenticationToken"] = token;
            }

            //if (token.AuthorizedOperations.All(a => a.Code != _operation.ToString()))
            //{
            //    filterContext.Result = new HttpUnauthorizedResult(string.Format("You have not enough rights for this operation ('{0}')", _operation));
            //}
        }
    }
}