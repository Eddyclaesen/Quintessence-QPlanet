using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Security;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Sof;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class CustomerRelationshipManagementQueryService : SecuredUnityServiceBase, ICustomerRelationshipManagementQueryService
    {
        public List<CrmContactView> ListContacts()
        {
            LogTrace("List contacts.");

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                return repository.ListContacts();
            });
        }

        public List<CrmContactView> SearchContacts(SearchContactRequest searchContactRequest)
        {
            LogTrace("Search contacts.");

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                return repository.SearchContacts(searchContactRequest.CustomerName);
            });
        }

        public List<CrmAssociateView> ListProjectManagerAssociates()
        {
            LogTrace("List project manager associates.");

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                return repository.ListProjectManagerAssociates();
            });
        }

        public List<CrmActiveProjectView> ListActiveProjects(int contactId)
        {
            LogTrace("List active projects.");

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                return repository.ListActiveProjects(contactId);
            });
        }

        public List<CrmUnregisteredCandidateAppointmentView> ListCrmUnregisteredCandidateAppointments(Guid projectId)
        {
            LogTrace("List CRM unregistered candidate appointments.");

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                return repository.ListUnregisteredCandidateAppointments(projectId);
            });
        }

        public CrmContactView RetrieveContactDetailInformation(int id)
        {
            LogTrace("Retrieve contact detail information.");

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                return repository.RetrieveContactDetailInformation(id);
            });
        }

        public ContactDetailView RetrieveContactDetail(int id)
        {
            LogTrace("Retrieve contact detail.");

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                var contactDetail = repository.RetrieveContactDetail(id);

                if (contactDetail == null)
                {
                    var commandRepository = Container.Resolve<ICustomerRelationshipManagementCommandService>();
                    var contactDetailId = commandRepository.CreateNewContactDetail(id);
                    contactDetail = repository.RetrieveContactDetail(contactDetailId);
                }

                return contactDetail;
            });
        }

        public List<CrmTimesheetUnregisteredEntryView> ListTimesheetUnregisteredEntries(ListTimesheetUnregisteredEntriesRequest request)
        {
            return Execute(() =>
                {
                    var securityContext = Container.Resolve<SecurityContext>();

                    var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                    if (request.CurrentMonth)
                    {
                        var firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        var lastDay = firstDay.AddMonths(1).AddSeconds(-1);
                        return repository.ListTimesheetUnregisteredEntries(request.ProjectId, request.UserId ?? securityContext.UserId, request.IsProjectManager, firstDay, lastDay);
                    }

                    if (request.MonthDate.HasValue)
                    {
                        var firstDay = new DateTime(request.MonthDate.Value.Year, request.MonthDate.Value.Month, 1);
                        var lastDay = firstDay.AddMonths(1).AddSeconds(-1);
                        return repository.ListTimesheetUnregisteredEntries(request.ProjectId, request.UserId ?? securityContext.UserId, request.IsProjectManager, firstDay, lastDay);
                    }

                    throw new ArgumentOutOfRangeException();
                });
        }

        public void CheckUnregisteredCandidatesForProject(Guid id)
        {
            LogTrace(string.Format("Check remaining candidates to add for project with id '{0}'", id));

            Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                var unregisteredCandidates = repository.ListUnregisteredCandidateAppointments(id);

                if (unregisteredCandidates.Any())
                    ValidationContainer.RegisterValidationFaultEntry("QP1002", "There are still candidates to add for this project.");
            });
        }

        public List<CrmAssessorAppointmentView> ListCoAssessors(Guid projectId)
        {
            LogTrace(string.Format("List co-assessors for project with id '{0}'", projectId));

            return Execute(() =>
                {
                    var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                    var coAssessors = repository.ListCoAssessors(projectId).ToList();

                    return coAssessors;
                });
        }

        public CrmAppointmentView RetrieveCrmAppointment(int id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                var appointment = repository.RetrieveCrmAppointment(id);

                return appointment;
            });
        }

        public List<CrmEmailView> ListCrmEmails()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                var crmEmails = repository.ListCrmEmails();

                return crmEmails;
            });
        }

        public List<CrmEmailView> ListCrmEmailsByContactId(int contactId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                var crmEmails = repository.ListCrmEmailsByContactId(contactId);

                return crmEmails;
            });
        }

        public CrmFormattedAppointmentView RetrieveFormattedCrmAppointment(int crmCandidateAppointmentId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                var formattedCrmAppointment = repository.RetrieveFormattedCrmAppointment(crmCandidateAppointmentId);

                return formattedCrmAppointment;
            });
        }

        public CrmProjectView RetrieveCrmProject(int id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                var crmProject = repository.RetrieveCrmProject(id);

                return crmProject;
            });
        }

        public List<CrmAppointmentTrainingView> ListProjectTrainingAppointments(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                var trainingAppointments = repository.ListProjectTrainingAppointments(projectId);

                return trainingAppointments;
            });
        }

        public List<CrmUnsynchronizedEmployeeView> ListUnsynchronizedEmployees()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                var unsynchronizedEmployees = repository.ListUnsynchronizedEmployees();

                return unsynchronizedEmployees;
            });
        }

        public CrmEmailView RetrieveCrmEmail(int id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ICustomerRelationshipManagementQueryRepository>();

                var crmEmail = repository.RetrieveCrmEmail(id);

                return crmEmail;
            });
        }
    }
}
