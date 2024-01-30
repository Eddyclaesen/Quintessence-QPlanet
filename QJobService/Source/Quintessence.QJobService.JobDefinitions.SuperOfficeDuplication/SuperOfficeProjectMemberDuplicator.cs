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
    public class SuperOfficeProjectMemberDuplication : ReplicationBase, IJobDefinition
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
                    jobControllerService.WriteError("SuperOfficeProjectMemberDuplicator", ex);
                throw;
            }
        }

        internal void Run(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficeProjectMemberDuplicator started");

                new SuperOfficeProjectMemberDuplicator(jobControllerService, duplicationDataAccess, superOfficeAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("SuperOfficeProjectMemberDuplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficeProjectMemberDuplicator ended");
            }
        }
    }

    public class SuperOfficeProjectMemberDuplicator : Duplicator
    {
        public SuperOfficeProjectMemberDuplicator(IJobControllerService jobControllerService, IDuplicationDataAccess duplicationDataAccess, ISuperOfficeAccess superOfficeAccess)
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

                int fromProjectId = -1;
                int affected = 0;
                bool projectsFetched = false;
                do
                {
                    List<int> projects = _duplicationDataAccess.RetrieveProjectIds(fromProjectId, batchSize).ToList();
                    projectsFetched = projects.Any();
                    if (projectsFetched)
                    {
                        fromProjectId = projects.Last();

                        List<SuperOfficeProjectMember> projectMembers = new List<SuperOfficeProjectMember>();
                        foreach (int projectId in projects)
                        {
                            IEnumerable<SuperOfficeProjectMember> superOfficeProjectMembers = await _superOfficeAccess.RetrieveProjectMembersByProjectId(projectId);
                            projectMembers.AddRange(superOfficeProjectMembers);
                        }

                        // Sync with db
                        affected += _duplicationDataAccess.RegisterProjectMembers(projectMembers);

                    }
                } while (projectsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} ProjectMember(s) duplicated", affected));

            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }
    }
}
