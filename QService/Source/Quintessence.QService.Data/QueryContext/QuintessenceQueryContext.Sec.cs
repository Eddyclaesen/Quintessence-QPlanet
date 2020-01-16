using System.Data.Entity.Infrastructure;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : ISecQueryContext
    {
        public DbQuery<UserView> Users { get { return Set<UserView>().AsNoTracking(); } }
        public DbQuery<RoleView> Roles { get { return Set<RoleView>().AsNoTracking(); } }
        public DbQuery<AuthenticationTokenView> AuthenticationTokens { get { return Set<AuthenticationTokenView>().AsNoTracking(); } }
        public DbQuery<RoleOperationView> RoleOperations { get { return Set<RoleOperationView>().AsNoTracking(); } }
    }
}
