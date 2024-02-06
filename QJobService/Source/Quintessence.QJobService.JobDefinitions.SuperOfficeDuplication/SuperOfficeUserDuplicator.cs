using System;
using System.Collections.Generic;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;

using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models;
using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.WebApi;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication
{
    public class SuperOfficeUserDuplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                // Resolve dependencies
                IDuplicationDataAccess duplicationDataAccess = new DuplicationDataAccess();
                ISuperOfficeAccess superOfficeAccess = new SuperOfficeAccess();

                List<string> superOfficeSettings = new List<string>
                {
                    SuperOffice.Constants.SettingKeys.TicketServiceUri,
                    SuperOffice.Constants.SettingKeys.TicketServiceApiKey,
                    SuperOffice.Constants.SettingKeys.SuperOfficeCustomerStateUri,
                    SuperOffice.Constants.SettingKeys.SuperOfficeAppToken
                };
                IDictionary<string, string> settings = duplicationDataAccess.RetrieveDuplicationSettingsByKeys(superOfficeSettings);
                superOfficeAccess.Initialize(settings[SuperOffice.Constants.SettingKeys.TicketServiceUri],
                                             settings[SuperOffice.Constants.SettingKeys.TicketServiceApiKey],
                                             settings[SuperOffice.Constants.SettingKeys.SuperOfficeCustomerStateUri],
                                             settings[SuperOffice.Constants.SettingKeys.SuperOfficeAppToken]);

                Run(jobControllerService, duplicationDataAccess, superOfficeAccess);
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("SuperOfficeUserDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficeUserDuplicator started");

                new SuperOfficeUserDuplicator(jobControllerService, duplicationDataAccess, superOfficeAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("SuperOfficeUserDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficeUserDuplicator ended");
            }
        }
    }

    public class SuperOfficeUserDuplicator : Duplicator
    {
        public SuperOfficeUserDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
            : base(jobControllerService, duplicationDataAccess, superOfficeAccess)
        {
        }

        public async System.Threading.Tasks.Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.SuperOffice.SuperOfficeUserDuplication);
                if (!await TestSuperOfficeAccess(jobHistoryId))
                    return;

                // Fetch from SuperOffice
                IEnumerable<SuperOfficeUser> users = await _superOfficeAccess.RetrieveUsers();

                // Sync with db
                int affected = _duplicationDataAccess.RegisterUsers(users);
                RegisterJobEnd(jobHistoryId, true, String.Format("{0} Users(s) duplicated", affected));

            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}
