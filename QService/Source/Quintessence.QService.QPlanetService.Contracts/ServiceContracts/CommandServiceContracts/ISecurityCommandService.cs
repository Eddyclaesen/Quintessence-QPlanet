using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface ISecurityCommandService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void RevokeAuthenticationTokens(Guid userId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteUser(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteUserGroup(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateUser(UpdateUserRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void SynchronizeUsers(List<SynchronizeUserRequest> requests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewUser(CreateNewUserRequest request);
    }
}
