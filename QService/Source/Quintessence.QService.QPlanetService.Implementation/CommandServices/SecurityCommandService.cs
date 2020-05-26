using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Security;
using Quintessence.QService.DataModel.Sec;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.WorkspaceManagement;
using Quintessence.QService.QPlanetService.Contracts.SecurityContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class SecurityCommandService : SecuredUnityServiceBase, ISecurityCommandService
    {
        public void RevokeAuthenticationTokens(Guid userId)
        {
            LogTrace("Revoke authentication tokens for id: {0}.", userId);

            ExecuteTransaction(() =>
            {
                //ValidateAuthorization(OperationType.ADMINREVOK);

                var repository = Container.Resolve<ISecurityManagementCommandRepository>();

                repository.RevokeAuthenticationTokens(userId);
            });
        }

        public void DeleteUser(Guid id)
        {
            LogTrace("Delete user (id: {0}).", id);

            ExecuteTransaction(() =>
            {
                //ValidateAuthorization(OperationType.ADMINREVOK);

                var repository = Container.Resolve<ISecurityManagementCommandRepository>();

                repository.Delete<User>(id);
            });
        }

        public void DeleteUserGroup(Guid id)
        {
            LogTrace("Delete usergroupd (id: {0}).", id);

            ExecuteTransaction(() =>
            {
                //ValidateAuthorization(OperationType.ADMINREVOK);

                var repository = Container.Resolve<ISecurityManagementCommandRepository>();

                //repository.Delete<UserGroup>(id);
            });
        }

        public void UpdateUser(UpdateUserRequest request)
        {
            LogTrace("Update user (id: {0}).", request.Id);

            ExecuteTransaction(() =>
            {
                //ValidateAuthorization(OperationType.ADMINREVOK);

                var repository = Container.Resolve<ISecurityManagementCommandRepository>();

                var workspaceManagementQueryService = Container.Resolve<IWorkspaceManagementQueryService>();
                var workspaceManagementCommandService = Container.Resolve<IWorkspaceManagementCommandService>();

                var user = repository.Retrieve<User>(request.Id);

                Mapper.DynamicMap(request, user, typeof(UpdateUserRequest), typeof(User));

                if (ValidationContainer.ValidateObject(user))
                    repository.Save(user);

                if (user.IsEmployee)
                {
                    var employee = repository.RetrieveEmployee(user.Id) ?? repository.PrepareEmployee(user.Id);

                    var updateEmployeeRequest = request as UpdateEmployeeRequest;
                    if (updateEmployeeRequest != null)
                        Mapper.DynamicMap(updateEmployeeRequest, employee);

                    repository.SaveEmployee(employee);
                }
                else
                {
                    var employee = repository.RetrieveEmployee(user.Id);

                    if (employee != null)
                        repository.DeleteEmployee(employee.Id);
                }

                var userProfile = workspaceManagementQueryService.RetrieveUserProfile(user.Id);
                Mapper.DynamicMap(request, userProfile);
                var updateUserProfileRequest = Mapper.DynamicMap<UpdateUserProfileRequest>(userProfile);
                workspaceManagementCommandService.UpdateUserProfile(updateUserProfileRequest);

                if (!string.IsNullOrWhiteSpace(request.ResetPassword))
                {
                    if (request.Id == Container.Resolve<SecurityContext>().UserId)
                        ValidationContainer.RegisterEntityValidationFaultEntry("Unable to change your own password via this way. You can change your password through 'Workspace'.");

                    repository.ResetPassword(request.Id, request.ResetPassword);
                }
            });
        }

        public void SynchronizeUsers(List<SynchronizeUserRequest> requests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISecurityManagementCommandRepository>();

                var workspaceManagementCommandService = Container.Resolve<IWorkspaceManagementCommandService>();

                foreach (var request in requests)
                {
                    var user = repository.PrepareUser(request.UserName);
                    user.AssociateId = request.Id;
                    user.Password = request.Password;
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;

                    if (ValidationContainer.ValidateObject(user))
                        repository.Save(user);

                    var userProfileRequest = new CreateNewUserProfileRequest
                        {
                            UserId = user.Id,
                            LanguageId = 1
                        };
                    workspaceManagementCommandService.CreateNewUserProfile(userProfileRequest);

                    repository.ResetPassword(user.Id, request.Password);
                }
            });
        }

        public Guid CreateNewUser(CreateNewUserRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISecurityManagementCommandRepository>();

                var workspaceManagementCommandService = Container.Resolve<IWorkspaceManagementCommandService>();

                var user = repository.PrepareUser(request.UserName);
                user.AssociateId = -1;
                user.Password = request.Password;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;

                if(ValidationContainer.ValidateObject(user))
                {
                    repository.Save(user);
                }

                var userProfileRequest = new CreateNewUserProfileRequest
                {
                    UserId = user.Id,
                    LanguageId = request.LanguageId
                };
                workspaceManagementCommandService.CreateNewUserProfile(userProfileRequest);

                repository.ResetPassword(user.Id, request.Password);

                return user.Id;
            });
        }
    }
}
