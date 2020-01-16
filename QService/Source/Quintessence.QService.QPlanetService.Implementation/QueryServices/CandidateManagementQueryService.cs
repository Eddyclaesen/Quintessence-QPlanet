using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.SharePoint.Client;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Microsoft.Practices.Unity;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class CandidateManagementQueryService : SecuredUnityServiceBase, ICandidateManagementQueryService
    {
        #region Retrieve methods
        public CandidateView RetrieveCandidate(Guid id)
        {
            LogTrace(string.Format("Retrieve candidate (id = '{0}')", id));

            return Execute(() =>
            {
                var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                return repository.RetrieveCandidateDetail(id);
            });
        }
        #endregion

        #region List methods
        public List<CandidateView> ListCandidates()
        {
            LogTrace("List candidates.");

            return Execute(() =>
            {
                ////ValidateAuthorization(OperationType.DIMSEARCH);

                var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                var candidates = repository.ListCandidates();

                return candidates;
            });
        }

        public List<CandidateView> ListCandidatesByFullName(ListCandidatesByFullNameRequest listCandidatesByFullNameRequest)
        {
            LogTrace("List candidates by full name.");

            return Execute(() =>
                {
                    ////ValidateAuthorization(ActivityType.DIMSEARCH);

                    var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                    return repository.ListCandidatesByFullName(listCandidatesByFullNameRequest.FirstName,
                                                               listCandidatesByFullNameRequest.LastName);
                });
        }

        public RetrieveDayProgramDashboardResponse RetrieveDayProgramDashboard(RetrieveDayProgramDashboardRequest request)
        {
            LogTrace("Retrieve the dayprogram for the {0}", request.Date.ToShortDateString());

            return Execute(() =>
            {
                var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                var response = new RetrieveDayProgramDashboardResponse();

                response.Date = request.Date;

                response.ProgramComponents = repository.ListProgramComponents(request.Date);

                return response;
            });
        }

        public List<ProgramComponentView> RetrieveAssessmentRoomProgram(Guid assessmentRoomId, DateTime dateTime)
        {
            LogTrace("Retrieve the dayprogram for the assessment room {0} on {1}", assessmentRoomId.ToString(), dateTime.ToShortDateString());

            return Execute(() =>
            {
                var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                var programComponents = repository.ListProgramComponentsByAssessmentRoom(assessmentRoomId, dateTime);

                return programComponents;
            });
        }

        public List<ProgramComponentView> ListProgramComponentsByCandidate(Guid projectCandidateId)
        {
            LogTrace("List program components for project candidate with id {0}", projectCandidateId);

            return Execute(() =>
            {
                var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                var programComponents = repository.ListProgramComponentsByCandidate(projectCandidateId);

                return programComponents;
            });
        }

        public ProgramComponentView RetrieveProgramComponent(Guid id)
        {
            LogTrace("Retrieve program component {0}", id);

            return Execute(() =>
            {
                var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                var programComponent = repository.Retrieve<ProgramComponentView>(id);

                return programComponent;
            });
        }

        public List<Guid> CheckForProgramComponentCollisions(int officeid, DateTime dateTime)
        {
            LogTrace("Checking for collisions for office {0} on {1}", officeid, dateTime.ToShortDateString());

            return Execute(() =>
            {
                var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                var programComponents = repository.ListProgramComponentsByOfficeId(officeid, dateTime);

                var programComponentIds = new List<Guid>();

                foreach (var programComponent in programComponents)
                {
                    foreach (var possibleCollisionProgramComponent in programComponents
                        .Where(pc => pc.Id != programComponent.Id)
                        .Where(pc => (programComponent.LeadAssessorUserId.HasValue && pc.LeadAssessorUserId.HasValue && programComponent.LeadAssessorUserId == pc.LeadAssessorUserId)
                                     || (programComponent.LeadAssessorUserId.HasValue && pc.CoAssessorUserId.HasValue && programComponent.LeadAssessorUserId == pc.CoAssessorUserId)
                                     || (programComponent.CoAssessorUserId.HasValue && pc.LeadAssessorUserId.HasValue && programComponent.CoAssessorUserId == pc.LeadAssessorUserId)
                                     || (programComponent.CoAssessorUserId.HasValue && pc.CoAssessorUserId.HasValue && programComponent.CoAssessorUserId == pc.CoAssessorUserId)))
                    {
                        if ((programComponent.Start >= possibleCollisionProgramComponent.Start && programComponent.Start < possibleCollisionProgramComponent.End)
                            || (programComponent.End > possibleCollisionProgramComponent.Start && programComponent.End <= possibleCollisionProgramComponent.End))
                        {
                            programComponentIds.Add(programComponent.Id);
                        }
                    }
                }

                return programComponentIds.Distinct().ToList();
            });
        }

        public List<ProgramComponentSpecialView> ListSpecialEvents()
        {
            LogTrace("List special events");

            return Execute(() =>
            {
                var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                var programComponent = repository.List<ProgramComponentSpecialView>();

                return programComponent;
            });
        }

        public Dictionary<string, List<string>> CheckForUnplannedEvents(int officeId, DateTime dateTime)
        {
            LogTrace("Check for unplanned events for office {0} on {1}", officeId, dateTime.ToShortDateString());

            return Execute(() =>
            {
                var repository = Container.Resolve<ICandidateManagementQueryRepository>();
                var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();
                var customerRelationshipManagementQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

                var programComponents = repository.ListProgramComponentsByOfficeId(officeId, dateTime);

                var simulationCombinationIds = new Dictionary<Guid, List<Guid>>();
                var projectCandidateCategoryDetailTypeIds = new Dictionary<Guid, List<Guid>>();

                foreach (var programComponent in programComponents)
                {
                    if (programComponent.SimulationCombinationId.HasValue)
                    {
                        if (!simulationCombinationIds.ContainsKey(programComponent.ProjectCandidateId))
                            simulationCombinationIds[programComponent.ProjectCandidateId] = new List<Guid>();
                        simulationCombinationIds[programComponent.ProjectCandidateId].Add(programComponent.SimulationCombinationId.Value);
                    }
                    else if (programComponent.ProjectCandidateCategoryDetailTypeId.HasValue)
                    {
                        if (!projectCandidateCategoryDetailTypeIds.ContainsKey(programComponent.ProjectCandidateId))
                            projectCandidateCategoryDetailTypeIds[programComponent.ProjectCandidateId] = new List<Guid>();
                        projectCandidateCategoryDetailTypeIds[programComponent.ProjectCandidateId].Add(programComponent.ProjectCandidateCategoryDetailTypeId.Value);
                    }
                }

                var projectCandidates = projectManagementQueryService.ListProjectCandidateDetails(new ListProjectCandidateDetailsRequest { Date = dateTime });

                var messages = new Dictionary<string, List<string>>();

                foreach (var projectCandidate in projectCandidates)
                {
                    var appointment = customerRelationshipManagementQueryService.RetrieveFormattedCrmAppointment(projectCandidate.CrmCandidateAppointmentId);

                    if (appointment.OfficeId != officeId)
                        continue;

                    var simulationCombinations = projectManagementQueryService.ListProjectCandidateSimulationCombinations(projectCandidate.Id);

                    var projectCategoryDetailTypes = projectManagementQueryService.ListProjectCategoryDetails(projectCandidate.ProjectId);
                    var projectCandidateCategoryDetailTypes = projectManagementQueryService.ListProjectCandidateCategoryDetailTypes(projectCandidate.Id);

                    var simulationCombinationMessages = simulationCombinations
                        .Where(simulationCombination => !simulationCombinationIds.ContainsKey(projectCandidate.Id) || !simulationCombinationIds[projectCandidate.Id].Contains(simulationCombination.SimulationCombinationId))
                        .Select(simulationCombination => string.Format("{0} has not been planned for {1}.", simulationCombination.SimulationName, projectCandidate.CandidateFullName))
                        .ToList();

                    var duringSurveyProjectCategoryDetailTypeIds = new List<Guid>();
                    duringSurveyProjectCategoryDetailTypeIds.AddRange(projectCategoryDetailTypes.OfType<ProjectCategoryDetailType1View>().Where(pcdt => pcdt.SurveyPlanningId == 1).Select(pcdt => pcdt.Id));
                    duringSurveyProjectCategoryDetailTypeIds.AddRange(projectCategoryDetailTypes.OfType<ProjectCategoryDetailType2View>().Where(pcdt => pcdt.SurveyPlanningId == 1).Select(pcdt => pcdt.Id));
                    duringSurveyProjectCategoryDetailTypeIds.AddRange(projectCategoryDetailTypes.OfType<ProjectCategoryDetailType3View>().Where(pcdt => pcdt.SurveyPlanningId == 1).Select(pcdt => pcdt.Id));

                    var projectCandidateCategoryDetailTypeMessages = projectCandidateCategoryDetailTypes
                        .Where(pcd => duringSurveyProjectCategoryDetailTypeIds.Contains(pcd.ProjectCategoryDetailTypeId))
                        .Where(detailType => !projectCandidateCategoryDetailTypeIds.ContainsKey(projectCandidate.Id) || !projectCandidateCategoryDetailTypeIds[projectCandidate.Id].Contains(detailType.Id))
                        .Select(detailType => string.Format("{0} has not been planned for {1}.", detailType.ProjectCategoryDetailTypeName, projectCandidate.CandidateFullName))
                        .ToList();

                    if (simulationCombinationMessages.Count > 0 || projectCandidateCategoryDetailTypeMessages.Count > 0)
                    {
                        messages[projectCandidate.CandidateFullName] = new List<string>();
                        messages[projectCandidate.CandidateFullName].AddRange(simulationCombinationMessages);
                        messages[projectCandidate.CandidateFullName].AddRange(projectCandidateCategoryDetailTypeMessages);
                    }
                }

                return messages;
            });
        }

        public List<ProgramComponentView> ListProgramComponentsByUser(Guid userId, DateTime start, DateTime end)
        {
            LogTrace("List program components for user with id {0}", userId);

            return Execute(() =>
            {
                var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                var programComponents = repository.ListProgramComponentsByUser(userId, start, end);

                return programComponents;
            });
        }

        public List<ProgramComponentView> ListProgramComponentsByDate(DateTime dateTime)
        {
            LogTrace("List program components for {0}", dateTime.ToShortDateString());

            return Execute(() =>
            {
                var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                var start = dateTime.Date;
                var end = dateTime.Date.AddDays(1).AddSeconds(-1);

                var programComponents = repository.ListProgramComponentsByDate(start, end);

                return programComponents;
            });
        }

        public List<ProgramComponentView> ListProgramComponentsByRoom(Guid roomId, DateTime dateTime)
        {
            LogTrace("List program components for {0} on {1}", roomId, dateTime.ToShortDateString());

            return Execute(() =>
            {
                var repository = Container.Resolve<ICandidateManagementQueryRepository>();

                var start = dateTime.Date;
                var end = dateTime.Date.AddDays(1).AddSeconds(-1);

                var programComponents = repository.ListProgramComponentsByRoom(roomId, start, end);

                return programComponents;
            });
        }

        public ProgramComponentView RetrieveLinkedProgramComponent(Guid programComponentId)
        {
            LogTrace();

            return Execute(() =>
            {
                using (var repository = Container.Resolve<ICandidateManagementQueryRepository>())
                {
                    var programComponent = repository.Retrieve<ProgramComponentView>(programComponentId);
                    var linkedProgramComponent = repository.RetrieveLinkedProgramComponent(programComponent.ProjectCandidateId, programComponent.SimulationCombinationId, programComponent.SimulationCombinationTypeCode);

                    return linkedProgramComponent;
                }
            });
        }

        #endregion

    }
}
