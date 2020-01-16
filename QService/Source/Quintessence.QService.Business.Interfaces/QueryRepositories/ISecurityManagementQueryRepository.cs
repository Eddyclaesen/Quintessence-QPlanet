using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface ISecurityManagementQueryRepository : IQueryRepository
    {
        AuthenticationTokenView RetrieveAuthenticationTokenDetail(Guid id);
        List<UserView> SearchUsers(string name = null);
        UserView RetrieveUserDetail(Guid id);
        AuthenticationTokenView RetrieveAuthenticationToken(Guid id);
        UserView RetrieveUserByCrmAssociateId(int? associateId);
        UserView RetrieveUser(Guid id);
        List<UserView> ListCustomerAssistants();
        RoleView RetrieveRole(Guid id);
        List<UserView> ListUsersInRole(Guid id);
        List<RoleOperationView> ListOperationsInRole(Guid id);
    }
}
