using System;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class AuthenticationQueryService : UnityServiceBase, IAuthenticationQueryService
    {
        public AuthenticationTokenView RetrieveAuthenticationTokenDetail(Guid id)
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                var token = repository.RetrieveAuthenticationTokenDetail(id);

                return token;
            });
        }

        public AuthenticationTokenView RetrieveAuthenticationToken(Guid id)
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                return repository.RetrieveAuthenticationToken(id);
            });
        }

        public bool VerifyOperation(Guid tokenId, string domain, string operation)
        {
            //TODO
            return true;
        }
    }
}