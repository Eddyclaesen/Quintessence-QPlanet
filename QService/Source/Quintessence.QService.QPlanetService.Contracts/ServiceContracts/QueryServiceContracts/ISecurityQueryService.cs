using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface ISecurityQueryService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        SearchUserResponse SearchUser(SearchUserRequest searchUserRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        UserView RetrieveUserDetail(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        UserView RetrieveUserByCrmAssociateId(int? associateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        UserView RetrieveUser(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<UserView> ListUsers();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<RoleView> ListRoles();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<UserView> ListCustomerAssistants();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        RoleView RetrieveRole(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<UserView> ListUsersInRole(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<RoleOperationView> ListOperationsInRole(Guid id);
    }
}
