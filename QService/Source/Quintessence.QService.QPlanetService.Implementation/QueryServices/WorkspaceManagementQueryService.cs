using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.WorkspaceManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Wsm;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class WorkspaceManagementQueryService : SecuredUnityServiceBase, IWorkspaceManagementQueryService
    {
        public UserProfileView RetrieveUserProfile(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IWorkspaceManagementQueryRepository>();

                return repository.RetrieveUserProfile(id);
            });
        }

        public List<ProgramComponentView> ListProgramComponents(ListProgramComponentsRequest request)
        {
            LogTrace();

            return Execute(() =>
                {
                    var candidateManagementService = Container.Resolve<ICandidateManagementQueryService>();

                    var programComponents = candidateManagementService.ListProgramComponentsByUser(request.UserId, request.Start, request.End);

                    return programComponents;
                });
        }
    }
}
