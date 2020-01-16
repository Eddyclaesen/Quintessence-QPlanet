using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Wsm;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : IWsmQueryContext
    {
        public DbQuery<UserProfileView> UserProfiles { get { return Set<UserProfileView>().AsNoTracking(); } }
    }
}
