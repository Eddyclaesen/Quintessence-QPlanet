using System.Data.Entity.Infrastructure;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    /// <summary>
    /// Interface for the Security Management data context
    /// </summary>
    public interface ISecQueryContext : IQuintessenceQueryContext
    {
        DbQuery<UserView> Users { get; }
        DbQuery<RoleView> Roles { get; }
        DbQuery<AuthenticationTokenView> AuthenticationTokens { get; }
        DbQuery<RoleOperationView> RoleOperations { get; }
    }
}
