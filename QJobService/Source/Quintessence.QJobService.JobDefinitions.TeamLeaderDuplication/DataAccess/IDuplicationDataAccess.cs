using System;
using System.Collections.Generic;

using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.DataAccess
{
    public interface IDuplicationDataAccess
    {
        // Setting
        string RetrieveDuplicationSettingsByKey(string key, string defaultValue);
        IDictionary<string, string> RetrieveDuplicationSettingsByKeys(IEnumerable<string> keys );
        
        void RegisterDuplicationError(DateTime logDateUtc, string info);

        // JobHistory
        Guid RegisterDuplicationJobHistory(string JobName, DateTime startDate, DateTime? endDate, bool succeeded, string info);
        Guid RegisterDuplicationJobHistoryStart(string jobName);
        void RegisterDuplicationJobHistoryEnd(Guid duplicationJobHistoryId, bool succeeded, string info);

        void TruncateDuplicationTables();
        int RegisterUsers(IEnumerable<TeamLeaderUser> users);
        int RegisterContacts(IEnumerable<TeamLeaderContact> contacts);
        int RegisterCompanies(IEnumerable<TeamLeaderCompany> companies);
        int RegisterProjects(IEnumerable<TeamLeaderProject> projects);
        int RegisterTasks(IEnumerable<TeamLeaderTask> tasks);
        int RegisterTasksIfNotExisting(IEnumerable<TeamLeaderTask> tasks);
        int RegisterTimeTrackings(IEnumerable<TeamLeaderTimeTracking> timeTrackings);
        int RegisterDeals(IEnumerable<TeamLeaderDeal> deals);
        int RegisterCalls(IEnumerable<TeamLeaderCall> calls);
        int RegisterMeetings(IEnumerable<TeamLeaderMeeting> meetings);
        int RegisterContactCompanyRelations(IEnumerable<TeamLeaderContactCompanyRelation> contactCompanyRelations);
        int RegisterPlannedTasks(IEnumerable<TeamLeaderPlannedTask> plannedTasks);
        int RegisterContactProjectRelations(int projectTeamLeaderId, IEnumerable<TeamLeaderContactProjectRelation> contactProjectRelations);
        int RegisterProducts(IEnumerable<TeamLeaderProduct> products);
        int RegisterDealSources(IEnumerable<TeamLeaderDealSource> dealSources);
        int RegisterDealPhases(IEnumerable<TeamLeaderDealPhase> dealPhases);
        int EnrichMeeting(int meetingId, TeamLeaderMeetingDetail meetingDetail);
        int EnrichCompany(int companyId, TeamLeaderCompanyDetail companyDetail);
        int EnrichTask(int taskId, TeamLeaderTaskDetail taskDetail);
        int EnrichCall(int callId, TeamLeaderCallDetail callDetail);
        
        IEnumerable<int> RetrieveUserTeamLeaderIds();
        IEnumerable<int> RetrieveProjectTeamLeaderIds();
        IEnumerable<int> RetrieveDealIds();
        IEnumerable<int> RetrieveMeetingIds();
        IEnumerable<int> RetrieveCompanyIds();
        IEnumerable<int> RetrieveTaskIds();
        IEnumerable<int> RetrieveCallIds();
        IDictionary<int, int?> RetrieveTimeTrackingRelatedTaskIds(int fromTimeTrackingId, int numberOfRecordsToRetrieve);
    }
}
