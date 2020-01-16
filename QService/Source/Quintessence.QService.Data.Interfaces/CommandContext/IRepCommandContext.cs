using System.Data.Entity;
using Quintessence.QService.DataModel.Rep;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    public interface IRepCommandContext : IQuintessenceCommandContext
    {
        IDbSet<CandidateReportDefinition> CandidateReportDefinitions { get; set; }
        IDbSet<CandidateReportDefinitionField> CandidateReportDefinitionFields { get; set; }
        IDbSet<ReportDefinition> ReportDefinitions { get; set; }
        IDbSet<ReportParameter> ReportParameters { get; set; }
        IDbSet<ReportParameterValue> ReportParameterValues { get; set; }
    }
}
