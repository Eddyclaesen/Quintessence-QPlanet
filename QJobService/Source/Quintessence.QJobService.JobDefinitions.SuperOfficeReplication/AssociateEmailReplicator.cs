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
    public class AssociateEmailReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("AssociateEmailReplication started");

                IReplicationDataAccess replicationDataAccess = new ReplicationDataAccess();
                List<string> superOfficeSettings = new List<string> { SuperOffice.Constants.SettingKeys.TicketServiceUri, SuperOffice.Constants.SettingKeys.TicketServiceApiKey, SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri, SuperOffice.Constants.SettingKeys.SuperOfficeAppToken };

                ISuperOfficeAccess superOfficeAccess = new SuperOfficeAccess();
                IDictionary<string, string> settings = replicationDataAccess.RetrieveCrmReplicationSettingsByKeys(superOfficeSettings);
                superOfficeAccess.Initialize(settings[SuperOffice.Constants.SettingKeys.TicketServiceUri], settings[SuperOffice.Constants.SettingKeys.TicketServiceApiKey], settings[SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri], settings[SuperOffice.Constants.SettingKeys.SuperOfficeAppToken]);

                new AssociateEmailReplicator(jobControllerService, replicationDataAccess, superOfficeAccess).RunAsync().Wait();                
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("AssociateEmailReplication", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("AssociateEmailReplication ended");
            }
        }
    }

    public class AssociateEmailReplicator : Replicator
    {
        public AssociateEmailReplicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ISuperOfficeAccess superOfficeAccess)
            : base(jobControllerService, replicationDataAccess, superOfficeAccess)
        {
        }

        public async Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.SuperOffice.AssociateEmailReplication);
                if (!await TestSuperOfficeAccess(jobHistoryId))
                    return;

                // Fetch from TeamLeader
                List<User> users = new List<User>(await _superOfficeAccess.RetrieveUsers());
                _replicationDataAccess.EnrichSuperOfficeUsersWithAssociateId(users);

                // Sync with db
                int affected = _replicationDataAccess.SyncCrmAssociateEmails(users);
                RegisterJobEnd(jobHistoryId, true, String.Format("{0} Associate email(s) synced", affected));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }
    }
}