using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.WebApi
{
    public interface ITeamLeaderAccess
    {
        Task<bool> TestAccess();

        Task<IEnumerable<CustomField>> RetrieveCustomFieldsByTargetObject(string customFieldTargetObject);
        Task<CustomField> RetrieveCustomFieldById(int customFieldId);

        Task<IEnumerable<User>> RetrieveUsers();

        Task<IEnumerable<Project>> RetrieveProjects(int pageSize, int pageNumber, IEnumerable<CustomField> customFields);
        Task<Project> RetrieveProjectByTeamLeaderId(int teamLeaderProjectId, IEnumerable<CustomField> customFields);
        Task<IEnumerable<ProjectUser>> RetrieveProjectUsers(int teamLeaderProjectId);


        Task<IEnumerable<Company>> RetrieveCompanies(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields);
        Task<Company> RetrieveCompanyByTeamLeaderId(int teamLeaderCompanyId, IEnumerable<CustomField> customFields);

        Task<IEnumerable<Contact>> RetrieveContacts(int pageSize, int pageNumber, DateTime? modifiedSince, IEnumerable<CustomField> customFields);
        Task<Contact> RetrieveContactByTeamLeaderId(int teamLeaderContactId, IEnumerable<CustomField> customFields);

        Task<IEnumerable<CompanyContact>> RetrieveCompanyRelatedContacts(int teamLeaderCompanyId);

        Task<TeamLeaderTask> RetrieveTaskByTeamLeaderId(int teamLeaderTaskId, IEnumerable<CustomField> customFields);

        Task<TimeTracking> RetrieveTimeTrackingByTeamLeaderId(int teamLeaderTimeTrackingId);

        Task<IEnumerable<PlannedTask>> RetrievePlannedTasksByProjectIdAndUserId(DateTime fromDate, DateTime toDate, int projectId, int? userId);
    }
}
