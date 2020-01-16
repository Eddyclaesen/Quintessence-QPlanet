using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : ICrmQueryContext
    {
        public DbQuery<CrmContactView> Contacts { get { return Set<CrmContactView>().AsNoTracking(); } }
        public DbQuery<CrmAssociateView> Associates { get { return Set<CrmAssociateView>().AsNoTracking(); } }
        public DbQuery<CrmActiveProjectView> CrmActiveProjects { get { return Set<CrmActiveProjectView>().AsNoTracking(); } }
        public DbQuery<CrmProjectView> CrmProjects { get { return Set<CrmProjectView>().AsNoTracking(); } }
        public DbQuery<ContactDetailView> ContactDetails { get { return Set<ContactDetailView>().AsNoTracking(); } }
        public DbQuery<CrmTimesheetUnregisteredEntryView> TimesheetUnregisteredEntries { get { return Set<CrmTimesheetUnregisteredEntryView>().AsNoTracking(); } }
        public DbQuery<CrmAppointmentView> CrmAppointments { get { return Set<CrmAppointmentView>().AsNoTracking(); } }
        public DbQuery<CrmEmailView> CrmEmails { get { return Set<CrmEmailView>().AsNoTracking(); } }
        public DbQuery<CrmUnsynchronizedEmployeeView> CrmUnsynchronizedEmployees { get { return Set<CrmUnsynchronizedEmployeeView>().AsNoTracking(); } }

        public IEnumerable<CrmUnregisteredCandidateAppointmentView> ListUnregisteredCrmCandidateAppointments(Guid projectId)
        {
            var query = Database.SqlQuery<CrmUnregisteredCandidateAppointmentView>("CrmCandidateAppointment_ListUnregisteredCrmCandidateAppointments {0}", projectId);
            return query;
        }

        public IEnumerable<CrmAssessorAppointmentView> ListCoAssessorAppointments(Guid projectId)
        {
            var query = Database.SqlQuery<CrmAssessorAppointmentView>("CrmCandidateAppointment_ListCoAssessors {0}", projectId);
            return query;
        }

        public CrmFormattedAppointmentView RetrieveFormattedCrmAppointment(int appointmentId)
        {
            var query = Database.SqlQuery<CrmFormattedAppointmentView>("CrmCandidateAppointment_RetrieveFormattedCrmAppointment {0}", appointmentId);
            return query.SingleOrDefault();
        }

        public IEnumerable<CrmAppointmentTrainingView> ListProjectTrainingAppointments(Guid projectId)
        {
            var query = Database.SqlQuery<CrmAppointmentTrainingView>("CrmCandidateAppointment_ListProjectTrainingAppointments {0}", projectId);
            return query;
        }
    }
}
