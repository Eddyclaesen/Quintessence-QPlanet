using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface IReportManagementQueryRepository : IQueryRepository
    {
        List<CandidateReportDefinitionView> ListCandidateReportDefinitions();
        List<CandidateReportDefinitionView> ListCandidateReportDefinitionsForCustomer(int contactId);
        List<CandidateScoreReportTypeView> ListCandidateScoreReportTypes();
        CandidateReportDefinitionView RetrieveCandidateReportDefinition(Guid id);
        List<ReportTypeView> ListReportTypes();
        List<ReportDefinitionView> ListReportDefinitionsByReportType(int reportTypeId);
        List<ReportDefinitionView> ListReportDefinitions();
        ReportParameterView RetrieveReportParameter(Guid id);
    }
}