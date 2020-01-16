using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class SecurityQueryService : SecuredUnityServiceBase, ISecurityQueryService
    {
        public SearchUserResponse SearchUser(SearchUserRequest searchUserRequest)
        {
            return Execute(() =>
                {
                    var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                    var users = repository.SearchUsers(name: searchUserRequest.Name);

                    return new SearchUserResponse { Users = users };
                });
        }

        public UserView RetrieveUserDetail(Guid id)
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                return repository.RetrieveUserDetail(id);
            });
        }

        public UserView RetrieveUserByCrmAssociateId(int? associateId)
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                return repository.RetrieveUserByCrmAssociateId(associateId);
            });
        }

        public UserView RetrieveUser(Guid id)
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                return repository.RetrieveUser(id);
            });
        }

        public List<UserView> ListUsers()
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                return repository.List<UserView>();
            });
        }

        public List<RoleView> ListRoles()
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                return repository.List<RoleView>();
            });
        }

        public RoleView RetrieveRole(Guid id)
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                return repository.RetrieveRole(id);
            });
        }

        public List<UserView> ListUsersInRole(Guid id)
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                return repository.ListUsersInRole(id);
            });
        }

        public List<RoleOperationView> ListOperationsInRole(Guid id)
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                return repository.ListOperationsInRole(id);
            });
        }

        public List<UserView> ListCustomerAssistants()
        {
            return Execute(() =>
            {
                var repository = Container.Resolve<ISecurityManagementQueryRepository>();

                return repository.ListCustomerAssistants();
            });
        }
    }
}
