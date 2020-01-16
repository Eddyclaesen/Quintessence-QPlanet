using System;
using System.Threading.Tasks;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.WebApi;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication
{
    public abstract class Replicator
    {
        private Guid? _jobId;
        protected IJobControllerService _jobControllerService;
        protected IReplicationDataAccess _replicationDataAccess;
        protected ITeamLeaderAccess _teamLeaderAccess;
        
        protected Replicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
        {
            _jobControllerService = jobControllerService;
            _replicationDataAccess = replicationDataAccess;
            _teamLeaderAccess = teamLeaderAccess;
        }

        protected Guid RegisterJobStart(string jobName)
        {
            if(!_jobId.HasValue)
                _jobId = _replicationDataAccess.RetrieveCrmReplicationJobIdByName(jobName);

            return _replicationDataAccess.RegisterCrmReplicationJobHistoryStart(_jobId.GetValueOrDefault());
        }

        protected void RegisterJobEnd(Guid? jobHistoryId, bool succeeded, string info)
        {
            _replicationDataAccess.RegisterCrmReplicationJobHistoryEnd(jobHistoryId.GetValueOrDefault(), succeeded, info);
        }


        protected async Task<bool> TestTeamLeaderAccess(Guid? jobHistoryId)
        {
            bool success = await _teamLeaderAccess.TestAccess();
            if (!success)
                _replicationDataAccess.RegisterCrmReplicationJobHistoryEnd(jobHistoryId.GetValueOrDefault(), false, "No Access to TeamLeader web API.");
            return success;
        }
    }
}
