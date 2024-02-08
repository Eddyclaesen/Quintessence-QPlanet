using System;
using System.Linq;
using System.Collections.Generic;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;

using SOC = Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice;

using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models;
using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.WebApi;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication
{
    public class SuperOfficePersonDuplication : ReplicationBase, IJobDefinition
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
                    jobControllerService.WriteError("SuperOfficePersonDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficePersonDuplicator started");

                new SuperOfficePersonDuplicator(jobControllerService, duplicationDataAccess, superOfficeAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("SuperOfficePersonDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficePersonDuplicator ended");
            }
        }
    }

    public class SuperOfficePersonDuplicator : Duplicator
    {
        public SuperOfficePersonDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
            : base(jobControllerService, duplicationDataAccess, superOfficeAccess)
        {
        }

        public async System.Threading.Tasks.Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.SuperOffice.SuperOfficePersonDuplication);
                if (!await TestSuperOfficeAccess(jobHistoryId))
                    return;


                int batchSize;
                if (!int.TryParse(_duplicationDataAccess.RetrieveDuplicationSettingsByKey(SOC.Constants.SettingKeys.DuplicatorBatchSize, "100"), out batchSize))
                    batchSize = 100;

                int pageNumber = 0;
                int affected = 0;
                bool contactsFetched;
                do
                {
                    IEnumerable<SuperOfficePerson> superOfficePersons = await _superOfficeAccess.RetrievePersons(batchSize, pageNumber);

                    List<SuperOfficePerson> persons = new List<SuperOfficePerson>(superOfficePersons);
                    contactsFetched = persons.Any();

                    // Sync with db
                    affected += _duplicationDataAccess.RegisterPersons(persons);
                    pageNumber++;

                } while (contactsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} Contact(s) duplicated", affected));

            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}
