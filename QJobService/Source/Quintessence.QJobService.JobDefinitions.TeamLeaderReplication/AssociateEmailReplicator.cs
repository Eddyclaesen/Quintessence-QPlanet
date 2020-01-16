using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.WebApi;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication
{
    public class AssociateEmailReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("AssociateEmailReplication started");

                IReplicationDataAccess replicationDataAccess = new ReplicationDataAccess();
                List<string> teamLeaderSettings = new List<string> { TeamLeader.Constants.SettingKeys.ApiGroup, TeamLeader.Constants.SettingKeys.ApiKey, TeamLeader.Constants.SettingKeys.ApiBaseUrl };
                IDictionary<string, string> settings = replicationDataAccess.RetrieveCrmReplicationSettingsByKeys(teamLeaderSettings);
                TeamLeaderAccess.Initialize(settings[TeamLeader.Constants.SettingKeys.ApiGroup], settings[TeamLeader.Constants.SettingKeys.ApiKey], settings[TeamLeader.Constants.SettingKeys.ApiBaseUrl]);

                ITeamLeaderAccess teamLeaderAccess = new TeamLeaderAccess();

                new AssociateEmailReplicator(jobControllerService, replicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("AssociateEmailReplication", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("AssociateEmailReplication ended");
            }
        }
    }

    public class AssociateEmailReplicator : Replicator
    {
        public AssociateEmailReplicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
            : base(jobControllerService, replicationDataAccess, teamLeaderAccess)
        {
        }

        public async Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.TeamLeader.AssociateEmailReplication);
                if (!await TestTeamLeaderAccess(jobHistoryId))
                    return;

                // Fetch from TeamLeader
                List<User> users = new List<User>(await _teamLeaderAccess.RetrieveUsers());
                _replicationDataAccess.EnrichTeamLeaderUsersWithAssociateId(users);

                // Sync with db
                int affected = _replicationDataAccess.SyncCrmAssociateEmails(users);
                RegisterJobEnd(jobHistoryId, true, String.Format("{0} Associate email(s) synced", affected));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }
    }
}