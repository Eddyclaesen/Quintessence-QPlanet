using System.Data.Entity;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Sec;

namespace Quintessence.QService.Data.CommandContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceCommandContext : ISecCommandContext
    {
        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<AuthenticationToken> AuthenticationTokens { get; set; }
        public IDbSet<Employee> Employees { get; set; }
    }
}
