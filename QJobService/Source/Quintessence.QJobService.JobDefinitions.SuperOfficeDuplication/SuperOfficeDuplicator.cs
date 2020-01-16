using System;
using System.Collections.Generic;
using System.Threading;
using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;

using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice;
using Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.WebApi;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication
{
    public class SuperOfficeDuplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            DateTime startDateTime = DateTime.Now;
            IDuplicationDataAccess duplicationDataAccess = null;

            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficeDuplicator started");

                duplicationDataAccess = new DuplicationDataAccess();
                duplicationDataAccess.TruncateDuplicationTables();

                ISuperOfficeAccess superOfficeAccess = new SuperOfficeAccess();

                List<string> superOfficeSettings = new List<string> { SuperOffice.Constants.SettingKeys.TicketServiceUri, SuperOffice.Constants.SettingKeys.TicketServiceApiKey, SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri, SuperOffice.Constants.SettingKeys.SuperOfficeAppToken };
                IDictionary<string, string> settings = duplicationDataAccess.RetrieveDuplicationSettingsByKeys(superOfficeSettings);
                superOfficeAccess.Initialize(settings[SuperOffice.Constants.SettingKeys.TicketServiceUri], settings[SuperOffice.Constants.SettingKeys.TicketServiceApiKey], settings[SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri], settings[SuperOffice.Constants.SettingKeys.SuperOfficeAppToken]);

                new SuperOfficeDuplicator(jobControllerService, duplicationDataAccess, superOfficeAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("SuperOfficeDuplicator", ex);

                if (duplicationDataAccess != null)
                    duplicationDataAccess.RegisterDuplicationError(DateTime.UtcNow, ex.ToString());
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                {
                    string duration = String.Format("{0} sec", (int)((DateTime.Now - startDateTime).TotalSeconds));
                    jobControllerService.WriteInformation("SuperOfficeDuplicator ended " + duration);
                }
            }
        }
    }

    public class SuperOfficeDuplicator : Duplicator
    {
        public SuperOfficeDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
            : base(jobControllerService, duplicationDataAccess, superOfficeAccess)
        {
        }

        public async System.Threading.Tasks.Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.SuperOffice.SuperOfficeDuplication);
                if (!await TestSuperOfficeAccess(jobHistoryId))
                    return;

                // List methods
                new SuperOfficeUserDuplication().Run(_jobControllerService, _duplicationDataAccess, _superOfficeAccess);
                new SuperOfficePersonDuplication().Run(_jobControllerService, _duplicationDataAccess, _superOfficeAccess);
                new SuperOfficeContactDuplication().Run(_jobControllerService, _duplicationDataAccess, _superOfficeAccess);
                new SuperOfficeProjectDuplication().Run(_jobControllerService, _duplicationDataAccess, _superOfficeAccess);
                new SuperOfficeAppointmentDuplication().Run(_jobControllerService, _duplicationDataAccess, _superOfficeAccess);
                new SuperOfficeSaleDuplication().Run(_jobControllerService, _duplicationDataAccess, _superOfficeAccess);
                Thread.Sleep(2000);
                // Detail Methods
                new SuperOfficeAppointmentDetailDuplication().Run(_jobControllerService, _duplicationDataAccess, _superOfficeAccess);
                new SuperOfficeProjectMemberDuplication().Run(_jobControllerService, _duplicationDataAccess, _superOfficeAccess);

                RegisterJobEnd(jobHistoryId, true, "All duplicated");
            }
            catch (Exception ex)
            {
                _duplicationDataAccess.RegisterDuplicationError(DateTime.UtcNow, ex.ToString());

                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

    }
}
