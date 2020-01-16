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
    public class TeamLeaderTimeTrackingDuplication : ReplicationBase, IJobDefinition
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
                    jobControllerService.WriteError("TeamLeaderTimeTrackingDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderTimeTrackingDuplicator started");

                new TeamLeaderTimeTrackingDuplicator(jobControllerService, duplicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("TeamLeaderTimeTrackingDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderTimeTrackingDuplicator ended");
            }
        }
    }

    public class TeamLeaderTimeTrackingDuplicator : Duplicator
    {
        public TeamLeaderTimeTrackingDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
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
                bool timeTrackingsFetched;
                DateTime fromDate = new DateTime(2016, 12, 1);
                DateTime toDate; 
                do
                {
                    toDate = fromDate.AddMonths(1);
                    IEnumerable<TeamLeaderTimeTracking> teamLeaderTimeTrackings = await _teamLeaderAccess.RetrieveTimeTrackings(fromDate, toDate);

                    List<TeamLeaderTimeTracking> timeTrackings = new List<TeamLeaderTimeTracking>(teamLeaderTimeTrackings);
                    timeTrackingsFetched = timeTrackings.Any();

                    // Sync with db
                    affected += _duplicationDataAccess.RegisterTimeTrackings(timeTrackings);
                    fromDate = fromDate.AddMonths(1);

                } while (fromDate < DateTime.Today.AddMonths(1) || timeTrackingsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} TimeTracking(s) duplicated", affected));

            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}