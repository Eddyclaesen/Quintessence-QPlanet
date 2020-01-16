using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;
using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.Models;
using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.WebApi;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication
{
    public class TeamLeaderDealDuplication : ReplicationBase, IJobDefinition
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
                    jobControllerService.WriteError("TeamLeaderDealDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderDealDuplicator started");

                new TeamLeaderDealDuplicator(jobControllerService, duplicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("TeamLeaderDealDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderDealDuplicator ended");
            }
        }
    }

    public class TeamLeaderDealDuplicator : Duplicator
    {
        public TeamLeaderDealDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
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

                IEnumerable<TeamLeaderDealSource> teamLeaderDealSources = await _teamLeaderAccess.RetrieveDealSources();
                _duplicationDataAccess.RegisterDealSources(teamLeaderDealSources);

                IEnumerable<TeamLeaderDealPhase> teamLeaderDealPhases = await _teamLeaderAccess.RetrieveDealPhases();
                _duplicationDataAccess.RegisterDealPhases(teamLeaderDealPhases);

                int batchSize;
                if (!int.TryParse(_duplicationDataAccess.RetrieveDuplicationSettingsByKey(TLC.Constants.SettingKeys.DuplicatorBatchSize, "100"), out batchSize))
                    batchSize = 100;

                // Fetch from TeamLeader
                List<CustomField> dealCustomFields = new List<CustomField>(await _teamLeaderAccess.RetrieveCustomFieldsByTargetObject(TLC.Constants.CustomFieldTargetObject.Deal));


                int pageNumber = 0;
                int affected = 0;
                int loopCounter = 0;
                bool dealsFetched;
                do
                {
                    IEnumerable<TeamLeaderDeal> teamLeaderDeals = await _teamLeaderAccess.RetrieveDeals(batchSize, pageNumber, null, dealCustomFields);

                    List<TeamLeaderDeal> deals = new List<TeamLeaderDeal>(teamLeaderDeals);
                    dealsFetched = deals.Any();

                    // Sync with db
                    affected += _duplicationDataAccess.RegisterDeals(deals);
                    pageNumber++;

                    loopCounter++;
                    if (loopCounter % 20 == 0)
                        Thread.Sleep(200);
 

                } while (dealsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} Deal(s) duplicated", affected));

            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}