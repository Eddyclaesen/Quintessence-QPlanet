using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;


namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface IReportManagementCommandService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewCandidateReportDefinition(CreateNewCandidateReportDefinitionRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateCandidateReportDefinition(UpdateCandidateReportDefinitionRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteCandidateReportDefinition(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewCandidateReportDefinitionField(CreateNewCandidateReportDefinitionFieldRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateCandidateReportDefinitionField(UpdateCandidateReportDefinitionFieldRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteCandidateReportDefinitionField(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewReportDefinition(CreateNewReportDefinitionRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateReportDefinition(UpdateReportDefinitionRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteReportDefinition(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateReportDefinitions(List<UpdateReportDefinitionRequest> requests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewReportParameter(CreateNewReportParameterRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteReportParameterValue(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewReportParameterValue(CreateNewReportParameterValueRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateReportParameter(UpdateReportParameterRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteReportParameter(Guid id);
    }
}
