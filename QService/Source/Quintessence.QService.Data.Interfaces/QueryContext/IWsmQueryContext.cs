using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.QueryModel.Wsm;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    public interface IWsmQueryContext : IQuintessenceQueryContext
    {
        DbQuery<UserProfileView> UserProfiles { get; }
    }
}