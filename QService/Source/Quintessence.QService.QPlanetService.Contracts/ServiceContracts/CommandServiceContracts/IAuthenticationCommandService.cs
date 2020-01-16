using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Authentication;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface IAuthenticationCommandService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        NegociateAuthenticationTokenResponse NegociateAuthenticationToken(string username, string password);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void ChangePassword(ChangePasswordRequest request);
    }
}
