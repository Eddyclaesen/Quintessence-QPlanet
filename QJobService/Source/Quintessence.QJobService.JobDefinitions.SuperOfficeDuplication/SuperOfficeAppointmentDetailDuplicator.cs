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
    public class SuperOfficeAppointmentDetailDuplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                // Resolve dependencies
                IDuplicationDataAccess duplicationDataAccess = new DuplicationDataAccess();
                ISuperOfficeAccess superOfficeAccess = new SuperOfficeAccess();

                List<string> superOfficeSettings = new List<string> { SuperOffice.Constants.SettingKeys.TicketServiceUri, SuperOffice.Constants.SettingKeys.TicketServiceApiKey, SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri, SuperOffice.Constants.SettingKeys.SuperOfficeAppToken };
                IDictionary<string, string> settings = duplicationDataAccess.RetrieveDuplicationSettingsByKeys(superOfficeSettings);
                superOfficeAccess.Initialize(settings[SuperOffice.Constants.SettingKeys.TicketServiceUri], settings[SuperOffice.Constants.SettingKeys.TicketServiceApiKey], settings[SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri], settings[SuperOffice.Constants.SettingKeys.SuperOfficeAppToken]);

                Run(jobControllerService, duplicationDataAccess, superOfficeAccess);
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("SuperOfficeAppointmentDetailDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficeAppointmentDetailDuplicator started");

                new SuperOfficeAppointmentDetailDuplicator(jobControllerService, duplicationDataAccess, superOfficeAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("SuperOfficeAppointmentDetailDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficeAppointmentDetailDuplicator ended");
            }
        }
    }

    public class SuperOfficeAppointmentDetailDuplicator : Duplicator
    {
        public SuperOfficeAppointmentDetailDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
            : base(jobControllerService, duplicationDataAccess, superOfficeAccess)
        {
        }

        public async System.Threading.Tasks.Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.SuperOffice.SuperOfficeAppointmentDetailDuplication);
                if (!await TestSuperOfficeAccess(jobHistoryId))
                    return;

                // Fetch from SuperOffice
                int batchSize;
                if (!int.TryParse(_duplicationDataAccess.RetrieveDuplicationSettingsByKey(SOC.Constants.SettingKeys.DuplicatorBatchSize, "100"), out batchSize))
                    batchSize = 100;

                int fromAppointmentId = -1;
                int affected = 0;
                bool appointmentsFetched= false;
                do
                {
                    List<Appointment> appointments = _duplicationDataAccess.RetrieveAppointmentIds(fromAppointmentId, batchSize).ToList();
                    appointmentsFetched = appointments.Any();
                    if (appointmentsFetched)
                    {                        
                        IEnumerable<int> superOfficeAppointmentIds = appointments.Select(a => a.AppointmentId).ToList();
                        fromAppointmentId = superOfficeAppointmentIds.Last();

                        IEnumerable<SuperOfficeAppointmentDetail> superOfficeAppointmentDetails = await _superOfficeAccess.RetrieveAppointmentDetailsByIds(superOfficeAppointmentIds);

                        // Sync with db
                        affected += _duplicationDataAccess.EnrichAppointments(appointments, superOfficeAppointmentDetails);

                    }
                } while (appointmentsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} AppointmentDetail(s) duplicated", affected));

            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }
    }
}
