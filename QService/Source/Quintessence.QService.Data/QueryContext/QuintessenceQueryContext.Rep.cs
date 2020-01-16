using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : IRepQueryContext
    {
        public DbQuery<CandidateReportDefinitionView> CandidateReportDefinitions { get { return Set<CandidateReportDefinitionView>().AsNoTracking(); } }
        public DbQuery<CandidateReportDefinitionFieldView> CandidateReportDefinitionFields { get { return Set<CandidateReportDefinitionFieldView>().AsNoTracking(); } }
        public DbQuery<CandidateScoreReportTypeView> CandidateScoreReportTypes { get { return Set<CandidateScoreReportTypeView>().AsNoTracking(); } }
        public DbQuery<ReportTypeView> ReportTypes { get { return Set<ReportTypeView>().AsNoTracking(); } }
        public DbQuery<ReportDefinitionView> ReportDefinitions { get { return Set<ReportDefinitionView>().AsNoTracking(); } }
        public DbQuery<ReportParameterView> ReportParameters { get { return Set<ReportParameterView>().AsNoTracking(); } }
    }
}
