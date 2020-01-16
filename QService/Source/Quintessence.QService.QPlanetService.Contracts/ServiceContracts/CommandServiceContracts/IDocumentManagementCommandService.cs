using System;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface IDocumentManagementCommandService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        int CreateNewTrainingChecklist();
    }
}