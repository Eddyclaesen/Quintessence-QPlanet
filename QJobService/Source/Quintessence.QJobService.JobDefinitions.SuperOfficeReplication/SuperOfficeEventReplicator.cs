using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.WebApi;
using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication
{
    public class SuperOfficeEventReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficeEventReplicator started");

                IReplicationDataAccess replicationDataAccess = new ReplicationDataAccess();
                ISuperOfficeAccess superOfficeAccess = new SuperOfficeAccess();
                
                List<string> superOfficeSettings = new List<string> { SuperOffice.Constants.SettingKeys.TicketServiceUri, SuperOffice.Constants.SettingKeys.TicketServiceApiKey, SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri, SuperOffice.Constants.SettingKeys.SuperOfficeAppToken };                
                IDictionary<string, string> settings = replicationDataAccess.RetrieveCrmReplicationSettingsByKeys(superOfficeSettings);
                superOfficeAccess.Initialize(settings[SuperOffice.Constants.SettingKeys.TicketServiceUri], settings[SuperOffice.Constants.SettingKeys.TicketServiceApiKey], settings[SuperOffice.Constants.SettingKeys.SuperOfficeBaseUri], settings[SuperOffice.Constants.SettingKeys.SuperOfficeAppToken]);

                new TeamLeaderEventReplicator(jobControllerService, replicationDataAccess, superOfficeAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if(jobControllerService != null)
                    jobControllerService.WriteError("SuperOfficeEventReplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("SuperOfficeEventReplicator ended");
            }
        }
    }

    public class TeamLeaderEventReplicator : Replicator
    {
        public TeamLeaderEventReplicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ISuperOfficeAccess superOfficeAccess)
            : base(jobControllerService, replicationDataAccess, superOfficeAccess)
        {                
        }

        public async Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.SuperOffice.SuperOfficeEventReplication);
                if (!await TestSuperOfficeAccess(jobHistoryId))
                    return;

                int batchSize;
                if (!int.TryParse(_replicationDataAccess.RetrieveCrmReplicationSettingByKey(SuperOffice.Constants.SettingKeys.EventBatchSize, "10"), out batchSize))
                    batchSize = 10;
                int maxProcessCount;
                if (!int.TryParse(_replicationDataAccess.RetrieveCrmReplicationSettingByKey(SuperOffice.Constants.SettingKeys.EventMaxProcessCount, "2"), out maxProcessCount))
                    maxProcessCount = 2;

                IEnumerable<CrmReplicationSuperOfficeEvent> superOfficeEvents = _replicationDataAccess.RetrieveCrmReplicationSuperOfficeEvents(batchSize, maxProcessCount);
                int totalHandled = 0;
                int totalUnHandled = 0;
                List<string> unhandledEventObjects = new List<string>();
                // Avoid loading customfields more than once per type per batch, reduce number of TeamLeader calls :
                //Dictionary<string, IEnumerable<CustomField>> customFieldsPerTargetObject = new Dictionary<string, IEnumerable<CustomField>>();

                foreach (CrmReplicationSuperOfficeEvent superOfficeEvent in superOfficeEvents)
                {
                    bool handled = false;
                    bool skipEvent = false;
                    try
                    {
                        skipEvent = unhandledEventObjects.Contains(BuildSuperOfficeEventKey(superOfficeEvent));
                        if (!skipEvent)
                        {
                            switch (superOfficeEvent.EventType)
                            {
                                // Project
                                case SuperOffice.Constants.EventTypes.ProjectAdded:
                                case SuperOffice.Constants.EventTypes.ProjectEdited:
                                    handled = await HandleSuperOfficeProjectAddOrEdit(superOfficeEvent);
                                    break;
                                case SuperOffice.Constants.EventTypes.ProjectDeleted:
                                    handled = HandleSuperOfficeProjectDelete(superOfficeEvent);
                                    break;

                                // Company
                                case SuperOffice.Constants.EventTypes.ContactAdded:
                                case SuperOffice.Constants.EventTypes.ContactEdited:
                                    handled = await HandleSuperOfficeContactAddOrEdit(superOfficeEvent);
                                    break;
                                case SuperOffice.Constants.EventTypes.ContactDeleted:
                                    handled = HandleSuperOfficeContactDelete(superOfficeEvent);
                                    break;

                                // Person
                                case SuperOffice.Constants.EventTypes.PersonAdded:
                                case SuperOffice.Constants.EventTypes.PersonEdited:
                                    handled = await HandleSuperOfficePersonAddOrEdit(superOfficeEvent);
                                    break;
                                case SuperOffice.Constants.EventTypes.PersonDeleted:
                                    handled = HandleSuperOfficePersonDelete(superOfficeEvent);
                                    break;

                                // Appointment
                                case SuperOffice.Constants.EventTypes.ActivityAdded:
                                case SuperOffice.Constants.EventTypes.ActivityEdited:
                                    handled = await HandleSuperOfficeActivityAddOrEdit(superOfficeEvent);
                                    break;
                                case SuperOffice.Constants.EventTypes.ActivityDeleted:
                                    handled = HandleSuperOfficeActivityDelete(superOfficeEvent);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _replicationDataAccess.RegisterSuperOfficeEventError(superOfficeEvent.Id, DateTime.UtcNow, ex.ToString());
                        handled = false;  // Swallow this ex, so we can handle the other events.
                    }
                    finally
                    {
                        if (handled)
                        {
                            _replicationDataAccess.DeleteCrmReplicationSuperOfficeEventById(superOfficeEvent.Id);
                            totalHandled++;
                        }
                        else
                        {
                            if (!skipEvent)
                                _replicationDataAccess.UpdateCrmReplicationSuperOfficeProcessCountById(superOfficeEvent.Id);
                            totalUnHandled++;
                            unhandledEventObjects.Add(BuildSuperOfficeEventKey(superOfficeEvent));
                        }
                    }
                }

                RegisterJobEnd(jobHistoryId, true, totalUnHandled > 0 ? String.Format("{0} Event(s) handled, {1} unhanled", totalHandled, totalUnHandled) : String.Format("{0} Event(s) handled", totalHandled));
            }
            catch (Exception ex)
            {
                RegisterJobEnd(jobHistoryId, false, ex.ToString());
                throw;
            }
        }

        private string BuildSuperOfficeEventKey(CrmReplicationSuperOfficeEvent superOfficeEvent)
        {
            return String.Format("{0}-{1}",
                String.IsNullOrEmpty(superOfficeEvent.ObjectType) ? "Unknown" : superOfficeEvent.ObjectType,
                String.IsNullOrEmpty(superOfficeEvent.ObjectId) ? "Unknown" : superOfficeEvent.ObjectId);
        }

        #region Project

        private async Task<bool> HandleSuperOfficeProjectAddOrEdit(CrmReplicationSuperOfficeEvent superOfficeEvent)
        {
            bool handled = false;

            int superOfficeProjectId;
            if (int.TryParse(superOfficeEvent.ObjectId, out superOfficeProjectId))
            {
                Project project = await _superOfficeAccess.RetrieveProjectBySuperOfficeId(superOfficeProjectId);
                if (project != null)
                {
                    _replicationDataAccess.EnrichSuperOfficeProjectsWithStatusId(new List<Project> {project});

                    _replicationDataAccess.EnrichSuperOfficeProjectWithAssociateId(project);
                    _replicationDataAccess.EnrichSuperOfficeProjectsWithCompanyId(new List<Project> {project});

                    int affected = _replicationDataAccess.SyncFullCrmProject(project);
                    handled = affected == 1;
                }
            }
            return handled;
        }

        private bool HandleSuperOfficeProjectDelete(CrmReplicationSuperOfficeEvent superOfficeEvent)
        {
            bool handled = false;

            int superOfficeProjectId;
            if (int.TryParse(superOfficeEvent.ObjectId, out superOfficeProjectId))
            {
                _replicationDataAccess.DeleteCrmReplicationProjectBySuperOfficeId(superOfficeProjectId);
                handled = true;
            }
            return handled;
        }
        #endregion

        #region Company  // Companies in SuperOffice (contacts), zijn Contacten in crm : CrmReplicationContact

        private async Task<bool> HandleSuperOfficeContactAddOrEdit(CrmReplicationSuperOfficeEvent superOfficeEvent)
        {
            bool handled = false;

            int superOfficeContactId;
            if (int.TryParse(superOfficeEvent.ObjectId, out superOfficeContactId))
            {
                Company company = await _superOfficeAccess.RetrieveCompanyBySuperOfficeId(superOfficeContactId);
                if (company != null)
                {
                    _replicationDataAccess.EnrichSuperOfficeCompanyWithCrmIds(company);

                    int affected = _replicationDataAccess.SyncFullCrmContact(company);
                    handled = affected == 1;
                }
            }
            return handled;
        }

        private bool HandleSuperOfficeContactDelete(CrmReplicationSuperOfficeEvent superOfficeEvent)
        {
            bool handled = false;

            int superOfficeContactId;
            if (int.TryParse(superOfficeEvent.ObjectId, out superOfficeContactId))
            {
                _replicationDataAccess.DeleteCrmReplicationContactAndEmailBySuperOfficeId(superOfficeContactId);
                handled = true;
            }
            return handled;
        }
        #endregion

        #region Person

        private async Task<bool> HandleSuperOfficePersonAddOrEdit(CrmReplicationSuperOfficeEvent superOfficeEvent)
        {
            bool handled = false;

            int superOfficePersonId;
            if (int.TryParse(superOfficeEvent.ObjectId, out superOfficePersonId))
            {
                Person person = await _superOfficeAccess.RetrievePersonBySuperOfficeId(superOfficePersonId);
                if (person != null)
                {
                    _replicationDataAccess.EnrichSuperOfficePersonWithCompanyInfo(new List<Person> {person});
                    _replicationDataAccess.EnrichSuperOfficePersonWithCrmReplicationPersonId(new List<Person> {person});

                    int affected1 = _replicationDataAccess.SyncFullCrmPerson(person);
                    int affected2 = _replicationDataAccess.SyncFullCrmEmail(person);
                    handled = (affected1 == 1) && (affected2 == 1);
                }
            }
            return handled;
        }

        private bool HandleSuperOfficePersonDelete(CrmReplicationSuperOfficeEvent superOfficeEvent)
        {
            bool handled = false;

            int superOfficePersonId;
            if (int.TryParse(superOfficeEvent.ObjectId, out superOfficePersonId))
            {
                _replicationDataAccess.DeleteCrmReplicationPersonAndEmailBySuperOfficeId(superOfficePersonId);
                handled = true;
            }
            return handled;
        }
        #endregion

        #region Appointment

        private async Task<bool> HandleSuperOfficeActivityAddOrEdit(CrmReplicationSuperOfficeEvent superOfficeEvent)
        {
            bool handled = false;

            int superOfficeAppointmentId;
            if (int.TryParse(superOfficeEvent.ObjectId, out superOfficeAppointmentId))
            {
                Appointment appointment = await _superOfficeAccess.RetrieveAppointmentBySuperOfficeId(superOfficeAppointmentId);
                if (appointment != null)
                {
                    bool projectMustBeSaved = !String.IsNullOrEmpty(appointment.Code) && appointment.Code != "0";
                    if (projectMustBeSaved || appointment.IsCompleted)
                    {
                        _replicationDataAccess.EnrichSuperOfficeAppointment(appointment);

                        if (projectMustBeSaved)
                        {
                            int affected = _replicationDataAccess.SyncFullCrmAppointment(appointment);
                            handled = affected == 1;
                        }
                        else
                            handled = true;

                        if (appointment.IsCompleted && appointment.ProjectId.HasValue)
                            _replicationDataAccess.SyncFullCrmAppointmentTimeSheet(appointment);
                        else
                            _replicationDataAccess.DeleteCrmReplicationAppointmentTimeSheetBySuperOfficeId(appointment.Id.Value);
                    }
                    else
                        handled = true;
                }
            }
            return handled;
        }

        private bool HandleSuperOfficeActivityDelete(CrmReplicationSuperOfficeEvent superOfficeEvent)
        {
            bool handled = false;

            int superOfficeAppointmentId;
            if (int.TryParse(superOfficeEvent.ObjectId, out superOfficeAppointmentId))
            {
                _replicationDataAccess.DeleteCrmReplicationAppointmentBySuperOfficeId(superOfficeAppointmentId);
                handled = true;
                _replicationDataAccess.DeleteCrmReplicationAppointmentTimeSheetBySuperOfficeId(superOfficeAppointmentId);
            }
            return handled;
        }
        #endregion
    }
}