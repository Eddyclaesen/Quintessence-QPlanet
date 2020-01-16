using System;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Authentication;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class AuthenticationCommandService : UnityServiceBase, IAuthenticationCommandService
    {
        public NegociateAuthenticationTokenResponse NegociateAuthenticationToken(string username, string password)
        {
            LogManager.LogTrace("User {0} negociates new authentication token.", username);

            return ExecuteTransaction(() =>
                {
                    var repository = Container.Resolve<ISecurityManagementCommandRepository>();

                    var tokenId = repository.NegociateAuthenticationToken(username, password);

                    var response = new NegociateAuthenticationTokenResponse();
                    response.AuthenticationTokenId = tokenId;

                    return response;
                });
        }

        public void ChangePassword(ChangePasswordRequest request)
        {
            LogManager.LogTrace("Change password for current user.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISecurityManagementCommandRepository>();

                var user = repository.RetrieveUser(request.UserName, request.CurrentPassword);

                repository.ChangePassword(user.Id, request.CurrentPassword, request.NewPassword, request.ConfirmPassword);
            });
        }
    }
}
