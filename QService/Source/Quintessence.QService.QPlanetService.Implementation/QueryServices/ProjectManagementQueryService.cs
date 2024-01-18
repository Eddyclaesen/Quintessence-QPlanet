using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.Core.Security;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base.Behaviors;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class ProjectManagementQueryService : SecuredUnityServiceBase, IProjectManagementQueryService
    {
        public List<ProjectTypeView> ListProjectTypes()
        {
            LogTrace();

            return Execute(() =>
                {
                    var repository = Container.Resolve<IProjectManagementQueryRepository>();

                    return repository.ListProjectTypes();
                });
        }

        [SecureOperationBehavior("PRM", "Retrieve project")]
        public ProjectView RetrieveProject(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.RetrieveProject(id);
            });
        }

        [SecureOperationBehavior("PRM", "Retrieve project")]
        public ProjectView RetrieveProjectDetail(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.RetrieveProjectDetail(id);
            });
        }

        [SecureOperationBehavior("PRM", "Retrieve project")]
        public AssessmentDevelopmentProjectView RetrieveAssessmentDevelopmentProjectDetail(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var project = repository.RetrieveAssessmentDevelopmentProjectDetail(id);

                return project;
            });
        }

        public ConsultancyProjectView RetrieveConsultancyProjectDetail(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.RetrieveConsultancyProjectDetail(id);
            });
        }

        public List<ProjectTypeCategoryView> ListAvailableProjectCategories(Guid projectTypeId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListAvailableProjectCategories(projectTypeId);
            });
        }

        public ProjectCategoryDetailView RetrieveMainProjectCategoryDetail(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.RetrieveMainProjectCategoryDetail(projectId);
            });
        }

        public ProjectCategoryDetailView RetrieveMainProjectCategoryDetailDetail(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectCategoryDetail = repository.RetrieveMainProjectCategoryDetailDetail(projectId);

                return projectCategoryDetail;
            });
        }

        public ListProjectsResponse ListProjects(ListProjectsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.PRMALLMYPR);

                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var pagingInfo = Mapper.DynamicMap<PagingInfo>(request.DataTablePaging);

                var response = new ListProjectsResponse
                {
                    Projects = repository.ListProjects(pagingInfo),
                    DataTablePaging = Mapper.DynamicMap<DataTablePaging>(pagingInfo)
                };

                return response;
            });
        }

        public List<ProjectCategoryDetailDictionaryIndicatorView> ListProjectCategoryDetailDictionaryIndicators(ListProjectCategoryDetailDictionaryIndicatorsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectCategoryDetailDictionaryIndicators(request.ProjectCategoryDetailId, request.LanguageId);
            });
        }

        public List<ProjectCategoryDetailSimulationCombinationView> ListProjectCategoryDetailSimulationCombinations(Guid projectCategoryDetailId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectCategoryDetailSimulationCombinations(projectCategoryDetailId);
            });
        }

        public List<ProjectRoleDictionaryLevelView> ListProjectRoleDictionaryIndicators(ListProjectRoleDictionaryIndicatorsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectRoleDictionaryLevels(request.ProjectRoleId, request.LanguageId);
            });
        }

        public List<ProjectCategoryDetailCompetenceSimulationView> ListProjectCategoryDetailCompetenceSimulations(Guid projectCategoryDetailId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectCategoryDetailCompetenceSimulations(projectCategoryDetailId);
            });
        }

        public ListProjectCandidatesResponse ListProjectCandidateDetailsByProjectId(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
                {
                    var repository = Container.Resolve<IProjectManagementQueryRepository>();
                    var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

                    var candidates = repository.ListProjectCandidateDetails(projectId);

                    var project = repository.RetrieveProjectDetail(projectId);

                    //Retrieve selected subcategories for project (e.g. NEOPIR, Schein, Scope,...)
                    //var projectCategoryDetailTypes = project.ProjectCategoryDetails.Where(pcd => !pcd.ProjectTypeCategory.IsMain);
                    var coAssessors = crmQueryService.ListCoAssessors(projectId);

                    foreach (var projectCandidate in candidates)
                    {
                        //EnsureProjectCategoryDetailTypes(projectCandidate, projectCategoryDetailTypes);
                        projectCandidate.ProjectCandidateDetail.CoAssessors = coAssessors.Where(ca => ca.Code == projectCandidate.Code).ToList();
                        projectCandidate.TheoremListRequests =
                            repository.ListCulturalFitCandidateRequests(projectCandidate.CandidateId);
                    }

                    var response = new ListProjectCandidatesResponse
                        {
                            Candidates = candidates,
                            Project = project
                        };

                    return response;
                });
        }

        public List<ProjectCandidateView> ListProjectCandidatesByCandidateId(Guid candidateId)
        {
            LogTrace("List ProjectCandidates.");

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.List<ProjectCandidateView>(candidates => candidates.Where(c => c.CandidateId == candidateId));
            });
        }


        public void ValidateProject(Guid id)
        {
            LogTrace();

            Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var project = repository.RetrieveProjectWithCrmProjects(id);

                if (!project.CrmProjects.Any(p => new List<int> { 3, 4 }.Contains(p.ProjectStatusId)))
                    ValidationContainer.RegisterValidationFaultEntry("QP1001", "There is no running CRM project linked with this project.");
            });
        }

        public List<ProjectRoleView> ListProjectRolesForQuintessenceAndContact(int contactId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectRolesForQuintessenceAndContact(contactId);
            });
        }

        public ProjectTypeCategoryView RetrieveProjectTypeCategory(RetrieveProjectTypeCategoryRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                if (request.Id.HasValue)
                    return repository.RetrieveProjectTypeCategory(request.Id.Value);

                if (request.ProjectCandidateCategoryDetailTypeId.HasValue)
                {
                    var projectCandidateCategoryDetailType = repository.RetrieveProjectCandidateCategoryDetailType(request.ProjectCandidateCategoryDetailTypeId.Value);
                    var projectCategoryDetailType = repository.Retrieve<ProjectCategoryDetailView>(projectCandidateCategoryDetailType.ProjectCategoryDetailTypeId);
                    return repository.RetrieveProjectTypeCategory(projectCategoryDetailType.ProjectTypeCategoryId);
                }

                return null;
            });
        }

        public List<ProjectTypeCategoryView> ListSubcategories()
        {
            LogTrace("List subcategories.");

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListSubcategories();
            });
        }

        public MainProjectView RetrieveMainProject(Guid id)
        {
            LogTrace("Retrieve main project.");

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.RetrieveMainProject(id);
            });
        }

        public List<ProjectCandidateView> ListUserProjectCandidates(ListUserProjectCandidatesRequest request)
        {
            LogTrace("List project candidates for user.");

            return Execute(() =>
            {
                var securityContext = Container.Resolve<SecurityContext>();

                var securityQueryService = Container.Resolve<ISecurityQueryService>();
                var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

                var user = securityQueryService.RetrieveUser(securityContext.UserId);

                var associateId = user.AssociateId;

                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var userProjectCandidates = repository.ListUserProjectCandidates(request.StartDate, request.EndDate);

                foreach (var projectCandidate in userProjectCandidates)
                {
                    var coAssessors = crmQueryService.ListCoAssessors(projectCandidate.ProjectId);
                    projectCandidate.ProjectCandidateDetail.CoAssessors = coAssessors.Where(ca => ca.Code == projectCandidate.Code).ToList();
                }

                //Return project candidates where user is lead or co assessor.
                return userProjectCandidates
                    .Where(pc => pc.ProjectCandidateDetail.AssociateId == associateId
                        || pc.ProjectCandidateDetail.CoAssessors.Select(ca => ca.AssociateId).Contains(associateId))
                    .ToList();
            });
        }

        public List<ProjectCandidateView> ListQCandidateProjectCandidates(ListUserProjectCandidatesRequest request)
        {
            LogTrace("List project candidates for QCandidate.");

            return Execute(() =>
            {
                var securityContext = Container.Resolve<SecurityContext>();

                var securityQueryService = Container.Resolve<ISecurityQueryService>();
                var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var userProjectCandidates = repository.ListQCandidateProjectCandidates(request.StartDate, request.EndDate);

                return userProjectCandidates
                    .ToList();
            });
        }

        public ListProjectCandidateSimulationScoresResponse ListProjectCandidateSimulationScores(Guid projectCandidateId)
        {
            LogTrace("Retrieve project candidate simulation scores project.");

            return Execute(() =>
            {
                var commandService = Container.Resolve<IProjectManagementCommandService>();
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectCandidate = repository.RetrieveProjectCandidateDetail(projectCandidateId);

                var mainProjectCategoryDetail = repository.RetrieveMainProjectCategoryDetail(projectCandidate.ProjectId);

                var projectCategoryDetailDictionaryIndicators = repository.ListProjectCategoryDetailDictionaryIndicators(mainProjectCategoryDetail.Id, projectCandidate.ReportLanguageId);
                var competenceSimulations = repository.ListProjectCategoryDetailCompetenceSimulations(mainProjectCategoryDetail.Id);

                var competenceScores = repository.ListProjectCandidateCompetenceSimulationScores(projectCandidateId);

                ListProjectCandidateSimulationScoresResponse response = null;

                switch (mainProjectCategoryDetail.ProjectTypeCategory.Code)
                {
                    case "FA":
                    case "FD":
                        response = new ListProjectCandidateFocusedSimulationScoresResponse();
                        var focusedIndicatorScores = repository.ListProjectCandidateIndicatorSimulationFocusedScores(projectCandidateId);                        
                        foreach (var competenceSimulation in competenceSimulations)
                        {
                            foreach (var projectCategoryDetailDictionaryIndicator in projectCategoryDetailDictionaryIndicators.Where(di => di.DictionaryCompetenceId == competenceSimulation.DictionaryCompetenceId))
                            {
                                if (competenceScores.FirstOrDefault(score =>
                                                    score.DictionaryCompetenceId == projectCategoryDetailDictionaryIndicator.DictionaryCompetenceId
                                                    && score.SimulationCombinationId == competenceSimulation.SimulationCombinationId) == null)
                                {
                                    var request = new CreateNewProjectCandidateCompetenceSimulationScoreRequest();
                                    request.ProjectCandidateId = projectCandidateId;
                                    request.SimulationCombinationId = competenceSimulation.SimulationCombinationId;
                                    request.DictionaryCompetenceId = projectCategoryDetailDictionaryIndicator.DictionaryCompetenceId;
                                    var scoreId = commandService.CreateNewProjectCandidateCompetenceSimulationScore(request);

                                    competenceScores.Add(repository.Retrieve<ProjectCandidateCompetenceSimulationScoreView>(scoreId));
                                }

                                if (focusedIndicatorScores.FirstOrDefault(score =>
                                                    score.DictionaryIndicatorId == projectCategoryDetailDictionaryIndicator.DictionaryIndicatorId
                                                    && score.SimulationCombinationId == competenceSimulation.SimulationCombinationId) == null)
                                {
                                    var request = new CreateNewProjectCandidateIndicatorSimulationScoreRequest();
                                    request.ProjectCandidateId = projectCandidateId;
                                    request.SimulationCombinationId = competenceSimulation.SimulationCombinationId;
                                    request.DictionaryIndicatorId = projectCategoryDetailDictionaryIndicator.DictionaryIndicatorId;
                                    var scoreId = commandService.CreateNewProjectCandidateIndicatorSimulationScore(request);

                                    focusedIndicatorScores.Add(repository.Retrieve<ProjectCandidateIndicatorSimulationFocusedScoreView>(scoreId));
                                }
                            }
                        }
                        ((ListProjectCandidateFocusedSimulationScoresResponse)response).ProjectCandidateIndicatorSimulationFocusedScores = focusedIndicatorScores;
                        break;

                    default:
                        response = new ListProjectCandidateStandardSimulationScoresResponse();
                        var standardIndicatorScores = repository.ListProjectCandidateIndicatorSimulationScores(projectCandidateId);
                        foreach (var competenceSimulation in competenceSimulations)
                        {
                            foreach (var projectCategoryDetailDictionaryIndicator in projectCategoryDetailDictionaryIndicators.Where(di => di.DictionaryCompetenceId == competenceSimulation.DictionaryCompetenceId))
                            {
                                if (competenceScores.FirstOrDefault(score =>
                                                    score.DictionaryCompetenceId == projectCategoryDetailDictionaryIndicator.DictionaryCompetenceId
                                                    && score.SimulationCombinationId == competenceSimulation.SimulationCombinationId) == null)
                                {
                                    var request = new CreateNewProjectCandidateCompetenceSimulationScoreRequest();
                                    request.ProjectCandidateId = projectCandidateId;
                                    request.SimulationCombinationId = competenceSimulation.SimulationCombinationId;
                                    request.DictionaryCompetenceId = projectCategoryDetailDictionaryIndicator.DictionaryCompetenceId;
                                    var scoreId = commandService.CreateNewProjectCandidateCompetenceSimulationScore(request);

                                    competenceScores.Add(repository.Retrieve<ProjectCandidateCompetenceSimulationScoreView>(scoreId));
                                }

                                if (standardIndicatorScores.FirstOrDefault(score =>
                                                    score.DictionaryIndicatorId == projectCategoryDetailDictionaryIndicator.DictionaryIndicatorId
                                                    && score.SimulationCombinationId == competenceSimulation.SimulationCombinationId) == null)
                                {
                                    var request = new CreateNewProjectCandidateIndicatorSimulationScoreRequest();
                                    request.ProjectCandidateId = projectCandidateId;
                                    request.SimulationCombinationId = competenceSimulation.SimulationCombinationId;
                                    request.DictionaryIndicatorId = projectCategoryDetailDictionaryIndicator.DictionaryIndicatorId;
                                    var scoreId = commandService.CreateNewProjectCandidateIndicatorSimulationScore(request);

                                    standardIndicatorScores.Add(repository.Retrieve<ProjectCandidateIndicatorSimulationScoreView>(scoreId));
                                }
                            }
                        }
                        ((ListProjectCandidateStandardSimulationScoresResponse)response).ProjectCandidateIndicatorSimulationScores = standardIndicatorScores;
                        break;
                }

                response.ProjectCandidate = projectCandidate;
                response.ProjectCandidateCompetenceSimulationScores = competenceScores;

                return response;
            });
        }

        public ListProjectCandidateScoresResponse ListProjectCandidateScores(Guid projectCandidateId)
        {
            LogTrace("Retrieve candidate scores.");

            var projectCandidateSimulationScores = ListProjectCandidateSimulationScores(projectCandidateId);

            return Execute(() =>
                {
                    var repository = Container.Resolve<IProjectManagementQueryRepository>();
                    var reportQueryService = Container.Resolve<IReportManagementQueryService>();
                    var commandService = Container.Resolve<IProjectManagementCommandService>();

                    var project = repository.RetrieveProject(projectCandidateSimulationScores.ProjectCandidate.ProjectId) as AssessmentDevelopmentProjectView;

                    if (project == null)
                        throw new InvalidOperationException("Project is not a Assessment/Development project.");

                    var reportTypes = reportQueryService.ListCandidateScoreReportTypes();

                    var projectReportType = reportTypes.FirstOrDefault(rp => rp.Id == project.CandidateScoreReportTypeId);

                    if (projectReportType == null)
                        throw new InvalidOperationException("The defined report type on the project is not found.");

                    var projectCandidateStandardSimulationScores = projectCandidateSimulationScores as ListProjectCandidateStandardSimulationScoresResponse;
                    var projectCandidateFocusedSimulationScores = projectCandidateSimulationScores as ListProjectCandidateFocusedSimulationScoresResponse;

                    ListProjectCandidateScoresResponse response = null;

                    switch (projectReportType.Code)
                    {
                        case "CL": //Clustered based
                            response = new ListProjectCandidateClusterScoresResponse();
                            var projectCandidateClusterScores = repository.ListProjectCandidateClusterScores(projectCandidateId);                                                        
                            
                        //First time we try to retrieve the scores: create all the records
                            if (projectCandidateClusterScores == null || projectCandidateClusterScores.Count == 0)
                            {
                                foreach (var projectCandidateCompetenceSimulationScoreClusterGroup in projectCandidateSimulationScores.ProjectCandidateCompetenceSimulationScores.GroupBy(pccss => pccss.DictionaryClusterId))
                                {
                                    var createNewProjectCandidateClusterScoreRequest = new CreateNewProjectCandidateClusterScoreRequest
                                        {
                                            ProjectCandidateId = projectCandidateId,
                                            DictionaryClusterId = projectCandidateCompetenceSimulationScoreClusterGroup.Key,
                                            Statement = string.Join(Environment.NewLine, projectCandidateSimulationScores.ProjectCandidateCompetenceSimulationScores
                                                .Where(pccss => pccss.DictionaryClusterId == projectCandidateCompetenceSimulationScoreClusterGroup.Key)
                                                .Select(pccss => pccss.Remarks))
                                        };
                                    var projectCandidateClusterScoreId = commandService.CreateNewProjectCandidateClusterScore(createNewProjectCandidateClusterScoreRequest);

                                    foreach (var projectCandidateCompetenceSimulationScoreCompetenceGroup in projectCandidateCompetenceSimulationScoreClusterGroup.GroupBy(pccsscg => pccsscg.DictionaryCompetenceId))
                                    {
                                        var createNewProjectCandidateCompetenceScoreRequest = new CreateNewProjectCandidateCompetenceScoreRequest
                                            {
                                                DictionaryCompetenceId = projectCandidateCompetenceSimulationScoreCompetenceGroup.Key,
                                                ProjectCandidateClusterScoreId = projectCandidateClusterScoreId,
                                                ProjectCandidateId = projectCandidateId
                                            };

                                        var projectCandidateCompetenceScoreId = commandService.CreateNewProjectCandidateCompetenceScore(createNewProjectCandidateCompetenceScoreRequest);

                                        if (projectCandidateStandardSimulationScores != null)
                                        {
                                            foreach (var projectCandidateStandardSimulationScoreDictionaryIndicatorId in projectCandidateStandardSimulationScores.ProjectCandidateIndicatorSimulationScores
                                                .Where(pciss => pciss.DictionaryClusterId == projectCandidateCompetenceSimulationScoreClusterGroup.Key
                                                    && pciss.DictionaryCompetenceId == projectCandidateCompetenceSimulationScoreCompetenceGroup.Key)
                                                .Select(pciss => pciss.DictionaryIndicatorId)
                                                .Distinct())
                                            {
                                                var createNewProjectCandidateIndicatorScoreRequest = new CreateNewProjectCandidateIndicatorScoreRequest
                                                {
                                                    DictionaryIndicatorId = projectCandidateStandardSimulationScoreDictionaryIndicatorId,
                                                    ProjectCandidateCompetenceScoreId = projectCandidateCompetenceScoreId,
                                                    ProjectCandidateId = projectCandidateId
                                                };

                                                commandService.CreateNewProjectCandidateIndicatorScore(createNewProjectCandidateIndicatorScoreRequest);
                                            }
                                        }

                                        if (projectCandidateFocusedSimulationScores != null)
                                        {
                                            foreach (var projectCandidateStandardSimulationScoreDictionaryIndicatorId in projectCandidateFocusedSimulationScores.ProjectCandidateIndicatorSimulationFocusedScores
                                                .Where(pciss => pciss.DictionaryClusterId == projectCandidateCompetenceSimulationScoreClusterGroup.Key
                                                    && pciss.DictionaryCompetenceId == projectCandidateCompetenceSimulationScoreCompetenceGroup.Key)
                                                .Select(pciss => pciss.DictionaryIndicatorId)
                                                .Distinct())
                                            {
                                                var createNewProjectCandidateIndicatorScoreRequest = new CreateNewProjectCandidateIndicatorScoreRequest
                                                {
                                                    DictionaryIndicatorId = projectCandidateStandardSimulationScoreDictionaryIndicatorId,
                                                    ProjectCandidateCompetenceScoreId = projectCandidateCompetenceScoreId,
                                                    ProjectCandidateId = projectCandidateId
                                                };

                                                commandService.CreateNewProjectCandidateIndicatorScore(createNewProjectCandidateIndicatorScoreRequest);
                                            }
                                            response.ProjectCandidateIndicatorSimulationFocusedScores = projectCandidateFocusedSimulationScores.ProjectCandidateIndicatorSimulationFocusedScores;
                                        }
                                    }
                                }
                                projectCandidateClusterScores = repository.ListProjectCandidateClusterScores(projectCandidateId);
                            }
                            ((ListProjectCandidateClusterScoresResponse)response).ProjectCandidateClusterScores = projectCandidateClusterScores;
                            break;

                        case "CO": //Competence base
                            response = new ListProjectCandidateCompetenceScoresResponse();
                            var projectCandidateCompetenceScores = repository.ListProjectCandidateCompetenceScores(projectCandidateId);
                            

                            if (projectCandidateCompetenceScores == null || projectCandidateCompetenceScores.Count == 0)
                            {
                                foreach (var projectCandidateCompetenceSimulationScoreCompetenceGroup in projectCandidateSimulationScores.ProjectCandidateCompetenceSimulationScores.GroupBy(pccsscg => pccsscg.DictionaryCompetenceId))
                                {
                                    var createNewProjectCandidateCompetenceScoreRequest = new CreateNewProjectCandidateCompetenceScoreRequest
                                    {
                                        DictionaryCompetenceId = projectCandidateCompetenceSimulationScoreCompetenceGroup.Key,
                                        ProjectCandidateId = projectCandidateId,
                                        Statement = string.Join(string.Empty, projectCandidateCompetenceSimulationScoreCompetenceGroup.Select(pccssc => pccssc.Remarks))
                                    };

                                    var projectCandidateCompetenceScoreId = commandService.CreateNewProjectCandidateCompetenceScore(createNewProjectCandidateCompetenceScoreRequest);

                                    if (projectCandidateStandardSimulationScores != null)
                                    {
                                        foreach (var projectCandidateStandardSimulationScoreDictionaryIndicatorId in projectCandidateStandardSimulationScores.ProjectCandidateIndicatorSimulationScores
                                            .Where(pciss => pciss.DictionaryCompetenceId == projectCandidateCompetenceSimulationScoreCompetenceGroup.Key)
                                                .Select(pciss => pciss.DictionaryIndicatorId)
                                                .Distinct())
                                        {
                                            var createNewProjectCandidateIndicatorScoreRequest = new CreateNewProjectCandidateIndicatorScoreRequest
                                            {
                                                DictionaryIndicatorId = projectCandidateStandardSimulationScoreDictionaryIndicatorId,
                                                ProjectCandidateCompetenceScoreId = projectCandidateCompetenceScoreId,
                                                ProjectCandidateId = projectCandidateId
                                            };

                                            commandService.CreateNewProjectCandidateIndicatorScore(createNewProjectCandidateIndicatorScoreRequest);
                                        }
                                        response.ProjectCandidateIndicatorSimulationScores = projectCandidateStandardSimulationScores.ProjectCandidateIndicatorSimulationScores;
                                    }

                                    if (projectCandidateFocusedSimulationScores != null)
                                    {
                                        foreach (var projectCandidateStandardSimulationScoreDictionaryIndicatorId in projectCandidateFocusedSimulationScores.ProjectCandidateIndicatorSimulationFocusedScores
                                            .Where(pciss => pciss.DictionaryCompetenceId == projectCandidateCompetenceSimulationScoreCompetenceGroup.Key)
                                                .Select(pciss => pciss.DictionaryIndicatorId)
                                                .Distinct())
                                        {
                                            var createNewProjectCandidateIndicatorScoreRequest = new CreateNewProjectCandidateIndicatorScoreRequest
                                            {
                                                DictionaryIndicatorId = projectCandidateStandardSimulationScoreDictionaryIndicatorId,
                                                ProjectCandidateCompetenceScoreId = projectCandidateCompetenceScoreId,
                                                ProjectCandidateId = projectCandidateId
                                            };

                                            commandService.CreateNewProjectCandidateIndicatorScore(createNewProjectCandidateIndicatorScoreRequest);
                                        }
                                        response.ProjectCandidateIndicatorSimulationFocusedScores = projectCandidateFocusedSimulationScores.ProjectCandidateIndicatorSimulationFocusedScores;
                                    }
                                }
                                projectCandidateCompetenceScores = repository.ListProjectCandidateCompetenceScores(projectCandidateId);
                            }
                            ((ListProjectCandidateCompetenceScoresResponse)response).ProjectCandidateCompetenceScores = projectCandidateCompetenceScores;
                            break;

                        default:
                            throw new InvalidOperationException("The report type code is not linked with a scores type.");
                    }

                    if (projectCandidateFocusedSimulationScores != null)
                        response.ProjectCandidateIndicatorSimulationFocusedScores = projectCandidateFocusedSimulationScores.ProjectCandidateIndicatorSimulationFocusedScores;
                    if (projectCandidateStandardSimulationScores != null)
                        response.ProjectCandidateIndicatorSimulationScores = projectCandidateStandardSimulationScores.ProjectCandidateIndicatorSimulationScores;
                    response.ProjectCandidateCompetenceSimulationScores = projectCandidateSimulationScores.ProjectCandidateCompetenceSimulationScores;
                    response.ProjectCandidate = projectCandidateSimulationScores.ProjectCandidate;
                    response.Project = project;
                    response.ReportTypes = reportTypes;

                    return response;
                });
        }

        public RetrieveProjectCandidateResumeResponse RetrieveProjectCandidateResume(Guid projectCandidateId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();
                var commandService = Container.Resolve<IProjectManagementCommandService>();

                var response = new RetrieveProjectCandidateResumeResponse();

                response.ProjectCandidate = repository.RetrieveProjectCandidate(projectCandidateId);
                response.ProjectCandidateResume = repository.RetrieveProjectCandidateResume(projectCandidateId);

                var project = repository.RetrieveProject(response.ProjectCandidate.ProjectId);

                if (!(project is AssessmentDevelopmentProjectView))
                    throw new InvalidOperationException(string.Format("Unable to retrieve project candidate resumes for this type of project ({0})", project.GetType().Name));

                response.Project = (AssessmentDevelopmentProjectView)project;

                if (response.ProjectCandidateResume == null)
                {
                    commandService.CreateNewProjectCandidateResume(new CreateNewProjectCandidateResumeRequest { ProjectCandidateId = projectCandidateId });
                    response.ProjectCandidateResume = repository.RetrieveProjectCandidateResume(projectCandidateId);
                }

                var reportManagementQueryService = Container.Resolve<IReportManagementQueryService>();

                if (response.Project.CandidateReportDefinitionId.HasValue)
                {
                    var reportDefinition = reportManagementQueryService.RetrieveCandidateReportDefinition(response.Project.CandidateReportDefinitionId.Value);

                    //Generate extra resume entries for each reportdefinition field
                    if (reportDefinition.CandidateReportDefinitionFields != null)
                    {
                        foreach (var candidateReportDefinitionField in reportDefinition.CandidateReportDefinitionFields.Where(crdf => crdf.IsActive))
                        {
                            if (response.ProjectCandidateResume.ProjectCandidateResumeFields.Any(pcrf => pcrf.CandidateReportDefinitionFieldId == candidateReportDefinitionField.Id))
                                continue;

                            var request = new CreateNewProjectCandidateResumeFieldRequest
                                {
                                    ProjectCandidateResumeId = response.ProjectCandidateResume.Id,
                                    CandidateReportDefinitionFieldId = candidateReportDefinitionField.Id
                                };
                            var projectCandidateResumeFieldId = commandService.CreateNewProjectCandidateResumeField(request);
                            var projectCandidateResumeField = repository.Retrieve<ProjectCandidateResumeFieldView>(projectCandidateResumeFieldId);
                            projectCandidateResumeField.ProjectCandidateResume = response.ProjectCandidateResume;
                            response.ProjectCandidateResume.ProjectCandidateResumeFields.Add(projectCandidateResumeField);
                        }
                    }
                }

                response.ProjectCandidateClusterScores = repository.ListProjectCandidateClusterScores(projectCandidateId);
                response.ProjectCandidateCompetenceScores = repository.ListProjectCandidateCompetenceScores(projectCandidateId);
                response.ProjectCandidateCompetenceSimulationScores = repository.ListProjectCandidateCompetenceSimulationScores(projectCandidateId);
                response.Advices = repository.ListAdvices();

                return response;
            });
        }

        public List<ProjectCandidateView> ListProjectCandidatesByProjectId(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();
                //var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

                var candidates = repository.ListProjectCandidates(projectId);

                //var project = repository.RetrieveProjectDetail(projectId);
                //var projectCategoryDetailTypes = project.ProjectCategoryDetails.Where(pcd => !pcd.ProjectTypeCategory.IsMain);

                //foreach (var projectCandidate in candidates)
                //{
                //    EnsureProjectCategoryDetailTypes(projectCandidate, projectCategoryDetailTypes);
                //}

                return candidates;
            });
        }

        public List<ProposalView> ListProposals(int year)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var proposals = repository.ListProposalsByYear(year);

                return proposals;
            });
        }

        public List<int> ListProposalYears()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var proposals = repository.ListProposalYears();

                return proposals;
            });
        }

        public ProposalView RetrieveProposal(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var proposal = repository.RetrieveProposal(id);

                return proposal;
            });
        }

        public List<ProjectCandidateResumeView> ListProjectCandidateResumes(Guid projectCandidateId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var resumes = repository.ListProjectCandidateResumes(projectCandidateId);

                return resumes;
            });
        }

        public FrameworkAgreementView RetrieveFrameworkAgreement(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var frameworkAgreement = repository.RetrieveFrameworkAgreement(id);

                return frameworkAgreement;
            });
        }

        public List<FrameworkAgreementView> ListFrameworkAgreementsByYear(int year)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var frameworkAgreements = repository.ListFrameworkAgreementsByYear(year);

                return frameworkAgreements;
            });
        }

        public List<int> ListFrameworkAgreementYears()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var years = repository.ListFrameworkAgreementYears();

                return years;
            });
        }

        public List<ProjectCandidateReportRecipientView> ListProjectCandidateReportRecipientsByProjectCandidateId(Guid projectCandidateId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var reportRecipients = repository.ListProjectCandidateReportRecipientsByProjectCandidateId(projectCandidateId);

                return reportRecipients;
            });
        }

        public List<ReportStatusView> ListReportStatuses()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var reportStatuses = repository.List<ReportStatusView>().OrderBy(rs => rs.SortOrder).ToList();

                return reportStatuses;
            });
        }

        public ReportStatusView RetrieveReportStatusByCode(string code)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var reportStatus = repository.RetrieveReportStatusByCode(code);

                return reportStatus;
            });
        }

        public List<ProjectTypeCategoryLevelView> ListProjectTypeCategoryLevels()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectTypeCategoryLevels = repository.List<ProjectTypeCategoryLevelView>().ToList();

                return projectTypeCategoryLevels;
            });
        }

        public ProjectTypeCategoryUnitPriceView RetrieveProjectTypeCategoryUnitPrice(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectTypeCategoryUnitPrice = repository.Retrieve<ProjectTypeCategoryUnitPriceView>(id);

                return projectTypeCategoryUnitPrice;
            });
        }

        public List<ProjectCandidateView> ListProjectCandidateDetails(ListProjectCandidateDetailsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectCandidates(request.ProjectId, request.CandidateId, request.Date);
            });
        }

        public List<UnplannedProjectCandidateEvent> ListAvailableEvents(Guid roomId, DateTime dateTime)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();
                var candidateManagementQueryService = Container.Resolve<ICandidateManagementQueryService>();
                var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();
                var customerRelationshipManagementQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

                var room = infrastructureQueryService.RetrieveAssessmentRoom(roomId);

                var projectCandidates = repository.ListProjectCandidates(date: dateTime);
                var programComponentSpecials = candidateManagementQueryService.ListSpecialEvents();

                var unplannedProjectCandidateEvents = new List<UnplannedProjectCandidateEvent>();

                var projectDictionary = new Dictionary<Guid, ProjectView>();

                foreach (var projectCandidate in projectCandidates)
                {
                    var formattedAppointment = customerRelationshipManagementQueryService.RetrieveFormattedCrmAppointment(projectCandidate.CrmCandidateAppointmentId);

                    if (formattedAppointment.OfficeId != room.OfficeId)
                        continue;

                    if (!projectDictionary.ContainsKey(projectCandidate.ProjectId))
                        projectDictionary[projectCandidate.ProjectId] = repository.RetrieveProjectDetail(projectCandidate.ProjectId);

                    var project = projectDictionary[projectCandidate.ProjectId];

                    var programComponents = candidateManagementQueryService.ListProgramComponentsByCandidate(projectCandidate.Id);

                    #region List simulation combinations assigned to project
                    List<ProjectCategoryDetailSimulationCombinationView> simulationCombinations = null;

                    var projectMainCategoryDetail = project.ProjectCategoryDetails.FirstOrDefault(pcd => pcd.ProjectTypeCategory.IsMain);
                    if (projectMainCategoryDetail != null)
                        simulationCombinations = repository.ListProjectCategoryDetailSimulationCombinations(projectMainCategoryDetail.Id);
                    #endregion

                    #region List subcategories
                    var projectCandidateCategoryDetailTypes = repository.ListProjectCandidateCategoryDetailTypes(projectCandidate.Id);
                    #endregion

                    if (simulationCombinations != null)
                        unplannedProjectCandidateEvents
                            .AddRange(simulationCombinations
                                .Where(sc => programComponents.All(pc => pc.SimulationCombinationId != sc.SimulationCombinationId))
                                .Select(sc => new UnplannedProjectCandidateEvent
                                    {
                                        ProjectCandidateId = projectCandidate.Id,
                                        ProjectCandidateFullName = projectCandidate.CandidateFullName,
                                        AssessmentRoomId = roomId,
                                        SimulationCombinationId = sc.SimulationCombinationId,
                                        SimulationSetName = sc.SimulationSetName,
                                        SimulationDepartmentName = sc.SimulationDepartmentName,
                                        SimulationLevelName = sc.SimulationLevelName,
                                        SimulationName = sc.SimulationName
                                        ,ContactName = project.Contact.Name
                                    }));

                    if (projectCandidateCategoryDetailTypes.Count > 0)
                    {
                        var projectCategoryDetailTypes = project.ProjectCategoryDetails;

                        var duringSurveyProjectCategoryDetailTypeIds = new List<Guid>();
                        duringSurveyProjectCategoryDetailTypeIds.AddRange(projectCategoryDetailTypes.OfType<ProjectCategoryDetailType1View>().Where(pcdt => pcdt.SurveyPlanningId == 1).Select(pcdt => pcdt.Id));
                        duringSurveyProjectCategoryDetailTypeIds.AddRange(projectCategoryDetailTypes.OfType<ProjectCategoryDetailType2View>().Where(pcdt => pcdt.SurveyPlanningId == 1).Select(pcdt => pcdt.Id));
                        duringSurveyProjectCategoryDetailTypeIds.AddRange(projectCategoryDetailTypes.OfType<ProjectCategoryDetailType3View>().Where(pcdt => pcdt.SurveyPlanningId == 1).Select(pcdt => pcdt.Id));


                        unplannedProjectCandidateEvents
                            .AddRange(projectCandidateCategoryDetailTypes
                                .Where(pcd => duringSurveyProjectCategoryDetailTypeIds.Contains(pcd.ProjectCategoryDetailTypeId) && programComponents.All(pc => pc.ProjectCandidateCategoryDetailTypeId != pcd.Id))
                                .Select(pcd => new UnplannedProjectCandidateEvent
                                {
                                    ProjectCandidateId = projectCandidate.Id,
                                    ProjectCandidateFullName = projectCandidate.CandidateFullName,
                                    AssessmentRoomId = roomId,
                                    ProjectCandidateCategoryDetailTypeId = pcd.Id,
                                    ProjectCategoryDetailTypeName = pcd.ProjectCategoryDetailTypeName
                                    ,ContactName = project.Contact.Name
                                }));
                    }
                    unplannedProjectCandidateEvents.AddRange(programComponentSpecials
                        .OrderBy(pcs => pcs.Name)
                        .Select(pcs => new UnplannedProjectCandidateEvent
                        {
                            ProjectCandidateId = projectCandidate.Id,
                            ProjectCandidateFullName = projectCandidate.CandidateFullName,
                            AssessmentRoomId = roomId,
                            ProgramComponentSpecialId = pcs.Id,
                            ProgramComponentSpecialName = pcs.Name
                            ,ContactName = project.Contact.Name
                        }));
                }

                return unplannedProjectCandidateEvents;
            });
        }

        public ProjectCandidateView RetrieveProjectCandidateDetail(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectCandidateDetail = repository.RetrieveProjectCandidateDetail(id);

                return projectCandidateDetail;
            });
        }

        public ProjectCandidateView RetrieveProjectCandidateDetailWithTypes(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectCandidateDetail = repository.RetrieveProjectCandidateDetailWithTypes(id);

                return projectCandidateDetail;
            });
        }

        public ProjectCandidateCategoryDetailTypeView RetrieveProjectCandidateCategoryDetailType(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectCandidateCategoryDetailType = repository.RetrieveProjectCandidateCategoryDetailType(id);

                return projectCandidateCategoryDetailType;
            });
        }

        public List<DayPlanAssessorView> ListProjectCandidateAssessorsForPlanning(int officeId, DateTime dateTime)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();
                var customerRelationshipManagementQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
                var securityQueryService = Container.Resolve<ISecurityQueryService>();

                var projectCandidates = repository.ListProjectCandidateDetailsForPlanning(officeId, dateTime);

                foreach (var projectCandidate in projectCandidates)
                    projectCandidate.ProjectCandidateDetail.CoAssessors = customerRelationshipManagementQueryService.ListCoAssessors(projectCandidate.ProjectId);

                var assessorIds = projectCandidates.Where(pc => pc.ProjectCandidateDetail.LeadAssessorUserId.HasValue).Select(pc => pc.ProjectCandidateDetail.LeadAssessorUserId.Value).ToList();
                assessorIds.AddRange(projectCandidates
                    .SelectMany(pc => pc.ProjectCandidateDetail.CoAssessors.Where(ca => ca.Code == pc.Code && ca.UserId.HasValue).Select(ca => ca.UserId.Value)));

                var workspaceQueryService = Container.Resolve<IWorkspaceManagementQueryService>();
                var dayPlanAssessors = new List<DayPlanAssessorView>();
                foreach (var assessorId in assessorIds.Distinct().ToList())
                {
                    var user = workspaceQueryService.RetrieveUserProfile(assessorId);
                    dayPlanAssessors.Add(new DayPlanAssessorView
                        {
                            AssessorId = assessorId,
                            FullName = user.FullName,
                            UserName = user.UserName,
                            Email = user.Emails.Any() ? user.Emails.FirstOrDefault().Address : "",
                            Color = user.Color
                        });
                }
                return dayPlanAssessors;
            });
        }

        public ProjectTypeCategoryUnitPriceOverviewResponse ListProjectTypeCategoryUnitPriceOverview()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();
                var projectTypeCategoryUnitPriceOverviewResponse = new ProjectTypeCategoryUnitPriceOverviewResponse
                    {
                        ProjectTypeCategories = repository.ListProjectTypeCategories(),
                        ProjectTypeCategoryLevels = repository.ListProjectTypeCategoryLevels(),
                        ProjectTypeCategoryUnitPrices = repository.ListProjectTypeCategoryUnitPrices()
                    };

                return projectTypeCategoryUnitPriceOverviewResponse;
            });
        }

        public List<ProjectTypeCategoryUnitPriceView> ListProjectTypeCategoryUnitPricesByCategory(Guid projectTypeCategoryId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();
                var projectTypeCategoryUnitPrices =
                    repository.ListProjectTypeCategoryUnitPricesByCategory(projectTypeCategoryId);
                return projectTypeCategoryUnitPrices;
            });
        }

        public InvoiceOverviewResponse ListInvoiceOverview(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var project = repository.RetrieveAssessmentDevelopmentProjectDetail(projectId);

                var projectCandidates =
                    repository.ListProjectCandidateDetails(projectId);

                var projectProducts = repository.ListProjectProducts(projectId);

                var invoiceOverviewResponse = new InvoiceOverviewResponse
                    {
                        Project = project,
                        ProjectCandidates = projectCandidates,
                        ProjectProducts = projectProducts
                    };
                return invoiceOverviewResponse;
            });
        }

        public List<ProjectProductView> ListProjectProducts(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectProducts = repository.ListProjectProducts(projectId);

                return projectProducts;
            });
        }

        public ProjectProductView RetrieveProjectProduct(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectProduct = repository.Retrieve<ProjectProductView>(id);

                return projectProduct;
            });
        }

        public ProjectEvaluationView RetrieveProjectEvaluationByCrmProject(int crmProjectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectEvaluation = repository.RetrieveProjectEvaluationByCrmProject(crmProjectId);

                var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
                var crmProject = crmQueryService.RetrieveCrmProject(crmProjectId);

                //If there is no project evaluation for that CRM project and that CRM project exists, create a new project evaluation.
                if (projectEvaluation == null && crmProject != null)
                {
                    var commandService = Container.Resolve<IProjectManagementCommandService>();
                    var request = new CreateProjectEvaluationRequest
                        {
                            CrmProjectId = crmProjectId,
                            LessonsLearned = "",
                            Evaluation = ""
                        };
                    var createdId = commandService.CreateProjectEvaluation(request);

                    projectEvaluation = repository.Retrieve<ProjectEvaluationView>(createdId);
                }

                return projectEvaluation;
            });
        }

        public ProjectEvaluationView RetrieveProjectEvaluation(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectEvaluation = repository.Retrieve<ProjectEvaluationView>(id);

                return projectEvaluation;
            });
        }

        public string CreateEvaluationFormVerificationCode()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var verificationCode = repository.CreateEvaluationFormVerificationCode();

                return verificationCode;
            });
        }

        public EvaluationFormView RetrieveEvaluationForm(RetrieveEvaluationFormRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return request.Id != null ? repository.Retrieve<EvaluationFormView>(request.Id.Value) : repository.RetrieveEvaluationFormByCode(request.VerificationCode);
            });
        }

        public bool ValidateQCareVerificationCode(string verificationCode)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ValidateQCareVerificationCode(verificationCode);
            });
        }

        public List<TheoremListRequestView> ListCulturalFitContactRequests(int contactId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListCulturalFitContactRequests(contactId);
            });
        }

        public TheoremListRequestView RetrieveTheoremListRequest(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.Retrieve<TheoremListRequestView>(id);
            });
        }

        public List<EvaluationFormView> ListEvaluationFormsByCrmProject(int crmProjectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var evaluationForms = repository.ListEvaluationFormsByCrmProject(crmProjectId);

                return evaluationForms;
            });
        }

        public List<ProjectComplaintView> ListProjectComplaintsByCrmProject(int crmProjectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectComplaints = repository.ListProjectComplaintByCrmProject(crmProjectId);

                return projectComplaints;
            });
        }

        public List<EvaluationFormTypeView> ListEvaluationFormTypes()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var evaluationFormTypes = repository.ListEvaluationFormTypes();

                return evaluationFormTypes;
            });
        }

        public List<MailStatusTypeView> ListMailStatusTypes()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var mailStatusTypes = repository.ListMailStatusTypes();

                return mailStatusTypes;
            });
        }

        public ProjectTypeCategoryUnitPriceView RetrieveProjectTypeCategoryUnitPriceByTypeAndLevel(Guid projectTypeCategoryId, Guid projectTypeCategoryLevelId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectTypeCategoryUnitPrice = repository.RetrieveProjectTypeCategoryUnitPrice(projectTypeCategoryId, projectTypeCategoryLevelId);

                return projectTypeCategoryUnitPrice;
            });
        }

        public List<ComplaintTypeView> ListComplaintTypes()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var complaintTypes = repository.ListComplaintTypes();

                return complaintTypes;
            });
        }

        //public ProjectComplaintView RetrieveProjectComplaintByCrmProject(int crmProjectId)
        //{
        //    LogTrace();

        //    return Execute(() =>
        //                   {
        //                       var repository = Container.Resolve<IProjectManagementQueryRepository>();

        //                       var projectComplaint = repository.RetrieveProjectComplaintByCrmProject(crmProjectId);

        //                       var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
        //                       var crmProject = crmQueryService.RetrieveCrmProject(crmProjectId);

        //                       var defaultComplaintType = ListComplaintTypes().FirstOrDefault();

        //                       If there is no project evaluation for that CRM project and that CRM project exists, create a new project evaluation.


        //                       if (projectComplaint == null && crmProject != null)
        //                       {
        //                           var commandService = Container.Resolve<IProjectManagementCommandService>();
        //                           var request = new CreateProjectComplaintRequest
        //                                         {
        //                                             CrmProjectId = crmProjectId,
        //                                             ComplaintDate = DateTime.Now,
        //                                             ComplaintSeverityTypeId = (int)ComplaintSeverityType.Low,
        //                                             ComplaintTypeId = defaultComplaintType.Id,
        //                                             Details = "",
        //                                             FollowUp = "",
        //                                             Subject = "",
        //                                         };
        //                           var createdId = commandService.CreateProjectComplaint(request);

        //                           projectComplaint = repository.Retrieve<ProjectComplaintView>(createdId);
        //                       }


        //                       return projectComplaint;
        //                   });
        //}

        public ProjectComplaintView RetrieveProjectComplaint(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectComplaint = repository.Retrieve<ProjectComplaintView>(id);

                return projectComplaint;
            });
        }

        public ListProductScoresResponse ListProductScores(ListProductScoresRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var response = new ListProductScoresResponse();

                response.NeopirScores = repository.ListNeopirScores(request.ProjectCandidateId);
                response.LeaderScores = repository.ListLeiderschapScores(request.ProjectCandidateId);
                response.RoiScores = repository.ListRoiScores(request.ProjectCandidateId);
                response.ProjectCandidate = repository.RetrieveProjectCandidate(request.ProjectCandidateId);
                response.Project = repository.RetrieveProjectDetail(response.ProjectCandidate.ProjectId);
                response.MotivationInterview = false;

                List<ProjectCategoryDetailSimulationCombinationView> simulationCombinations = null;

                var projectMainCategoryDetail = response.Project.ProjectCategoryDetails.FirstOrDefault(pcd => pcd.ProjectTypeCategory.IsMain);
                if (projectMainCategoryDetail != null)
                {
                    simulationCombinations = repository.ListProjectCategoryDetailSimulationCombinations(projectMainCategoryDetail.Id);

                    for (int i = 0; i < simulationCombinations.Count; i++)
                    {
                        if (simulationCombinations[i].SimulationName.Contains("Motivatie interview")) response.MotivationInterview = true;
                    }
                }

                if (response.Project.ROI) response.MotivationInterview = true;

                return response;
            });
        }

        public RetrieveProjectCandidateReportingResponse RetrieveProjectCandidateReporting(Guid projectCandidateId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();
                var reportManagementQueryService = Container.Resolve<IReportManagementQueryService>();
                var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

                var response = new RetrieveProjectCandidateReportingResponse();

                //Retrieve project candidate and look up its co-assessors
                response.ProjectCandidate = repository.RetrieveProjectCandidateDetail(projectCandidateId);
                var coAssessors = crmQueryService.ListCoAssessors(response.ProjectCandidate.ProjectId);
                response.ProjectCandidate.ProjectCandidateDetail.CoAssessors = coAssessors.Where(ca => ca.Code == response.ProjectCandidate.Code).ToList();

                response.Project = (AssessmentDevelopmentProjectView)repository.RetrieveProjectDetail(response.ProjectCandidate.ProjectId);

                if (response.Project.CandidateReportDefinitionId.HasValue)
                    response.CandidateReportDefinition = reportManagementQueryService.RetrieveCandidateReportDefinition(response.Project.CandidateReportDefinitionId.Value);

                response.ReportDefinitions = reportManagementQueryService.ListReportDefinitions(new ListReportDefinitionsRequest { Code = "CANDIDATE" });

                return response;
            });
        }

        public ConsultancyProjectView RetrieveProjectByProjectPlan(Guid projectPlanId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var project = (ConsultancyProjectView)repository.RetrieveProjectByProjectPlan(projectPlanId);

                return project;
            });
        }

        public List<ProjectCategoryDetailView> ListProjectCategoryDetails(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectCategoryDetails = repository.ListProjectCategoryDetails(projectId);

                return projectCategoryDetails;
            });
        }

        public List<ProjectReportRecipientView> ListProjectReportRecipientsByProjectId(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectReportRecipients = repository.ListProjectReportRecipientsByProjectId(projectId);

                return projectReportRecipients;
            });
        }

        public ListProjectCandidateOverviewResponse ListProjectCandidateOverviewEntries(ListProjectCandidateOverviewRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var entries = new List<ProjectCandidateOverviewEntryView>();

                Task.WaitAll(new[]
                    {
                        Task.Run(() => entries.AddRange(repository.ListProjectCandidateOverviewEntries(request.StartDate, request.EndDate, request.CustomerAssistantId).Where(x => x.FollowUpDone == false))),
                        Task.Run(() => entries.AddRange(repository.ListProjectCandidateCategoryOverviewEntries(request.StartDate, request.EndDate, request.CustomerAssistantId))),
                        Task.Run(() => entries.AddRange(repository.ListProjectCandidateReservedOverviewEntries(request.StartDate, request.EndDate, request.CustomerAssistantId))),
                        Task.Run(() => entries.AddRange(repository.ListProjectCandidateCancelledOverviewEntries(request.StartDate, request.EndDate, request.CustomerAssistantId)))
                    });

                var response = new ListProjectCandidateOverviewResponse { Entries = entries };
                return response;
            });
        }

        public ProjectCandidateOverviewEntryView RetrieveProjectCandidateOverviewEntry(Guid id, ProjectCandidateOverviewEntryType overviewEntryType)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                ProjectCandidateOverviewEntryView projectCandidateOverviewEntry;

                if (overviewEntryType == ProjectCandidateOverviewEntryType.ProjectCandidate)
                    projectCandidateOverviewEntry = repository.RetrieveProjectCandidateOverviewProjectCandidateEntry(id);
                else
                    projectCandidateOverviewEntry = repository.RetrieveProjectCandidateOverviewProjectCandidateCategoryEntry(id);

                return projectCandidateOverviewEntry;
            });
        }

        public ProjectCategoryDetailView RetrieveProjectCategoryDetail(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.Retrieve<ProjectCategoryDetailView>(id);
            });
        }

        public List<ProjectCandidateReportingOverviewEntryView> ListProjectCandidateReportingOverviewEntries(ListProjectCandidateReportingOverviewEntriesRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();
                var entries = repository.ListProjectCandidateReportingOverviewEntries(request.StartDate, request.CustomerAssistantId);

                return entries;
            });
        }

        public ProjectCandidateReportingOverviewEntryView RetrieveProjectCandidateReportingOverviewEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectCandidateOverviewEntry = repository.RetrieveProjectCandidateReportingOverviewProjectCandidateEntry(id);

                return projectCandidateOverviewEntry;
            });
        }

        public bool HasCandidates(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.HasCandidates(projectId);
            });
        }

        public bool HasProjectProducts(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.HasProjectProducts(projectId);
            });
        }

        public bool HasProjectFixedPrices(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.HasProjectFixedPrices(projectId);
            });
        }

        public TheoremListRequestView RetrieveTheoremListRequestByProjectAndCandidate(Guid projectId, Guid candidateId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.RetrieveTheoremListRequestByProjectAndCandidate(projectId, candidateId);
            });
        }

        public List<ProjectInvoiceAmountOverviewEntryView> ListProjectInvoiceAmountOverviewEntries(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectInvoiceAmountOverviewEntries(id);
            });
        }

        public List<SimulationContextLoginView> ListSimulationContextLogins(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListSimulationContextLogins(id);
            });
        }

        public List<ProjectCandidateView> ListProjectCandidates(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectCandidatesWithCandidateDetail(id);
            });
        }

        public ProjectCandidateView RetrieveProjectCandidateDetailExtended(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();
                var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();

                var projectCandidate = repository.RetrieveProjectCandidateDetailWithTypes(id);

                //Retrieve selected subcategories for project (e.g. NEOPIR, Schein, Scope,...)
                var coAssessors = crmQueryService.ListCoAssessors(projectCandidate.ProjectId);

                projectCandidate.ProjectCandidateDetail.CoAssessors = coAssessors.Where(ca => ca.Code == projectCandidate.Code).ToList();
                projectCandidate.TheoremListRequests = repository.ListCulturalFitCandidateRequests(projectCandidate.CandidateId);

                return projectCandidate;
            });
        }

        public List<ProjectCategoryDetailSimulationCombinationView> ListProjectCandidateSimulationCombinations(Guid projectCandidateId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectCandidate = repository.RetrieveProjectCandidate(projectCandidateId);

                var project = repository.RetrieveProjectDetail(projectCandidate.ProjectId);

                var projectMainCategoryDetail = project.ProjectCategoryDetails.FirstOrDefault(pcd => pcd.ProjectTypeCategory.IsMain);

                return projectMainCategoryDetail != null
                    ? repository.ListProjectCategoryDetailSimulationCombinations(projectMainCategoryDetail.Id)
                    : new List<ProjectCategoryDetailSimulationCombinationView>();
            });
        }

        public List<ProjectCandidateCategoryDetailTypeView> ListProjectCandidateCategoryDetailTypes(Guid projectCandidateId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectCandidateCategoryDetailTypes(projectCandidateId);
            });
        }

        public ProjectDnaView RetrieveProjectDna(int crmProjectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectDna = repository.RetrieveProjectDna(crmProjectId);

                if (projectDna == null)
                {
                    var commandService = Container.Resolve<IProjectManagementCommandService>();

                    commandService.CreateNewProjectDna(new CreateNewProjectDnaRequest { CrmProjectId = crmProjectId, ApprovedForReferences = true });

                    projectDna = repository.RetrieveProjectDna(crmProjectId);
                }
                else
                {
                    var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();

                    var languages = infrastructureQueryService.ListLanguages();

                    var missingLanguageIds = languages.Where(l => !projectDna.ProjectDnaCommercialTranslations.Select(pdct => pdct.LanguageId).Contains(l.Id)).Select(l => l.Id).ToList();

                    if (missingLanguageIds.Any())
                    {
                        var commandService = Container.Resolve<IProjectManagementCommandService>();

                        var request = new CreateNewProjectDnaCommercialTranslationsRequest
                            {
                                ProjectDnaId = projectDna.Id,
                                LanguageIds = missingLanguageIds
                            };
                        commandService.CreateNewProjectDnaCommercialTranslations(request);

                        projectDna = repository.RetrieveProjectDna(crmProjectId);
                    }
                }

                projectDna.ProjectDnaTypes = repository.ListDnaTypes(projectDna.Id);
                projectDna.ProjectDnaContactPersons = repository.ListProjectDnaContactPersons(projectDna.Id);

                return projectDna;
            });
        }

        public ProjectCategoryDetailView RetrieveProjectMainCategoryDetail(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectCategoryDetail = repository.RetrieveMainProjectCategoryDetail(projectId);

                return projectCategoryDetail;
            });
        }

        public ProjectCandidateView RetrieveProjectCandidate(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var projectCandidate = repository.RetrieveProjectCandidate(id);

                return projectCandidate;
            });
        }

        public ProjectPlanView RetrieveProjectPlanDetail(RetrieveProjectPlanDetailRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                if (request.ProjectId.HasValue)
                {
                    var project = (ConsultancyProjectView)repository.Retrieve<ProjectView>(request.ProjectId.Value);
                    return repository.RetrieveProjectPlanDetail(project.ProjectPlanId);
                }

                if (request.ProjectPlanId.HasValue)
                {
                    return repository.RetrieveProjectPlanDetail(request.ProjectPlanId.Value);
                }

                throw new ArgumentNullException("Please supply a ProjectId or ProjectPlanId");
            });
        }

        public ProjectPlanPhaseView RetrieveProjectPlanPhase(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.Retrieve<ProjectPlanPhaseView>(id);
            });
        }

        public ProjectPlanPhaseView RetrieveProjectPlanPhaseDetail(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.RetrieveProjectPlanPhaseDetail(id);
            });
        }

        public ProjectPlanPhaseActivityView RetrieveProjectPlanPhaseActivity(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.RetrieveProjectPlanPhaseEntry<ProjectPlanPhaseActivityView>(id);
            });
        }

        public List<ProjectPriceIndexView> ListProjectPriceIndices(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectPriceIndices(projectId);
            });
        }

        public ProjectPriceIndexView RetrieveProjectPriceIndex(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.Retrieve<ProjectPriceIndexView>(id);
            });
        }

        public List<TimesheetEntryView> ListAllProjectTimesheets(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
                {

                    var repository = Container.Resolve<IProjectManagementQueryRepository>();

                    return repository.ListProjectTimesheets(projectId);
                });
        }


        public List<ProductsheetEntryView> ListProjectUserProductsheets(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var securityContext = Container.Resolve<SecurityContext>();

                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectProductsheets(projectId, securityContext.UserId);
            });
        }

        public List<TimesheetEntryView> ListProjectTimesheets(Guid projectId, DateTime dateFrom, DateTime dateTo)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectTimesheets(projectId, dateFrom: dateFrom, dateTo: dateTo);
            });
        }

        public List<TimesheetEntryView> ListUserTimesheets(int year, int month, Guid userId, bool isProjectManager)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var dateFrom = new DateTime(year, month, 1);
                var dateTo = dateFrom.AddMonths(1);
                return repository.ListProjectTimesheets(userId: userId, dateFrom: dateFrom, dateTo: dateTo, isProjectManager: isProjectManager);
            });
        }

        public ProjectPlanPhaseProductView RetrieveProjectPlanPhaseProduct(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.RetrieveProjectPlanPhaseEntry<ProjectPlanPhaseProductView>(id);
            });
        }

        public ProjectPlanPhaseEntryView RetrieveProjectPlanPhaseEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.RetrieveProjectPlanPhaseEntry(id);
            });
        }

        public List<ProjectPlanPhaseActivityView> ListRelatedProjectPlanPhaseActivities(Guid projectPlanPhaseId, Guid activityId, Guid profileId, decimal duration)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListRelatedProjectPlanPhaseActivities(projectPlanPhaseId, activityId, profileId, duration);
            });
        }

        public List<ProjectPlanPhaseProductView> ListRelatedProjectPlanPhaseProducts(Guid projectPlanPhaseId, Guid productId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListRelatedProjectPlanPhaseProducts(projectPlanPhaseId, productId);
            });
        }

        public List<ProjectPlanPhaseProductView> ListProjectPlanPhaseProducts(Guid projectPlanPhaseId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectPlanPhaseProducts(projectPlanPhaseId);
            });
        }

        public List<ProjectFixedPriceView> ListProjectFixedPrices(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectFixedPrices(id);
            });
        }

        public ProjectFixedPriceView RetrieveProjectFixedPrice(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.Retrieve<ProjectFixedPriceView>(id);
            });
        }

        public List<ProjectPlanPhaseView> ListActiveProjectPlanPhases(Guid projectId, int? year, int? month)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var project = (ConsultancyProjectView)repository.Retrieve<ProjectView>(projectId);

                return repository.ListActiveProjectPlanPhases(project.ProjectPlanId, year, month);
            });
        }

        public List<SubProjectView> ListSubProjects(Guid projectId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListSubProjects(projectId);
            });
        }

        public List<TimesheetEntryView> RetrieveProjectTimesheet(Guid projectId, Guid? userId, int year, int month, bool isProjectManager = false)
        {
            LogTrace();

            return Execute(() =>
            {
                if (!userId.HasValue)
                {
                    var securityContext = Container.Resolve<SecurityContext>();
                    userId = securityContext.UserId;
                }

                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var dateFrom = new DateTime(year, month, 1);
                var dateTo = dateFrom.AddMonths(1);
                return repository.ListProjectTimesheets(projectId, userId, dateFrom, dateTo, isProjectManager);
            });
        }

        public List<ProductsheetEntryView> RetrieveProjectProductsheet(Guid projectId, int? year, int? month)
        {
            LogTrace();

            return Execute(() =>
            {
                var securityContext = Container.Resolve<SecurityContext>();

                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectProductsheets(projectId, securityContext.UserId, year, month);
            });
        }

        public ListProjectsResponse ListRecentProjects(ListProjectsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.PRMALLMYPR);

                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                var securityContext = Container.Resolve<SecurityContext>();

                var pagingInfo = Mapper.DynamicMap<PagingInfo>(request.DataTablePaging);

                var response = new ListProjectsResponse
                {
                    Projects = repository.ListRecentProjects(securityContext.UserId, pagingInfo),
                    DataTablePaging = Mapper.DynamicMap<DataTablePaging>(pagingInfo)
                };

                return response;
            });
        }

        public ProjectView RetrieveProjectWithCrmProjects(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.RetrieveProjectWithCrmProjects(id);
            });
        }

        public List<SearchCrmProjectResultItemView> SearchCrmProjects(SearchCrmProjectsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.SearchCrmProjects(request.ProjectId, request.ProjectName, request.WithPlannedStatus, request.WithRunningStatus, request.WithDoneStatus, request.WithStoppedStatus);
            });
        }

        public List<ProjectStatusCodeViewType> ListPossibleStatusses(int statusCode)
        {
            LogTrace();

            return Execute(() =>
            {
                switch ((ProjectStatusCodeViewType)statusCode)
                {
                    case ProjectStatusCodeViewType.Draft:
                        return new List<ProjectStatusCodeViewType>
                        {
                            ProjectStatusCodeViewType.Draft,
                            ProjectStatusCodeViewType.Ready
                        };

                    case ProjectStatusCodeViewType.Ready:
                        return new List<ProjectStatusCodeViewType>
                        {
                            ProjectStatusCodeViewType.Ready,
                            ProjectStatusCodeViewType.Started,
                            ProjectStatusCodeViewType.Stopped
                        };

                    case ProjectStatusCodeViewType.Started:
                    case ProjectStatusCodeViewType.Restarted:
                        return new List<ProjectStatusCodeViewType>
                        {
                            ProjectStatusCodeViewType.Stopped,
                            ProjectStatusCodeViewType.Finished
                        };

                    case ProjectStatusCodeViewType.Stopped:
                        return new List<ProjectStatusCodeViewType>
                        {
                            ProjectStatusCodeViewType.Restarted
                        };

                    default:
                        return new List<ProjectStatusCodeViewType>
                        {
                            (ProjectStatusCodeViewType)statusCode
                        };
                }
            });
        }

        public List<ProjectRoleView> ListProjectRolesForQuintessence()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectRolesForQuintessence();
            });
        }

        public List<ProjectRoleView> ListProjectRolesForContacts()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();

                return repository.ListProjectRolesForContacts();
            });
        }

        public ProjectRoleView RetrieveProjectRole(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IProjectManagementQueryRepository>();
                var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();

                var projectRole = repository.RetrieveProjectRole(id);

                var languages = infrastructureQueryService.ListLanguages();
                var missingLanguages = languages.Where(l => !projectRole.ProjectRoleTranslations.Select(prt => prt.LanguageId).Contains(l.Id)).ToList();

                if (languages.Any(l => projectRole.ProjectRoleTranslations.Count == 0 || missingLanguages.Count > 0))
                {
                    var commandService = Container.Resolve<IProjectManagementCommandService>();
                    foreach (var language in missingLanguages)
                        commandService.CreateNewProjectRoleLanguage(new CreateNewProjectRoleLanguageRequest { ProjectRoleId = id, LanguageId = language.Id });

                    projectRole = repository.RetrieveProjectRole(id);
                }

                return projectRole;
            });
        }

    }

}
