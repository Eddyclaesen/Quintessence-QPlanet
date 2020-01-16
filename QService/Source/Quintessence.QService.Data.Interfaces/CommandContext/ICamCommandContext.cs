using System.Data.Entity;
using Quintessence.QService.DataModel.Cam;
using Quintessence.QService.DataModel.Prm;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    /// <summary>
    /// Interface for the Candidate Management data context
    /// </summary>
    public interface ICamCommandContext : IQuintessenceCommandContext
    {
        IDbSet<Candidate> Candidates { get; set; }
        IDbSet<ProgramComponent> ProgramComponents { get; set; }
        IDbSet<ProgramComponentSpecial> ProgramComponentSpecials { get; set; }
    }
}
