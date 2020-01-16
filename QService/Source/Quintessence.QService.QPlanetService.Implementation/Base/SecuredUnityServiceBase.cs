using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Core.Security;
using Quintessence.QService.Core.Testing;
using Quintessence.QService.QPlanetService.Contracts.SecurityContracts;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QService.QPlanetService.Implementation.Base
{
    public abstract class SecuredUnityServiceBase : UnityServiceBase
    {
        protected SecuredUnityServiceBase()
        {
            var service = Container.Resolve<IAuthenticationQueryService>();
            var token = service.RetrieveAuthenticationTokenDetail(RetrieveAuthenticationToken());
            Container.RegisterInstance(token);
            Container.RegisterInstance(ConvertToSecurityContext(token));
        }

        private SecurityContext ConvertToSecurityContext(AuthenticationTokenView token)
        {
            return new SecurityContext
                {
                    TokenId = token.Id,
                    UserId = token.UserId,
                    UserName = @"QPlanet\" + token.User.UserName,
                };
        }

        protected virtual Guid RetrieveAuthenticationToken()
        {
            //When not in a wcf context
            if (OperationContext.Current == null)
                return StaticAuthenticationToken.TokenId;

            var authenticationTokenId = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("authenticationTokenId", "security");
            return new Guid(authenticationTokenId);
        }

        protected void ValidateAuthorization(OperationType operationType)
        {
            try
            {
                var token = Container.Resolve<AuthenticationTokenView>();

                if (token == null)
                {
                    ValidationContainer.RegisterAuthenticationFaultEntry("Unable to retrieve a valid token.");
                    return;
                }

                if (token.ValidTo < DateTime.Now)
                {
                    ValidationContainer.RegisterAuthenticationFaultEntry(string.Format("Authentication code for User '{0}' (username: '{1}') is expired.", token.User.FullName, token.User.UserName));
                    return;
                }

                //if (token.AuthorizedOperations.All(a => a.Code != operationType.ToString()))
                //{
                //    var service = Container.Resolve<IAuthenticationQueryService>();
                //    var operation = service.RetrieveOperation(operationType.ToString());
                //
                //    ValidationContainer.RegisterAuthenticationFaultEntry(
                //        operation == null
                //            ? string.Format("Operation with code '{0}' was not found.", operationType)
                //            : string.Format("User '{0}' (username: '{1}') is not authorized for operation '{2}' (code: '{3}')", token.User.FullName, token.User.UserName, operation.Name, operationType));
                //
                //    return;
                //}
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                ValidationContainer.RegisterException(exception);
            }

            ValidationContainer.Validate();
        }

        protected void LogTrace(string message = null, params object[] args)
        {
            var token = Container.Resolve<AuthenticationTokenView>();

            if (token == null)
            {
                ValidationContainer.RegisterAuthenticationFaultEntry("Unable to retrieve a valid token.");
                return;
            }

            if (token.ValidTo < DateTime.Now)
            {
                ValidationContainer.RegisterAuthenticationFaultEntry(string.Format("Authentication code for User '{0}' (username: '{1}') is expired.", token.User.FullName, token.User.UserName));
                return;
            }

            if (message == null)
            {
                var stack = new StackTrace();
                var previousFrame = stack.GetFrame(1);
                var method = previousFrame.GetMethod();

                message = string.Format("{0}({1})", method.Name, string.Join(", ", method.GetParameters().Select(p => p.Name)));
            }

            LogManager.LogTrace("{0};{1}", token.User.UserName, string.Format(message, args));
        }
    }
}
