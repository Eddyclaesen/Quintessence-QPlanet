using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.WebApi;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication
{
    public class ContactIdReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("ContactIdReplication started");

                IReplicationDataAccess replicationDataAccess = new ReplicationDataAccess();
                List<string> teamLeaderSettings = new List<string> { TeamLeader.Constants.SettingKeys.ApiGroup, TeamLeader.Constants.SettingKeys.ApiKey, TeamLeader.Constants.SettingKeys.ApiBaseUrl };
                IDictionary<string, string> settings = replicationDataAccess.RetrieveCrmReplicationSettingsByKeys(teamLeaderSettings);
                TeamLeaderAccess.Initialize(settings[TeamLeader.Constants.SettingKeys.ApiGroup], settings[TeamLeader.Constants.SettingKeys.ApiKey], settings[TeamLeader.Constants.SettingKeys.ApiBaseUrl]);

                ITeamLeaderAccess teamLeaderAccess = new TeamLeaderAccess();

                new ContactIdReplicator(jobControllerService, replicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("ContactIdReplication", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("ContactIdReplication ended");
            }
        }
    }

    public class ContactIdReplicator : Replicator
    {
        public ContactIdReplicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
            : base(jobControllerService, replicationDataAccess, teamLeaderAccess)
        {
        }

        public async Task RunAsync()
        {            
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.TeamLeader.ContactIdReplication);
                if (!await TestTeamLeaderAccess(jobHistoryId))
                    return;

                // Fetch from TeamLeader
                List<CustomField> companyCustomFields = new List<CustomField>(await _teamLeaderAccess.RetrieveCustomFieldsByTargetObject(TeamLeader.Constants.CustomFieldTargetObject.Company));

                int pageSize = 100;
                int pageNumber = 0;
                int affected = 0;
                bool companiesFetched;
                do
                {
                    IEnumerable<Company> teamLeaderCompanies = await _teamLeaderAccess.RetrieveCompanies(pageSize, pageNumber, null, companyCustomFields);

                    List<Company> companies = new List<Company>(teamLeaderCompanies);
                    companiesFetched = companies.Any();

                    // Sync with db
                    affected += _replicationDataAccess.SyncCrmContactIds(companies);
                    pageNumber++;

                } while (companiesFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} ContactId(s) synced", affected));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }
    }
}