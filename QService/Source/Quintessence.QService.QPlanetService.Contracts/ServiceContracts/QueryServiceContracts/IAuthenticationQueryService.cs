using System;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface IAuthenticationQueryService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AuthenticationTokenView RetrieveAuthenticationTokenDetail(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AuthenticationTokenView RetrieveAuthenticationToken(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        bool VerifyOperation(Guid tokenId, string domain, string operation);
    }
}
