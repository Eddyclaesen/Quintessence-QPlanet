using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.JobDefinitions.Replication;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.DataAccess;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.WebApi;
using Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication
{
    public class TeamLeaderEventReplication : ReplicationBase, IJobDefinition
    {
        public void Run(IJobControllerService jobControllerService)
        {
            try
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderEventReplicator started");

                IReplicationDataAccess replicationDataAccess = new ReplicationDataAccess();
                List<string> teamLeaderSettings = new List<string> {TeamLeader.Constants.SettingKeys.ApiGroup, TeamLeader.Constants.SettingKeys.ApiKey, TeamLeader.Constants.SettingKeys.ApiBaseUrl};
                IDictionary<string, string> settings = replicationDataAccess.RetrieveCrmReplicationSettingsByKeys(teamLeaderSettings);
                TeamLeaderAccess.Initialize(settings[TeamLeader.Constants.SettingKeys.ApiGroup], settings[TeamLeader.Constants.SettingKeys.ApiKey], settings[TeamLeader.Constants.SettingKeys.ApiBaseUrl]);

                ITeamLeaderAccess teamLeaderAccess = new TeamLeaderAccess();

                new TeamLeaderEventReplicator(jobControllerService, replicationDataAccess, teamLeaderAccess).RunAsync().Wait();
            }
            catch (Exception ex)
            {
                if(jobControllerService != null)
                    jobControllerService.WriteError("TeamLeaderEventReplicator", ex);
                throw;
            }
            finally
            {
                if (jobControllerService != null)
                    jobControllerService.WriteInformation("TeamLeaderEventReplicator ended");
            }
        }
    }

    public class TeamLeaderEventReplicator : Replicator
    {
        public TeamLeaderEventReplicator(IJobControllerService jobControllerService, IReplicationDataAccess replicationDataAccess, ITeamLeaderAccess teamLeaderAccess)
                : base(jobControllerService, replicationDataAccess, teamLeaderAccess)
        {                
        }

        public async Task RunAsync()
        {
            Guid? jobHistoryId = null;
            try
            {
                jobHistoryId = RegisterJobStart(Constants.JobNames.TeamLeader.TeamLeaderEventReplication);
                if (!await TestTeamLeaderAccess(jobHistoryId))
                    return;

                int batchSize;
                if (!int.TryParse(_replicationDataAccess.RetrieveCrmReplicationSettingByKey(TeamLeader.Constants.SettingKeys.EventBatchSize, "10"), out batchSize))
                    batchSize = 10;
                int maxProcessCount;
                if (!int.TryParse(_replicationDataAccess.RetrieveCrmReplicationSettingByKey(TeamLeader.Constants.SettingKeys.EventMaxProcessCount, "2"), out maxProcessCount))
                    maxProcessCount = 2;

                IEnumerable<CrmReplicationTeamLeaderEvent> teamLeaderEvents = _replicationDataAccess.RetrieveCrmReplicationTeamLeaderEvents(batchSize, maxProcessCount);
                int totalHandled = 0;
                int totalUnHandled = 0;
                List<string> unhandledEventObjects = new List<string>();
                // Avoid loading customfields more than once per type per batch, reduce number of TeamLeader calls :
                Dictionary<string, IEnumerable<CustomField>> customFieldsPerTargetObject = new Dictionary<string, IEnumerable<CustomField>>();
 
                foreach (CrmReplicationTeamLeaderEvent teamLeaderEvent in teamLeaderEvents)
                {
                    bool handled = false;
                    bool skipEvent = false;
                    try
                    {
                        skipEvent = unhandledEventObjects.Contains(BuildTeamLeaderEventKey(teamLeaderEvent));
                        if (!skipEvent)
                        {
                            switch (teamLeaderEvent.EventType)
                            {
                                // Project
                                case TeamLeader.Constants.EventTypes.ProjectAdded:
                                case TeamLeader.Constants.EventTypes.ProjectEdited:
                                    handled = await HandleTeamLeaderProjectAddOrEdit(teamLeaderEvent);
                                    break;
                                case TeamLeader.Constants.EventTypes.ProjectDeleted:
                                    handled = HandleTeamLeaderProjectDelete(teamLeaderEvent);
                                    break;

                                // Contact
                                case TeamLeader.Constants.EventTypes.ContactAdded:
                                case TeamLeader.Constants.EventTypes.ContactEdited:
                                    handled = await HandleTeamLeaderContactAddOrEdit(teamLeaderEvent);
                                    break;
                                case TeamLeader.Constants.EventTypes.ContactDeleted:
                                    handled = HandleTeamLeaderContactDelete(teamLeaderEvent);
                                    break;

                                // Company
                                case TeamLeader.Constants.EventTypes.CompanyAdded:
                                case TeamLeader.Constants.EventTypes.CompanyEdited:
                                    handled = await HandleTeamLeaderCompanyAddOrEdit(teamLeaderEvent);
                                    break;
                                case TeamLeader.Constants.EventTypes.CompanyDeleted:
                                    handled = HandleTeamLeaderCompanyDelete(teamLeaderEvent);
                                    break;

                                // Relation Company - Contact :
                                case TeamLeader.Constants.EventTypes.RelatedContactsUpdated:
                                    handled = await HandleTeamLeaderCompanyContactRelationUpdate(teamLeaderEvent);
                                    break;

                                // Task
                                case TeamLeader.Constants.EventTypes.TaskAdded:
                                case TeamLeader.Constants.EventTypes.TaskEdited:
                                    await EnsureCustomFieldsPerTargetObject(customFieldsPerTargetObject, TeamLeader.Constants.CustomFieldTargetObject.Task);
                                    handled = await HandleTeamLeaderTaskAddOrEdit(teamLeaderEvent, customFieldsPerTargetObject[TeamLeader.Constants.CustomFieldTargetObject.Task]);
                                    break;
                                case TeamLeader.Constants.EventTypes.TaskDeleted:
                                    handled = HandleTeamLeaderTaskDelete(teamLeaderEvent);
                                    break;

                                // TimeTracking
                                case TeamLeader.Constants.EventTypes.TimeTrackingAdded:
                                case TeamLeader.Constants.EventTypes.TimeTrackingEdited:
                                    handled = await HandleTeamLeaderTimeTrackAddOrEdit(teamLeaderEvent);
                                    break;
                                case TeamLeader.Constants.EventTypes.TimeTrackingDeleted:
                                    handled = HandleTeamLeaderTimeTrackDelete(teamLeaderEvent);
                                    break;
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        _replicationDataAccess.RegisterTeamLeaderEventError(teamLeaderEvent.Id, DateTime.UtcNow, ex.ToString());
                        handled = false;  // Swallow this ex, so we can handle the other events.
                    }
                    finally
                    {
                        if (handled)
                        {
                            _replicationDataAccess.DeleteCrmReplicationreamLeaderEventById(teamLeaderEvent.Id);
                            totalHandled++;
                        }
                        else
                        {
                            if(!skipEvent)
                                _replicationDataAccess.UpdateCrmReplicationreamLeaderEventProcessCountById(teamLeaderEvent.Id);
                            totalUnHandled++;
                            unhandledEventObjects.Add(BuildTeamLeaderEventKey(teamLeaderEvent));
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

        private string BuildTeamLeaderEventKey(CrmReplicationTeamLeaderEvent teamLeaderEvent)
        {
            return String.Format("{0}-{1}", 
                String.IsNullOrEmpty(teamLeaderEvent.ObjectType) ? "Unknown" : teamLeaderEvent.ObjectType,
                String.IsNullOrEmpty(teamLeaderEvent.ObjectId) ? "Unknown" : teamLeaderEvent.ObjectId);
        }

        private async Task EnsureCustomFieldsPerTargetObject(Dictionary<string, IEnumerable<CustomField>> customFieldsPerEventType, string targetObject)
        {
            if (!customFieldsPerEventType.ContainsKey(targetObject))
            {
                var taskCustomFields = await _teamLeaderAccess.RetrieveCustomFieldsByTargetObject(targetObject);
                customFieldsPerEventType.Add(targetObject, taskCustomFields);
            } 
        }

        #region Project

        private async Task<bool> HandleTeamLeaderProjectAddOrEdit(CrmReplicationTeamLeaderEvent teamLeaderEvent)
        {
            bool handled = false;

            int teamLeaderProjectId;
            if (int.TryParse(teamLeaderEvent.ObjectId, out teamLeaderProjectId))
            {
                var projectCustomFields = await _teamLeaderAccess.RetrieveCustomFieldsByTargetObject(TeamLeader.Constants.CustomFieldTargetObject.Project);
                Project project = await _teamLeaderAccess.RetrieveProjectByTeamLeaderId(teamLeaderProjectId, projectCustomFields);
 
                _replicationDataAccess.EnrichTeamLeaderProjectsWithStatusId(new List<Project>{project});

                IEnumerable<ProjectUser> projectUsers = await _teamLeaderAccess.RetrieveProjectUsers(teamLeaderProjectId);
                _replicationDataAccess.EnrichTeamLeaderProjectWithAssociateId(project, projectUsers);
                _replicationDataAccess.EnrichTeamLeaderProjectsWithCompanyId(new List<Project> { project });

                int affected = _replicationDataAccess.SyncFullCrmProject(project);
                handled = affected == 1;
            }
            return handled;
        }

        private bool HandleTeamLeaderProjectDelete(CrmReplicationTeamLeaderEvent teamLeaderEvent)
        {
            bool handled = false;

            int teamLeaderProjectId;
            if (int.TryParse(teamLeaderEvent.ObjectId, out teamLeaderProjectId))
            {
                _replicationDataAccess.DeleteCrmReplicationProjectByTeamLeaderId(teamLeaderProjectId);
                handled = true;
            }
            return handled;
        }
        #endregion

        #region Contact  // Contacten in TeamLeader, zijn Persons in crm : CrmReplicationPerson

        private async Task<bool> HandleTeamLeaderContactAddOrEdit(CrmReplicationTeamLeaderEvent teamLeaderEvent)
        {
            bool handled = false;

            int teamLeaderContactId;
            if (int.TryParse(teamLeaderEvent.ObjectId, out teamLeaderContactId))
            {
                var contactCustomFields = await _teamLeaderAccess.RetrieveCustomFieldsByTargetObject(TeamLeader.Constants.CustomFieldTargetObject.Contact);
                Contact contact = await _teamLeaderAccess.RetrieveContactByTeamLeaderId(teamLeaderContactId, contactCustomFields);

                _replicationDataAccess.EnrichTeamLeaderContactWithCompanyInfo(new List<Contact> { contact });
                _replicationDataAccess.EnrichTeamLeaderContactWithLegacyId(new List<Contact> { contact });

                int affected1 = _replicationDataAccess.SyncFullCrmPerson(contact);
                int affected2 = _replicationDataAccess.SyncFullCrmEmail(contact);
                handled = (affected1 == 1) && (affected2 == 1);

            }
            return handled;
        }

        private bool HandleTeamLeaderContactDelete(CrmReplicationTeamLeaderEvent teamLeaderEvent)
        {
            bool handled = false;

            int teamLeaderContactId;
            if (int.TryParse(teamLeaderEvent.ObjectId, out teamLeaderContactId))
            {
                _replicationDataAccess.DeleteCrmReplicationPersonAndEmailByTeamLeaderId(teamLeaderContactId);
                handled = true;
            }
            return handled;
        }
        #endregion

        #region Company  // Companies in TeamLeader, zijn Contacten in crm : CrmReplicationContact

        private async Task<bool> HandleTeamLeaderCompanyAddOrEdit(CrmReplicationTeamLeaderEvent teamLeaderEvent)
        {
            bool handled = false;

            int teamLeaderContactId;
            if (int.TryParse(teamLeaderEvent.ObjectId, out teamLeaderContactId))
            {
                var companyCustomFields = await _teamLeaderAccess.RetrieveCustomFieldsByTargetObject(TeamLeader.Constants.CustomFieldTargetObject.Company);
                Company company = await _teamLeaderAccess.RetrieveCompanyByTeamLeaderId(teamLeaderContactId, companyCustomFields);

                _replicationDataAccess.EnrichTeamLeaderCompanyWithUserIds(company);

                int affected = _replicationDataAccess.SyncFullCrmContact(company);
                handled = affected == 1;
            }
            return handled;
        }

        private bool HandleTeamLeaderCompanyDelete(CrmReplicationTeamLeaderEvent teamLeaderEvent)
        {
            bool handled = false;

            int teamLeaderContactId;
            if (int.TryParse(teamLeaderEvent.ObjectId, out teamLeaderContactId))
            {
                _replicationDataAccess.DeleteCrmReplicationContactAndEmailByTeamLeaderId(teamLeaderContactId);
                handled = true;
            }
            return handled;
        }
        #endregion

        #region CompanyContacts
        
        private async Task<bool> HandleTeamLeaderCompanyContactRelationUpdate(CrmReplicationTeamLeaderEvent teamLeaderEvent)
        {
            bool handled = false;

            int teamLeaderCompanyId;
            if (int.TryParse(teamLeaderEvent.ObjectId, out teamLeaderCompanyId))
            {
                CrmReplicationContact company = _replicationDataAccess.RetrieveCrmReplicationContactByTeamLeaderId(teamLeaderCompanyId);
                if (company != null)
                {
                    List<CompanyContact> companyContacts = new List<CompanyContact>(await _teamLeaderAccess.RetrieveCompanyRelatedContacts(teamLeaderCompanyId));
                    _replicationDataAccess.EnrichTeamLeaderCompanyContacts(companyContacts);

                    int affected = _replicationDataAccess.SyncFullCrmCompanyContactRelation(company, companyContacts);
                    handled = affected > 1;        
                }

            }
            return handled;
        }
        #endregion

        #region Task

        private async Task<bool> HandleTeamLeaderTaskAddOrEdit(CrmReplicationTeamLeaderEvent teamLeaderEvent, IEnumerable<CustomField> taskCustomFields )
        {
            bool handled = false;

            int teamLeaderTaskId;
            if (int.TryParse(teamLeaderEvent.ObjectId, out teamLeaderTaskId))
            {
                TeamLeaderTask task = await _teamLeaderAccess.RetrieveTaskByTeamLeaderId(teamLeaderTaskId, taskCustomFields);

                if (String.IsNullOrEmpty(task.Code) || task.Code == "0")
                    handled = true;
                else
                {
                    _replicationDataAccess.EnrichTeamLeaderTask(task);

                    IEnumerable<PlannedTask> plannedTasks = await _teamLeaderAccess.RetrievePlannedTasksByProjectIdAndUserId(task.AppointmentStartDateTime.Value, task.AppointmentEndDateTime.Value, task.ProjectId.Value, task.ResponsibleUser);
                    if (plannedTasks != null)
                    {
                        PlannedTask relatedPlannedTask = plannedTasks.FirstOrDefault(p => p.TaskId == task.Id);
                        if (relatedPlannedTask != null && relatedPlannedTask.Duration.HasValue)
                            task.AppointmentStartDateTime = relatedPlannedTask.AppointmentStartDateTime;
                    }


                    int affected = _replicationDataAccess.SyncFullCrmAppointment(task);
                    handled = affected == 1;   
                }
            }
            return handled;
        }

        private bool HandleTeamLeaderTaskDelete(CrmReplicationTeamLeaderEvent teamLeaderEvent)
        {
            bool handled = false;

            int teamLeaderTaskId;
            if (int.TryParse(teamLeaderEvent.ObjectId, out teamLeaderTaskId))
            {
                _replicationDataAccess.DeleteCrmReplicationAppointmentByTeamLeaderId(teamLeaderTaskId);
                handled = true;
            }
            return handled;
        }
        #endregion

        #region timetracking

        private async Task<bool> HandleTeamLeaderTimeTrackAddOrEdit(CrmReplicationTeamLeaderEvent teamLeaderEvent)
        {
            bool handled = false;

            int teamLeaderTimeTrackingId;
            if (int.TryParse(teamLeaderEvent.ObjectId, out teamLeaderTimeTrackingId))
            {
                TimeTracking timeTracking = await _teamLeaderAccess.RetrieveTimeTrackingByTeamLeaderId(teamLeaderTimeTrackingId);
                _replicationDataAccess.EnrichTeamLeaderTimeTracking(timeTracking);

                if (!_replicationDataAccess.IsQPlanetProject(timeTracking.ProjectLegacyId))
                    handled = true;
                else
                {
                    int affected = _replicationDataAccess.SyncFullCrmAppointmentTimeSheet(timeTracking);
                    handled = affected == 1;   
                }
            }
            return handled;
        }

        private bool HandleTeamLeaderTimeTrackDelete(CrmReplicationTeamLeaderEvent teamLeaderEvent)
        {
            bool handled = false;

            int teamLeaderTimeTrackingId;
            if (int.TryParse(teamLeaderEvent.ObjectId, out teamLeaderTimeTrackingId))
            {
                _replicationDataAccess.DeleteCrmReplicationAppointmentTimeSheetByTeamLeaderId(teamLeaderTimeTrackingId);
                handled = true;
            }
            return handled;
        }

        #endregion
    }
}