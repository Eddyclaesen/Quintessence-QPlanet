using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface ICustomerRelationshipManagementQueryRepository : IQueryRepository
    {
        List<CrmContactView> ListContacts();
        CrmAssociateView RetrieveAssociate(string username);
        List<CrmContactView> SearchContacts(string customerName = null);
        List<CrmAssociateView> ListProjectManagerAssociates();
        List<CrmActiveProjectView> ListActiveProjects(int contactId);
        CrmContactView RetrieveContactDetailInformation(int id);
        ContactDetailView RetrieveContactDetail(int id);
        ContactDetailView RetrieveContactDetail(Guid id);
        List<CrmTimesheetUnregisteredEntryView> ListTimesheetUnregisteredEntries(Guid? projectId, Guid userId, bool isProjectManager, DateTime firstDay, DateTime lastDay);
        List<CrmUnregisteredCandidateAppointmentView> ListUnregisteredCandidateAppointments(Guid projectId);
        List<CrmAssessorAppointmentView> ListCoAssessors(Guid projectId);
        CrmAppointmentView RetrieveCrmAppointment(int id);
        List<CrmEmailView> ListCrmEmails();
        List<CrmEmailView> ListCrmEmailsByContactId(int contactId);
        CrmFormattedAppointmentView RetrieveFormattedCrmAppointment(int crmCandidateAppointmentId);
        CrmProjectView RetrieveCrmProject(int id);
        List<CrmAppointmentTrainingView> ListProjectTrainingAppointments(Guid projectId);
        List<CrmUnsynchronizedEmployeeView> ListUnsynchronizedEmployees();
        CrmEmailView RetrieveCrmEmail(int id);
    }
}
