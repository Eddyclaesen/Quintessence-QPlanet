using System;
using Quintessence.QService.DataModel.Crm;

namespace Quintessence.QService.Business.Interfaces.CommandRepositories
{
    public interface ICustomerRelationshipManagementCommandRepository : ICommandRepository
    {
        ContactDetail PrepareContactDetail(int contactId);
        void Save(ContactDetail contactDetail);
        ContactDetail RetrieveContactDetail(Guid id);
        int CreateCandidateInfo(string firstName, string lastName);
    }
}
