using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.QueryModel.Cam;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    /// <summary>
    /// Interface for the Candidate Management data context
    /// </summary>
    public interface ICamQueryContext : IQuintessenceQueryContext
    {
        DbQuery<CandidateView> Candidates { get; }
        DbQuery<ProgramComponentView> ProgramComponents { get; }
        DbQuery<ProgramComponentSpecialView> ProgramComponentSpecials { get; }
    }
}
