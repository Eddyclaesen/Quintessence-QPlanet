using System;
using System.Collections.Generic;
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
    public class TeamLeaderMeetingDetailDuplication : ReplicationBase, IJobDefinition
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
                    jobControllerService.WriteError("TeamLeaderMeetingDetailDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderMeetingDetailDuplicator started");

                new TeamLeaderMeetingDetailDuplicator(jobControllerService, duplicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("TeamLeaderMeetingDetailDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderMeetingDetailDuplicator ended");
            }
        }
    }

    public class TeamLeaderMeetingDetailDuplicator : Duplicator
    {
        public TeamLeaderMeetingDetailDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
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

                // Fetch from TeamLeader
                List<CustomField> contactCustomFields = new List<CustomField>(await _teamLeaderAccess.RetrieveCustomFieldsByTargetObject(TLC.Constants.CustomFieldTargetObject.Meeting));

                int affected = 0;
                int loopCounter = 0;
                DateTime startDateTime = DateTime.Now;
                TeamLeaderMeetingDetail teamLeaderMeetingDetail;
                IEnumerable<int> meetingTeamLeaderIds = _duplicationDataAccess.RetrieveMeetingIds();
                foreach (int meetingTeamLeaderId in meetingTeamLeaderIds)
                {
                    try
                    {
                        teamLeaderMeetingDetail = await _teamLeaderAccess.RetrieveMeetingDetailByMeetingId(meetingTeamLeaderId, contactCustomFields);

                        // Sync with db
                        affected += _duplicationDataAccess.EnrichMeeting(meetingTeamLeaderId, teamLeaderMeetingDetail);

                        loopCounter++;
                        if (loopCounter % 20 == 0)
                            startDateTime = TLC.Throttler.Throttle(startDateTime, TimeSpan.FromSeconds(6));
                        if (loopCounter % 500 == 0)
                            Thread.Sleep(5000);
                    }
                    catch {}
                }

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} Meeting(s) eniched", affected));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}