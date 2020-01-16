using System.Data.Entity;
using Quintessence.QService.DataModel.Sec;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    /// <summary>
    /// Interface for the Security Management data context
    /// </summary>
    public interface ISecCommandContext : IQuintessenceCommandContext
    {
        IDbSet<User> Users { get; set; }
        IDbSet<Role> Roles { get; set; }
        IDbSet<AuthenticationToken> AuthenticationTokens { get; set; }
        IDbSet<Employee> Employees { get; set; }
    }
}
