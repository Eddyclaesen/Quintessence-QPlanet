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
    public class ProjectIdReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("ProjectIdReplication started");

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

                new ProjectIdReplicator(jobControllerService, replicationDataAccess, superOfficeAccess).RunAsync().Wait();

            }
            catch (Exception ex)
            {
                if (jobControllerService != null)
                    jobControllerService.WriteError("ProjectIdReplication", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("ProjectIdReplication ended");
            }

        }
    }

    public class ProjectIdReplicator : Replicator
    {
        public ProjectIdReplicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ISuperOfficeAccess superOfficeAccess)
            : base(jobControllerService, replicationDataAccess, superOfficeAccess)
        {                
        }

        public async Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {                
                jobHistoryId = RegisterJobStart(Constants.JobNames.SuperOffice.ProjectIdReplication);
                if (!await TestSuperOfficeAccess(jobHistoryId))
                    return;
 
                //List<CustomField> projectCustomFields = new List<CustomField>(await _teamLeaderAccess.RetrieveCustomFieldsByTargetObject(TeamLeader.Constants.CustomFieldTargetObject.Project));

                int pageSize = 100;
                int pageNumber = 0;
                int affected = 0;
                bool projectsFetched;
                bool currentBookYearOnly = false;
                do
                {
                    IEnumerable<Project> superOfficeProjects = await _superOfficeAccess.RetrieveProjects(pageSize, pageNumber);
                    projectsFetched = false;

                    List<Project> projects = new List<Project>();
                    foreach (var project in superOfficeProjects)
                    {
                        projectsFetched = true;                        
                        //if (!currentBookYearOnly || (currentBookYearOnly && project.IsOfCurrentBookYear()))
                            projects.Add(project);
                    }

                    // Sync with db
                    affected += _replicationDataAccess.SyncCrmProjectIds(projects);
                    pageNumber++;

                } while (projectsFetched);

                RegisterJobEnd(jobHistoryId, true, String.Format("{0} ProjectId(s) synced", affected));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }
    }
}