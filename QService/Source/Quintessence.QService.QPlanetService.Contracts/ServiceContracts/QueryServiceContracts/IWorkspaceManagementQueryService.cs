using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.WorkspaceManagement;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Wsm;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface IWorkspaceManagementQueryService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        UserProfileView RetrieveUserProfile(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProgramComponentView> ListProgramComponents(ListProgramComponentsRequest request);
    }
}