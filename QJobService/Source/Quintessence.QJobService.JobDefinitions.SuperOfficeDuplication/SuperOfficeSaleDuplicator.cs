using System;
using System.Collections.Generic;
using System.Linq;
using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;

using SOC = Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice;

using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models;
using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.WebApi;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication
{
    public class SuperOfficeSaleDuplication : ReplicationBase, IJobDefinition
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
                    jobControllerService.WriteError("SuperOfficeSaleDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficeSaleDuplicator started");

                new SuperOfficeSaleDuplicator(jobControllerService, duplicationDataAccess, superOfficeAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("SuperOfficeSaleDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficeSaleDuplicator ended");
            }
        }
    }

    public class SuperOfficeSaleDuplicator : Duplicator
    {
        public SuperOfficeSaleDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
            : base(jobControllerService, duplicationDataAccess, superOfficeAccess)
        {
        }

        public async System.Threading.Tasks.Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.SuperOffice.SuperOfficeSaleDuplication);
                if (!await TestSuperOfficeAccess(jobHistoryId))
                    return;

                // Fetch from SuperOffice
                int batchSize;
                if (!int.TryParse(_duplicationDataAccess.RetrieveDuplicationSettingsByKey(SOC.Constants.SettingKeys.DuplicatorBatchSize, "100"), out batchSize))
                    batchSize = 100;

                int pageNumber = 0;
                int affected = 0;
                bool contactsFetched;
                do
                {
                    IEnumerable<SuperOfficeSale> superOfficeSales = await _superOfficeAccess.RetrieveSales(batchSize, pageNumber);

                    List<SuperOfficeSale> sales = new List<SuperOfficeSale>(superOfficeSales);
                    contactsFetched = sales.Any();

                    // Sync with db
                    affected += _duplicationDataAccess.RegisterSales(sales);
                    pageNumber++;

                } while (contactsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} Sale(s) duplicated", affected));

            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }
    }
}
