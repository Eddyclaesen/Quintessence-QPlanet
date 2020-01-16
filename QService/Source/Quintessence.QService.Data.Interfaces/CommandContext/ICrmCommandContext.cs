using System;
using System.Data.Entity;
using Quintessence.QService.DataModel.Crm;
using Quintessence.QService.QueryModel.Crm;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    /// <summary>
    /// Interface for the Dictionairy Management data context
    /// </summary>
    public interface ICrmCommandContext : IQuintessenceCommandContext
    {
        IDbSet<ContactDetail> ContactDetails { get; set; }
        int CreateCandidateInfo(string firstName, string lastName);
    }
}
