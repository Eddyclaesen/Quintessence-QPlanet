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
    public class TeamLeaderPlannedTaskDuplication : ReplicationBase, IJobDefinition
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
                    jobControllerService.WriteError("TeamLeaderPlannedTaskDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderPlannedTaskDuplicator started");

                new TeamLeaderPlannedTaskDuplicator(jobControllerService, duplicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("TeamLeaderPlannedTaskDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderPlannedTaskDuplicator ended");
            }
        }
    }

    public class TeamLeaderPlannedTaskDuplicator : Duplicator
    {
        public TeamLeaderPlannedTaskDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
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
                IEnumerable<int> userTeamLeaderIds = _duplicationDataAccess.RetrieveUserTeamLeaderIds();
                foreach (int userTeamLeaderId in userTeamLeaderIds)
                {
                    bool plannedTasksFetched;
                    DateTime fromDate = new DateTime(2016, 12, 1);
                    do
                    {
                        DateTime toDate = fromDate.AddMonths(1);
                        IEnumerable<TeamLeaderPlannedTask> teamLeaderPlannedTasks = await _teamLeaderAccess.RetrievePlannedTasksByUserTeamLeaderId(userTeamLeaderId, fromDate, toDate);

                        List<TeamLeaderPlannedTask> plannedTasks = new List<TeamLeaderPlannedTask>(teamLeaderPlannedTasks);
                        plannedTasksFetched = plannedTasks.Any();

                        // Sync with db
                        affected += _duplicationDataAccess.RegisterPlannedTasks(plannedTasks);
                        fromDate = fromDate.AddMonths(1);

                        loopCounter++;
                        if (loopCounter % 20 == 0)
                            startDateTime = TLC.Throttler.Throttle(startDateTime, TimeSpan.FromSeconds(5));
 
                    } while (fromDate < DateTime.Today.AddMonths(1) || plannedTasksFetched);
                }

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} PlannedTask(s) duplicated", affected));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}