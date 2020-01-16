using System;
using System.Threading.Tasks;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.WebApi;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication
{
    public abstract class Replicator
    {
        private Guid? _jobId;
        protected IJobControllerService _jobControllerService;
        protected IReplicationDataAccess _replicationDataAccess;
        protected ISuperOfficeAccess _superOfficeAccess;

        protected Replicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ISuperOfficeAccess superOfficeAccess)
        {
            _jobControllerService = jobControllerService;
            _replicationDataAccess = replicationDataAccess;
            _superOfficeAccess = superOfficeAccess;
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


        protected async Task<bool> TestSuperOfficeAccess(Guid? jobHistoryId)
        {
            bool success = await _superOfficeAccess.TestAccess();
            if (!success)
                _replicationDataAccess.RegisterCrmReplicationJobHistoryEnd(jobHistoryId.GetValueOrDefault(), false, "No Access to SuperOffice web API.");
            return success;
        }
    }
}
