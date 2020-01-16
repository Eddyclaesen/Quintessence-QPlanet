using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface IReportManagementQueryService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        GenerateReportResponse GenerateReport(GenerateReportRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CandidateReportDefinitionView> ListCandidateReportDefinitions();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        CandidateReportDefinitionView RetrieveCandidateReportDefinition(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        CandidateReportDefinitionFieldView RetrieveCandidateReportDefinitionField(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CandidateReportDefinitionView> ListCandidateReportDefinitionsForCustomer(int contactId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CandidateScoreReportTypeView> ListCandidateScoreReportTypes();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ReportTypeView> ListReportTypes();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ReportDefinitionView> ListReportDefinitions(ListReportDefinitionsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ReportDefinitionView RetrieveReportDefinition(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ReportParameterView> ListReportParameters();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ReportParameterView RetrieveReportParameter(Guid id);
    }
}
