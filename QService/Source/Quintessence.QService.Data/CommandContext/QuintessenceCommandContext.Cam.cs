using System.Data.Entity;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Cam;

namespace Quintessence.QService.Data.CommandContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceCommandContext : ICamCommandContext
    {
        public IDbSet<Candidate> Candidates { get; set; }
        public IDbSet<ProgramComponent> ProgramComponents { get; set; }
        public IDbSet<ProgramComponentSpecial> ProgramComponentSpecials { get; set; }
    }
}
