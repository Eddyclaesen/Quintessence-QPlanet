using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.WorkspaceManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface IWorkspaceManagementCommandService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateUserProfile(UpdateUserProfileRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CreateNewUserProfile(CreateNewUserProfileRequest request);
    }
}