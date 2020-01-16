using System;
using System.Collections.Generic;
using System.Threading;
using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.WebApi;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication
{
    public class TeamLeaderDuplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            DateTime startDateTime = DateTime.Now;
            IDuplicationDataAccess duplicationDataAccess = null;

            try
            {                
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderDuplicator started");

                duplicationDataAccess = new DuplicationDataAccess();
                duplicationDataAccess.TruncateDuplicationTables();

                List<string> teamLeaderSettings = new List<string> { TLC.Constants.SettingKeys.ApiGroup, TLC.Constants.SettingKeys.ApiKey, TLC.Constants.SettingKeys.ApiBaseUrl };
                IDictionary<string, string> settings = duplicationDataAccess.RetrieveDuplicationSettingsByKeys(teamLeaderSettings);
                TeamLeaderAccess.Initialize(settings[TLC.Constants.SettingKeys.ApiGroup], settings[TLC.Constants.SettingKeys.ApiKey], settings[TLC.Constants.SettingKeys.ApiBaseUrl]);

                ITeamLeaderAccess teamLeaderAccess = new TeamLeaderAccess();

                new TeamLeaderDuplicator(jobControllerService, duplicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if(jobControllerService != null)
                    jobControllerService.WriteError("TeamLeaderDuplicator", ex);

                if (duplicationDataAccess != null)
                    duplicationDataAccess.RegisterDuplicationError(DateTime.UtcNow, ex.ToString());
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                {
                    string duration = String.Format("{0} sec", (int) ((DateTime.Now - startDateTime).TotalSeconds));
                    jobControllerService.WriteInformation("TeamLeaderDuplicator ended " + duration);
                }
            }
        }
    }

    public class TeamLeaderDuplicator : Duplicator
    {
        public TeamLeaderDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
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

                // List methods
                new TeamLeaderUserDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                new TeamLeaderContactDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                new TeamLeaderCompanyDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                new TeamLeaderContactCompanyRelationDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                new TeamLeaderProjectDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                new TeamLeaderTaskDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                new TeamLeaderTimeTrackingDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                new TeamLeaderDealDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                new TeamLeaderCallDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                new TeamLeaderMeetingDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                new TeamLeaderProductDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                Thread.Sleep(5000);

                // Detail methods
                new TeamLeaderPlannedTaskDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                Thread.Sleep(10000);
                new TeamLeaderContactProjectRelationDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                Thread.Sleep(10000);         
                new TeamLeaderCallDetailDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                Thread.Sleep(10000);
                new TeamLeaderCompanyDetailDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                Thread.Sleep(10000);               
                new TeamLeaderMeetingDetailDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);

                Thread.Sleep(10000);
                new TeamLeaderTimeTrackingRelatedTaskDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);

                Thread.Sleep(10000);
                new TeamLeaderTaskDetailDuplication().Run(_jobControllerService, _duplicationDataAccess, _teamLeaderAccess);
                
                RegisterJobEnd(jobHistoryId, true, "All duplicated");
            }
            catch (Exception ex)
            {
                _duplicationDataAccess.RegisterDuplicationError(DateTime.UtcNow, ex.ToString());

                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}