using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.Models;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.WebApi
{
    public interface ITeamLeaderAccess
    {
        Task<bool> TestAccess();

        Task<IEnumerable<CustomField>> RetrieveCustomFieldsByTargetObject(string customFieldTargetObject);
        Task<CustomField> RetrieveCustomFieldById(int customFieldId);

        Task<IEnumerable<TeamLeaderUser>> RetrieveUsers();
        Task<IEnumerable<TeamLeaderContact>> RetrieveContacts(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields);
        Task<IEnumerable<TeamLeaderCompany>> RetrieveCompanies(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields);
        Task<IEnumerable<TeamLeaderProject>> RetrieveProjects(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields);
        Task<IEnumerable<TeamLeaderTask>> RetrieveTasks(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields);
        Task<IEnumerable<TeamLeaderTimeTracking>> RetrieveTimeTrackings(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<TeamLeaderDeal>> RetrieveDeals(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields);
        Task<IEnumerable<TeamLeaderCall>> RetrieveCalls(int pageSize, int pageNumber, DateTime? fromDate, DateTime? toDate);
        Task<IEnumerable<TeamLeaderMeeting>> RetrieveMeetings(int pageSize, int pageNumber, DateTime? fromDate, DateTime? toDate);
        Task<TeamLeaderMeetingDetail> RetrieveMeetingDetailByMeetingId(int meetingTeamLeaderId, IEnumerable<CustomField> customFields);
        Task<TeamLeaderCompanyDetail> RetrieveCompanyDetailByCompanyId(int companyTeamLeaderId);
        Task<TeamLeaderTask> RetrieveTaskByTaskId(int taskTeamLeaderId, IEnumerable<CustomField> customFields);
        Task<TeamLeaderTaskDetail> RetrieveTaskDetailByTaskId(int taskTeamLeaderId);
        Task<TeamLeaderCallDetail> RetrieveCallDetailByCallId(int callTeamLeaderId, IEnumerable<CustomField> customFields);
        Task<IEnumerable<TeamLeaderContactCompanyRelation>> RetrieveContactCompanyRelation(int pageSize, int pageNumber);
        Task<IEnumerable<TeamLeaderPlannedTask>> RetrievePlannedTasksByUserTeamLeaderId(int userTeamLeaderId, DateTime fromDate, DateTime toDate);
        Task<IEnumerable<TeamLeaderContactProjectRelation>> RetrieveContactProjectRelationsByProjectTeamLeaderId(int projectTeamLeaderId);
        Task<IEnumerable<TeamLeaderProduct>> RetrieveProducts(int pageSize, int pageNumber);
        Task<IEnumerable<TeamLeaderDealPhase>> RetrieveDealPhases();
        Task<IEnumerable<TeamLeaderDealSource>> RetrieveDealSources();
    }
}
