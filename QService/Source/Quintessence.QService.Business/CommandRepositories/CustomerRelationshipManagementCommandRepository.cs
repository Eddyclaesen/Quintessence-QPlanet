using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Base;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Extensions;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Crm;

namespace Quintessence.QService.Business.CommandRepositories
{
    public class CustomerRelationshipManagementCommandRepository : CommandRepositoryBase<ICrmCommandContext>, ICustomerRelationshipManagementCommandRepository
    {
        public CustomerRelationshipManagementCommandRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public ContactDetail PrepareContactDetail(int contactId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var contactDetail = context.Create<ContactDetail>();
                        contactDetail.ContactId = contactId;
                        return contactDetail;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void Save(ContactDetail contactDetail)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        context.CreateOrUpdate(c => c.ContactDetails, contactDetail);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ContactDetail RetrieveContactDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ContactDetails.SingleOrDefault(cd => cd.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public int CreateCandidateInfo(string firstName, string lastName)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.CreateCandidateInfo(firstName, lastName);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }   
        }
    }
}
