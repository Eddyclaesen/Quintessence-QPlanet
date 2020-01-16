using System.Data.Entity;
using Quintessence.QService.DataModel.Wsm;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    public interface IWsmCommandContext : IQuintessenceCommandContext
    {
        IDbSet<UserProfile> UserProfiles { get; set; }
    }
}