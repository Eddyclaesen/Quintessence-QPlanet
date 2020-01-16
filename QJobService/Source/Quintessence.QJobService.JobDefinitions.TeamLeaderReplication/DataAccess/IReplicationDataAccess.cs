using System;
using System.Collections.Generic;

using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.DataAccess
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
        void RegisterTeamLeaderEventError(int teamLeaderEventId, DateTime logDateUtc, string info);


        // JobHistory
        Guid RegisterCrmReplicationJobHistory(Guid crmReplicationJobId, DateTime startDate, DateTime? endDate, bool succeeded, string info);
        Guid RegisterCrmReplicationJobHistoryStart(Guid crmReplicationJobId);
        void RegisterCrmReplicationJobHistoryEnd(Guid crmReplicationJobHistoryId, bool succeeded, string info);


        // Event
        IEnumerable<CrmReplicationTeamLeaderEvent> RetrieveCrmReplicationTeamLeaderEvents(int maxEventsToReturn, int maxProcessCount);
        void DeleteCrmReplicationreamLeaderEventById(int id);
        void UpdateCrmReplicationreamLeaderEventProcessCountById(int id);

        // UserGroup
        int? RetrieveCrmUserGroupByName(string userGroupName);

        // User
        void EnrichTeamLeaderUsersWithAssociateId(IEnumerable<User> users);

        // Company <-> Contact
        CrmReplicationContact RetrieveCrmReplicationContactByTeamLeaderId(int companyTeamleaderId);
        void EnrichTeamLeaderCompanies(IEnumerable<Company> companies);
        void EnrichTeamLeaderCompanyWithUserIds(Company company);
        int SyncFullCrmContact(Company company);
        int SyncCrmContactIds(IEnumerable<Company> companies);
        void DeleteCrmReplicationContactAndEmailByTeamLeaderId(int companyTeamleaderId);

        // Contact <-> Person
        int SyncCrmPersonIds(IEnumerable<Contact> contacts);
        void EnrichTeamLeaderContactWithCompanyInfo(IEnumerable<Contact> contacts);
        void EnrichTeamLeaderContactWithLegacyId(IEnumerable<Contact> contacts);
        int SyncFullCrmPerson(Contact contact);
        void DeleteCrmReplicationPersonAndEmailByTeamLeaderId(int contactTeamleaderId);

        // Project
        void EnrichTeamLeaderProjectsWithStatusId(IEnumerable<Project> projects);
        void EnrichTeamLeaderProjectWithAssociateId(Project project, IEnumerable<ProjectUser> projectUsers);
        void EnrichTeamLeaderProjectsWithCompanyId(IEnumerable<Project> projects);
        int SyncFullCrmProject(Project project);
        int SyncCrmProjectIds(IEnumerable<Project> projects);
        void DeleteCrmReplicationProjectByTeamLeaderId(int projectTeamLeaderId);
        bool IsQPlanetProject(int? projectId);

        // Associate
        int SyncCrmAssociates(IEnumerable<User> users);
        int SyncCrmAssociateEmails(IEnumerable<User> users);
        
        // Company - Contact relation
        void EnrichTeamLeaderCompanyContacts(IEnumerable<CompanyContact> companyContacts);
        int SyncFullCrmCompanyContactRelation(CrmReplicationContact company, IEnumerable<CompanyContact> companyContacts);
        int SyncFullCrmEmail(Contact contact);

        // Task
        void EnrichTeamLeaderTask(TeamLeaderTask task);
        int SyncFullCrmAppointment(TeamLeaderTask task);
        void DeleteCrmReplicationAppointmentByTeamLeaderId(int teamLeaderTaskId);

        // TimeTracking
        int SyncFullCrmAppointmentTimeSheet(TimeTracking timeTracking);
        void DeleteCrmReplicationAppointmentTimeSheetByTeamLeaderId(int teamLeaderTimeTrackingId);
        void EnrichTeamLeaderTimeTracking(TimeTracking timeTracking);
    }
}
