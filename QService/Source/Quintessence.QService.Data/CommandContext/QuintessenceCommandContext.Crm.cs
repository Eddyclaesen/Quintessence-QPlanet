using System;
using System.Data.Entity;
using System.Linq;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Crm;
using Quintessence.QService.QueryModel.Crm;

namespace Quintessence.QService.Data.CommandContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceCommandContext : ICrmCommandContext
    {
        public IDbSet<ContactDetail> ContactDetails { get; set; }
        public int CreateCandidateInfo(string firstName, string lastName)
        {
            var query = Database.SqlQuery<int>("CrmCandidateInfo_Create {0}, {1}", firstName, lastName);
            return query.Single();
        }

    }
}
