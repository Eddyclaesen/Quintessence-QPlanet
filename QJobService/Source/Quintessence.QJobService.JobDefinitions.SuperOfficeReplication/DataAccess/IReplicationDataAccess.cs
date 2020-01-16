using System;
using System.Collections.Generic;

using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess
{
    public interface IReplicationDataAccess
    {
        // Setting
        string RetrieveCrmReplicationSettingByKey(string key, string defaultValue);
        IDictionary<string, string> RetrieveCrmReplicationSettingsByKeys(IEnumerable<string> keys );

        // Job
        CrmReplicationJob RetrieveCrmReplicationJobByName(string jobName);
        Guid? RetrieveCrmReplicationJobIdByName(string jobName);
        IEnumerable<CrmReplicationJob> RetrieveCrmReplicationJobs();
        void RegisterSuperOfficeEventError(int superOfficeEventId, DateTime logDateUtc, string info);

        // JobHistory
        Guid RegisterCrmReplicationJobHistory(Guid crmReplicationJobId, DateTime startDate, DateTime? endDate, bool succeeded, string info);
        Guid RegisterCrmReplicationJobHistoryStart(Guid crmReplicationJobId);
        void RegisterCrmReplicationJobHistoryEnd(Guid crmReplicationJobHistoryId, bool succeeded, string info);

        // Event
        IEnumerable<CrmReplicationSuperOfficeEvent> RetrieveCrmReplicationSuperOfficeEvents(int maxEventsToReturn, int maxProcessCount);
        void DeleteCrmReplicationSuperOfficeEventById(int id);
        void UpdateCrmReplicationSuperOfficeProcessCountById(int id);

        //// UserGroup
        //int? RetrieveCrmUserGroupByName(string userGroupName);

        // User
        void EnrichSuperOfficeUsersWithAssociateId(IEnumerable<User> users);

        //// Company <-> Contact
        //CrmReplicationContact RetrieveCrmReplicationContactByTeamLeaderId(int companyTeamleaderId);
        //void EnrichTeamLeaderCompanies(IEnumerable<Company> companies);
        void EnrichSuperOfficeCompanyWithCrmIds(Company company);
        int SyncFullCrmContact(Company company);
        int SyncCrmContactIds(IEnumerable<Company> companies);
        void DeleteCrmReplicationContactAndEmailBySuperOfficeId(int companySuperOfficeId);

        // Contact <-> Person
        int SyncCrmPersonIds(IEnumerable<PersonOverview> persons);
        void EnrichSuperOfficePersonWithCompanyInfo(IEnumerable<Person> persons);
        void EnrichSuperOfficePersonWithCrmReplicationPersonId(IEnumerable<Person> persons);
        int SyncFullCrmPerson(Person person);
        void DeleteCrmReplicationPersonAndEmailBySuperOfficeId(int personSuperOfficeId);

        // Project
        void EnrichSuperOfficeProjectsWithStatusId(IEnumerable<Project> projects);
        void EnrichSuperOfficeProjectWithAssociateId(Project project);
        void EnrichSuperOfficeProjectsWithCompanyId(IEnumerable<Project> projects);
        int SyncFullCrmProject(Project project);
        int SyncCrmProjectIds(IEnumerable<Project> projects);
        void DeleteCrmReplicationProjectBySuperOfficeId(int projectSuperOfficeId);
        bool IsQPlanetProject(int? projectId);

        // Associate
        int SyncCrmAssociates(IEnumerable<User> users);
        int SyncCrmAssociateEmails(IEnumerable<User> users);
        
        int SyncFullCrmEmail(Person person);

        // Appointment
        void EnrichSuperOfficeAppointment(Appointment appointment);
        int SyncFullCrmAppointment(Appointment appointment);
        void DeleteCrmReplicationAppointmentBySuperOfficeId(int superOfficeAppointmentId);

        // TimeTracking
        int SyncFullCrmAppointmentTimeSheet(Appointment appointment);
        void DeleteCrmReplicationAppointmentTimeSheetBySuperOfficeId(int superOfficeTimeTrackingId);        
    }
}
