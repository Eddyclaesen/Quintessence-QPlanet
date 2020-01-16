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
    public class TeamLeaderProductDuplication : ReplicationBase, IJobDefinition
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
                    jobControllerService.WriteError("TeamLeaderProductDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderProductDuplicator started");

                new TeamLeaderProductDuplicator(jobControllerService, duplicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("TeamLeaderProductDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderProductDuplicator ended");
            }
        }
    }

    public class TeamLeaderProductDuplicator : Duplicator
    {
        public TeamLeaderProductDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
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
                bool productsFetched;
                do
                {
                    IEnumerable<TeamLeaderProduct> teamLeaderProducts = await _teamLeaderAccess.RetrieveProducts(batchSize, pageNumber);

                    List<TeamLeaderProduct> products = new List<TeamLeaderProduct>(teamLeaderProducts);
                    productsFetched = products.Any();

                    // Sync with db
                    affected += _duplicationDataAccess.RegisterProducts(products);
                    pageNumber++;

                } while (productsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} Product(s) duplicated", affected));

            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}