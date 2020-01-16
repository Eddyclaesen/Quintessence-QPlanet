using System;
using System.Collections.Generic;
using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.WebApi;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication
{
    public class TeamLeaderContactProjectRelationDuplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                // Resolve dependencies
                IDuplicationDataAccess duplicationDataAccess = new DuplicationDataAccess();
                List<string> teamLeaderSettings = new List<string> { TLC.Constants.SettingKeys.ApiGroup, TLC.Constants.SettingKeys.ApiKey, TLC.Constants.SettingKeys.ApiBaseUrl };
                IDictionary<string, string> settings = duplicationDataAccess.RetrieveDuplicationSettingsByKeys(teamLeaderSettings);
                TeamLeaderAccess.Initialize(settings[TLC.Constants.SettingKeys.ApiGroup], settings[TLC.Constants.SettingKeys.ApiKey], settings[TLC.Constants.SettingKeys.ApiBaseUrl]);

                ITeamLeaderAccess teamLeaderAccess = new TeamLeaderAccess();
                Run(jobControllerService, duplicationDataAccess, teamLeaderAccess);
            }
            catch (Exception ex)
            {
                if(jobControllerService != null)
                    jobControllerService.WriteError("TeamLeaderContactProjectRelationDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderContactProjectRelationDuplicator started");

                new TeamLeaderContactProjectRelationDuplicator(jobControllerService, duplicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("TeamLeaderContactProjectRelationDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderContactProjectRelationDuplicator ended");
            }
        }
    }

    public class TeamLeaderContactProjectRelationDuplicator : Duplicator
    {
        public TeamLeaderContactProjectRelationDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
            : base(jobControllerService, duplicationDataAccess, teamLeaderAccess)
        {                
        }

        public async System.Threading.Tasks.Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.TeamLeader.TeamLeaderContactDuplication);
                if (!await TestTeamLeaderAccess(jobHistoryId))
                    return;

                int affected = 0;
                int loopCounter = 0;
                DateTime startDateTime = DateTime.Now;
                IEnumerable<int> projectTeamLeaderIds = _duplicationDataAccess.RetrieveProjectTeamLeaderIds();
                foreach (int projectTeamLeaderId in projectTeamLeaderIds)
                {
                    IEnumerable<TeamLeaderContactProjectRelation> teamLeaderContactProjectRelations = await _teamLeaderAccess.RetrieveContactProjectRelationsByProjectTeamLeaderId(projectTeamLeaderId);
                    List<TeamLeaderContactProjectRelation> contactProjectRelations = new List<TeamLeaderContactProjectRelation>(teamLeaderContactProjectRelations);

                    // Sync with db
                    affected += _duplicationDataAccess.RegisterContactProjectRelations(projectTeamLeaderId, contactProjectRelations);
                    loopCounter++;
                    if(loopCounter % 20 == 0)
                        startDateTime = TLC.Throttler.Throttle(startDateTime, TimeSpan.FromSeconds(5));
                }

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} ContactProjectRelation(s) duplicated", affected));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}