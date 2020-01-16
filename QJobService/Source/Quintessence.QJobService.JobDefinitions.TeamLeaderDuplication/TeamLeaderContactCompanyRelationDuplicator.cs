using System;
using System.Collections.Generic;
using System.Linq;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.WebApi;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication
{
    public class TeamLeaderContactCompanyRelationDuplication : ReplicationBase, IJobDefinition
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
                    jobControllerService.WriteError("TeamLeaderContactCompanyRelationDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderContactCompanyRelationDuplicator started");

                new TeamLeaderContactCompanyRelationDuplicator(jobControllerService, duplicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("TeamLeaderContactCompanyRelationDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderContactCompanyRelationDuplicator ended");
            }
        }
    }

    public class TeamLeaderContactCompanyRelationDuplicator : Duplicator
    {
        public TeamLeaderContactCompanyRelationDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
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

                int batchSize;
                if (!int.TryParse(_duplicationDataAccess.RetrieveDuplicationSettingsByKey(TLC.Constants.SettingKeys.DuplicatorBatchSize, "100"), out batchSize))
                    batchSize = 100;

                int pageNumber = 0;
                int affected = 0;
                bool relationsFetched;
                do
                {
                    IEnumerable<TeamLeaderContactCompanyRelation> teamLeaderContactCompanyRelations = await _teamLeaderAccess.RetrieveContactCompanyRelation(batchSize, pageNumber);

                    List<TeamLeaderContactCompanyRelation> contactCompanyRelations = new List<TeamLeaderContactCompanyRelation>(teamLeaderContactCompanyRelations);
                    relationsFetched = contactCompanyRelations.Any();

                    // Sync with db
                    affected += _duplicationDataAccess.RegisterContactCompanyRelations(contactCompanyRelations);
                    pageNumber++;

                } while (relationsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} ContactCompanyRelation(s) duplicated", affected));

            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}