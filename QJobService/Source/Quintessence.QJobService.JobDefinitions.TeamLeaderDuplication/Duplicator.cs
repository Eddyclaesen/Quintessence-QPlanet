using System;
using System.Threading.Tasks;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.WebApi;


namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication
{
    public abstract class Duplicator
    {
        protected IJobControllerService _jobControllerService;
        protected IDuplicationDataAccess _duplicationDataAccess;
        protected ITeamLeaderAccess _teamLeaderAccess;

        protected Duplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
        {
            _jobControllerService = jobControllerService;
            _duplicationDataAccess = duplicationDataAccess;
            _teamLeaderAccess = teamLeaderAccess;
        }

        protected Guid RegisterJobStart(string jobName)
        {
            return _duplicationDataAccess.RegisterDuplicationJobHistoryStart(jobName);
        }

        protected void RegisterJobEnd(Guid? jobHistoryId, bool succeeded, string info)
        {
            _duplicationDataAccess.RegisterDuplicationJobHistoryEnd(jobHistoryId.GetValueOrDefault(), succeeded, info);
        }


        protected async Task<bool> TestTeamLeaderAccess(Guid? jobHistoryId)
        {
            bool success = await _teamLeaderAccess.TestAccess();
            if (!success)
                _duplicationDataAccess.RegisterDuplicationJobHistoryEnd(jobHistoryId.GetValueOrDefault(), false, "No Access to TeamLeader web API.");
            return success;
        }
    }
}
