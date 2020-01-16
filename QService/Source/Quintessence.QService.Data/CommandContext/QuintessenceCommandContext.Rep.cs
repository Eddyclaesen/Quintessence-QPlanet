using System.Data.Entity;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Rep;

namespace Quintessence.QService.Data.CommandContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceCommandContext : IRepCommandContext
    {
        public IDbSet<CandidateReportDefinition> CandidateReportDefinitions { get; set; }
        public IDbSet<CandidateReportDefinitionField> CandidateReportDefinitionFields { get; set; }
        public IDbSet<ReportDefinition> ReportDefinitions { get; set; }
        public IDbSet<ReportParameter> ReportParameters { get; set; }
        public IDbSet<ReportParameterValue> ReportParameterValues { get; set; }
    }
}
