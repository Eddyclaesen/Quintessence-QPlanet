using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Cam;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : ICamQueryContext
    {
        public DbQuery<CandidateView> Candidates { get { return Set<CandidateView>().AsNoTracking(); } }
        public DbQuery<ProgramComponentView> ProgramComponents { get { return Set<ProgramComponentView>().AsNoTracking(); } }
        public DbQuery<ProgramComponentSpecialView> ProgramComponentSpecials { get { return Set<ProgramComponentSpecialView>().AsNoTracking(); } }
    }
}
