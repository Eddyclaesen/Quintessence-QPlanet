using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.DataModel.Wsm;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.WorkspaceManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class WorkspaceManagementCommandService : SecuredUnityServiceBase, IWorkspaceManagementCommandService
    {
        public void UpdateUserProfile(UpdateUserProfileRequest request)
        {
            LogTrace("Update user profile (id: {0}).", request.Id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IWorkspaceManagementCommandRepository>();

                var userProfile = repository.Retrieve<UserProfile>(request.Id);

                Mapper.DynamicMap(request, userProfile);

                repository.Save(userProfile);
            });
        }

        public void CreateNewUserProfile(CreateNewUserProfileRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IWorkspaceManagementCommandRepository>();

                var userProfile = repository.Prepare<UserProfile>();

                userProfile.Id = request.UserId;
                userProfile.UserId = request.UserId;
                userProfile.LanguageId = request.LanguageId;

                repository.Save(userProfile);
            });
        }
    }
}