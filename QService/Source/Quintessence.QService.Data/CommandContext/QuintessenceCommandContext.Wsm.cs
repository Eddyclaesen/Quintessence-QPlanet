using System.Data.Entity;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Wsm;

namespace Quintessence.QService.Data.CommandContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceCommandContext : IWsmCommandContext
    {
        public IDbSet<UserProfile> UserProfiles { get; set; }
    }
}
