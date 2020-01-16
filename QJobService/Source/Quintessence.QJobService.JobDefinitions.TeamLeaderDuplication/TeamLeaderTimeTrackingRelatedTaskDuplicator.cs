using System;
using System.Collections.Generic;
using System.Linq;
using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;

using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.Models;
using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.WebApi;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication
{
    public class TeamLeaderTimeTrackingRelatedTaskDuplication : ReplicationBase, IJobDefinition
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
                    jobControllerService.WriteError("TeamLeaderTimeTrackingRelatedTaskDuplication", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderTimeTrackingRelatedTaskDuplicator started");

                new TeamLeaderTimeTrackingRelatedTaskDuplicator(jobControllerService, duplicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("TeamLeaderTimeTrackingRelatedTaskDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderTimeTrackingRelatedTaskDuplicator ended");
            }
        }
    }

    public class TeamLeaderTimeTrackingRelatedTaskDuplicator : Duplicator
    {
        public TeamLeaderTimeTrackingRelatedTaskDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
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

                bool relatedTaskIdsFetched;
                int numberOfTaskIdsToRetrieve = 100;
                int lastRetrievedTimeTrackingId = -1;

                // Fetch from TeamLeader
                List<CustomField> taskCustomFields = new List<CustomField>(await _teamLeaderAccess.RetrieveCustomFieldsByTargetObject(TLC.Constants.CustomFieldTargetObject.Task));


                do
                {
                    // Key = timetracking.TeamLeaderId, Value = RelatedObjectId (= task.teamLeaderId
                    IDictionary<int, int?> relatedIds = _duplicationDataAccess.RetrieveTimeTrackingRelatedTaskIds(lastRetrievedTimeTrackingId, numberOfTaskIdsToRetrieve);
                    relatedTaskIdsFetched = relatedIds.Any();


                    List<TeamLeaderTask> teamLeaderTasksToRegister = new List<TeamLeaderTask>();
                    foreach (var relatedId in relatedIds)
                    {
                        if (!relatedId.Value.HasValue)
                            continue;

                        lastRetrievedTimeTrackingId = relatedId.Key;
                        TeamLeaderTask teamLeaderTask = await _teamLeaderAccess.RetrieveTaskByTaskId(relatedId.Value.Value, taskCustomFields);
                        teamLeaderTasksToRegister.Add(teamLeaderTask);
                        
                        loopCounter++;
                        if (loopCounter % 20 == 0)
                            startDateTime = TLC.Throttler.Throttle(startDateTime, TimeSpan.FromSeconds(6));
                    }

                    // Sync with db
                    affected += _duplicationDataAccess.RegisterTasksIfNotExisting(teamLeaderTasksToRegister);

                } while (relatedTaskIdsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} TimeTracking related Task(s) duplicated", affected));

            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}