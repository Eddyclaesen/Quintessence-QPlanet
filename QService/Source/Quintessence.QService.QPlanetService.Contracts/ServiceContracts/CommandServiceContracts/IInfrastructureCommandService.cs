using System;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface IInfrastructureCommandService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateMailTemplate(CreateMailTemplateRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateMailTemplateTranslation(CreateMailTemplateTranslationRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateMailTemplateTranslation(UpdateMailTemplateTranslationRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateMailTemplate(UpdateMailTemplateRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void ScheduleJob(Guid jobDefinitionId);
    }
}