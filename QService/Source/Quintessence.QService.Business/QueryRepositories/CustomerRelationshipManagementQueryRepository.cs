using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Base;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class CustomerRelationshipManagementQueryRepository : QueryRepositoryBase<ICrmQueryContext>, ICustomerRelationshipManagementQueryRepository
    {
        public CustomerRelationshipManagementQueryRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public List<CrmContactView> ListContacts()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var contacts = context.Contacts.ToList();

                        return contacts;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public CrmAssociateView RetrieveAssociate(string username)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        throw new NotImplementedException();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<CrmContactView> SearchContacts(string customerName = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.Contacts
                            .Where(c => (c.Name + " " + c.Department).Contains(customerName))
                            .ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<CrmAssociateView> ListProjectManagerAssociates()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.Associates.ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<CrmActiveProjectView> ListActiveProjects(int contactId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.CrmActiveProjects
                            .Where(p => p.ContactId == contactId && (p.ProjectStatusId == 2 || p.ProjectStatusId == 3))
                            .ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public CrmContactView RetrieveContactDetailInformation(int id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    CrmContactView contact;
                    using (var context = CreateContext())
                    {
                        contact = context.Contacts
                            .Include(c => c.CustomerAssistant)
                            .Include(c => c.AccountManager)
                            .Single(c => c.Id == id);
                    }

                    var securityRepository = Container.Resolve<ISecurityManagementQueryRepository>();

                    if (contact.CustomerAssistantId.HasValue && contact.CustomerAssistant != null)
                        contact.CustomerAssistant.User = securityRepository.RetrieveUserByCrmAssociateId(contact.CustomerAssistantId);

                    if (contact.AccountManagerId.HasValue && contact.AccountManager != null)
                        contact.AccountManager.User = securityRepository.RetrieveUserByCrmAssociateId(contact.AccountManagerId);

                    return contact;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ContactDetailView RetrieveContactDetail(int id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var contact = context.ContactDetails
                            .SingleOrDefault(c => c.ContactId == id);

                        return contact;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ContactDetailView RetrieveContactDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var contact = context.ContactDetails
                            .SingleOrDefault(c => c.Id == id);

                        return contact;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<CrmTimesheetUnregisteredEntryView> ListTimesheetUnregisteredEntries(Guid? projectId, Guid userId, bool isProjectManager, DateTime firstDay, DateTime lastDay)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var timeAndMaterialPricingModelId = (int) PricingModelType.TimeAndMaterial;
                        var entries = context.TimesheetUnregisteredEntries
                            .Include(e => e.Project)
                            .Include(e => e.Project.Contact)
                            .Include(e => e.User)
                            .Where(e => (projectId == null || e.ProjectId == projectId)
                                        && e.Project.PricingModelId == timeAndMaterialPricingModelId
                                        && (userId == null || (e.UserId == userId && !isProjectManager) || (e.Project.ProjectManagerId == userId && isProjectManager)))
                            .ToList();

                        entries = entries.Where(e => e.StartDate >= firstDay && e.EndDate <= lastDay && e.Project is ConsultancyProjectView).ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<CrmUnregisteredCandidateAppointmentView> ListUnregisteredCandidateAppointments(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListUnregisteredCrmCandidateAppointments(projectId)
                            .Where(ucca => ucca.AssessorType == "LA");

                        return entries.ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }   
        }

        public List<CrmAssessorAppointmentView> ListCoAssessors(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListCoAssessorAppointments(projectId);

                        return entries.ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }   
        }

        public CrmAppointmentView RetrieveCrmAppointment(int id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var appointment = context.CrmAppointments.FirstOrDefault(a => a.Id == id);

                        return appointment;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }   
        }

        public List<CrmEmailView> ListCrmEmails()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var crmEmails = context.CrmEmails.ToList();

                        return crmEmails;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }   
        }

        public List<CrmEmailView> ListCrmEmailsByContactId(int contactId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var crmEmails = context.CrmEmails
                            .Where(e => e.ContactId == contactId)
                            .ToList();

                        return crmEmails;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }   
        }

        public CrmFormattedAppointmentView RetrieveFormattedCrmAppointment(int crmCandidateAppointmentId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var formattedCrmAppointment = context.RetrieveFormattedCrmAppointment(crmCandidateAppointmentId);

                        return formattedCrmAppointment;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }   
        }

        public CrmProjectView RetrieveCrmProject(int id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var crmProject = context.CrmProjects.OrderBy(p => p.Id).FirstOrDefault(p => p.Id == id);

                        return crmProject;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }  
        }

        public List<CrmAppointmentTrainingView> ListProjectTrainingAppointments(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var trainingAppointments = context.ListProjectTrainingAppointments(projectId).ToList();

                        return trainingAppointments;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }  
        }

        public List<CrmUnsynchronizedEmployeeView> ListUnsynchronizedEmployees()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var unsynchronizedEmployees = context.CrmUnsynchronizedEmployees.ToList();

                        return unsynchronizedEmployees;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }  
        }

        public CrmEmailView RetrieveCrmEmail(int id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var crmEmail = context.CrmEmails.FirstOrDefault(e => e.Id == id);

                        return crmEmail;
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
