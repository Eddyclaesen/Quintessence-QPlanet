using System.Data.Entity.Infrastructure;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    public interface IRepQueryContext : IQuintessenceQueryContext
    {
        DbQuery<CandidateReportDefinitionView> CandidateReportDefinitions { get; }
        DbQuery<CandidateReportDefinitionFieldView> CandidateReportDefinitionFields { get; }
        DbQuery<CandidateScoreReportTypeView> CandidateScoreReportTypes { get; }
        DbQuery<ReportTypeView> ReportTypes { get; }
        DbQuery<ReportDefinitionView> ReportDefinitions { get; }
        DbQuery<ReportParameterView> ReportParameters { get; }
    }
}