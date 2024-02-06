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
    public class PersonIdReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("PersonIdReplication started");

                IReplicationDataAccess replicationDataAccess = new ReplicationDataAccess();
                ISuperOfficeAccess superOfficeAccess = new SuperOfficeAccess();

                List<string> superOfficeSettings = new List<string>
                {
                    SuperOffice.Constants.SettingKeys.TicketServiceUri,
                    SuperOffice.Constants.SettingKeys.TicketServiceApiKey,
                    SuperOffice.Constants.SettingKeys.SuperOfficeCustomerStateUri,
                    SuperOffice.Constants.SettingKeys.SuperOfficeAppToken
                };
                IDictionary<string, string> settings = replicationDataAccess.RetrieveCrmReplicationSettingsByKeys(superOfficeSettings);
                superOfficeAccess.Initialize(settings[SuperOffice.Constants.SettingKeys.TicketServiceUri],
                                             settings[SuperOffice.Constants.SettingKeys.TicketServiceApiKey],
                                             settings[SuperOffice.Constants.SettingKeys.SuperOfficeCustomerStateUri],
                                             settings[SuperOffice.Constants.SettingKeys.SuperOfficeAppToken]);

                new PersonIdReplicator(jobControllerService, replicationDataAccess, superOfficeAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("PersonIdReplication", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("PersonIdReplication ended");
            }
        }
    }

    public class PersonIdReplicator : Replicator
    {
        public PersonIdReplicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ISuperOfficeAccess superOfficeAccess)
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

                // Fetch from TeamLeader
                //List<CustomField> contactCustomFields = new List<CustomField>(await _teamLeaderAccess.RetrieveCustomFieldsByTargetObject(TeamLeader.Constants.CustomFieldTargetObject.Contact));

                int pageSize = 100;
                int pageNumber = 0;
                int affected = 0;
                bool personsFetched;
                do
                {
                    IEnumerable<PersonOverview> superOfficePersons = await _superOfficeAccess.RetrievePersons(pageSize, pageNumber);

                    List<PersonOverview> persons = new List<PersonOverview>(superOfficePersons);
                    personsFetched = persons.Any();

                    // Sync with db
                    affected += _replicationDataAccess.SyncCrmPersonIds(persons);
                    pageNumber++;

                } while (personsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} PersonId(s) synced", affected));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }
    }
}
