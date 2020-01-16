using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.WebApi;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication
{
    public class ContactIdReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("ContactIdReplication started");

                IReplicationDataAccess replicationDataAccess = new ReplicationDataAccess();
                ISuperOfficeAccess superOfficeAccess = new SuperOfficeAccess();

                List<string> superOfficeSettings = new List<string> { SuperOffice.Constants.SettingKeys.TicketServiceUri, SuperOffice.Constants.SettingKeys.TicketServiceApiKey, SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri, SuperOffice.Constants.SettingKeys.SuperOfficeAppToken };
                IDictionary<string, string> settings = replicationDataAccess.RetrieveCrmReplicationSettingsByKeys(superOfficeSettings);
                superOfficeAccess.Initialize(settings[SuperOffice.Constants.SettingKeys.TicketServiceUri], settings[SuperOffice.Constants.SettingKeys.TicketServiceApiKey], settings[SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri], settings[SuperOffice.Constants.SettingKeys.SuperOfficeAppToken]);
                
                new ContactIdReplicator(jobControllerService, replicationDataAccess, superOfficeAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("ContactIdReplication", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("ContactIdReplication ended");
            }
        }
    }

    public class ContactIdReplicator : Replicator
    {
        public ContactIdReplicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ISuperOfficeAccess superOfficeAccess)
            : base(jobControllerService, replicationDataAccess, superOfficeAccess)
        {
        }

        public async Task RunAsync()
        {            
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.SuperOffice.ContactIdReplication);
                if (!await TestSuperOfficeAccess(jobHistoryId))
                    return;

                // Fetch from SuperOffice
                //List<CustomField> companyCustomFields = new List<CustomField>(await superOfficeAccess.RetrieveCustomFieldsByTargetObject(TeamLeader.Constants.CustomFieldTargetObject.Company));

                int pageSize = 100;
                int pageNumber = 0;
                int affected = 0;
                bool companiesFetched;
                do
                {
                    IEnumerable<Company> teamLeaderCompanies = await _superOfficeAccess.RetrieveCompanies(pageSize, pageNumber);

                    List<Company> companies = new List<Company>(teamLeaderCompanies);
                    companiesFetched = companies.Any();

                    // Sync with db
                    affected += _replicationDataAccess.SyncCrmContactIds(companies);
                    pageNumber++;

                } while (companiesFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} ContactId(s) synced", affected));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }
    }
}