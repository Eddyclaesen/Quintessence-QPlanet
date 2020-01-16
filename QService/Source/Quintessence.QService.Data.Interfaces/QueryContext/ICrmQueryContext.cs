using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    /// <summary>
    /// Interface for the Dictionairy Management data context
    /// </summary>
    public interface ICrmQueryContext : IQuintessenceQueryContext
    {
        DbQuery<CrmContactView> Contacts { get; }
        DbQuery<CrmAssociateView> Associates { get; }
        DbQuery<CrmActiveProjectView> CrmActiveProjects { get; }
        DbQuery<CrmProjectView> CrmProjects { get; }
        DbQuery<ContactDetailView> ContactDetails { get; }
        DbQuery<CrmTimesheetUnregisteredEntryView> TimesheetUnregisteredEntries { get; }
        DbQuery<CrmAppointmentView> CrmAppointments { get; }
        DbQuery<CrmEmailView> CrmEmails { get; }
        DbQuery<CrmUnsynchronizedEmployeeView> CrmUnsynchronizedEmployees { get; }
        IEnumerable<CrmUnregisteredCandidateAppointmentView> ListUnregisteredCrmCandidateAppointments(Guid projectId);
        IEnumerable<CrmAssessorAppointmentView> ListCoAssessorAppointments(Guid projectId);
        CrmFormattedAppointmentView RetrieveFormattedCrmAppointment(int appointmentId);
        IEnumerable<CrmAppointmentTrainingView> ListProjectTrainingAppointments(Guid projectId);
    }
}
