using System;
using System.Threading.Tasks;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.WebApi;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication
{
    public class Duplicator
    {
        protected IJobControllerService _jobControllerService;
        protected IDuplicationDataAccess _duplicationDataAccess;
        protected ISuperOfficeAccess _superOfficeAccess;

        protected Duplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
        {
            _jobControllerService = jobControllerService;
            _duplicationDataAccess = duplicationDataAccess;
            _superOfficeAccess = superOfficeAccess;
        }

        protected Guid RegisterJobStart(string jobName)
        {
            return _duplicationDataAccess.RegisterDuplicationJobHistoryStart(jobName);
        }

        protected void RegisterJobEnd(Guid? jobHistoryId, bool succeeded, string info)
        {
            _duplicationDataAccess.RegisterDuplicationJobHistoryEnd(jobHistoryId.GetValueOrDefault(), succeeded, info);
        }


        protected async Task<bool> TestSuperOfficeAccess(Guid? jobHistoryId)
        {
            bool success = await _superOfficeAccess.TestAccess();
            if (!success)
                _duplicationDataAccess.RegisterDuplicationJobHistoryEnd(jobHistoryId.GetValueOrDefault(), false, "No Access to SuperOffice web API.");
            return success;
        }
    }
}
