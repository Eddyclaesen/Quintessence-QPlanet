using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.WebApi;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication
{
    public class ProjectIdReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("ProjectIdReplication started");

                IReplicationDataAccess replicationDataAccess = new ReplicationDataAccess();
                List<string> teamLeaderSettings = new List<string> { TeamLeader.Constants.SettingKeys.ApiGroup, TeamLeader.Constants.SettingKeys.ApiKey, TeamLeader.Constants.SettingKeys.ApiBaseUrl };
                IDictionary<string, string> settings = replicationDataAccess.RetrieveCrmReplicationSettingsByKeys(teamLeaderSettings);
                TeamLeaderAccess.Initialize(settings[TeamLeader.Constants.SettingKeys.ApiGroup], settings[TeamLeader.Constants.SettingKeys.ApiKey], settings[TeamLeader.Constants.SettingKeys.ApiBaseUrl]);

                ITeamLeaderAccess teamLeaderAccess = new TeamLeaderAccess();

                new ProjectIdReplicator(jobControllerService, replicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("ProjectIdReplication", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("ProjectIdReplication ended");
            }

        }
    }

    public class ProjectIdReplicator : Replicator
    {
        public ProjectIdReplicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
                : base(jobControllerService, replicationDataAccess, teamLeaderAccess)
        {                
        }

        public async Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {                
                jobHistoryId = RegisterJobStart(Constants.JobNames.TeamLeader.ProjectIdReplication);
                if (!await TestTeamLeaderAccess(jobHistoryId))
                    return;
 
                List<CustomField> projectCustomFields = new List<CustomField>(await _teamLeaderAccess.RetrieveCustomFieldsByTargetObject(TeamLeader.Constants.CustomFieldTargetObject.Project));

                int pageSize = 100;
                int pageNumber = 0;
                int affected = 0;
                bool projectsFetched;
                bool currentBookYearOnly = false;
                do
                {
                    IEnumerable<Project> teamLeaderProjects = await _teamLeaderAccess.RetrieveProjects(pageSize, pageNumber, projectCustomFields);
                    projectsFetched = false;

                    List<Project> projects = new List<Project>();
                    foreach (var project in teamLeaderProjects)
                    {
                        projectsFetched = true;                        
                        if (!currentBookYearOnly || (currentBookYearOnly && project.IsOfCurrentBookYear()))
                            projects.Add(project);
                    }

                    // Sync with db
                    affected += _replicationDataAccess.SyncCrmProjectIds(projects);
                    pageNumber++;

                } while (projectsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} ProjectId(s) synced", affected));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }
    }
}