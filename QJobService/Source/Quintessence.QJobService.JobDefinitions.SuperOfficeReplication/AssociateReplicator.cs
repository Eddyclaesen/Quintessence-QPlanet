using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.WebApi;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication
{
    public class AssociateReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("AssociateReplication started");

                IReplicationDataAccess replicationDataAccess = new ReplicationDataAccess();
                List<string> superOfficeSettings = new List<string> { SuperOffice.Constants.SettingKeys.TicketServiceUri, SuperOffice.Constants.SettingKeys.TicketServiceApiKey, SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri, SuperOffice.Constants.SettingKeys.SuperOfficeAppToken };

                ISuperOfficeAccess superOfficeAccess = new SuperOfficeAccess();
                IDictionary<string, string> settings = replicationDataAccess.RetrieveCrmReplicationSettingsByKeys(superOfficeSettings);
                superOfficeAccess.Initialize(settings[SuperOffice.Constants.SettingKeys.TicketServiceUri], settings[SuperOffice.Constants.SettingKeys.TicketServiceApiKey], settings[SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri], settings[SuperOffice.Constants.SettingKeys.SuperOfficeAppToken]);

                new AssociateReplicator(jobControllerService, replicationDataAccess, superOfficeAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("AssociateReplication", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("AssociateReplication ended");
            }
        }
    }

    public class AssociateReplicator : Replicator
    {
        public AssociateReplicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ISuperOfficeAccess superOfficeAccess)
            : base(jobControllerService, replicationDataAccess, superOfficeAccess)
        {
        }

        public async Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.SuperOffice.AssociateReplication);
                if (!await TestSuperOfficeAccess(jobHistoryId))
                    return;

                // Fetch from SuperOffice
                IEnumerable<User> users = await _superOfficeAccess.RetrieveUsers();

                // Sync with db
                int affected = _replicationDataAccess.SyncCrmAssociates(users);
                RegisterJobEnd(jobHistoryId, true, String.Format("{0} Associate(s) synced", affected));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }
    }
}