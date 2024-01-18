using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Transactions;
using AutoMapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.DataModel.Prm;
using Quintessence.QService.DataModel.Prm.Interfaces;
using Quintessence.QService.DataModel.Sim;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QPlanetService.Implementation.Base.Behaviors;
using Quintessence.QService.QueryModel.Dim;
using Quintessence.QService.QueryModel.Prm;
using UpdateProjectCandidateInvoicingRequest = Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement.UpdateProjectCandidateInvoicingRequest;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.DataModel.Scm;
using System.Data.SqlClient;
using System.Configuration;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class ProjectManagementCommandService : SecuredUnityServiceBase, IProjectManagementCommandService
    {
        public Guid CreateNewProject(CreateNewProjectRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var queryService = Container.Resolve<IProjectManagementQueryService>();
                var dictionaryManagementQueryService = Container.Resolve<IDictionaryManagementQueryService>();

                LogTrace("Prepare project {0} (type: {1}).", request.ProjectName, request.ProjectTypeId);

                var project = repository.PrepareProject(request.ProjectTypeId, request.ProjectName);

                ProjectView mainProject = null;

                //create as subproject
                if (request.MainProjectId.HasValue)
                {
                    mainProject = queryService.RetrieveProjectDetail(request.MainProjectId.Value);

                    project.ProjectManagerId = mainProject.ProjectManagerId;
                    project.CustomerAssistantId = mainProject.CustomerAssistantId;
                    project.ContactId = mainProject.ContactId;
                }
                else
                {
                    project.ProjectManagerId = request.ProjectManagerUserId;
                    project.CustomerAssistantId = request.CustomerAssistantUserId;
                    project.ContactId = request.ContactId;
                }

                LogTrace("Save project {0} (id: {1}, type: {2}).", project.Name, project.Id, project.ProjectTypeId);

                var assessmentDevelopmentProject = project as AssessmentDevelopmentProject;
                if (assessmentDevelopmentProject != null)
                {
                    assessmentDevelopmentProject.CandidateScoreReportTypeId = 2;
                    assessmentDevelopmentProject.ReportDeadlineStep = 2; //Default 2 workdays
                }

                var consultancyProject = project as ConsultancyProject;
                if (consultancyProject != null)
                {
                    var projectPlan = repository.Prepare<ProjectPlan>();
                    repository.Save(projectPlan);
                    consultancyProject.ProjectPlanId = projectPlan.Id;
                }

                ProjectView copyProject = null;
                if (request.CopyProjectId.HasValue)
                {
                    copyProject = queryService.RetrieveProjectDetail(request.CopyProjectId.Value);
                }

                if (request.CopyProjectId.HasValue)
                {
                    queryService.RetrieveProjectDetail(request.CopyProjectId.Value);
                    project.Remarks = copyProject.Remarks;
                    project.DepartmentInformation = copyProject.DepartmentInformation;
                    project.CoProjectManagerId = copyProject.CoProjectManagerId;
                    project.Confidential = copyProject.Confidential;

                    if (copyProject is AssessmentDevelopmentProjectView && assessmentDevelopmentProject != null)
                    {
                        ((AssessmentDevelopmentProject)project).FunctionTitle = ((AssessmentDevelopmentProjectView)copyProject).FunctionTitle;
                        ((AssessmentDevelopmentProject)project).FunctionTitleEN = ((AssessmentDevelopmentProjectView)copyProject).FunctionTitleEN;
                        ((AssessmentDevelopmentProject)project).FunctionTitleFR = ((AssessmentDevelopmentProjectView)copyProject).FunctionTitleFR;
                        ((AssessmentDevelopmentProject)project).FunctionInformation = ((AssessmentDevelopmentProjectView)copyProject).FunctionInformation;
                        ((AssessmentDevelopmentProject)project).CandidateScoreReportTypeId = ((AssessmentDevelopmentProjectView)copyProject).CandidateScoreReportTypeId;
                        ((AssessmentDevelopmentProject)project).PhoneCallRemarks = ((AssessmentDevelopmentProjectView)copyProject).PhoneCallRemarks;
                        ((AssessmentDevelopmentProject)project).ReportRemarks = ((AssessmentDevelopmentProjectView)copyProject).ReportRemarks;
                        ((AssessmentDevelopmentProject)project).IsRevisionByPmRequired = ((AssessmentDevelopmentProjectView)copyProject).IsRevisionByPmRequired;
                        ((AssessmentDevelopmentProject)project).SendReportToParticipant = ((AssessmentDevelopmentProjectView)copyProject).SendReportToParticipant;
                        ((AssessmentDevelopmentProject)project).SendReportToParticipantRemarks = ((AssessmentDevelopmentProjectView)copyProject).SendReportToParticipantRemarks;
                        ((AssessmentDevelopmentProject)project).ReportDeadlineStep = ((AssessmentDevelopmentProjectView)copyProject).ReportDeadlineStep;
                    }
                    else
                    {
                        ((ConsultancyProject)project).ProjectInformation = ((ConsultancyProjectView)copyProject).ProjectInformation;
                    }                                        
                }

                //If the new project is a copy of another project, check if it's an AssessmentDevelopment project then try to copy the dictionary
                if (request.CopyProjectId.HasValue)
                {

                    if (copyProject is AssessmentDevelopmentProjectView && assessmentDevelopmentProject != null)
                    {
                        var dictionaryId = ((AssessmentDevelopmentProjectView)copyProject).DictionaryId;

                        if (dictionaryId != null)
                        {
                            var dictionary = dictionaryManagementQueryService.RetrieveDictionary(dictionaryId.Value);

                            //Only copy the dictionary if it is a Quintessence dictionary or
                            //if the customer on the copy project is the same as the customer on the target project
                            if (!dictionary.ContactId.HasValue || dictionary.ContactId.Value == project.ContactId)
                            {
                                ((AssessmentDevelopmentProject)project).DictionaryId = dictionaryId;
                            }
                        }
                    }
                }

                if (!ValidationContainer.ValidateObject(project))
                    return Guid.Empty;

                var projectId = repository.Save(project);

                if (request.CopyProjectId.HasValue)
                {
                    var copyAssesmentProject = copyProject as AssessmentDevelopmentProjectView;

                    if (copyAssesmentProject != null && assessmentDevelopmentProject != null)
                    {
                        Guid? copyProjectMainCategoryDetailId = null;
                        Guid? mainProjectCategoryDetailId = null;
                        Guid? projectRoleId = null;

                        //Copy the project category details
                        foreach (var projectCategoryDetailView in copyAssesmentProject.ProjectCategoryDetails)
                        {
                            if(request.ConvertProjectType != "NA" && projectCategoryDetailView.ProjectTypeCategory.IsMain)
                            {
                                projectCategoryDetailView.ProjectTypeCategoryId = Guid.Parse(request.ConvertProjectType);
                            }

                            var projectCategoryDetail = repository.PrepareProjectCategoryDetail(projectCategoryDetailView.ProjectTypeCategoryId);
                            
                            projectCategoryDetail.ProjectId = projectId;
                            projectCategoryDetail.UnitPrice = projectCategoryDetailView.UnitPrice;

                            if (projectCategoryDetailView.ProjectTypeCategory.IsMain)
                            {
                                copyProjectMainCategoryDetailId = projectCategoryDetailView.Id;

                                if (copyAssesmentProject.MainProjectCategoryDetail is ProjectCategoryFaDetailView ||
                                    copyAssesmentProject.MainProjectCategoryDetail is ProjectCategoryFdDetailView)
                                {
                                    projectRoleId = (Guid?)((dynamic)copyAssesmentProject.MainProjectCategoryDetail).ProjectRoleId;

                                    if (projectRoleId.HasValue)
                                    {
                                        var projectRole = queryService.RetrieveProjectRole(projectRoleId.Value);

                                        //Only assign a projectrole if it is one of Quintessence or one of the customer of the new project
                                        if (projectRole.ContactId.HasValue && projectRole.ContactId.Value != assessmentDevelopmentProject.ContactId)
                                            projectRoleId = null;
                                    }
                                }

                                mainProjectCategoryDetailId = repository.Save(projectCategoryDetail);
                            }
                            else
                            {
                                
                                repository.Save(projectCategoryDetail);

                                //copy projectcategorydetailtype descriptions
                                if (projectCategoryDetail is ProjectCategoryDetailType1)
                                {
                                    var copyprojectdetailModel = Mapper.Map<ProjectCategoryDetailType1View>(projectCategoryDetailView);
                                    var projectdetailModel = Mapper.Map<ProjectCategoryDetailType1>(projectCategoryDetail);
                                    projectdetailModel.Description = copyprojectdetailModel.Description;

                                    var updateProjectCategoryDetailTypeRequest = new UpdateProjectCategoryDetailTypeRequest
                                    {
                                        Id = projectdetailModel.Id,
                                        Description = copyprojectdetailModel.Description,
                                        SurveyPlanningId = copyprojectdetailModel.SurveyPlanningId,
                                        AuditVersionid = copyprojectdetailModel.Audit.VersionId
                                    };
                                    
                                    UpdateProjectCategoryDetailType1(updateProjectCategoryDetailTypeRequest);
                                }

                                if (projectCategoryDetail is ProjectCategoryDetailType2)
                                {
                                    var copyprojectdetailModel = Mapper.Map<ProjectCategoryDetailType2View>(projectCategoryDetailView);
                                    var projectdetailModel = Mapper.Map<ProjectCategoryDetailType2>(projectCategoryDetail);
                                    projectdetailModel.Description = copyprojectdetailModel.Description;

                                    var updateProjectCategoryDetailTypeRequest = new UpdateProjectCategoryDetailTypeRequest
                                    {
                                        Id = projectdetailModel.Id,
                                        Description = copyprojectdetailModel.Description,
                                        SurveyPlanningId = copyprojectdetailModel.SurveyPlanningId,
                                        AuditVersionid = copyprojectdetailModel.Audit.VersionId
                                    };

                                    UpdateProjectCategoryDetailType2(updateProjectCategoryDetailTypeRequest);
                                }

                                if (projectCategoryDetail is ProjectCategoryDetailType3)
                                {
                                    var copyprojectdetailModel = Mapper.Map<ProjectCategoryDetailType3View>(projectCategoryDetailView);
                                    var projectdetailModel = Mapper.Map<ProjectCategoryDetailType3>(projectCategoryDetail);
                                    projectdetailModel.Description = copyprojectdetailModel.Description;

                                    var updateProjectCategoryDetailTypeRequest = new UpdateProjectCategoryDetailTypeRequest
                                    {
                                        Id = projectdetailModel.Id,
                                        Description = copyprojectdetailModel.Description,
                                        SurveyPlanningId = copyprojectdetailModel.SurveyPlanningId,
                                        AuditVersionid = copyprojectdetailModel.Audit.VersionId,
                                        IncludeInCandidateReport = copyprojectdetailModel.IncludeInCandidateReport
                                    };

                                    UpdateProjectCategoryDetailType3(updateProjectCategoryDetailTypeRequest);
                                }
                                
                            }
                        }

                        //Only copy if there is a main project category detail
                        if (mainProjectCategoryDetailId.HasValue && ((AssessmentDevelopmentProject)project).DictionaryId.HasValue)
                        {
                            //Retrieve all information at once to prevent deadlock
                            var projectCategoryDetailSimulationCombinations = queryService.ListProjectCategoryDetailSimulationCombinations(copyProjectMainCategoryDetailId.Value);

                            //Only handle non defined by role indicators
                            var projectCategoryDetailDictionaryIndicators = queryService.ListProjectCategoryDetailDictionaryIndicators(new ListProjectCategoryDetailDictionaryIndicatorsRequest(copyProjectMainCategoryDetailId.Value))
                                .Where(pcddi => !pcddi.IsDefinedByRole)
                                .ToList();
                            var projectCategoryDetailCompetenceSimulations = queryService.ListProjectCategoryDetailCompetenceSimulations(copyProjectMainCategoryDetailId.Value);

                            if (projectCategoryDetailSimulationCombinations.Count > 0)
                            {
                                LinkProjectCategoryDetail2SimulationCombinations(mainProjectCategoryDetailId.Value,
                                    projectCategoryDetailSimulationCombinations.Select(pcdsc => pcdsc.SimulationCombinationId).ToList());
                            }

                            //Only perform the next steps if a dictionary is assigned to the project
                            if (((AssessmentDevelopmentProject)project).DictionaryId.HasValue)
                            {
                                if (projectRoleId.HasValue)
                                    AssignProjectRole(mainProjectCategoryDetailId.Value, projectRoleId.Value);

                                //Assign by indicators
                                if (projectCategoryDetailDictionaryIndicators.Count > 0)
                                {
                                    LinkProjectCategoryDetail2DictionaryIndicators(new LinkProjectCategoryDetail2DictionaryIndicatorsRequest
                                        {
                                            ProjectCategoryDetailId = mainProjectCategoryDetailId.Value,
                                            DictionaryIndicatorIds = projectCategoryDetailDictionaryIndicators.Select(pcddi => pcddi.DictionaryIndicatorId).ToList()
                                        });
                                }

                                foreach (var projectCategoryDetailCompetenceSimulation in projectCategoryDetailCompetenceSimulations)
                                {
                                    LinkProjectCategoryDetail2Competence2Combination(mainProjectCategoryDetailId.Value,
                                        projectCategoryDetailCompetenceSimulation.DictionaryCompetenceId,
                                        projectCategoryDetailCompetenceSimulation.SimulationCombinationId);
                                }
                            }
                        }
                    }
                }

                //Create as subproject
                if (request.MainProjectId.HasValue)
                {
                    repository.LinkSubProject(request.MainProjectId.Value, projectId, request.ProjectCandidateId);

                    mainProject = queryService.RetrieveProjectWithCrmProjects(request.MainProjectId.Value);
                    foreach (var crmProjectView in mainProject.CrmProjects)
                        repository.LinkProject2CrmProject(projectId, crmProjectView.Id);
                }
                else
                {
                    if (request.CrmProjectId.HasValue)
                        repository.LinkProject2CrmProject(projectId, request.CrmProjectId.Value);
                }

                return projectId;
            });
        }

        public void LinkProject2CrmProject(Guid id, int crmProjectId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                LogTrace("Link project {0} with crmproject {1}.", id, crmProjectId);

                repository.LinkProject2CrmProject(id, crmProjectId);
            });
        }

        public void UnlinkProject2CrmProject(Guid id, int crmProjectId)
        {
            LogTrace();

            ExecuteTransaction(() =>
                {
                    var repository = Container.Resolve<IProjectManagementCommandRepository>();

                    LogTrace("Unlink project {0} with crmproject {1}.", id, crmProjectId);

                    var project = repository.RetrieveProject(id);

                    var statusCodeType = (ProjectStatusCodeType)project.StatusCode;
                    switch (statusCodeType)
                    {
                        case ProjectStatusCodeType.Ready:
                        case ProjectStatusCodeType.Started:
                            if (repository.ListProject2CrmProject(id).All(p => p.CrmProjectId == crmProjectId))
                                ValidationContainer.RegisterEntityValidationFaultEntry(string.Format("Unable to unlink CRM project because at least one project must be linked when status is '{0}'.", statusCodeType));
                            break;
                    }

                    repository.UnlinkProject2CrmProject(id, crmProjectId);
                });
        }

        public void UpdateAssessmentDevelopmentProject(UpdateAssessmentDevelopmentProjectRequest updateProjectRequest)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                LogTrace("Update Assessment & Development project (id: {0}).", updateProjectRequest.Id);

                var project = (AssessmentDevelopmentProject)repository.RetrieveProject(updateProjectRequest.Id);

                if (project.Audit.VersionId != updateProjectRequest.AuditVersionid)
                    ValidationContainer.RegisterVersionMismatchEntry(updateProjectRequest, project);

                var storeStatusCode = (ProjectStatusCodeType)project.StatusCode;
                var pricingModelId = project.PricingModelId;

                updateProjectRequest.Lock = project.Lock;

                Mapper.DynamicMap(updateProjectRequest, project);
                if (project is AssessmentDevelopmentProject)
                    Mapper.DynamicMap(updateProjectRequest, project);

                var queryService = Container.Resolve<IProjectManagementQueryService>();
                var existingProjectCategoryDetails = queryService.RetrieveProjectDetail(updateProjectRequest.Id).ProjectCategoryDetails;

                var request = new ListProjectCandidateDetailsRequest { ProjectId = updateProjectRequest.Id };
                var candidates = queryService.ListProjectCandidateDetails(request).Where(c => c.IsCancelled.Equals(false));
                var candidateIds = candidates.Select(c => c.Id).ToList();

                if (updateProjectRequest.ProjectTypeCategoryId.HasValue)
                {
                    ProjectCategoryDetailView existingMainProjectCategoryDetail = null;
                    if (existingProjectCategoryDetails != null)
                    {
                        if (existingProjectCategoryDetails.Count(pcd => pcd.ProjectTypeCategory.IsMain) > 1)
                        {
                            //Remove all excessive main project category details
                            existingProjectCategoryDetails
                                .Where(pcd => pcd.ProjectTypeCategory.IsMain && pcd.ProjectTypeCategoryId != updateProjectRequest.ProjectTypeCategoryId)
                                .ForEach(pcd => repository.Delete<ProjectCategoryDetail>(pcd.Id));

                            existingProjectCategoryDetails = queryService.RetrieveProjectDetail(updateProjectRequest.Id).ProjectCategoryDetails.OrderByDescending(pcd => pcd.Audit.CreatedOn).ToList();

                            //If there are still more than one, it means that there are more projecttypecategories of the same projecttypecategory.
                            //We also need to remove all these details, but keep the last created on
                            if (existingProjectCategoryDetails.Count(pcd => pcd.ProjectTypeCategory.IsMain) > 1)
                            {
                                existingProjectCategoryDetails
                                    .Where(pcd => pcd.ProjectTypeCategory.IsMain && pcd.ProjectTypeCategoryId == updateProjectRequest.ProjectTypeCategoryId)
                                    .Skip(1)
                                    .ForEach(pcd => repository.Delete<ProjectCategoryDetail>(pcd.Id));

                                existingProjectCategoryDetails = queryService.RetrieveProjectDetail(updateProjectRequest.Id).ProjectCategoryDetails.OrderByDescending(pcd => pcd.Audit.CreatedOn).ToList();
                            }
                        }

                        existingMainProjectCategoryDetail = existingProjectCategoryDetails.FirstOrDefault(pcd => pcd.ProjectTypeCategory.IsMain);

                        if (existingMainProjectCategoryDetail != null && existingMainProjectCategoryDetail.ProjectTypeCategoryId != updateProjectRequest.ProjectTypeCategoryId)
                        {
                            //Remove existing scores of candidates
                            foreach (var candidate in candidates)
                                repository.DeleteProjectCandidateScores(candidate.Id);
                            
                            //Remove existing linked indicators, simulations and combinations
                            repository.DeleteProjectCategoryDetailDictionaryIndicators(existingMainProjectCategoryDetail.Id);
                            repository.DeleteProjectCategoryDetailCompetenceSimulations(existingMainProjectCategoryDetail.Id);
                            repository.DeleteProjectCategoryDetailSimulationCombinations(existingMainProjectCategoryDetail.Id);                                                     

                            repository.Delete<ProjectCategoryDetail>(existingMainProjectCategoryDetail.Id);
                            existingMainProjectCategoryDetail = null;
                        }
                    }

                    if (existingMainProjectCategoryDetail == null)
                    {
                        var projectCategoryDetail = repository.PrepareProjectCategoryDetail(updateProjectRequest.ProjectTypeCategoryId.Value);
                        projectCategoryDetail.ProjectId = project.Id;
                        repository.Save(projectCategoryDetail);
                    }
                }
                else
                {
                    foreach (var existingProjectCategoryDetail in existingProjectCategoryDetails.Where(pcd => pcd.ProjectTypeCategory.IsMain))
                    {
                        //Remove existing scores of candidates
                        foreach (var candidate in candidates)
                            repository.DeleteProjectCandidateScores(candidate.Id);

                        //Remove existing linked indicators, simulations and combinations
                        repository.DeleteProjectCategoryDetailDictionaryIndicators(existingProjectCategoryDetail.Id);
                        repository.DeleteProjectCategoryDetailCompetenceSimulations(existingProjectCategoryDetail.Id);
                        repository.DeleteProjectCategoryDetailSimulationCombinations(existingProjectCategoryDetail.Id);

                        repository.Delete<ProjectCategoryDetail>(existingProjectCategoryDetail.Id);
                    }
                }

                //Update Subcategories
                if (existingProjectCategoryDetails != null)
                {
                    foreach (var existingProjectCategoryDetail in existingProjectCategoryDetails.Where(pcd => !pcd.ProjectTypeCategory.IsMain))
                    {
                        if (!updateProjectRequest.SelectedProjectTypeCategoryIds.Contains(existingProjectCategoryDetail.ProjectTypeCategoryId))
                        {
                            //Try to delete projectcategorydetail only when projectstatus is draft
                           //if (((ProjectStatusCodeType)project.StatusCode) != ProjectStatusCodeType.Draft)
                           //{
                           //    var projectTypeCategory = repository.RetrieveProjectTypeCategory(existingProjectCategoryDetail.ProjectTypeCategoryId);
                           //    ValidationContainer.RegisterEntityValidationFaultEntry(string.Format("Unable to remove project category '{0}' because status is not '{1}'.", projectTypeCategory.Name, ProjectStatusCodeType.Draft));
                           //}
                           //else
                           //{
                             repository.Delete<ProjectCategoryDetail>(existingProjectCategoryDetail.Id);
                             repository.DeleteProjectCandidateCategoryDetail(candidateIds, existingProjectCategoryDetail.Id);
                           //}
                        }
                    }
                }

                //Create new projectCategoryDetails
                foreach (var projectTypeCategoryId in updateProjectRequest.SelectedProjectTypeCategoryIds.Where(projectTypeCategoryId => existingProjectCategoryDetails == null || !existingProjectCategoryDetails.Select(pcd => pcd.ProjectTypeCategoryId).Contains(projectTypeCategoryId)))
                {
                    var projectCategoryDetail = repository.PrepareProjectCategoryDetail(projectTypeCategoryId);
                    projectCategoryDetail.ProjectId = project.Id;
                    repository.Save(projectCategoryDetail);
                    foreach (var projectCandidateView in candidates)
                        EnsureProjectCategoryDetailTypes(projectCandidateView, new[] { queryService.RetrieveProjectCategoryDetail(projectCategoryDetail.Id) });
                }

                if (updateProjectRequest.StatusCode != (int)storeStatusCode)
                    ValidateStatusUpdate(storeStatusCode, project);

                //Validate PricingModelChange
                if (pricingModelId != project.PricingModelId)
                {
                    switch (pricingModelId)
                    {
                        case 1: //Time & material
                            //if candidates: unable to change pricingmodel.
                            if (queryService.HasCandidates(project.Id))
                            {
                                ValidationContainer.RegisterEntityValidationFaultEntry("Unable to change the pricing model of this project since there are already candidates registered.");
                            }
                            if (queryService.HasProjectProducts(project.Id))
                            {
                                ValidationContainer.RegisterEntityValidationFaultEntry("Unable to change the pricing model of this project since there are products defined.");
                            }
                            break;

                        case 2: //Fixed price
                            if (queryService.HasProjectFixedPrices(project.Id))
                            {
                                ValidationContainer.RegisterEntityValidationFaultEntry("Unable to change the pricing model of this project since there are already fixed price entries defined.");
                            }
                            break;
                    }
                }

                Validate(project);

                if (ValidationContainer.ValidateObject(project))
                    repository.Save(project);

                //var projectCategoryDetailTypes = queryService.ListProjectCategoryDetails(project.Id).Where(pcd => !pcd.ProjectTypeCategory.IsMain).ToList();

                //foreach (var candidate in candidates)
                //{
                //    EnsureProjectCategoryDetailTypes(candidate, projectCategoryDetailTypes);
                //}

                return updateProjectRequest.Id;
            });
        }

        private void EnsureProjectCategoryDetailTypes(ProjectCandidateView projectCandidate, IEnumerable<ProjectCategoryDetailView> projectCategoryDetailTypes)
        {
            var queryService = Container.Resolve<IProjectManagementQueryService>();

            var detailTypeRequests = new List<UpdateProjectCandidateCategoryDetailTypeRequest>();
            //Loop selected subcategories
            foreach (var projectCategoryDetailType in projectCategoryDetailTypes)
            {
                var projectCandidateCategoryDetailType =
                        projectCandidate.ProjectCandidateCategoryDetailTypes.FirstOrDefault(
                            pccdt => pccdt.ProjectCategoryDetailTypeId == projectCategoryDetailType.Id);
                //If subcategory was already linked with project candidate, then continue loop
                if (projectCandidateCategoryDetailType != null)
                {
                    //Check if scheduled date is filled in when survey planning id  = 2 or 3 (before or after)
                    var request = EnsureSurveyPlanningDependentFields(projectCandidateCategoryDetailType, projectCandidate);
                    if (request != null)
                        detailTypeRequests.Add(request);

                }
                else //Else create new link between project candidate and subcategory
                {
                    var projectCandidateCategoryDetailTypeId = CreateNewProjectCandidateCategoryDetailTypeInternal(projectCandidate.Id, projectCategoryDetailType.Id);
                    var link = queryService.RetrieveProjectCandidateCategoryDetailType(projectCandidateCategoryDetailTypeId);
                    link.ProjectCandidate = projectCandidate;
                    projectCandidate.ProjectCandidateCategoryDetailTypes.Add(link);
                }
            }
            //Update the Project Candidate Category Detail Types
            if (detailTypeRequests.Count > 0)
                UpdateProjectCandidateCategoryDetailTypesInternal(detailTypeRequests);

        }

        private UpdateProjectCandidateCategoryDetailTypeRequest EnsureSurveyPlanningDependentFields(ProjectCandidateCategoryDetailTypeView projectCandidateCategoryDetailType, ProjectCandidateView projectCandidate)
        {
            UpdateProjectCandidateCategoryDetailTypeRequest request = null;
            if (projectCandidateCategoryDetailType is ProjectCandidateCategoryDetailType1View)
            {
                var projectCandidateCategoryDetailType1 = projectCandidateCategoryDetailType as ProjectCandidateCategoryDetailType1View;
                if (projectCandidateCategoryDetailType1.ScheduledDate == null && (projectCandidateCategoryDetailType1.SurveyPlanningId == (int)SurveyPlanningType.Before || projectCandidateCategoryDetailType1.SurveyPlanningId == (int)SurveyPlanningType.After))
                {
                    DateTime scheduledDate;
                    switch (projectCandidateCategoryDetailType1.SurveyPlanningId)
                    {

                        case 2: //Before
                            scheduledDate = projectCandidate.ProjectCandidateDetail.AssessmentStartDate.GetValueOrDefault().AddDays(-1);
                            projectCandidateCategoryDetailType1.ScheduledDate = scheduledDate;
                            break;
                        case 3: //After
                            scheduledDate = projectCandidate.ProjectCandidateDetail.AssessmentStartDate.GetValueOrDefault().AddDays(1);
                            projectCandidateCategoryDetailType1.ScheduledDate = scheduledDate;
                            break;
                        default:
                            projectCandidateCategoryDetailType1.ScheduledDate = null;
                            break;
                    }
                    request = Mapper.DynamicMap<UpdateProjectCandidateCategoryDetailType1Request>(projectCandidateCategoryDetailType1);
                }

            }

            if (projectCandidateCategoryDetailType is ProjectCandidateCategoryDetailType3View)
            {
                var projectCandidateCategoryDetailType3 = projectCandidateCategoryDetailType as ProjectCandidateCategoryDetailType3View;
                request = Mapper.DynamicMap<UpdateProjectCandidateCategoryDetailType3Request>(projectCandidateCategoryDetailType3);
            }
            return request;
        }

        public void UpdateConsultancyProject(UpdateConsultancyProjectRequest updateProjectRequest)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                LogTrace("Update Consultancy project (id: {0}).", updateProjectRequest.Id);

                var project = repository.RetrieveProject(updateProjectRequest.Id);

                if (project.Audit.VersionId != updateProjectRequest.AuditVersionid)
                    ValidationContainer.RegisterVersionMismatchEntry(updateProjectRequest, project);

                var storeStatusCode = (ProjectStatusCodeType)project.StatusCode;

                Mapper.DynamicMap(updateProjectRequest, project);
                if (project is ConsultancyProject)
                    Mapper.DynamicMap(updateProjectRequest, (ConsultancyProject)project);

                if (updateProjectRequest.StatusCode != (int)storeStatusCode)
                    ValidateStatusUpdate(storeStatusCode, project);

                Validate(project);

                if (ValidationContainer.ValidateObject(project))
                    repository.Save(project);

                return updateProjectRequest.Id;
            });
        }

        public void LinkProject2Candidate(Guid projectId, Guid candidateId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();


                var project2Candidate = repository.RetrieveProject2Candidate(projectId, candidateId);

                if (project2Candidate != null)
                    return;

                repository.LinkProject2Candidate(projectId, candidateId);

            });
        }

        public void UnlinkProject2Candidate(Guid projectId, Guid candidateId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                LogTrace("Unlink project {0} with candidate {1}.", projectId, candidateId);

                repository.UnlinkProject2Candidate(projectId, candidateId);
            });
        }

        public void LinkProjectCategoryDetail2DictionaryIndicators(LinkProjectCategoryDetail2DictionaryIndicatorsRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
                {
                    var repository = Container.Resolve<IProjectManagementCommandRepository>();
                    var dictionaryManagementQueryService = Container.Resolve<IDictionaryManagementQueryService>();

                    var dictionaryIndicatorIds = request.DictionaryIndicatorIds ?? new List<Guid>();

                    if (request.DictionaryLevelIds != null && request.DictionaryLevelIds.Count > 0)
                    {
                        //Check if competence of given level is already added. If so this is not possible.
                        var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();
                        var selectedIndicators = projectManagementQueryService.ListProjectCategoryDetailDictionaryIndicators(new ListProjectCategoryDetailDictionaryIndicatorsRequest { ProjectCategoryDetailId = request.ProjectCategoryDetailId });
                        var addedCompetenceIds = new List<Guid>();

                        foreach (var dictionaryLevelId in request.DictionaryLevelIds)
                        {
                            var dictionaryLevel = dictionaryManagementQueryService.RetrieveDictionaryLevel(dictionaryLevelId);
                            var dictionaryCompetenceId = dictionaryLevel.DictionaryCompetenceId;

                            if (addedCompetenceIds.Contains(dictionaryCompetenceId) ||
                                selectedIndicators.Any(i => i.DictionaryCompetenceId == dictionaryCompetenceId))
                            {
                                ValidationContainer.RegisterEntityValidationFaultEntry(string.Format("Unable to add a dictionary level '{0}' of a competence that is already added.", dictionaryLevel.Name));
                                continue;
                            }

                            foreach (var indicator in dictionaryManagementQueryService.ListIndicatorsByDictionaryLevel(dictionaryLevelId))
                            {
                                //Only add those indicators which are standard or distinct
                                if (indicator.IsStandard.GetValueOrDefault() || indicator.IsDistinctive.GetValueOrDefault())
                                    dictionaryIndicatorIds.Add(indicator.Id);
                            }
                            addedCompetenceIds.Add(dictionaryLevel.DictionaryCompetenceId);
                        }
                    }

                    IEnumerable<DictionaryIndicatorView> dictionaryIndicators = null;
                    if (request.IsDefinedByRole)
                    {
                        dictionaryIndicators = dictionaryManagementQueryService.ListDictionaryIndicators(dictionaryIndicatorIds)
                            .Where(di => di.IsStandard.GetValueOrDefault() || di.IsDistinctive.GetValueOrDefault());
                    }

                    foreach (var dictionaryIndicatorId in dictionaryIndicatorIds)
                    {
                        var projectCategoryDetail2DictionaryIndicator = repository.RetrieveProjectCategoryDetail2DictionaryIndicator(request.ProjectCategoryDetailId, dictionaryIndicatorId);

                        if (projectCategoryDetail2DictionaryIndicator != null)
                            continue;

                        if (request.IsDefinedByRole || dictionaryIndicators == null)
                        {
                            var dictionaryIndicator = dictionaryManagementQueryService.RetrieveDictionaryIndicatorAdmin(dictionaryIndicatorId);
                            repository.LinkProjectCategoryDetail2DictionaryIndicator(request.ProjectCategoryDetailId, dictionaryIndicatorId, isDefinedByRole: request.IsDefinedByRole, isStandard: dictionaryIndicator.IsStandard.GetValueOrDefault(), isDistinctive: dictionaryIndicator.IsDistinctive.GetValueOrDefault());
                        }
                        else
                        {
                            var dictionaryIndicator = dictionaryIndicators.FirstOrDefault(di => di.Id == dictionaryIndicatorId);

                            if (dictionaryIndicator != null)
                                repository.LinkProjectCategoryDetail2DictionaryIndicator(request.ProjectCategoryDetailId, dictionaryIndicatorId, false, dictionaryIndicator.IsStandard.GetValueOrDefault(), dictionaryIndicator.IsDistinctive.GetValueOrDefault());
                            else
                                repository.LinkProjectCategoryDetail2DictionaryIndicator(request.ProjectCategoryDetailId, dictionaryIndicatorId);
                        }
                    }
                });
        }

        public void MarkProjectCategoryDetailDictionaryIndicator(Guid id, bool isStandard = false, bool isDistinctive = false)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCategoryDetail2DictionaryIndicator = repository.RetrieveProjectCategoryDetail2DictionaryIndicator(id);

                if (projectCategoryDetail2DictionaryIndicator == null)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("Unable to retrieve link to dictionary indicator.");
                    return;
                }

                if (projectCategoryDetail2DictionaryIndicator.IsDefinedByRole)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("Unable to change standard or distinctive on indicator that is defined by role.");
                    return;
                }

                if (isStandard && !isDistinctive)
                    repository.MarkProjectCategoryDetail2DictionaryIndicatorAsStandard(id);
                else if (!isStandard && isDistinctive)
                    repository.MarkProjectCategoryDetail2DictionaryIndicatorAsDistinctive(id);
                else
                    repository.UnMarkProjectCategoryDetail2DictionaryIndicator(id);
            });
        }

        public void UnlinkProjectCategoryDetail2DictionaryIndicator(Guid projectCategoryDetail2DictionaryIndicatorId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.DeleteProjectCategoryDetail2DictionaryIndicator(projectCategoryDetail2DictionaryIndicatorId);
            });
        }

        public void LinkProjectCategoryDetail2SimulationCombinations(Guid projectCategoryDetailId, List<Guid> simulationCombinationIds)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                foreach (var simulationCombinationId in simulationCombinationIds)
                {
                    var projectCategoryDetail2SimulationCombination = repository.RetrieveProjectCategoryDetail2SimulationCombination(projectCategoryDetailId, simulationCombinationId);

                    if (projectCategoryDetail2SimulationCombination != null)
                        continue;

                    repository.LinkProjectCategoryDetail2SimulationCombination(projectCategoryDetailId, simulationCombinationId);
                }
            });
        }

        public void UnlinkProjectCategoryDetail2Combination(Guid projectCategoryDetail2SimulationCombinationId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.DeleteProjectCategoryDetail2SimulationCombination(projectCategoryDetail2SimulationCombinationId);

                //TODO: remove simulation combination from matrix

            });
        }

        public void LinkProjectCategoryDetail2Competence2Combination(Guid projectCategoryDetailId, Guid dictionaryCompetenceId, Guid simulationCombinationId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCategoryDetail2Competence2Combination = repository.RetrieveProjectCategoryDetail2Competence2Combination(projectCategoryDetailId, dictionaryCompetenceId, simulationCombinationId);

                if (projectCategoryDetail2Competence2Combination == null)
                {
                    projectCategoryDetail2Competence2Combination = repository.PrepareProjectCategoryDetail2Competence2Combination(projectCategoryDetailId);
                    projectCategoryDetail2Competence2Combination.SimulationCombinationId = simulationCombinationId;
                    projectCategoryDetail2Competence2Combination.DictionaryCompetenceId = dictionaryCompetenceId;

                    repository.Save(projectCategoryDetail2Competence2Combination);
                }
            });
        }

        public void SetRoiOrder(string projectId, string detailId, string[] order)
        {
            LogTrace();

            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quintessence"].ToString()))
            {
                connection.Open();

                var remove = "delete Quintessence.dbo.ProjectRoiOrder where ProjectId = @ProjectId";
                using (var r_cmd = new SqlCommand(remove, connection))
                {
                    r_cmd.Parameters.AddWithValue("@ProjectId", projectId);
                    r_cmd.ExecuteNonQuery();
                }

                var sql = "insert into Quintessence.dbo.ProjectRoiOrder(ProjectId, ProjectCategoryDetailId, CompetenceId, [Order], Audit_CreatedOn) Values (@ProjectId, @ProjectCategoryDetailId, @CompetenceId, @Order, getdate())";
                using(var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@ProjectId", System.Data.SqlDbType.NVarChar, -1);
                    cmd.Parameters.Add("@ProjectCategoryDetailId", System.Data.SqlDbType.NVarChar, -1);
                    cmd.Parameters.Add("@CompetenceId", System.Data.SqlDbType.NVarChar, -1);
                    cmd.Parameters.Add("@Order", System.Data.SqlDbType.Int);

                    for (int i = 0; i < order.Length; i++)
                    {
                        cmd.Parameters["@ProjectId"].Value = projectId;
                        cmd.Parameters["@ProjectCategoryDetailId"].Value = detailId;
                        cmd.Parameters["@CompetenceId"].Value = order[i];
                        cmd.Parameters["@Order"].Value = i+1;
                        
                        cmd.ExecuteNonQuery();
                    }                    
                }
                connection.Close();
            }            
        }

        public void LockRoiOrder(string projectId, string lockRoi)
        {
            LogTrace();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quintessence"].ToString()))
            {
                connection.Open();

                var sql = "update Quintessence.dbo.Project set Lock = " + (lockRoi == "true" ? "1" : "0") + " where Id = @ProjectId";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@ProjectId", System.Data.SqlDbType.NVarChar, -1);                                   
                    cmd.Parameters["@ProjectId"].Value = projectId;

                    cmd.ExecuteNonQuery();
                    
                }
                connection.Close();
            }
        }

        public void SaveRoiScores(Guid id, int? score)
        {
            LogTrace();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quintessence"].ToString()))
            {
                connection.Open();

                var sql = "update Quintessence.dbo.[ProjectCandidateRoiScore] set Score = @Score where [ProjectCandidateRoiScoreId] = @Id";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@Score", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar, -1);

                    if (score != null)
                    {
                        cmd.Parameters["@Score"].Value = score;
                    }
                    else
                    {
                        cmd.Parameters["@Score"].Value = DBNull.Value;
                    }
                    
                    cmd.Parameters["@Id"].Value = id.ToString();

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void UnlinkProjectCategoryDetail2Competence2Combination(Guid projectCategoryDetailId, Guid dictionaryCompetenceId, Guid simulationCombinationId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCategoryDetail2Competence2Combination = repository.RetrieveProjectCategoryDetail2Competence2Combination(projectCategoryDetailId, dictionaryCompetenceId, simulationCombinationId);

                if (projectCategoryDetail2Competence2Combination != null)
                    repository.Delete(projectCategoryDetail2Competence2Combination);
            });
        }

        public Guid CreateProjectRole(CreateProjectRoleRequest request)
        {
            LogTrace("Create project role.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var entity = repository.Prepare<ProjectRole>();

                Mapper.DynamicMap(request, entity, typeof(CreateProjectRoleRequest), typeof(ProjectRole));

                repository.Save(entity);

                return entity.Id;
            });
        }

        public void LinkProjectRole2DictionaryLevels(Guid projectRoleId, List<Guid> selectedLevelIds)
        {
            LogTrace("Link project role 2 dictionary levels.");

            ExecuteTransaction(() =>
                {
                    var dictionaryManagementQueryService = Container.Resolve<IDictionaryManagementQueryService>();
                    var repository = Container.Resolve<IProjectManagementCommandRepository>();

                    foreach (var selectedLevelId in selectedLevelIds)
                    {
                        repository.LinkProjectRole2DictionaryLevel(projectRoleId, selectedLevelId);

                        var dictionaryIndicators = dictionaryManagementQueryService.ListIndicatorsByDictionaryLevel(selectedLevelId);

                        foreach (var indicator in dictionaryIndicators)
                        {
                            repository.LinkProjectRole2DictionaryIndicator(projectRoleId, indicator.Id);
                        }
                    }
                });
        }

        public void UnlinkProjectRoleDictionaryLevel(Guid projectRoleId, Guid dictionaryLevelId)
        {
            LogTrace("Delete project role 2 dictionary level (project role id: {0} & dictionary level id: {1}).", projectRoleId, dictionaryLevelId);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var dictionaryManagementQueryService = Container.Resolve<IDictionaryManagementQueryService>();
                var indicators = dictionaryManagementQueryService.ListIndicatorsByDictionaryLevel(dictionaryLevelId);

                foreach (var indicator in indicators)
                {
                    repository.DeleteProjectRoleDictionaryIndicator(projectRoleId, indicator.Id);
                }

                repository.UnlinkProjectRole2DictionaryLevel(projectRoleId, dictionaryLevelId);
            });
        }

        public void UpdateProjectCategoryDetailSimulationInformation(UpdateProjectCategoryDetailSimulationInformationRequest request)
        {
            LogTrace("Update project category detail simulation remarks.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var detail = repository.Retrieve<ProjectCategoryDetail>(request.ProjectCategoryDetailId);

                ((dynamic)detail).SimulationRemarks = request.SimulationRemarks;
                ((dynamic)detail).SimulationContextId = request.SimulationContextId;

                switch (detail.GetType().Name)
                {
                    case "ProjectCategoryAcDetail":
                        repository.Save(detail as ProjectCategoryAcDetail);
                        break;
                    case "ProjectCategoryDcDetail":
                        repository.Save(detail as ProjectCategoryDcDetail);
                        break;
                    case "ProjectCategoryEaDetail":
                        repository.Save(detail as ProjectCategoryEaDetail);
                        break;
                    case "ProjectCategoryFaDetail":
                        repository.Save(detail as ProjectCategoryFaDetail);
                        break;
                    case "ProjectCategoryFdDetail":
                        repository.Save(detail as ProjectCategoryFdDetail);
                        break;
                    case "ProjectCategoryPsDetail":
                        repository.Save(detail as ProjectCategoryPsDetail);
                        break;
                    case "ProjectCategorySoDetail":
                        repository.Save(detail as ProjectCategorySoDetail);
                        break;
                    case "ProjectCategoryCaDetail":
                        repository.Save(detail as ProjectCategoryCaDetail);
                        break;
                }
            });
        }

        public void UpdateProjectCategoryDetailMatrixRemarks(Guid projectCategoryDetailId, string matrixRemarks, int scoringTypeCode)
        {
            LogTrace("Update project category detail matrix remarks.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var detail = repository.Retrieve<ProjectCategoryDetail>(projectCategoryDetailId);
                ((dynamic)detail).MatrixRemarks = matrixRemarks;
                ((dynamic)detail).ScoringTypeCode = scoringTypeCode;

                repository.Save(detail);
            });
        }

        public void AssignProjectRole(Guid projectCategoryDetailId, Guid projectRoleId)
        {
            LogTrace("Update project category detail project role.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var queryService = Container.Resolve<IProjectManagementQueryService>();

                var projectCategoryDetail = repository.Retrieve<ProjectCategoryDetail>(projectCategoryDetailId);

                if (projectCategoryDetail is IProjectCategoryDetailProjectRole)
                {
                    var detail = projectCategoryDetail as IProjectCategoryDetailProjectRole;

                    //If role is the same, do nothing
                    if (detail.ProjectRoleId == projectRoleId)
                        return;

                    detail.ProjectRoleId = projectRoleId;

                    var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

                    var project = (AssessmentDevelopmentProjectView)projectManagementQueryService.RetrieveProject(projectCategoryDetail.ProjectId);

                    //Remove existing
                    var listProjectCategoryDetailDictionaryIndicators = projectManagementQueryService.ListProjectCategoryDetailDictionaryIndicators(new ListProjectCategoryDetailDictionaryIndicatorsRequest(projectCategoryDetailId));
                    foreach (var existingProjectCategoryDetailDictionarLevel in listProjectCategoryDetailDictionaryIndicators.Where(e => e.IsDefinedByRole))
                        repository.DeleteProjectCategoryDetail2DictionaryIndicator(existingProjectCategoryDetailDictionarLevel.Id);

                    //Add new dictionary levels
                    var dictionaryIndicators = queryService.ListProjectRoleDictionaryIndicators(new ListProjectRoleDictionaryIndicatorsRequest(projectRoleId));

                    foreach (var indicator in dictionaryIndicators.Where(di => di.DictionaryId == project.DictionaryId))
                    {
                        if (indicator.IsStandard || indicator.IsDistinctive)
                            repository.LinkProjectCategoryDetail2DictionaryIndicator(projectCategoryDetailId, indicator.DictionaryIndicatorId, isDefinedByRole: true);
                    }

                    repository.Save(projectCategoryDetail);
                }
                else
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry(string.Format("Unable to assign a project role to {0}.", projectCategoryDetail.GetType().Name));
                }
            });
        }

        public void UnlinkProjectCategoryDetail2DictionaryLevel(UnlinkProjectCategoryDetail2DictionaryLevelRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
                {
                    var dictionaryIndicatorIds = new List<Guid>();

                    if (request.DictionaryCompetenceId.HasValue)
                    {
                        var dictionaryManagementQueryRepository = Container.Resolve<IDictionaryManagementQueryService>();

                        var dictionaryIndicators = dictionaryManagementQueryRepository.ListIndicatorsByDictionaryCompetence(request.DictionaryCompetenceId.Value);

                        dictionaryIndicatorIds.AddRange(dictionaryIndicators.Select(dl => dl.Id));
                    }

                    var repository = Container.Resolve<IProjectManagementCommandRepository>();
                    var queryService = Container.Resolve<IProjectManagementQueryService>();

                    #region Remove scoring if no scoring is entered for any candidate on competence
                    var projectCateogoryDetail = queryService.RetrieveProjectCategoryDetail(request.ProjectCategoryDetailId);
                    var projectCandidates = queryService.ListProjectCandidateDetails(new ListProjectCandidateDetailsRequest { ProjectId = projectCateogoryDetail.ProjectId });

                    var competenceHasScores = false;
                    var competenceScoresToDelete = new List<ProjectCandidateCompetenceSimulationScoreView>();
                    var focusedIndicatorScoresToDelete = new List<ProjectCandidateIndicatorSimulationFocusedScoreView>();
                    var standardIndicatorScoresToDelete = new List<ProjectCandidateIndicatorSimulationScoreView>();
                    foreach (var projectCandidate in projectCandidates)
                    {
                        if (competenceHasScores)
                            break;

                        var listProjectCandidateSimulationScoresResponse = queryService.ListProjectCandidateSimulationScores(projectCandidate.Id);

                        if (listProjectCandidateSimulationScoresResponse.ProjectCandidateCompetenceSimulationScores.Any(pccss => pccss.DictionaryCompetenceId == request.DictionaryCompetenceId && pccss.Score.HasValue))
                        {
                            ValidationContainer.RegisterEntityValidationFaultEntry("Unable to remove competence since there are already scores defined.");
                            competenceHasScores = true;
                        }
                        else
                        {
                            competenceScoresToDelete.AddRange(listProjectCandidateSimulationScoresResponse.ProjectCandidateCompetenceSimulationScores);

                            if (listProjectCandidateSimulationScoresResponse is ListProjectCandidateFocusedSimulationScoresResponse)
                            {
                                focusedIndicatorScoresToDelete.AddRange(((ListProjectCandidateFocusedSimulationScoresResponse)listProjectCandidateSimulationScoresResponse).ProjectCandidateIndicatorSimulationFocusedScores.Where(score => score.DictionaryCompetenceId == request.DictionaryCompetenceId));
                            }
                            else if (listProjectCandidateSimulationScoresResponse is ListProjectCandidateStandardSimulationScoresResponse)
                            {
                                standardIndicatorScoresToDelete.AddRange(((ListProjectCandidateStandardSimulationScoresResponse)listProjectCandidateSimulationScoresResponse).ProjectCandidateIndicatorSimulationScores.Where(score => score.DictionaryCompetenceId == request.DictionaryCompetenceId));
                            }
                        }
                    }

                    if (!competenceHasScores)
                    {
                        if (standardIndicatorScoresToDelete.Count > 0)
                            repository.DeleteProjectCandidateStandardIndicatorSimulationScores(standardIndicatorScoresToDelete.Select(score => score.Id));
                        if (focusedIndicatorScoresToDelete.Count > 0)
                            repository.DeleteProjectCandidateFocusedIndicatorSimulationScores(focusedIndicatorScoresToDelete.Select(score => score.Id));

                        repository.DeleteProjectCandidateCompetenceSimulationScores(competenceScoresToDelete.Select(score => score.Id));
                    }

                    #endregion

                    #region Remove linked dictionary indicators
                    foreach (var dictionaryIndicatorId in dictionaryIndicatorIds.Distinct())
                        repository.DeleteProjectCategoryDetail2DictionaryIndicator(request.ProjectCategoryDetailId, dictionaryIndicatorId);
                    #endregion

                    #region Remove matrix entries
                    var projectCategoryDetail2Competence2Combinations = repository.ListProjectCategoryDetail2Competence2CombinationByCompetence(request.ProjectCategoryDetailId, request.DictionaryCompetenceId);
                    foreach (var projectCategoryDetail2Competence2Combination in projectCategoryDetail2Competence2Combinations)
                        repository.Delete(projectCategoryDetail2Competence2Combination);
                    #endregion
                });
        }

        public Guid CreateNewProjectPlanPhase(CreateNewProjectPlanPhaseRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectPlanPhase = repository.Prepare<ProjectPlanPhase>();

                Mapper.DynamicMap(request, projectPlanPhase);

                if (ValidationContainer.ValidateObject(projectPlanPhase))
                    repository.Save(projectPlanPhase);

                return projectPlanPhase.Id;
            });
        }

        public Guid CreateNewProjectPlanPhaseEntry(CreateNewProjectPlanPhaseEntryRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var scmQueryRepository = Container.Resolve<ISupplyChainManagementQueryService>();

                ProjectPlanPhaseEntry projectPlanPhaseEntry = null;

                var createNewProjectPlanActivityRequest = request as CreateNewProjectPlanPhaseActivityRequest;
                if (createNewProjectPlanActivityRequest != null)
                {
                    var activityProfile = scmQueryRepository.RetrieveActivityProfile(createNewProjectPlanActivityRequest.ActivityProfileId);

                    projectPlanPhaseEntry = repository.Prepare<ProjectPlanPhaseActivity>();

                    Mapper.DynamicMap(createNewProjectPlanActivityRequest, (ProjectPlanPhaseActivity)projectPlanPhaseEntry);

                    ((ProjectPlanPhaseActivity)projectPlanPhaseEntry).ActivityId = activityProfile.ActivityId;
                    ((ProjectPlanPhaseActivity)projectPlanPhaseEntry).ProfileId = activityProfile.ProfileId;

                    if (ValidationContainer.ValidateObject(projectPlanPhaseEntry))
                        repository.Save(projectPlanPhaseEntry);

                    return projectPlanPhaseEntry.Id;
                }

                var createNewProjectPlanProductRequest = request as CreateNewProjectPlanPhaseProductRequest;
                if (createNewProjectPlanProductRequest != null)
                {               
                    projectPlanPhaseEntry = repository.Prepare<ProjectPlanPhaseProduct>();
                    Mapper.DynamicMap(createNewProjectPlanProductRequest, (ProjectPlanPhaseProduct)projectPlanPhaseEntry);


                    var queryRepository = Container.Resolve<IProjectManagementQueryRepository>();
                    var project = queryRepository.RetrieveProject(createNewProjectPlanProductRequest.ProjectId);
                    if (project.PricingModelType == PricingModelType.TimeAndMaterial)
                    {
                        var productSheetEntry = repository.Prepare<ProductSheetEntry>();
                        productSheetEntry.Name = createNewProjectPlanProductRequest.ProductName;
                        productSheetEntry.InvoiceRemarks = createNewProjectPlanProductRequest.Notes;
                        productSheetEntry.Date = createNewProjectPlanProductRequest.Deadline;
                        productSheetEntry.Quantity = createNewProjectPlanProductRequest.Quantity;
                        productSheetEntry.InvoiceAmount = createNewProjectPlanProductRequest.TotalPrice;
                        productSheetEntry.ProjectPlanPhaseId = createNewProjectPlanProductRequest.ProjectPlanPhaseId;
                        productSheetEntry.ProjectId = createNewProjectPlanProductRequest.ProjectId;
                        productSheetEntry.ProductId = createNewProjectPlanProductRequest.ProductId;
                        productSheetEntry.UserId = createNewProjectPlanProductRequest.UserId;

                        if (createNewProjectPlanProductRequest.NoInvoice)
                            productSheetEntry.InvoiceStatusCode = (int)InvoiceStatusType.NotBillable;
                        else
                            productSheetEntry.InvoiceStatusCode = (int)InvoiceStatusType.Planned;

                        repository.Save(productSheetEntry);
                        ((ProjectPlanPhaseProduct)projectPlanPhaseEntry).ProductsheetEntryId = productSheetEntry.Id;
                    }                    

                    repository.Save(projectPlanPhaseEntry);                    

                    return projectPlanPhaseEntry.Id;
                }

                throw new InvalidDataContractException(string.Format("Unable to handle the following contract {0}.", request.GetType()));
            });
        }

        public void UpdateProjectPlanPhaseEntry(UpdateProjectPlanPhaseEntryRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var updateProjectPlanActivityRequest = request as UpdateProjectPlanPhaseActivityRequest;
                if (updateProjectPlanActivityRequest != null)
                {
                    var activity = repository.Retrieve<ProjectPlanPhaseEntry>(request.Id) as ProjectPlanPhaseActivity;

                    Mapper.DynamicMap(updateProjectPlanActivityRequest, activity);

                    repository.Save(activity);

                    return;
                }

                var updateProjectPlanProductRequest = request as UpdateProjectPlanPhaseProductRequest;
                if (updateProjectPlanProductRequest != null)
                {
                    var product = repository.Retrieve<ProjectPlanPhaseEntry>(request.Id) as ProjectPlanPhaseProduct;

                    Mapper.DynamicMap(updateProjectPlanProductRequest, product);

                    repository.Save(product);

                    return;
                }

                throw new InvalidDataContractException(string.Format("Unable to handle the following contract {0}.", request.GetType()));
            });
        }

        public void DeleteProjectPlanPhaseEntry(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.Delete<ProjectPlanPhaseEntry>(id);
            });
        }

        public void DeleteProjectPlanPhaseProduct(Guid id, Guid entryId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                if (entryId == Guid.Empty)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("Unable to delete a product without an invoice entry.");
                    return;
                }

                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var entry = repository.RetrieveProductSheetEntry(entryId);
                if (entry.InvoiceStatusCode == (int)InvoiceStatusType.Invoiced)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("The product has allready been invoiced.");
                    return;
                }
                repository.Delete<ProjectPlanPhaseEntry>(id);
                repository.Delete<ProductSheetEntry>(entryId);
            });
        }

        public void UpdateProjectPlanPhaseEntryDeadline(UpdateProjectPlanPhaseEntryDeadlineRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

                var entry = projectManagementQueryService.RetrieveProjectPlanPhaseEntry(request.Id);

                if (entry is ProjectPlanPhaseActivityView)
                {
                    var activity = (ProjectPlanPhaseActivityView)entry;

                    var activities = projectManagementQueryService.ListRelatedProjectPlanPhaseActivities(activity.ProjectPlanPhaseId, activity.ActivityId, activity.ProfileId, activity.Duration);

                    foreach (var a in activities)
                    {
                        var storeActivity = (ProjectPlanPhaseActivity)repository.Retrieve<ProjectPlanPhaseEntry>(a.Id);
                        storeActivity.Deadline = request.Deadline;
                        storeActivity.Notes = request.Notes;
                        repository.Save(storeActivity);
                    }
                }

                if (entry is ProjectPlanPhaseProductView)
                {
                    var product = (ProjectPlanPhaseProductView)entry;

                    var products = projectManagementQueryService.ListRelatedProjectPlanPhaseProducts(product.ProjectPlanPhaseId, product.ProductId);

                    foreach (var p in products)
                    {
                        var storeProduct = (ProjectPlanPhaseProduct)repository.Retrieve<ProjectPlanPhaseEntry>(p.Id);
                        storeProduct.Deadline = request.Deadline;
                        storeProduct.Notes = request.Notes;
                        repository.Save(storeProduct);
                    }
                }
            });
        }

        public Guid CreateNewProjectPriceIndex(CreateNewProjectPriceIndexRequest request)
        {
            LogTrace("Create new project price index.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectPriceIndex = repository.Prepare<ProjectPriceIndex>();

                Mapper.DynamicMap(request, projectPriceIndex);

                repository.Save(projectPriceIndex);

                return projectPriceIndex.Id;
            });
        }

        public void UpdateProjectPriceIndex(UpdateProjectPriceIndexRequest request)
        {
            LogTrace("update project price index.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectPriceIndex = repository.Retrieve<ProjectPriceIndex>(request.Id);

                Mapper.DynamicMap(request, projectPriceIndex);

                repository.Save(projectPriceIndex);
            });
        }

        public void DeleteProjectPriceIndex(Guid id)
        {
            LogTrace("delete project price index.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.Delete<ProjectPriceIndex>(id);
            });
        }

        public void SaveTimesheetEntries(SaveTimesheetEntriesRequest request)
        {
            LogTrace("Save new timesheet entries and update exising entries.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                if (request.UpdateTimesheetEntries != null)
                    foreach (var updateTimesheetEntryRequest in request.UpdateTimesheetEntries)
                    {
                        var entry = repository.Retrieve<TimesheetEntry>(updateTimesheetEntryRequest.Id);

                        Mapper.DynamicMap(updateTimesheetEntryRequest, entry);

                        repository.Save(entry);
                    }

                if (request.CreateTimesheetEntries != null)
                    foreach (var createNewTimesheetEntryRequest in request.CreateTimesheetEntries)
                    {
                        var entry = repository.Prepare<TimesheetEntry>();

                        Mapper.DynamicMap(createNewTimesheetEntryRequest, entry);

                        repository.Save(entry);
                    }
            });
        }

        public void DeleteTimesheetEntry(Guid id)
        {
            LogTrace("Delete timesheet entry.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                repository.Delete<TimesheetEntry>(id);
            });
        }

        public void DeleteProductsheetEntry(Guid id)
        {
            LogTrace("Delete productsheet entry.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                repository.Delete<ProductSheetEntry>(id);
            });
        }

        public Guid CreateNewProjectFixedPrice(CreateNewProjectFixedPriceRequest request)
        {
            LogTrace("Create new project fixed price.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectFixedPrice = repository.Prepare<ProjectFixedPrice>();

                Mapper.DynamicMap(request, projectFixedPrice);
                projectFixedPrice.InvoiceStatusCode = (int)InvoiceStatusType.FixedPrice;

                if (ValidationContainer.ValidateObject(projectFixedPrice))
                    repository.Save(projectFixedPrice);

                return projectFixedPrice.Id;
            });
        }

        public void UpdateProjectFixedPrice(UpdateProjectFixedPriceRequest request)
        {
            LogTrace("update project fixed price.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectFixedPrice = repository.Retrieve<ProjectFixedPrice>(request.Id);

                Mapper.DynamicMap(request, projectFixedPrice);

                if (ValidationContainer.ValidateObject(projectFixedPrice))
                    repository.Save(projectFixedPrice);

                repository.Save(projectFixedPrice);
            });
        }

        public void DeleteProjectFixedPrice(Guid id)
        {
            LogTrace("delete project fixed price.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.Delete<ProjectFixedPrice>(id);
            });
        }

        public void DeleteProject(Guid id)
        {
            LogTrace("delete project.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.Delete<Project>(id);
            });
        }

        //public void SaveProductsheetEntries(SaveProductsheetEntriesRequest request)
        //{
        //    LogTrace("Save new productsheet entries and update exising entries.");

        //    ExecuteTransaction(() =>
        //    {
        //        var repository = Container.Resolve<IProjectManagementCommandRepository>();

        //        if (request.UpdateProductsheetEntries != null)
        //            foreach (var updateProductsheetEntryRequest in request.UpdateProductsheetEntries)
        //            {
        //                var entry = repository.Retrieve<ProductSheetEntry>(updateProductsheetEntryRequest.Id);

        //                Mapper.DynamicMap(updateProductsheetEntryRequest, entry);

        //                repository.Save(entry);
        //            }

        //        if (request.CreateProductsheetEntries != null)
        //            foreach (var createNewProductsheetEntryRequest in request.CreateProductsheetEntries)
        //            {
        //                var entry = repository.Prepare<ProductSheetEntry>();

        //                Mapper.DynamicMap(createNewProductsheetEntryRequest, entry);

        //                repository.Save(entry);
        //            }
        //    });
        //}

        public Guid CopyProjectRole(CopyProjectRoleRequest request)
        {
            LogTrace("Copy project role.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectRole = repository.Retrieve<ProjectRole>(request.ProjectRoleToCopyId);

                var projectRoleCopy = repository.Prepare<ProjectRole>();
                projectRoleCopy.Name = "Copy of " + projectRole.Name;
                projectRoleCopy.ContactId = request.ContactId;

                //Save the copied project role
                repository.Save(projectRoleCopy);

                //If project role is for customer, then don't look for linked dictionary levels
                if (projectRole.ContactId == null)
                {
                    var projectRole2DictionaryLevels = repository.List<ProjectRole2DictionaryLevel>(prdls => prdls.Where(p => p.ProjectRoleId == request.ProjectRoleToCopyId));
                    if (projectRole2DictionaryLevels.Count >= 1)
                    {
                        foreach (var dictionaryLevelId in projectRole2DictionaryLevels.Select(pd => pd.DictionaryLevelId))
                        {
                            repository.LinkProjectRole2DictionaryLevel(projectRoleCopy.Id, dictionaryLevelId);
                        }
                    }
                }

                return projectRoleCopy.Id;

            });
        }

        public void UnassignProjectRole(Guid projectCategoryDetailId)
        {
            LogTrace("Unassign project role.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCategoryDetail = repository.RetrieveProjectCategoryDetail(projectCategoryDetailId);

                if (projectCategoryDetail is IProjectCategoryDetailProjectRole)
                {
                    var detail = projectCategoryDetail as IProjectCategoryDetailProjectRole;

                    //If role is the same, do nothing
                    if (detail.ProjectRoleId == null)
                        return;

                    detail.ProjectRoleId = null;

                    var queryRepository = Container.Resolve<IProjectManagementQueryService>();

                    //Unlink indicators that are defined by role
                    var listProjectCategoryDetailDictionaryIndicators =
                        queryRepository.ListProjectCategoryDetailDictionaryIndicators(new ListProjectCategoryDetailDictionaryIndicatorsRequest(projectCategoryDetailId));
                    foreach (
                        var existingProjectCategoryDetailDictionarLevel in
                            listProjectCategoryDetailDictionaryIndicators.Where(e => e.IsDefinedByRole))
                        repository.DeleteProjectCategoryDetail2DictionaryIndicator(
                            existingProjectCategoryDetailDictionarLevel.Id);

                    repository.Save(projectCategoryDetail);
                }

            });
        }

        public Guid AddProjectCandidate(AddProjectCandidateRequest request)
        {
            LogTrace("Add project candidate.");

            return ExecuteTransaction(() =>
                {
                    var candidateId = request.CandidateId;
                    string firstName, lastName;
                    int languageId;

                    if (request.CandidateId == Guid.Empty)
                    {
                        //Add candidate in candidate management
                        var candidateManagementCommandService = Container.Resolve<ICandidateManagementCommandService>();
                        var createCandidateRequest = new CreateCandidateRequest
                            {
                                FirstName = request.FirstName,
                                LastName = request.LastName,
                                LanguageId = request.LanguageId,
                                Gender = request.Gender
                            };

                        firstName = request.FirstName;
                        lastName = request.LastName;
                        languageId = request.LanguageId;
                        candidateId = candidateManagementCommandService.CreateCandidate(createCandidateRequest);
                    }
                    else
                    {
                        var candidateManagementQueryService = Container.Resolve<ICandidateManagementQueryService>();
                        var candidate = candidateManagementQueryService.RetrieveCandidate(candidateId);

                        firstName = candidate.FirstName;
                        lastName = candidate.LastName;
                        languageId = candidate.LanguageId;
                    }

                    var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
                    var crmAppointment = crmQueryService.RetrieveCrmAppointment(request.AppointmentId);

                    var prmQueryService = Container.Resolve<IProjectManagementQueryService>();
                    var project = (AssessmentDevelopmentProjectView)prmQueryService.RetrieveProjectDetail(request.ProjectId);

                    var reportDeadline = crmAppointment.AppointmentDate.AddWorkdays(project.ReportDeadlineStep);
                    reportDeadline = reportDeadline.SetTime(10, 0, 0); //Default report deadline time is 10:00 AM

                    var crmCommandService = Container.Resolve<ICustomerRelationshipManagementCommandService>();
                    var createCandidateInfoRequest = new CreateCandidateInfoRequest
                        {
                            FirstName = firstName,
                            LastName = lastName
                        };
                    var crmCandidateInfoId = crmCommandService.CreateCandidateInfo(createCandidateInfoRequest);

                    //Retrieve report status ("In progress" as default)
                    var reportStatus = prmQueryService.RetrieveReportStatusByCode("PROGRESS"); //Status = In Progress

                    //Add project candidate
                    var prmRepository = Container.Resolve<IProjectManagementCommandRepository>();
                    var projectCandidate = prmRepository.Prepare<ProjectCandidate>();
                    projectCandidate.CandidateId = candidateId;
                    projectCandidate.CrmCandidateAppointmentId = request.AppointmentId;
                    projectCandidate.ProjectId = request.ProjectId;
                    projectCandidate.ReportDeadline = reportDeadline;
                    projectCandidate.CrmCandidateInfoId = crmCandidateInfoId;
                    projectCandidate.ReportLanguageId = languageId;
                    projectCandidate.ReportStatusId = reportStatus.Id;
                    switch (project.PricingModelId)
                    {
                        case 1://Time & Material
                            projectCandidate.InvoiceStatusCode = (int)InvoiceStatusType.Planned;
                            break;
                        case 2://Fixed price
                            projectCandidate.InvoiceStatusCode = (int)InvoiceStatusType.FixedPrice;
                            break;
                        default: throw new ArgumentOutOfRangeException("PricingModelId", "Invalid pricing model.");
                    }
                    projectCandidate.InvoiceAmount = project.MainProjectCategoryDetail == null ? 0 : project.MainProjectCategoryDetail.UnitPrice;

                    prmRepository.Save(projectCandidate);

                    var queryService = Container.Resolve<IProjectManagementQueryService>();
                    var projectCategoryDetailTypes = queryService.ListProjectCategoryDetails(projectCandidate.ProjectId).Where(pcd => !pcd.ProjectTypeCategory.IsMain);

                    EnsureProjectCategoryDetailTypes(queryService.RetrieveProjectCandidateDetailWithTypes(projectCandidate.Id), projectCategoryDetailTypes);

                    return projectCandidate.Id;
                });
        }

        public void DeleteProjectCandidate(Guid id)
        {
            LogTrace("Delete project candidate.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.Delete<ProjectCandidate>(id);

            });
        }

        public void UpdateProjectCategoryDetailType1(UpdateProjectCategoryDetailTypeRequest request)
        {
            LogTrace("Update project category detail type 1.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCategoryDetailType1 = repository.Retrieve<ProjectCategoryDetailType1>(request.Id);

                Mapper.DynamicMap(request, projectCategoryDetailType1);

                repository.Save(projectCategoryDetailType1);
            });
        }

        public void UpdateProjectCategoryDetailType2(UpdateProjectCategoryDetailTypeRequest request)
        {
            LogTrace("Update project category detail type 2.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCategoryDetailType2 = repository.Retrieve<ProjectCategoryDetailType2>(request.Id);

                Mapper.DynamicMap(request, projectCategoryDetailType2);

                repository.Save(projectCategoryDetailType2);
            });
        }

        public void UpdateProjectCategoryDetailType3(UpdateProjectCategoryDetailTypeRequest request)
        {
            LogTrace("Update project category detail type 3.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCategoryDetailType3 = repository.Retrieve<ProjectCategoryDetailType3>(request.Id);

                Mapper.DynamicMap(request, projectCategoryDetailType3);

                repository.Save(projectCategoryDetailType3);
            });
        }

        public Guid CreateNewProjectCandidateCategoryDetailType(Guid projectCandidateId, Guid projectCategoryDetailTypeId)
        {
            LogTrace();

            return ExecuteTransaction(() => CreateNewProjectCandidateCategoryDetailTypeInternal(projectCandidateId, projectCategoryDetailTypeId));
        }

        private Guid CreateNewProjectCandidateCategoryDetailTypeInternal(Guid projectCandidateId, Guid projectCategoryDetailTypeId)
        {
            var repository = Container.Resolve<IProjectManagementCommandRepository>();
            var queryService = Container.Resolve<IProjectManagementQueryService>();

            var projectCategoryDetailType = repository.Retrieve<ProjectCategoryDetail>(projectCategoryDetailTypeId);

            if (projectCategoryDetailType == null)
                throw new ArgumentOutOfRangeException("projectCategoryDetailTypeId",
                                                      "Unable to retrieve specified ProjectCategoryDetailType");

            var projectCandidate = queryService.RetrieveProjectCandidate(projectCandidateId);

            if (projectCategoryDetailType is ProjectCategoryDetailType1)
            {
                var scheduledDate =
                    CalculateScheduledDate(((ProjectCategoryDetailType1)projectCategoryDetailType).SurveyPlanningId,
                                           projectCandidate);
                return repository.LinkProjectCandidateCategoryDetailType1(projectCandidateId, projectCandidate.ProjectId,
                                                                          projectCategoryDetailTypeId,
                                                                          projectCategoryDetailType.UnitPrice,
                                                                          scheduledDate: scheduledDate);
            }

            if (projectCategoryDetailType is ProjectCategoryDetailType2)
            {
                return repository.LinkProjectCandidateCategoryDetailType2(projectCandidateId, projectCandidate.ProjectId,
                                                                          projectCategoryDetailTypeId,
                                                                          projectCategoryDetailType.UnitPrice);
            }

            if (projectCategoryDetailType is ProjectCategoryDetailType3)
            {
                var scheduledDate =
                    CalculateScheduledDate(((ProjectCategoryDetailType3)projectCategoryDetailType).SurveyPlanningId,
                                           projectCandidate);
                var projectTypeCategories = queryService.ListSubcategories();
                var projectTypeCategory =
                    projectTypeCategories.FirstOrDefault(ptc => ptc.Id == projectCategoryDetailType.ProjectTypeCategoryId);

                var loginCode = string.Empty;
                var languages = Container.Resolve<IInfrastructureQueryService>().ListLanguages();
                if (projectTypeCategory != null)
                {
                    switch (projectTypeCategory.Code)
                    {
                        case "NEOPIR":
                        case "LEIDERSTIJ":
                            using (var suppress = new TransactionScope(TransactionScopeOption.Suppress))
                            {
                                loginCode = repository.GenerateType3Code(projectCandidate.CrmCandidateInfoId,
                                                                         projectCandidate.CandidateFirstName,
                                                                         projectCandidate.CandidateLastName,
                                                                         languages.FirstOrDefault(
                                                                             l => l.Id == projectCandidate.CandidateLanguageId)
                                                                                  .Code, projectTypeCategory.Code);
                            }
                            break;
                    }
                }

                return repository.LinkProjectCandidateCategoryDetailType3(projectCandidateId, projectCandidate.ProjectId,
                                                                          projectCategoryDetailTypeId, loginCode, scheduledDate,
                                                                          projectCategoryDetailType.UnitPrice);
            }

            throw new ArgumentOutOfRangeException("projectCategoryDetailTypeId",
                                                  "Unable to retrieve specified ProjectCategoryDetailType");
        }

        private DateTime? CalculateScheduledDate(int surveyPlanningId, ProjectCandidateView projectCandidate)
        {
            DateTime? scheduledDate = null;
            var crmAppointment =
                Container.Resolve<ICustomerRelationshipManagementQueryService>().RetrieveCrmAppointment(
                    projectCandidate.CrmCandidateAppointmentId);

            switch (surveyPlanningId)
            {
                case 2: //Before
                    scheduledDate = crmAppointment.AppointmentDate.AddDays(-1);
                    break;
                case 3: //After
                    scheduledDate = crmAppointment.AppointmentDate.AddDays(1);
                    break;
            }
            return scheduledDate;
        }

        public Guid CreateSubcategory(CreateSubcategoryRequest request)
        {
            LogTrace("Create subcategory.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var subcategory = repository.Prepare<ProjectTypeCategory>();
                Mapper.DynamicMap(request, subcategory);
                repository.Save(subcategory);

                var projectType = repository.List<ProjectType>(pt => pt.Where(p => p.Code.ToUpper() == "ACDC")).SingleOrDefault();
                repository.LinkProjectType2ProjectTypeCategory(projectType.Id, subcategory.Id, false);

                repository.CreateSubcategoryDefaultValues(subcategory.Id, request.SubcategoryType);

                return subcategory.Id;
            });
        }

        public void UpdateSubcategory(UpdateSubcategoryRequest request)
        {
            LogTrace("Update subcategory.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectTypeCategory = repository.Retrieve<ProjectTypeCategory>(request.Id);

                Mapper.DynamicMap(request, projectTypeCategory);

                repository.Save(projectTypeCategory);

                foreach (var ptcdv in request.ProjectTypeCategoryDefaultValues)
                {
                    switch (ptcdv.Code)
                    {
                        case "DESCRIPTION":
                            var descriptionDefault = repository.Retrieve<ProjectTypeCategoryDefaultValue>(ptcdv.Id);
                            descriptionDefault.Value = ptcdv.Value;
                            repository.Save(descriptionDefault);
                            break;
                        case "MAILTEXTSTANDALONE":
                            var standaloneMailDefault = repository.Retrieve<ProjectTypeCategoryDefaultValue>(ptcdv.Id);
                            standaloneMailDefault.Value = ptcdv.Value;
                            repository.Save(standaloneMailDefault);
                            break;
                        case "MAILTEXTINTEGRATED":
                            var integratedMailDefault = repository.Retrieve<ProjectTypeCategoryDefaultValue>(ptcdv.Id);
                            integratedMailDefault.Value = ptcdv.Value;
                            repository.Save(integratedMailDefault);
                            break;
                    }

                }

            });
        }

        public void UpdateProjectCandidateCategoryDetailTypes(List<UpdateProjectCandidateCategoryDetailTypeRequest> requests)
        {
            LogTrace();

            ExecuteTransaction(() => UpdateProjectCandidateCategoryDetailTypesInternal(requests));
        }

        private void UpdateProjectCandidateCategoryDetailTypesInternal(IEnumerable<UpdateProjectCandidateCategoryDetailTypeRequest> requests)
        {
            foreach (var request in requests)
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();
                var infrastructureCommandService = Container.Resolve<IInfrastructureCommandService>();


                var jobDefinitions = infrastructureQueryService.ListJobDefinitions();
                var invoiceJobDefinition =
                    jobDefinitions.FirstOrDefault(
                        jd => string.Equals(jd.Name, "ValidateInvoiceStatus", StringComparison.InvariantCultureIgnoreCase));

                if (request is UpdateProjectCandidateCategoryDetailType1Request)
                {
                    //For now no fields are editable in this context //edited 7-4-2014: uncommented to allow editing
                    var detail = repository.RetrieveProjectCandidateCategoryDetailType<ProjectCandidateCategoryDetailType1>(request.Id);
                    Mapper.DynamicMap((UpdateProjectCandidateCategoryDetailType1Request)request, detail);
                    repository.Save(detail);
                }
                else if (request is UpdateProjectCandidateCategoryDetailType2Request)
                {
                    var detail =
                        repository.RetrieveProjectCandidateCategoryDetailType<ProjectCandidateCategoryDetailType2>(request.Id);
                    Mapper.DynamicMap((UpdateProjectCandidateCategoryDetailType2Request)request, detail);
                    repository.Save(detail);

                    //Launch ValidateInvoiceStatus-service. If the DeadLine is changed, added or removed, the invoice status must be changed accordingly, relative to the current date.
                    //if (invoiceJobDefinition != null)
                    //    infrastructureCommandService.ScheduleJob(invoiceJobDefinition.Id);
                }
                else if (request is UpdateProjectCandidateCategoryDetailType3Request)
                {
                    var detail =
                        repository.RetrieveProjectCandidateCategoryDetailType<ProjectCandidateCategoryDetailType3>(request.Id);
                    Mapper.DynamicMap((UpdateProjectCandidateCategoryDetailType3Request)request, detail);
                    repository.Save(detail);

                    //Launch ValidateInvoiceStatus-service. If the DeadLine is changed, added or removed, the invoice status must be changed accordingly, relative to the current date.
                    //if (invoiceJobDefinition != null)
                    //    infrastructureCommandService.ScheduleJob(invoiceJobDefinition.Id);
                }
            }
        }

        public void UpdateProjectReporting(UpdateProjectReportingRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var queryService = Container.Resolve<IProjectManagementQueryService>();

                var project = (AssessmentDevelopmentProject)repository.Retrieve<Project>(request.Id);

                List<ProjectCandidateView> projectCandidates = null;

                //Remove related scorings
                if (project.CandidateScoreReportTypeId != request.CandidateScoreReportTypeId)
                {
                    projectCandidates = queryService.ListProjectCandidates(request.Id);

                    repository.DeleteProjectCandidatesCompetenceScoring(projectCandidates.Select(pc => pc.Id));
                    repository.DeleteProjectCandidatesClusterScore(projectCandidates.Select(pc => pc.Id));
                }

                //Delete any previous resume fields
                if (project.CandidateReportDefinitionId != request.CandidateReportDefinitionId)
                {
                    if (projectCandidates == null)
                        projectCandidates = queryService.ListProjectCandidates(request.Id);

                    var resumeFieldIds = new List<Guid>();

                    foreach (var projectCandidate in projectCandidates)
                    {
                        var resumes = queryService.ListProjectCandidateResumes(projectCandidate.Id);
                        resumeFieldIds.AddRange(resumes.SelectMany(r => r.ProjectCandidateResumeFields.Select(pcrf => pcrf.Id)).ToList());
                    }

                    foreach (var resumeFieldId in resumeFieldIds)
                    {
                        repository.Delete<ProjectCandidateResumeField>(resumeFieldId);
                    }
                }

                Mapper.DynamicMap(request, project);

                repository.Save(project);
            });
        }

        public Guid CreateNewProjectCandidateIndicatorSimulationScore(CreateNewProjectCandidateIndicatorSimulationScoreRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidateIndicatorSimulationScore = repository.Prepare<ProjectCandidateIndicatorSimulationScore>();

                Mapper.DynamicMap(request, projectCandidateIndicatorSimulationScore);

                repository.Save(projectCandidateIndicatorSimulationScore);

                return projectCandidateIndicatorSimulationScore.Id;
            });
        }

        public Guid CreateNewProjectCandidateCompetenceSimulationScore(CreateNewProjectCandidateCompetenceSimulationScoreRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidateCompetenceSimulationScore = repository.Prepare<ProjectCandidateCompetenceSimulationScore>();

                Mapper.DynamicMap(request, projectCandidateCompetenceSimulationScore);

                repository.Save(projectCandidateCompetenceSimulationScore);

                return projectCandidateCompetenceSimulationScore.Id;
            });
        }

        public void UpdateProjectCandidateCompetenceSimulationScore(UpdateProjectCandidateCompetenceSimulationScoreRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidateCompetenceSimulationScore = repository.Retrieve<ProjectCandidateCompetenceSimulationScore>(request.Id);

                Mapper.DynamicMap(request, projectCandidateCompetenceSimulationScore);

                repository.Save(projectCandidateCompetenceSimulationScore);

                if (request.Indicators != null)
                {
                    foreach (var indicator in request.Indicators)
                    {
                        var projectCandidateIndicatorSimulationScore = repository.Retrieve<ProjectCandidateIndicatorSimulationScore>(indicator.Id);

                        Mapper.DynamicMap(indicator, projectCandidateIndicatorSimulationScore);

                        repository.Save(projectCandidateIndicatorSimulationScore);
                    }
                }
            });
        }

        public void MarkDictionaryIndicatorAsStandard(Guid projectRoleId, Guid dictionaryIndicatorId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.UpdateProjectRoleDictionaryIndicatorNorm(projectRoleId, dictionaryIndicatorId, 10);
            });
        }

        public void MarkDictionaryIndicatorAsDistinctive(Guid projectRoleId, Guid dictionaryIndicatorId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.UpdateProjectRoleDictionaryIndicatorNorm(projectRoleId, dictionaryIndicatorId, 20);
            });
        }

        public void DeleteProjectRoleDictionaryIndicator(Guid projectRoleId, Guid dictionaryIndicatorId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.DeleteProjectRoleDictionaryIndicator(projectRoleId, dictionaryIndicatorId);
            });
        }

        public Guid CreateNewProjectCandidateClusterScore(CreateNewProjectCandidateClusterScoreRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidateClusterScore = repository.Prepare<ProjectCandidateClusterScore>();

                Mapper.DynamicMap(request, projectCandidateClusterScore);

                repository.Save(projectCandidateClusterScore);

                return projectCandidateClusterScore.Id;
            });
        }

        public Guid CreateNewProjectCandidateCompetenceScore(CreateNewProjectCandidateCompetenceScoreRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidateCompetenceScore = repository.Prepare<ProjectCandidateCompetenceScore>();

                Mapper.DynamicMap(request, projectCandidateCompetenceScore);

                repository.Save(projectCandidateCompetenceScore);

                return projectCandidateCompetenceScore.Id;
            });
        }

        public Guid CreateNewProjectCandidateIndicatorScore(CreateNewProjectCandidateIndicatorScoreRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidateIndicatorScore = repository.Prepare<ProjectCandidateIndicatorScore>();

                Mapper.DynamicMap(request, projectCandidateIndicatorScore);

                repository.Save(projectCandidateIndicatorScore);

                return projectCandidateIndicatorScore.Id;
            });
        }

        public void UpdateProjectCandidateCompetenceScores(List<UpdateProjectCandidateCompetenceScoreRequest> projectCandidateCompetenceScores)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                foreach (var updateProjectCandidateCompetenceScoreRequest in projectCandidateCompetenceScores)
                {
                    var projectCandidateCompetenceScore = repository.Retrieve<ProjectCandidateCompetenceScore>(updateProjectCandidateCompetenceScoreRequest.Id);

                    Mapper.DynamicMap(updateProjectCandidateCompetenceScoreRequest, projectCandidateCompetenceScore);

                    foreach (var updateProjectCandidateIndicatorScoreRequest in updateProjectCandidateCompetenceScoreRequest.ProjectCandidateIndicatorScores)
                    {
                        var projectCandidateIndicatorScore = repository.Retrieve<ProjectCandidateIndicatorScore>(updateProjectCandidateIndicatorScoreRequest.Id);

                        Mapper.DynamicMap(updateProjectCandidateIndicatorScoreRequest, projectCandidateIndicatorScore);

                        repository.Save(projectCandidateIndicatorScore);
                    }

                    repository.Save(projectCandidateCompetenceScore);
                }
            });
        }

        public void UpdateProjectCandidateClusterScores(List<UpdateProjectCandidateClusterScoreRequest> projectCandidateClusterScores)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                foreach (var updateProjectCandidateClusterScoreRequest in projectCandidateClusterScores)
                {
                    var projectCandidateClusterScore = repository.Retrieve<ProjectCandidateClusterScore>(updateProjectCandidateClusterScoreRequest.Id);

                    Mapper.DynamicMap(updateProjectCandidateClusterScoreRequest, projectCandidateClusterScore);

                    foreach (var updateProjectCandidateCompetenceScoreRequest in updateProjectCandidateClusterScoreRequest.ProjectCandidateCompetenceScores)
                    {
                        var projectCandidateCompetenceScore = repository.Retrieve<ProjectCandidateCompetenceScore>(updateProjectCandidateCompetenceScoreRequest.Id);

                        Mapper.DynamicMap(updateProjectCandidateCompetenceScoreRequest, projectCandidateCompetenceScore);

                        foreach (var updateProjectCandidateIndicatorScoreRequest in updateProjectCandidateCompetenceScoreRequest.ProjectCandidateIndicatorScores)
                        {
                            var projectCandidateIndicatorScore = repository.Retrieve<ProjectCandidateIndicatorScore>(updateProjectCandidateIndicatorScoreRequest.Id);

                            Mapper.DynamicMap(updateProjectCandidateIndicatorScoreRequest, projectCandidateIndicatorScore);

                            repository.Save(projectCandidateIndicatorScore);
                        }

                        repository.Save(projectCandidateCompetenceScore);
                    }

                    repository.Save(projectCandidateClusterScore);
                }
            });
        }

        public Guid CreateNewProjectCandidateResume(CreateNewProjectCandidateResumeRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidateResume = repository.Prepare<ProjectCandidateResume>();

                Mapper.DynamicMap(request, projectCandidateResume);
                projectCandidateResume.AdviceId = 1;

                repository.Save(projectCandidateResume);

                return projectCandidateResume.Id;
            });
        }

        public void UpdateProjectCandidateResume(UpdateProjectCandidateResumeRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                foreach (var fieldRequest in request.ProjectCandidateResumeFields)
                {
                    var projectCandidateResumeField = repository.Retrieve<ProjectCandidateResumeField>(fieldRequest.Id);

                    Mapper.DynamicMap(fieldRequest, projectCandidateResumeField);

                    repository.Save(projectCandidateResumeField);
                }

                var projectCandidateResume = repository.Retrieve<ProjectCandidateResume>(request.Id);

                Mapper.DynamicMap(request, projectCandidateResume);

                repository.Save(projectCandidateResume);
            });
        }

        public Guid CreateNewProjectCandidateResumeField(CreateNewProjectCandidateResumeFieldRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidateResumeField = repository.Prepare<ProjectCandidateResumeField>();

                Mapper.DynamicMap(request, projectCandidateResumeField);

                repository.Save(projectCandidateResumeField);

                return projectCandidateResumeField.Id;
            });
        }

        public Guid CreateNewProposal(CreateNewProposalRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
                var sQueryService = Container.Resolve<ISecurityQueryService>();
                
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var proposal = repository.PrepareProposal();

                Mapper.DynamicMap(request, proposal);

                if (request.ContactId != null)
                {
                    var contact = crmQueryService.RetrieveContactDetailInformation(request.ContactId.Value);
                    var user = sQueryService.RetrieveUserByCrmAssociateId(contact.AssociateId);
                    proposal.BusinessDeveloperId = user.Id;
                }                

                repository.Save(proposal);

                return proposal.Id;
            });
        }

        public Guid UpdateProjectCandidateProposal(UpdateProjectCandidateProposalRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                //Update the proposalId in the projectCandidate
                var projectCandidate = repository.RetrieveProjectCandidate(request.ProjectCandidateId);
                projectCandidate.ProposalId = request.ProposalId;

                repository.Save(projectCandidate);

                return projectCandidate.Id;
            });
        }

        public Guid UpdateProjectCandidateCategoryType1Proposal(UpdateProjectCandidateDetailType1ProposalRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                //Update the proposalId in the projectCandidateCategoryType1
                var projectCandidateCategoryType1 = repository.ProjectCandidateCategoryType1(request.ProjectCandidateDetailType1Id);
                projectCandidateCategoryType1.ProposalId = request.ProposalId;

                repository.Save(projectCandidateCategoryType1);

                return projectCandidateCategoryType1.Id;
            });
        }

        public Guid UpdateProjectCandidateCategoryType2Proposal(UpdateProjectCandidateDetailType2ProposalRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                //Update the proposalId in the projectCandidateCategoryType2
                var projectCandidateCategoryType2 = repository.ProjectCandidateCategoryType2(request.ProjectCandidateDetailType2Id);
                projectCandidateCategoryType2.ProposalId = request.ProposalId;

                repository.Save(projectCandidateCategoryType2);

                return projectCandidateCategoryType2.Id;
            });
        }

        public Guid UpdateProjectCandidateCategoryType3Proposal(UpdateProjectCandidateDetailType3ProposalRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                //Update the proposalId in the projectCandidateCategoryType3
                var projectCandidateCategoryType3 = repository.ProjectCandidateCategoryType3(request.ProjectCandidateDetailType3Id);
                projectCandidateCategoryType3.ProposalId = request.ProposalId;

                repository.Save(projectCandidateCategoryType3);

                return projectCandidateCategoryType3.Id;
            });
        }

        public Guid UpdateProjectProductProposal(UpdateProjectProductProposalRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                //Update the proposalId in the projectProduct
                var projectProduct = repository.RetrieveProjectProduct(request.ProjectProductId);
                projectProduct.ProposalId = request.ProposalId;

                repository.Save(projectProduct);

                return projectProduct.Id;
            });
        }

        public Guid UpdateTimeSheetEntryProposal(UpdateTimeSheetEntryProposalRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                //Update the proposalId in the timeSheetEntry
                var timeSheetEntry = repository.RetrieveTimeSheetEntry(request.TimeSheetEntryId);
                timeSheetEntry.ProposalId = request.ProposalId;

                repository.Save(timeSheetEntry);

                return timeSheetEntry.Id;
            });
        }

        public Guid UpdateProductSheetEntryProposal(UpdateProductSheetEntryProposalRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                //Update the proposalId in the productSheetEntry
                var productSheetEntry = repository.RetrieveProductSheetEntry(request.ProductSheetEntryId);
                productSheetEntry.ProposalId = request.ProposalId;

                repository.Save(productSheetEntry);

                return productSheetEntry.Id;
            });
        }

        public Guid UpdateAcdcProjectFixedPriceProposal(UpdateAcdcProjectFixedPriceProposalRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                //Update the proposalId in the acdcProjectFixedPrice
                var acdcProjectFixedPrice = repository.RetrieveProjectFixedPrice(request.AcdcProjectFixedPriceId);
                acdcProjectFixedPrice.ProposalId = request.ProposalId;

                repository.Save(acdcProjectFixedPrice);

                return acdcProjectFixedPrice.Id;
            });
        }

        public Guid UpdateConsultancyProjectFixedPriceProposal(UpdateConsultancyProjectFixedPriceProposalRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                //Update the proposalId in the consultancyProjectFixedPrice
                var consultancyProjectFixedPrice = repository.RetrieveProjectFixedPrice(request.ConsultancyProjectFixedPriceId);
                consultancyProjectFixedPrice.ProposalId = request.ProposalId;

                repository.Save(consultancyProjectFixedPrice);

                return consultancyProjectFixedPrice.Id;
            });
        }

        //private Guid ExecuteTransaction(Action action)
        //{
        //    throw new NotImplementedException();
        //}

        public void UpdateProposal(UpdateProposalRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var proposal = repository.Retrieve<Proposal>(request.Id);
                
                //If proposal status is won and datewon is not filled in => datewon = now
                if ((ProposalStatusType) request.StatusCode == ProposalStatusType.Won
                    && proposal.StatusCode != request.StatusCode
                    && !request.DateWon.HasValue)
                {
                    request.DateWon = DateTime.Now;
                }

                Mapper.DynamicMap(request, proposal);

                repository.Save(proposal);
            });
        }

        public Guid CreateFrameworkAgreement(CreateFrameworkAgreementRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var frameworkAgreement = repository.Prepare<FrameworkAgreement>();

                Mapper.DynamicMap(request, frameworkAgreement);

                repository.Save(frameworkAgreement);

                return frameworkAgreement.Id;
            });
        }

        public Guid UpdateFrameworkAgreement(UpdateFrameworkAgreementRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var frameworkAgreement = repository.Retrieve<FrameworkAgreement>(request.Id);

                Mapper.DynamicMap(request, frameworkAgreement);

                repository.Save(frameworkAgreement);

                return frameworkAgreement.Id;
            });
        }

        public void DeleteFrameworkAgreement(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.Delete<FrameworkAgreement>(id);

            });
        }

        public void UpdateProjectCandidatesDetails(List<UpdateProjectCandidateDetailRequest> candidateRequests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var prmRepository = Container.Resolve<IProjectManagementCommandRepository>();
                var camQueryService = Container.Resolve<ICandidateManagementQueryService>();
                var camCommandService = Container.Resolve<ICandidateManagementCommandService>();

                var projectCandidatesToSave = new List<ProjectCandidate>();
                foreach (var updateProjectCandidateDetailRequest in candidateRequests)
                {
                    var candidate = camQueryService.RetrieveCandidate(updateProjectCandidateDetailRequest.CandidateId);
                    candidate.Email = updateProjectCandidateDetailRequest.CandidateEmail;
                    candidate.Phone = updateProjectCandidateDetailRequest.CandidatePhone;
                    candidate.Gender = updateProjectCandidateDetailRequest.CandidateGender;
                    candidate.LanguageId = updateProjectCandidateDetailRequest.CandidateLanguageId;
                    var updateCandidateRequest = Mapper.DynamicMap<UpdateCandidateRequest>(candidate);
                    camCommandService.UpdateCandidate(updateCandidateRequest);

                    var projectCandidate = prmRepository.Retrieve<ProjectCandidate>(updateProjectCandidateDetailRequest.Id);
                    Mapper.DynamicMap(updateProjectCandidateDetailRequest, projectCandidate);

                    projectCandidatesToSave.Add(projectCandidate);
                }

                foreach (var projectCandidate in projectCandidatesToSave)
                    prmRepository.Save(projectCandidate);
            });
        }

        public void UpdateProjectCandidatesDetail(UpdateProjectCandidateDetailRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var prmRepository = Container.Resolve<IProjectManagementCommandRepository>();
                var camQueryService = Container.Resolve<ICandidateManagementQueryService>();
                var camCommandService = Container.Resolve<ICandidateManagementCommandService>();

                var candidate = camQueryService.RetrieveCandidate(request.CandidateId);
                candidate.Email = request.CandidateEmail;
                candidate.Phone = request.CandidatePhone;
                candidate.Gender = request.CandidateGender;
                candidate.LanguageId = request.CandidateLanguageId;
                var updateCandidateRequest = Mapper.DynamicMap<UpdateCandidateRequest>(candidate);
                camCommandService.UpdateCandidate(updateCandidateRequest);

                var projectCandidate = prmRepository.Retrieve<ProjectCandidate>(request.Id);
                Mapper.DynamicMap(request, projectCandidate);
                prmRepository.Save(projectCandidate);
            });
        }

        public Guid CreateProjectCandidateReportRecipient(CreateProjectCandidateReportRecipientRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var reportRecipient = repository.Prepare<ProjectCandidateReportRecipient>();

                Mapper.DynamicMap(request, reportRecipient);

                repository.Save(reportRecipient);

                return reportRecipient.Id;
            });
        }

        public void DeleteProjectCandidateReportRecipient(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.Delete<ProjectCandidateReportRecipient>(id);

            });
        }

        public void UpdateProjectCandidateReportRecipient(UpdateProjectCandidateReportRecipientRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var reportRecipient = repository.Retrieve<ProjectCandidateReportRecipient>(request.Id);

                Mapper.DynamicMap(request, reportRecipient);

                repository.Save(reportRecipient);
            });
        }

        public void CreateProjectCandidateReportRecipients(List<CreateProjectCandidateReportRecipientRequest> requests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                foreach (var request in requests)
                {
                    var reportRecipient = repository.Prepare<ProjectCandidateReportRecipient>();

                    Mapper.DynamicMap(request, reportRecipient);

                    repository.Save(reportRecipient);
                }

            });
        }

        public void MarkDocumentAsImportant(Guid projectId, Guid uniqueId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.MarkDocumentAsImportant(projectId, uniqueId);
            });
        }



        public void UnmarkDocumentAsImportant(Guid projectId, Guid uniqueId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.UnmarkDocumentAsImportant(projectId, uniqueId);
            });
        }

        public Guid CreateNewProjectDna(CreateNewProjectDnaRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectDna = repository.Prepare<ProjectDna>();

                Mapper.DynamicMap(request, projectDna);

                repository.Save(projectDna);

                var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();

                var languages = infrastructureQueryService.ListLanguages();

                foreach (var language in languages)
                {
                    var projectDnaCommercialTranslation = repository.Prepare<ProjectDnaCommercialTranslation>();
                    projectDnaCommercialTranslation.ProjectDnaId = projectDna.Id;
                    projectDnaCommercialTranslation.LanguageId = language.Id;
                    repository.Save(projectDnaCommercialTranslation);
                }

                return projectDna.Id;
            });
        }

        public void UpdateProjectDna(UpdateProjectDnaRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectDna = repository.Retrieve<ProjectDna>(request.Id);

                var projectDnaProjectTypes = repository.ListProjectDnaProjectTypes(request.Id);
                var projectDnaProjectContactPersons = repository.ListProjectDnaProjectContactPersons(request.Id);

                foreach (var link in projectDnaProjectTypes.Where(link => !request.SelectedProjectDnaTypeIds.Contains(link.ProjectDnaTypeId)))
                    repository.DeleteProjectDna2ProjectDnaType(link.Id);

                foreach (var id in request.SelectedProjectDnaTypeIds.Where(id => !projectDnaProjectTypes.Select(link => link.ProjectDnaTypeId).Contains(id)))
                {
                    var link = repository.Prepare<ProjectDna2ProjectDnaType>();
                    link.ProjectDnaTypeId = id;
                    link.ProjectDnaId = request.Id;
                    repository.Save(link);
                }

                foreach (var link in projectDnaProjectContactPersons.Where(link => !request.SelectedProjectDnaContactPersonIds.Contains(link.CrmPersonId)))
                    repository.DeleteProjectDna2CrmContactPerson(link.Id);

                foreach (var id in request.SelectedProjectDnaContactPersonIds.Where(id => !projectDnaProjectContactPersons.Select(link => link.CrmPersonId).Contains(id)))
                {
                    var link = repository.Prepare<ProjectDna2CrmPerson>();
                    link.CrmPersonId = id;
                    link.ProjectDnaId = request.Id;
                    repository.Save(link);
                }

                Mapper.DynamicMap(request, projectDna);

                foreach (var updateProjectDnaCommercialTranslationRequest in request.ProjectDnaCommercialTranslations)
                {
                    var projectDnaCommercialTranslation = repository.Retrieve<ProjectDnaCommercialTranslation>(updateProjectDnaCommercialTranslationRequest.Id);
                    Mapper.DynamicMap(updateProjectDnaCommercialTranslationRequest, projectDnaCommercialTranslation);
                    repository.Save(projectDnaCommercialTranslation);
                }

                repository.Save(projectDna);
            });
        }


        public void UpdateProjectTypeCategoryUnitPrices(List<UpdateProjectTypeCategoryUnitPriceRequest> requests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var queryService = Container.Resolve<IProjectManagementQueryService>();

                var projectTypeCategoryUnitPricesToUpdate = new List<ProjectTypeCategoryUnitPrice>();
                var projectTypeCategoryUnitPricesToCreate = new List<ProjectTypeCategoryUnitPrice>();
                foreach (var request in requests)
                {
                    var projectTypeCategoryUnitPrice = queryService.RetrieveProjectTypeCategoryUnitPriceByTypeAndLevel(request.ProjectTypeCategoryId, request.ProjectTypeCategoryLevelId);

                    if (projectTypeCategoryUnitPrice == null && (request.UnitPrice != null && request.UnitPrice != default(decimal)))
                    {
                        //New unit price: create record
                        var createProjectTypeCategoryUnitPrice = repository.PrepareProjectTypeCategoryUnitPrice(request.ProjectTypeCategoryId, request.ProjectTypeCategoryLevelId, request.UnitPrice.GetValueOrDefault());
                        projectTypeCategoryUnitPricesToCreate.Add(createProjectTypeCategoryUnitPrice);

                    }
                    else if (projectTypeCategoryUnitPrice != null && (request.UnitPrice == null || request.UnitPrice == default(decimal)))
                    {
                        //No unit price: delete record
                        repository.Delete<ProjectTypeCategoryUnitPrice>(projectTypeCategoryUnitPrice.Id);
                    }
                    else if (projectTypeCategoryUnitPrice == null && (request.UnitPrice == null || request.UnitPrice == default(decimal)))
                    {
                        //Continue loop;
                        continue;
                    }
                    else
                    {
                        //Update unit price
                        var updateProjectTypeCategoryUnitPrice =
                            repository.Retrieve<ProjectTypeCategoryUnitPrice>(projectTypeCategoryUnitPrice.Id);
                        Mapper.DynamicMap(request, updateProjectTypeCategoryUnitPrice);
                        projectTypeCategoryUnitPricesToUpdate.Add(updateProjectTypeCategoryUnitPrice);
                    }

                }

                foreach (var projectTypeCategoryUnitPrice in projectTypeCategoryUnitPricesToUpdate)
                {
                    repository.Save(projectTypeCategoryUnitPrice);
                }

                foreach (var projectTypeCategoryUnitPrice in projectTypeCategoryUnitPricesToCreate)
                {
                    repository.Save(projectTypeCategoryUnitPrice);
                }
            });
        }

        public void CancelProjectCandidate(CancelProjectCandidateRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidate = repository.Retrieve<ProjectCandidate>(request.Id);

                Mapper.DynamicMap(request, projectCandidate);

                projectCandidate.IsCancelled = true;
                projectCandidate.InvoiceAmount = request.CancelledInvoiceAmount;
                projectCandidate.CancelledAppointmentDate = DateTime.Now;

                repository.Save(projectCandidate);
            });
        }

        public void UpdateCancelledProjectCandidates(List<UpdateCancelledProjectCandidateRequest> cancelledRequests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var prmRepository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidatesToSave = new List<ProjectCandidate>();
                foreach (var updateCancelledProjectCandidateRequest in cancelledRequests)
                {
                    var projectCandidate = prmRepository.Retrieve<ProjectCandidate>(updateCancelledProjectCandidateRequest.Id);
                    Mapper.DynamicMap(updateCancelledProjectCandidateRequest, projectCandidate);

                    projectCandidatesToSave.Add(projectCandidate);
                }

                foreach (var projectCandidate in projectCandidatesToSave)
                    prmRepository.Save(projectCandidate);
            });
        }

        public void UpdateCancelledProjectCandidate(UpdateCancelledProjectCandidateRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var prmRepository = Container.Resolve<IProjectManagementCommandRepository>();
                var projectCandidate = prmRepository.Retrieve<ProjectCandidate>(request.Id);
                Mapper.DynamicMap(request, projectCandidate);
                prmRepository.Save(projectCandidate);
            });
        }

        public void UpdateProjectCategoryDetailUnitPrices(List<UpdateUnitPriceRequest> unitPriceRequests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var prmRepository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCategoryDetailsToSave = new List<ProjectCategoryDetail>();
                foreach (var updateProjectCategoryDetailUnitPriceRequest in unitPriceRequests)
                {
                    var projectCategoryDetail = prmRepository.Retrieve<ProjectCategoryDetail>(updateProjectCategoryDetailUnitPriceRequest.Id);
                    Mapper.DynamicMap(updateProjectCategoryDetailUnitPriceRequest, projectCategoryDetail);

                    projectCategoryDetailsToSave.Add(projectCategoryDetail);
                }

                foreach (var projectCategoryDetail in projectCategoryDetailsToSave)
                    prmRepository.Save(projectCategoryDetail);
            });
        }

        public Guid CreateProjectTypeCategoryUnitPrice(CreateProjectTypeCategoryUnitPriceRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var queryService = Container.Resolve<IProjectManagementQueryService>();

                var projectTypeCategoryUnitPrice =
                    queryService.RetrieveProjectTypeCategoryUnitPriceByTypeAndLevel(request.ProjectTypeCategoryId, request.ProjectTypeCategoryLevelId);

                if (projectTypeCategoryUnitPrice == null)
                {
                    var newProjectTypeCategoryUnitPrice = repository.Prepare<ProjectTypeCategoryUnitPrice>();

                    Mapper.DynamicMap(request, newProjectTypeCategoryUnitPrice);

                    repository.Save(newProjectTypeCategoryUnitPrice);

                    return newProjectTypeCategoryUnitPrice.Id;
                }

                return projectTypeCategoryUnitPrice.Id;
            });
        }

        public void UpdateProjectTypeCategoryUnitPrice(UpdateProjectTypeCategoryUnitPriceRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectTypeCategoryUnitPrice = repository.Retrieve<ProjectTypeCategoryUnitPrice>(request.Id);

                Mapper.DynamicMap(request, projectTypeCategoryUnitPrice);

                repository.Save(projectTypeCategoryUnitPrice);
            });
        }

        public void DeleteProjectTypeCategoryUnitPrice(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.Delete<ProjectTypeCategoryUnitPrice>(id);
            });
        }

        public void UpdateInvoicing(List<UpdateBaseInvoicingRequest> requests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                foreach (var request in requests)
                {
                    var updateProjectCandidateInvoicingRequest = request as UpdateProjectCandidateInvoicingRequest;
                    if (updateProjectCandidateInvoicingRequest != null)
                    {
                        var projectCandidate = repository.Retrieve<ProjectCandidate>(updateProjectCandidateInvoicingRequest.Id);
                        Mapper.DynamicMap(updateProjectCandidateInvoicingRequest, projectCandidate);
                        if (updateProjectCandidateInvoicingRequest.InvoiceStatusCode == 30)
                            projectCandidate.InvoicedDate = DateTime.Now;
                        repository.Save(projectCandidate);
                        continue;
                    }

                    var updateProjectCandidateCategoryInvoicingRequest = request as UpdateProjectCandidateCategoryInvoicingRequest;
                    if (updateProjectCandidateCategoryInvoicingRequest != null)
                    {
                        switch (updateProjectCandidateCategoryInvoicingRequest.CategoryDetailType)
                        {
                            case 1:
                                var projectCandidateCategoryDetailType1 = repository.Retrieve<ProjectCandidateCategoryDetailType1>(updateProjectCandidateCategoryInvoicingRequest.Id);
                                Mapper.DynamicMap(updateProjectCandidateCategoryInvoicingRequest, projectCandidateCategoryDetailType1);
                                if (updateProjectCandidateCategoryInvoicingRequest.InvoiceStatusCode == (int)InvoiceStatusType.Invoiced)
                                    projectCandidateCategoryDetailType1.InvoicedDate = DateTime.Now;
                                repository.Save(projectCandidateCategoryDetailType1);
                                break;

                            case 2:
                                var projectCandidateCategoryDetailType2 = repository.Retrieve<ProjectCandidateCategoryDetailType2>(updateProjectCandidateCategoryInvoicingRequest.Id);
                                Mapper.DynamicMap(updateProjectCandidateCategoryInvoicingRequest, projectCandidateCategoryDetailType2);
                                if (updateProjectCandidateCategoryInvoicingRequest.InvoiceStatusCode == (int)InvoiceStatusType.Invoiced)
                                    projectCandidateCategoryDetailType2.InvoicedDate = DateTime.Now;
                                repository.Save(projectCandidateCategoryDetailType2);
                                break;

                            case 3:
                                var projectCandidateCategoryDetailType3 = repository.Retrieve<ProjectCandidateCategoryDetailType3>(updateProjectCandidateCategoryInvoicingRequest.Id);
                                Mapper.DynamicMap(updateProjectCandidateCategoryInvoicingRequest, projectCandidateCategoryDetailType3);
                                if (updateProjectCandidateCategoryInvoicingRequest.InvoiceStatusCode == (int)InvoiceStatusType.Invoiced)
                                    projectCandidateCategoryDetailType3.InvoicedDate = DateTime.Now;
                                repository.Save(projectCandidateCategoryDetailType3);
                                break;
                        }
                        continue;
                    }

                    var updateProductInvoicingRequest = request as UpdateProductInvoicingRequest;
                    if (updateProductInvoicingRequest != null)
                    {
                        var projectProduct = repository.Retrieve<ProjectProduct>(updateProductInvoicingRequest.Id);
                        Mapper.DynamicMap(updateProductInvoicingRequest, projectProduct);
                        if (updateProductInvoicingRequest.InvoiceStatusCode == (int)InvoiceStatusType.Invoiced)
                            projectProduct.InvoicedDate = DateTime.Now;
                        repository.Save(projectProduct);
                    }
                }
            });
        }

        public void CreateProjectProducts(List<CreateProjectProductRequest> requests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                foreach (var request in requests)
                {
                    var projectProduct = repository.Prepare<ProjectProduct>();

                    Mapper.DynamicMap(request, projectProduct);
                    projectProduct.InvoiceStatusCode = (int)InvoiceStatusType.Planned;

                    repository.Save(projectProduct);
                }
            });
        }

        public Guid CreateProjectProduct(CreateProjectProductRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                if (request.Deadline == null && !request.NoInvoice)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("This product has an invoice, a deadline must be selected.");
                    return Guid.Empty;
                }
                else if (request.Deadline != null && request.NoInvoice)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("This product has no invoice, please remove the deadline.");
                    return Guid.Empty;
                }

                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectProduct = repository.Prepare<ProjectProduct>();

                var project = repository.RetrieveProject(request.ProjectId);

                Mapper.DynamicMap(request, projectProduct);

                switch (project.PricingModelId)
                {
                    case 1: //Time & Material
                        projectProduct.InvoiceStatusCode = (int)InvoiceStatusType.Planned;
                        break;
                    case 2: //Fixed price
                        projectProduct.InvoiceStatusCode = (int)InvoiceStatusType.FixedPrice;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("request", "Invalid pricing model.");
                }
                if (request.NoInvoice)
                {
                    projectProduct.InvoiceStatusCode = (int)InvoiceStatusType.NotBillable;
                }

                repository.Save(projectProduct);

                return projectProduct.Id;
            });
        }

        public void DeleteProjectProduct(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                //Retrieve projectProduct to check invoice status
                var queryService = Container.Resolve<IProjectManagementQueryService>();
                var projectProduct = queryService.RetrieveProjectProduct(id);

                //If invoiced, then don't delete
                if (projectProduct.InvoiceStatusCode == (int)InvoiceStatusType.Invoiced) return;

                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                repository.Delete<ProjectProduct>(id);
            });
        }

        public void DeleteProposal(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var queryService = Container.Resolve<IProjectManagementQueryService>();
                var projectProduct = queryService.RetrieveProposal(id);

                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                repository.Delete<Proposal>(id);
            });
        }

        public Guid CreateProjectEvaluation(CreateProjectEvaluationRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var newProjectEvaluation = repository.Prepare<ProjectEvaluation>();

                Mapper.DynamicMap(request, newProjectEvaluation);

                repository.Save(newProjectEvaluation);

                return newProjectEvaluation.Id;
            });
        }

        public void UpdateEvaluationForm(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var evaluation = repository.Retrieve<EvaluationForm>(id);

                evaluation.MailStatusTypeId = 20;

                repository.Save(evaluation);

            });
        }

        public void UpdateProjectEvaluation(UpdateProjectEvaluationRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectEvaluation = repository.Retrieve<ProjectEvaluation>(request.Id);

                Mapper.DynamicMap(request, projectEvaluation);

                repository.Save(projectEvaluation);
            });
        }

        public Guid CreateEvaluationForm(CreateEvaluationFormRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var queryService = Container.Resolve<IProjectManagementQueryService>();

                EvaluationForm evaluationForm;

                switch (request.EvaluationFormTypeId)
                {
                    case 10:
                        evaluationForm = repository.Prepare<EvaluationFormCoaching>();
                        break;
                    case 20:
                        evaluationForm = repository.Prepare<EvaluationFormCustomProjects>();
                        break;
                    default:
                        evaluationForm = repository.Prepare<EvaluationFormAcdc>();
                        break;

                }

                Mapper.DynamicMap(request, evaluationForm);
                evaluationForm.MailStatusTypeId = 10;
                evaluationForm.VerificationCode = queryService.CreateEvaluationFormVerificationCode();

                repository.Save(evaluationForm);

                return evaluationForm.Id;
            });
        }

        public Guid CreateProjectComplaint(CreateProjectComplaintRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var newProjectComplaint = repository.Prepare<ProjectComplaint>();

                Mapper.DynamicMap(request, newProjectComplaint);

                repository.Save(newProjectComplaint);

                return newProjectComplaint.Id;
            });
        }

        public void UpdateProjectComplaint(UpdateProjectComplaintRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectComplaint = repository.Retrieve<ProjectComplaint>(request.Id);

                Mapper.DynamicMap(request, projectComplaint);

                repository.Save(projectComplaint);
            });
        }

        public void UpdateProjectPlanPhase(UpdateProjectPlanPhaseRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectPlanPhase = repository.Retrieve<ProjectPlanPhase>(request.Id);

                Mapper.DynamicMap(request, projectPlanPhase);

                if (ValidationContainer.ValidateObject(projectPlanPhase))
                    repository.Save(projectPlanPhase);
            });
        }

        public void DeleteProjectPlanPhase(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var queryService = Container.Resolve<IProjectManagementQueryService>();

                var projectPlanPhase = queryService.RetrieveProjectPlanPhaseDetail(id);
                var project = queryService.RetrieveProjectByProjectPlan(projectPlanPhase.ProjectPlanId);

                if (project.Status != ProjectStatusCodeViewType.Draft)
                {
                    if (projectPlanPhase.ProjectPlanPhaseEntries.Count > 0)
                        ValidationContainer.RegisterEntityValidationFaultEntry("There are already activities/product registered on this project plan phase.");
                }
                else
                {
                    foreach (var projectPlanPhaseActivity in projectPlanPhase.ProjectPlanPhaseEntries.OfType<ProjectPlanPhaseActivityView>())
                        repository.Delete<ProjectPlanPhaseActivity>(projectPlanPhaseActivity.Id);

                    foreach (var projectPlanPhaseProduct in projectPlanPhase.ProjectPlanPhaseEntries.OfType<ProjectPlanPhaseProductView>())
                        repository.Delete<ProjectPlanPhaseProduct>(projectPlanPhaseProduct.Id);
                }

                repository.Delete<ProjectPlanPhase>(id);
            });
        }

        public void UpdateProjectCandidateScoringCoAssessorId(UpdateProjectCandidateScoringCoAssessorIdRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidate = repository.Retrieve<ProjectCandidate>(request.Id);

                Mapper.DynamicMap(request, projectCandidate);

                repository.Save(projectCandidate);
            });
        }

        public void UpdateCustomerFeedback(UpdateCustomerFeedbackRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                //Customer feedback is located on AssessmentDevelopmentProject
                var project = repository.Retrieve<Project>(request.Id);
                var assessmentDevelopmentProject = project as AssessmentDevelopmentProject;

                if (assessmentDevelopmentProject == null)
                {
                    ValidationContainer.RegisterException(new InvalidCastException(string.Format("Project '{0}' is not an ACDC Project", request.Id)));
                    return;
                }

                Mapper.DynamicMap(request, assessmentDevelopmentProject);

                if (ValidationContainer.ValidateObject(assessmentDevelopmentProject))
                    repository.Save(assessmentDevelopmentProject);
            });
        }

        public void UpdateProjectUnitPrices(UpdateProjectUnitPricesRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var queryService = Container.Resolve<IProjectManagementQueryService>();

                //Save project category details unit prices
                var projectCategoryDetailsToSave = new List<ProjectCategoryDetail>();
                var unitPrices = new Dictionary<Guid, decimal>();

                foreach (var updateProjectCategoryDetailUnitPriceRequest in request.UnitPriceRequests)
                {
                    var projectCategoryDetail = repository.Retrieve<ProjectCategoryDetail>(updateProjectCategoryDetailUnitPriceRequest.Id);
                    Mapper.DynamicMap(updateProjectCategoryDetailUnitPriceRequest, projectCategoryDetail);

                    projectCategoryDetailsToSave.Add(projectCategoryDetail);

                    unitPrices.Add(projectCategoryDetail.Id, updateProjectCategoryDetailUnitPriceRequest.UnitPrice);
                }

                foreach (var projectCategoryDetail in projectCategoryDetailsToSave)
                    repository.Save(projectCategoryDetail);


                //Save project candidate invoice amounts
                var candidates = queryService.ListProjectCandidateDetails(new ListProjectCandidateDetailsRequest { ProjectId = request.ProjectId });

                var projectCandidateCategoryDetailTypesToSave = new List<IProjectCandidateCategoryDetailType>();
                var projectCandidatesToSave = new List<ProjectCandidate>();

                //foreach (var candidate in candidates)
                //{
                //    //Save the invoice amounts on the subcategories of the project candidate
                //    foreach (var projectCandidateCategoryDetailType in candidate.ProjectCandidateCategoryDetailTypes)
                //    {
                //        if (projectCandidateCategoryDetailType is ProjectCandidateCategoryDetailType1View)
                //        {
                //            var projectCandidateCategoryDetailType1 = repository.Retrieve<ProjectCandidateCategoryDetailType1>(projectCandidateCategoryDetailType.Id);
                //            projectCandidateCategoryDetailType1.InvoiceAmount = unitPrices[projectCandidateCategoryDetailType1.ProjectCategoryDetailType1Id];
                //            projectCandidateCategoryDetailTypesToSave.Add(projectCandidateCategoryDetailType1);
                //        }
                //        else if (projectCandidateCategoryDetailType is ProjectCandidateCategoryDetailType2View)
                //        {
                //            var projectCandidateCategoryDetailType2 = repository.Retrieve<ProjectCandidateCategoryDetailType2>(projectCandidateCategoryDetailType.Id);
                //            projectCandidateCategoryDetailType2.InvoiceAmount = unitPrices[projectCandidateCategoryDetailType2.ProjectCategoryDetailType2Id];
                //            projectCandidateCategoryDetailTypesToSave.Add(projectCandidateCategoryDetailType2);
                //        }
                //        else if (projectCandidateCategoryDetailType is ProjectCandidateCategoryDetailType3View)
                //        {
                //            var projectCandidateCategoryDetailType3 = repository.Retrieve<ProjectCandidateCategoryDetailType3>(projectCandidateCategoryDetailType.Id);
                //            projectCandidateCategoryDetailType3.InvoiceAmount = unitPrices[projectCandidateCategoryDetailType3.ProjectCategoryDetailType3Id];
                //            projectCandidateCategoryDetailTypesToSave.Add(projectCandidateCategoryDetailType3);
                //        }
                //    }
                //
                //    //Save the invoice amount on the project candidate itself
                //    var projectCandidate = repository.Retrieve<ProjectCandidate>(candidate.Id);
                //    projectCandidate.InvoiceAmount = unitPrices.FirstOrDefault(up => !candidate.ProjectCandidateCategoryDetailTypes.Select(pccdt => pccdt.ProjectCategoryDetailTypeId).Contains(up.Key)).Value;
                //
                //    projectCandidatesToSave.Add(projectCandidate);
                //}

                foreach (var projectCandidateCategoryDetailType in projectCandidateCategoryDetailTypesToSave)
                    repository.Save(projectCandidateCategoryDetailType);

                foreach (var projectCandidate in projectCandidatesToSave)
                    repository.Save(projectCandidate);
            });
        }

        public void CreateProjectReportRecipients(List<CreateProjectReportRecipientRequest> requests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                foreach (var request in requests)
                {
                    var reportRecipient = repository.Prepare<ProjectReportRecipient>();

                    Mapper.DynamicMap(request, reportRecipient);

                    repository.Save(reportRecipient);
                }

            });
        }

        public void DeleteProjectReportRecipient(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                repository.Delete<ProjectReportRecipient>(id);

            });
        }

        public void UpdateProjectCandidateOverviewEntryProjectCandidateField(UpdateProjectCandidateOverviewEntryProjectCandidateFieldRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
                {
                    var repository = Container.Resolve<IProjectManagementCommandRepository>();

                    var projectCandidate = repository.Retrieve<ProjectCandidate>(request.Id);

                    if (projectCandidate.Audit.VersionId != request.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProjectCandidate { Id = request.Id }, projectCandidate);
                        return;
                    }

                    var property = projectCandidate.GetType().GetProperty(request.PropertyName);

                    var type = property.PropertyType.IsGenericType ? property.PropertyType.GenericTypeArguments[0] : property.PropertyType;

                    var value = string.IsNullOrEmpty(request.PropertyValue) ? null : Convert.ChangeType(request.PropertyValue, type);

                    property.SetValue(projectCandidate, value);

                    projectCandidate.Audit.VersionId = request.AuditVersionid;

                    if (ValidationContainer.ValidateObject(projectCandidate))
                        repository.Save(projectCandidate);
                });
        }

        public void UpdateProjectCandidateOverviewEntryProjectCandidateCategoryField(UpdateProjectCandidateOverviewEntryProjectCandidateCategoryFieldRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var queryService = Container.Resolve<IProjectManagementQueryService>();

                //Determine ProjectCandidateCategoryDetailType
                var projectCandidateCategoryDetailType = queryService.RetrieveProjectCandidateCategoryDetailType(request.Id);                

                var propertyUpdated = false;

                if (projectCandidateCategoryDetailType is ProjectCandidateCategoryDetailType1View)
                {
                    var projectCandidateCategoryDetailType1 = repository.RetrieveProjectCandidateCategoryDetailType<ProjectCandidateCategoryDetailType1>(request.Id);

                    if (projectCandidateCategoryDetailType1.Audit.VersionId != request.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProjectCandidateCategoryDetailType1 { Id = request.Id }, projectCandidateCategoryDetailType1);
                        return;
                    }

                    if (TryUpdateProperty(projectCandidateCategoryDetailType1, request))
                    {
                        if (ValidationContainer.ValidateObject(projectCandidateCategoryDetailType1))
                            repository.Save(projectCandidateCategoryDetailType1);
                        propertyUpdated = true;
                    }                                       
                    
                }
                else if (projectCandidateCategoryDetailType is ProjectCandidateCategoryDetailType2View)
                {
                    var projectCandidateCategoryDetailType2 = repository.RetrieveProjectCandidateCategoryDetailType<ProjectCandidateCategoryDetailType2>(request.Id);

                    if (projectCandidateCategoryDetailType2.Audit.VersionId != request.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProjectCandidateCategoryDetailType2 { Id = request.Id }, projectCandidateCategoryDetailType2);
                        return;
                    }

                    if (TryUpdateProperty(projectCandidateCategoryDetailType2, request))
                    {
                        if (ValidationContainer.ValidateObject(projectCandidateCategoryDetailType2))
                            repository.Save(projectCandidateCategoryDetailType2);
                        propertyUpdated = true;
                    }                   
                }
                else if (projectCandidateCategoryDetailType is ProjectCandidateCategoryDetailType3View)
                {
                    var projectCandidateCategoryDetailType3 = repository.RetrieveProjectCandidateCategoryDetailType<ProjectCandidateCategoryDetailType3>(request.Id);

                    if (projectCandidateCategoryDetailType3.Audit.VersionId != request.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProjectCandidateCategoryDetailType3 { Id = request.Id }, projectCandidateCategoryDetailType3);
                        return;
                    }

                    if (TryUpdateProperty(projectCandidateCategoryDetailType3, request))
                    {
                        if (ValidationContainer.ValidateObject(projectCandidateCategoryDetailType3))
                            repository.Save(projectCandidateCategoryDetailType3);
                        propertyUpdated = true;
                    }    
                }

                if (!propertyUpdated)
                {
                    var projectCandidate = queryService.RetrieveProjectCandidate(projectCandidateCategoryDetailType.ProjectCandidateId);
                    var updateProjectCandidateOverviewEntryProjectCandidateFieldRequest = new UpdateProjectCandidateOverviewEntryProjectCandidateFieldRequest()
                    {
                        AuditVersionid = projectCandidate.Audit.VersionId,
                        Id = projectCandidate.Id,
                        PropertyName = request.PropertyName,
                        PropertyValue = request.PropertyValue
                    };
                    UpdateProjectCandidateOverviewEntryProjectCandidateField(updateProjectCandidateOverviewEntryProjectCandidateFieldRequest);
                }
            });
        }

        public bool TryUpdateProperty(IProjectCandidateCategoryDetailType projectCandidateCategoryDetailType, UpdateProjectCandidateOverviewEntryProjectCandidateCategoryFieldRequest request)
        {
            var propertyProjectCandidateCategory = projectCandidateCategoryDetailType.GetType().GetProperty(request.PropertyName);

            if (propertyProjectCandidateCategory == null)
            {
                return false;                
            }

            var type = propertyProjectCandidateCategory.PropertyType.IsGenericType ? propertyProjectCandidateCategory.PropertyType.GenericTypeArguments[0] : propertyProjectCandidateCategory.PropertyType;

            var value = string.IsNullOrEmpty(request.PropertyValue) ? null : Convert.ChangeType(request.PropertyValue, type);

            propertyProjectCandidateCategory.SetValue(projectCandidateCategoryDetailType, value);

            return true;
        }

        public void UpdateEvaluationFormCoachingPart1(UpdateEvaluationFormCoachingPart1Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCoaching = repository.Retrieve<EvaluationFormCoaching>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCoaching);

                repository.Save(evaluationFormCoaching);
            });
        }

        public void UpdateEvaluationFormCoachingPart2(UpdateEvaluationFormCoachingPart2Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCoaching = repository.Retrieve<EvaluationFormCoaching>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCoaching);

                repository.Save(evaluationFormCoaching);
            });
        }

        public void UpdateEvaluationFormCoachingPart3(UpdateEvaluationFormCoachingPart3Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCoaching = repository.Retrieve<EvaluationFormCoaching>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCoaching);

                repository.Save(evaluationFormCoaching);
            });
        }

        public void UpdateEvaluationFormCoachingPart4(UpdateEvaluationFormCoachingPart4Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCoaching = repository.Retrieve<EvaluationFormCoaching>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCoaching);

                repository.Save(evaluationFormCoaching);
            });
        }

        public void UpdateEvaluationFormCoachingPart5(UpdateEvaluationFormCoachingPart5Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCoaching = repository.Retrieve<EvaluationFormCoaching>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCoaching);

                repository.Save(evaluationFormCoaching);
            });
        }

        public void UpdateEvaluationFormCoachingPart6(UpdateEvaluationFormCoachingPart6Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCoaching = repository.Retrieve<EvaluationFormCoaching>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCoaching);

                repository.Save(evaluationFormCoaching);
            });
        }

        public void UpdateEvaluationFormCoachingPart7(UpdateEvaluationFormCoachingPart7Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCoaching = repository.Retrieve<EvaluationFormCoaching>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCoaching);

                repository.Save(evaluationFormCoaching);
            });
        }

        public void UpdateEvaluationFormCoachingCompleted(UpdateEvaluationFormCoachingCompletedRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCoaching = repository.Retrieve<EvaluationFormCoaching>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCoaching);

                repository.Save(evaluationFormCoaching);
            });
        }

        public Guid CreateCulturalFitContactRequest(CreateCulturalFitContactRequestRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                if (request.CrmEmailId <= default(int))
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("No contactperson was selected.");
                    return Guid.Empty;
                }
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var id = repository.CreateTheoremListRequestContact(request.ContactId, request.ProjectId, request.CrmEmailId, request.Deadline, request.TheoremListRequestTypeId, request.Description);

                return id;
            });
        }

        public void UpdateCulturalFitContactRequest(UpdateCulturalFitContactRequestRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var theoremListRequest = repository.Retrieve<TheoremListRequest>(request.Id);

                Mapper.DynamicMap(request, theoremListRequest);

                repository.Save(theoremListRequest);
            });
        }

        public void UpdateProjectProduct(UpdateProjectProductRequest request)
        {
            LogTrace();            

            ExecuteTransaction(() =>
            {
                if (request.Deadline == null && !request.NoInvoice)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("This product has an invoice, a deadline must be selected.");
                    return;
                }
                else if (request.Deadline != null && request.NoInvoice)
                {
                    ValidationContainer.RegisterEntityValidationFaultEntry("This product has no invoice, please remove the deadline.");
                    return;
                }

                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectProduct = repository.Retrieve<ProjectProduct>(request.Id);

                //Unable to modify invoice amount if status is invoiced.
                if (projectProduct.InvoiceStatusCode == (int)InvoiceStatusType.Invoiced)
                {
                    request.InvoiceAmount = projectProduct.InvoiceAmount;
                    request.Deadline = projectProduct.Deadline;
                    request.NoInvoice = projectProduct.NoInvoice;
                }
                else
                {
                    if (request.NoInvoice)
                    {
                        projectProduct.InvoiceStatusCode = (int)InvoiceStatusType.NotBillable;
                    }
                    else
                    {
                        projectProduct.InvoiceStatusCode = (int)InvoiceStatusType.Planned;
                    }
                }

                Mapper.DynamicMap(request, projectProduct);

                repository.Save(projectProduct);
            });
        }

        public void ReopenCulturalFitContactRequest(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var theoremLists = repository.RetrieveTheoremListsByTheoremListRequestId(id);

                if (theoremLists.Any(tl => tl.IsCompleted))
                {
                    foreach (var theoremList in theoremLists)
                    {
                        theoremList.IsCompleted = false;
                        repository.Save(theoremList);
                    }
                }

            });
        }

        public void UpdateEvaluationFormAcdcPart1(UpdateEvaluationFormAcdcPart1Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormAcdc = repository.Retrieve<EvaluationFormAcdc>(request.Id);

                Mapper.DynamicMap(request, evaluationFormAcdc);

                repository.Save(evaluationFormAcdc);
            });
        }

        public void UpdateEvaluationFormAcdcPart2(UpdateEvaluationFormAcdcPart2Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormAcdc = repository.Retrieve<EvaluationFormAcdc>(request.Id);

                Mapper.DynamicMap(request, evaluationFormAcdc);

                repository.Save(evaluationFormAcdc);
            });
        }

        public void UpdateEvaluationFormAcdcPart3(UpdateEvaluationFormAcdcPart3Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormAcdc = repository.Retrieve<EvaluationFormAcdc>(request.Id);

                Mapper.DynamicMap(request, evaluationFormAcdc);

                repository.Save(evaluationFormAcdc);
            });
        }

        public void UpdateEvaluationFormAcdcPart4(UpdateEvaluationFormAcdcPart4Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormAcdc = repository.Retrieve<EvaluationFormAcdc>(request.Id);

                Mapper.DynamicMap(request, evaluationFormAcdc);

                repository.Save(evaluationFormAcdc);
            });
        }

        public void UpdateEvaluationFormAcdcPart5(UpdateEvaluationFormAcdcPart5Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormAcdc = repository.Retrieve<EvaluationFormAcdc>(request.Id);

                Mapper.DynamicMap(request, evaluationFormAcdc);

                repository.Save(evaluationFormAcdc);
            });
        }

        public void UpdateEvaluationFormAcdcPart6(UpdateEvaluationFormAcdcPart6Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormAcdc = repository.Retrieve<EvaluationFormAcdc>(request.Id);

                Mapper.DynamicMap(request, evaluationFormAcdc);

                repository.Save(evaluationFormAcdc);
            });
        }

        public void UpdateEvaluationFormAcdcCompleted(UpdateEvaluationFormAcdcCompletedRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormAcdc = repository.Retrieve<EvaluationFormAcdc>(request.Id);

                Mapper.DynamicMap(request, evaluationFormAcdc);

                repository.Save(evaluationFormAcdc);
            });
        }

        public void UpdateEvaluationFormCustomProjectsPart1(UpdateEvaluationFormCustomProjectsPart1Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCustomProjects = repository.Retrieve<EvaluationFormCustomProjects>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCustomProjects);

                repository.Save(evaluationFormCustomProjects);
            });
        }

        public void UpdateEvaluationFormCustomProjectsPart2(UpdateEvaluationFormCustomProjectsPart2Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCustomProjects = repository.Retrieve<EvaluationFormCustomProjects>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCustomProjects);

                repository.Save(evaluationFormCustomProjects);
            });
        }

        public void UpdateEvaluationFormCustomProjectsPart3(UpdateEvaluationFormCustomProjectsPart3Request request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCustomProjects = repository.Retrieve<EvaluationFormCustomProjects>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCustomProjects);

                repository.Save(evaluationFormCustomProjects);
            });
        }

        public void UpdateEvaluationFormCustomProjectsCompleted(UpdateEvaluationFormCustomProjectsCompletedRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var evaluationFormCustomProjects = repository.Retrieve<EvaluationFormCustomProjects>(request.Id);

                Mapper.DynamicMap(request, evaluationFormCustomProjects);

                repository.Save(evaluationFormCustomProjects);
            });
        }

        public void UpdateProjectCandidateReportingOverviewEntryProjectCandidateField(
            UpdateProjectCandidateReportingOverviewEntryProjectCandidateFieldRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidate = repository.Retrieve<ProjectCandidate>(request.Id);

                if (projectCandidate.Audit.VersionId != request.AuditVersionid)
                {
                    ValidationContainer.RegisterVersionMismatchEntry(new ProjectCandidate { Id = request.Id }, projectCandidate);
                    return;
                }

                var property = projectCandidate.GetType().GetProperty(request.PropertyName);

                var type = property.PropertyType.IsGenericType ? property.PropertyType.GenericTypeArguments[0] : property.PropertyType;

                var value = string.IsNullOrEmpty(request.PropertyValue) ? null : TypeDescriptor.GetConverter(type).ConvertFromString(request.PropertyValue);

                property.SetValue(projectCandidate, value);

                projectCandidate.Audit.VersionId = request.AuditVersionid;

                if (ValidationContainer.ValidateObject(projectCandidate))
                    repository.Save(projectCandidate);
            });
        }

        public void UpdateProjectCandidateReportingOverviewEntryProjectField(
            UpdateProjectCandidateReportingOverviewEntryProjectFieldRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var project = repository.Retrieve<AssessmentDevelopmentProject>(request.Id);

                if (project.Audit.VersionId != request.AuditVersionid)
                {
                    ValidationContainer.RegisterVersionMismatchEntry(new AssessmentDevelopmentProject { Id = request.Id }, project);
                    return;
                }

                var property = project.GetType().GetProperty(request.PropertyName);

                var type = property.PropertyType.IsGenericType ? property.PropertyType.GenericTypeArguments[0] : property.PropertyType;

                var value = string.IsNullOrEmpty(request.PropertyValue) ? null : Convert.ChangeType(request.PropertyValue, type);

                property.SetValue(project, value);

                project.Audit.VersionId = request.AuditVersionid;

                if (ValidationContainer.ValidateObject(project))
                    repository.Save(project);
            });
        }

        public void UpdateProjectCandidateRemarks(UpdateProjectCandidateRemarksRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidate = repository.Retrieve<ProjectCandidate>(request.Id);

                Mapper.DynamicMap(request, projectCandidate);

                repository.Save(projectCandidate);
            });
        }

        public Guid CreateNewProjectRoleLanguage(CreateNewProjectRoleLanguageRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectRoleTranslation = repository.Prepare<ProjectRoleTranslation>();

                Mapper.DynamicMap(request, projectRoleTranslation);

                repository.Save(projectRoleTranslation);

                return projectRoleTranslation.Id;
            });
        }

        public void UpdateProjectManagerInvoicingEntries(List<UpdateInvoicingBaseRequest> requests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidatesToUpdate = new List<ProjectCandidate>();
                var projectCandidateCategoryType1ToUpdate = new List<ProjectCandidateCategoryDetailType1>();
                var projectCandidateCategoryType2ToUpdate = new List<ProjectCandidateCategoryDetailType2>();
                var projectCandidateCategoryType3ToUpdate = new List<ProjectCandidateCategoryDetailType3>();
                var projectProductsToUpdate = new List<ProjectProduct>();
                var projectFixedPricesToUpdate = new List<ProjectFixedPrice>();
                var productSheetEntriesToUpdate = new List<ProductSheetEntry>();
                var timesheetEntriesToUpdate = new List<TimesheetEntry>();

                foreach (var request in requests)
                {
                    var projectCandidateInvoicingRequest = request as UpdateProjectManagerProjectCandidateInvoicingRequest;
                    if (projectCandidateInvoicingRequest != null)
                    {
                        var projectCandidate = repository.Retrieve<ProjectCandidate>(projectCandidateInvoicingRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateInvoicingRequest, projectCandidate);
                        Mapper.DynamicMap(projectCandidateInvoicingRequest, projectCandidate);
                        if (updateInvoiceDate) projectCandidate.InvoicedDate = DateTime.Now;
                        projectCandidatesToUpdate.Add(projectCandidate);
                    }

                    var projectCandidateCategoryType1InvoicingRequest = request as UpdateProjectManagerProjectCandidateCategoryType1InvoicingRequest;
                    if (projectCandidateCategoryType1InvoicingRequest != null)
                    {
                        var projectCandidateCategoryDetailType1 = repository.Retrieve<ProjectCandidateCategoryDetailType1>(projectCandidateCategoryType1InvoicingRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateCategoryType1InvoicingRequest, projectCandidateCategoryDetailType1);
                        Mapper.DynamicMap(projectCandidateCategoryType1InvoicingRequest, projectCandidateCategoryDetailType1);
                        if (updateInvoiceDate) projectCandidateCategoryDetailType1.InvoicedDate = DateTime.Now;
                        projectCandidateCategoryType1ToUpdate.Add(projectCandidateCategoryDetailType1);
                    }

                    var projectCandidateCategoryType2InvoicingRequest = request as UpdateProjectManagerProjectCandidateCategoryType2InvoicingRequest;
                    if (projectCandidateCategoryType2InvoicingRequest != null)
                    {
                        var projectCandidateCategoryDetailType2 = repository.Retrieve<ProjectCandidateCategoryDetailType2>(projectCandidateCategoryType2InvoicingRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateCategoryType2InvoicingRequest, projectCandidateCategoryDetailType2);
                        Mapper.DynamicMap(projectCandidateCategoryType2InvoicingRequest, projectCandidateCategoryDetailType2);
                        if (updateInvoiceDate) projectCandidateCategoryDetailType2.InvoicedDate = DateTime.Now;
                        projectCandidateCategoryType2ToUpdate.Add(projectCandidateCategoryDetailType2);
                    }

                    var projectCandidateCategoryType3InvoicingRequest = request as UpdateProjectManagerProjectCandidateCategoryType3InvoicingRequest;
                    if (projectCandidateCategoryType3InvoicingRequest != null)
                    {
                        var projectCandidateCategoryDetailType3 = repository.Retrieve<ProjectCandidateCategoryDetailType3>(projectCandidateCategoryType3InvoicingRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateCategoryType3InvoicingRequest, projectCandidateCategoryDetailType3);
                        Mapper.DynamicMap(projectCandidateCategoryType3InvoicingRequest, projectCandidateCategoryDetailType3);
                        if (updateInvoiceDate) projectCandidateCategoryDetailType3.InvoicedDate = DateTime.Now;
                        projectCandidateCategoryType3ToUpdate.Add(projectCandidateCategoryDetailType3);
                    }

                    var projectProductRequest = request as UpdateProjectManagerProjectProductInvoicingRequest;
                    if (projectProductRequest != null)
                    {
                        var projectProduct = repository.Retrieve<ProjectProduct>(projectProductRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectProductRequest, projectProduct);
                        Mapper.DynamicMap(projectProductRequest, projectProduct);
                        if (updateInvoiceDate) projectProduct.InvoicedDate = DateTime.Now;
                        projectProductsToUpdate.Add(projectProduct);
                    }

                    var acdcProjectFixedPriceRequest = request as UpdateProjectManagerAcdcProjectFixedPriceInvoicingRequest;
                    if (acdcProjectFixedPriceRequest != null)
                    {
                        var projectFixedPrice = repository.Retrieve<ProjectFixedPrice>(acdcProjectFixedPriceRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(acdcProjectFixedPriceRequest, projectFixedPrice);
                        Mapper.DynamicMap(acdcProjectFixedPriceRequest, projectFixedPrice);

                        projectFixedPrice.Amount = acdcProjectFixedPriceRequest.InvoiceAmount > 0 ? Convert.ToDecimal(acdcProjectFixedPriceRequest.InvoiceAmount) : projectFixedPrice.Amount;
                        
                        if (updateInvoiceDate) projectFixedPrice.InvoicedDate = DateTime.Now;
                        projectFixedPricesToUpdate.Add(projectFixedPrice);
                    }

                    var consultancyProjectFixedPriceRequest = request as UpdateProjectManagerConsultancyProjectFixedPriceInvoicingRequest;
                    if (consultancyProjectFixedPriceRequest != null)
                    {
                        var projectFixedPrice = repository.Retrieve<ProjectFixedPrice>(consultancyProjectFixedPriceRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(consultancyProjectFixedPriceRequest, projectFixedPrice);
                        Mapper.DynamicMap(consultancyProjectFixedPriceRequest, projectFixedPrice);

                        projectFixedPrice.Amount = consultancyProjectFixedPriceRequest.InvoiceAmount > 0 ? Convert.ToDecimal(consultancyProjectFixedPriceRequest.InvoiceAmount) : projectFixedPrice.Amount;

                        if (updateInvoiceDate) projectFixedPrice.InvoicedDate = DateTime.Now;
                        projectFixedPricesToUpdate.Add(projectFixedPrice);
                    }

                    var productSheetEntryRequest = request as UpdateProjectManagerProductSheetEntryInvoicingRequest;
                    if (productSheetEntryRequest != null)
                    {
                        var productSheetEntry = repository.Retrieve<ProductSheetEntry>(productSheetEntryRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(productSheetEntryRequest, productSheetEntry);
                        Mapper.DynamicMap(productSheetEntryRequest, productSheetEntry);
                        if (updateInvoiceDate) productSheetEntry.InvoicedDate = DateTime.Now;
                        productSheetEntriesToUpdate.Add(productSheetEntry);
                    }

                    var timesheetEntryRequest = request as UpdateProjectManagerTimesheetEntryInvoicingRequest;
                    if (timesheetEntryRequest != null)
                    {
                        var timesheetEntry = repository.Retrieve<TimesheetEntry>(timesheetEntryRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(timesheetEntryRequest, timesheetEntry);
                        Mapper.DynamicMap(timesheetEntryRequest, timesheetEntry);
                        if (updateInvoiceDate) timesheetEntry.InvoicedDate = DateTime.Now;
                        timesheetEntriesToUpdate.Add(timesheetEntry);
                    }
                }

                //Update project candidates.
                foreach (var projectCandidate in projectCandidatesToUpdate)
                {
                    repository.Save(projectCandidate);
                }

                //Update project candidate category types 1.
                foreach (var type1 in projectCandidateCategoryType1ToUpdate)
                {
                    repository.Save(type1);
                }

                //Update project candidate category types 2.
                foreach (var type2 in projectCandidateCategoryType2ToUpdate)
                {
                    repository.Save(type2);
                }

                //Update project candidate category types 3.
                foreach (var type3 in projectCandidateCategoryType3ToUpdate)
                {
                    repository.Save(type3);
                }

                //Update project products.
                foreach (var projectProduct in projectProductsToUpdate)
                {
                    repository.Save(projectProduct);
                }

                //Update project fixed prices.
                foreach (var projectFixedPrice in projectFixedPricesToUpdate)
                {
                    repository.Save(projectFixedPrice);
                }

                //Update product sheet entries.
                foreach (var productSheetEntry in productSheetEntriesToUpdate)
                {
                    repository.Save(productSheetEntry);
                }

                //Update timesheet entries.
                foreach (var timesheetEntry in timesheetEntriesToUpdate)
                {
                    repository.Save(timesheetEntry);
                }
            });
        }

        public void UpdateCustomerAssistantInvoicingEntries(List<UpdateInvoicingBaseRequest> requests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidatesToUpdate = new List<ProjectCandidate>();
                var projectCandidateCategoryType1ToUpdate = new List<ProjectCandidateCategoryDetailType1>();
                var projectCandidateCategoryType2ToUpdate = new List<ProjectCandidateCategoryDetailType2>();
                var projectCandidateCategoryType3ToUpdate = new List<ProjectCandidateCategoryDetailType3>();
                var projectProductsToUpdate = new List<ProjectProduct>();
                var projectFixedPricesToUpdate = new List<ProjectFixedPrice>();
                var productSheetEntriesToUpdate = new List<ProductSheetEntry>();
                var timesheetEntriesToUpdate = new List<TimesheetEntry>();

                foreach (var request in requests)
                {
                    var projectCandidateInvoicingRequest = request as UpdateCustomerAssistantProjectCandidateInvoicingRequest;
                    if (projectCandidateInvoicingRequest != null)
                    {
                        var projectCandidate = repository.Retrieve<ProjectCandidate>(projectCandidateInvoicingRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateInvoicingRequest, projectCandidate);
                        Mapper.DynamicMap(projectCandidateInvoicingRequest, projectCandidate);
                        if (updateInvoiceDate) projectCandidate.InvoicedDate = DateTime.Now;
                        projectCandidatesToUpdate.Add(projectCandidate);
                    }

                    var projectCandidateCategoryType1InvoicingRequest = request as UpdateCustomerAssistantProjectCandidateCategoryType1InvoicingRequest;
                    if (projectCandidateCategoryType1InvoicingRequest != null)
                    {
                        var projectCandidateCategoryDetailType1 = repository.Retrieve<ProjectCandidateCategoryDetailType1>(projectCandidateCategoryType1InvoicingRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateCategoryType1InvoicingRequest, projectCandidateCategoryDetailType1);
                        Mapper.DynamicMap(projectCandidateCategoryType1InvoicingRequest, projectCandidateCategoryDetailType1);
                        if (updateInvoiceDate) projectCandidateCategoryDetailType1.InvoicedDate = DateTime.Now;
                        projectCandidateCategoryType1ToUpdate.Add(projectCandidateCategoryDetailType1);
                    }

                    var projectCandidateCategoryType2InvoicingRequest = request as UpdateCustomerAssistantProjectCandidateCategoryType2InvoicingRequest;
                    if (projectCandidateCategoryType2InvoicingRequest != null)
                    {
                        var projectCandidateCategoryDetailType2 = repository.Retrieve<ProjectCandidateCategoryDetailType2>(projectCandidateCategoryType2InvoicingRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateCategoryType2InvoicingRequest, projectCandidateCategoryDetailType2);
                        Mapper.DynamicMap(projectCandidateCategoryType2InvoicingRequest, projectCandidateCategoryDetailType2);
                        if (updateInvoiceDate) projectCandidateCategoryDetailType2.InvoicedDate = DateTime.Now;
                        projectCandidateCategoryType2ToUpdate.Add(projectCandidateCategoryDetailType2);
                    }

                    var projectCandidateCategoryType3InvoicingRequest = request as UpdateCustomerAssistantProjectCandidateCategoryType3InvoicingRequest;
                    if (projectCandidateCategoryType3InvoicingRequest != null)
                    {
                        var projectCandidateCategoryDetailType3 = repository.Retrieve<ProjectCandidateCategoryDetailType3>(projectCandidateCategoryType3InvoicingRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateCategoryType3InvoicingRequest, projectCandidateCategoryDetailType3);
                        Mapper.DynamicMap(projectCandidateCategoryType3InvoicingRequest, projectCandidateCategoryDetailType3);
                        if (updateInvoiceDate) projectCandidateCategoryDetailType3.InvoicedDate = DateTime.Now;
                        projectCandidateCategoryType3ToUpdate.Add(projectCandidateCategoryDetailType3);
                    }

                    var projectProductRequest = request as UpdateCustomerAssistantProjectProductInvoicingRequest;
                    if (projectProductRequest != null)
                    {
                        var projectProduct = repository.Retrieve<ProjectProduct>(projectProductRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectProductRequest, projectProduct);
                        Mapper.DynamicMap(projectProductRequest, projectProduct);
                        if (updateInvoiceDate) projectProduct.InvoicedDate = DateTime.Now;
                        projectProductsToUpdate.Add(projectProduct);
                    }

                    var acdcProjectFixedPriceRequest = request as UpdateCustomerAssistantAcdcProjectFixedPriceInvoicingRequest;
                    if (acdcProjectFixedPriceRequest != null)
                    {
                        var projectFixedPrice = repository.Retrieve<ProjectFixedPrice>(acdcProjectFixedPriceRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(acdcProjectFixedPriceRequest, projectFixedPrice);
                        Mapper.DynamicMap(acdcProjectFixedPriceRequest, projectFixedPrice);
                        if (updateInvoiceDate) projectFixedPrice.InvoicedDate = DateTime.Now;
                        projectFixedPricesToUpdate.Add(projectFixedPrice);
                    }

                    var consultancyProjectFixedPriceRequest = request as UpdateCustomerAssistantConsultancyProjectFixedPriceInvoicingRequest;
                    if (consultancyProjectFixedPriceRequest != null)
                    {
                        var projectFixedPrice = repository.Retrieve<ProjectFixedPrice>(consultancyProjectFixedPriceRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(consultancyProjectFixedPriceRequest, projectFixedPrice);
                        Mapper.DynamicMap(consultancyProjectFixedPriceRequest, projectFixedPrice);
                        if (updateInvoiceDate) projectFixedPrice.InvoicedDate = DateTime.Now;
                        projectFixedPricesToUpdate.Add(projectFixedPrice);
                    }

                    var productSheetEntryRequest = request as UpdateCustomerAssistantProductSheetEntryInvoicingRequest;
                    if (productSheetEntryRequest != null)
                    {
                        var productSheetEntry = repository.Retrieve<ProductSheetEntry>(productSheetEntryRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(productSheetEntryRequest, productSheetEntry);
                        Mapper.DynamicMap(productSheetEntryRequest, productSheetEntry);
                        if (updateInvoiceDate) productSheetEntry.InvoicedDate = DateTime.Now;
                        productSheetEntriesToUpdate.Add(productSheetEntry);
                    }

                    var timesheetEntryRequest = request as UpdateCustomerAssistantTimesheetEntryInvoicingRequest;
                    if (timesheetEntryRequest != null)
                    {
                        var timesheetEntry = repository.Retrieve<TimesheetEntry>(timesheetEntryRequest.Id);
                        var updateInvoiceDate = StatusCodeUpdatedToNotBillable(timesheetEntryRequest, timesheetEntry);
                        Mapper.DynamicMap(timesheetEntryRequest, timesheetEntry);
                        if (updateInvoiceDate) timesheetEntry.InvoicedDate = DateTime.Now;
                        timesheetEntriesToUpdate.Add(timesheetEntry);
                    }
                }

                //Update project candidates.
                foreach (var projectCandidate in projectCandidatesToUpdate)
                {
                    repository.Save(projectCandidate);
                }

                //Update project candidate category types 1.
                foreach (var type1 in projectCandidateCategoryType1ToUpdate)
                {
                    repository.Save(type1);
                }

                //Update project candidate category types 2.
                foreach (var type2 in projectCandidateCategoryType2ToUpdate)
                {
                    repository.Save(type2);
                }

                //Update project candidate category types 3.
                foreach (var type3 in projectCandidateCategoryType3ToUpdate)
                {
                    repository.Save(type3);
                }

                //Update project products.
                foreach (var projectProduct in projectProductsToUpdate)
                {
                    repository.Save(projectProduct);
                }

                //Update project fixed prices.
                foreach (var projectFixedPrice in projectFixedPricesToUpdate)
                {
                    repository.Save(projectFixedPrice);
                }

                //Update product sheet entries.
                foreach (var productSheetEntry in productSheetEntriesToUpdate)
                {
                    repository.Save(productSheetEntry);
                }

                //Update timesheet entries.
                foreach (var timesheetEntry in timesheetEntriesToUpdate)
                {
                    repository.Save(timesheetEntry);
                }
            });
        }

        public void UpdateAccountantInvoicingEntry(UpdateAccountantInvoicingBaseRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectCandidateInvoicingRequest = request as UpdateAccountantProjectCandidateInvoicingRequest;
                if (projectCandidateInvoicingRequest != null)
                {
                    var projectCandidate = repository.Retrieve<ProjectCandidate>(projectCandidateInvoicingRequest.Id);
                    if (projectCandidate.Audit.VersionId != projectCandidateInvoicingRequest.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProjectCandidate { Id = request.Id }, projectCandidate);
                        return;
                    }
                    var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateInvoicingRequest, projectCandidate);
                    Mapper.DynamicMap(projectCandidateInvoicingRequest, projectCandidate);
                    if (updateInvoiceDate) projectCandidate.InvoicedDate = DateTime.Now;
                    repository.Save(projectCandidate);
                    return;
                }

                var projectCandidateCategoryType1InvoicingRequest = request as UpdateAccountantProjectCandidateCategoryType1InvoicingRequest;
                if (projectCandidateCategoryType1InvoicingRequest != null)
                {

                    var projectCandidateCategoryDetailType1 = repository.Retrieve<ProjectCandidateCategoryDetailType1>(projectCandidateCategoryType1InvoicingRequest.Id);
                    if (projectCandidateCategoryDetailType1.Audit.VersionId != projectCandidateCategoryType1InvoicingRequest.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProjectCandidateCategoryDetailType1 { Id = request.Id }, projectCandidateCategoryDetailType1);
                        return;
                    }
                    var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateCategoryType1InvoicingRequest, projectCandidateCategoryDetailType1);
                    Mapper.DynamicMap(projectCandidateCategoryType1InvoicingRequest, projectCandidateCategoryDetailType1);
                    if (updateInvoiceDate) projectCandidateCategoryDetailType1.InvoicedDate = DateTime.Now;
                    repository.Save(projectCandidateCategoryDetailType1);
                    return;
                }

                var projectCandidateCategoryType2InvoicingRequest = request as UpdateAccountantProjectCandidateCategoryType2InvoicingRequest;
                if (projectCandidateCategoryType2InvoicingRequest != null)
                {

                    var projectCandidateCategoryDetailType2 = repository.Retrieve<ProjectCandidateCategoryDetailType2>(projectCandidateCategoryType2InvoicingRequest.Id);
                    if (projectCandidateCategoryDetailType2.Audit.VersionId != projectCandidateCategoryType2InvoicingRequest.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProjectCandidateCategoryDetailType2 { Id = request.Id }, projectCandidateCategoryDetailType2);
                        return;
                    }
                    var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateCategoryType2InvoicingRequest, projectCandidateCategoryDetailType2);
                    Mapper.DynamicMap(projectCandidateCategoryType2InvoicingRequest, projectCandidateCategoryDetailType2);
                    if (updateInvoiceDate) projectCandidateCategoryDetailType2.InvoicedDate = DateTime.Now;
                    repository.Save(projectCandidateCategoryDetailType2);
                    return;
                }

                var projectCandidateCategoryType3InvoicingRequest = request as UpdateAccountantProjectCandidateCategoryType3InvoicingRequest;
                if (projectCandidateCategoryType3InvoicingRequest != null)
                {
                    var projectCandidateCategoryDetailType3 = repository.Retrieve<ProjectCandidateCategoryDetailType3>(projectCandidateCategoryType3InvoicingRequest.Id);
                    if (projectCandidateCategoryDetailType3.Audit.VersionId != projectCandidateCategoryType3InvoicingRequest.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProjectCandidateCategoryDetailType3 { Id = request.Id }, projectCandidateCategoryDetailType3);
                        return;
                    }
                    var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectCandidateCategoryType3InvoicingRequest, projectCandidateCategoryDetailType3);
                    Mapper.DynamicMap(projectCandidateCategoryType3InvoicingRequest, projectCandidateCategoryDetailType3);
                    if (updateInvoiceDate) projectCandidateCategoryDetailType3.InvoicedDate = DateTime.Now;
                    repository.Save(projectCandidateCategoryDetailType3);
                    return;
                }

                var projectProductRequest = request as UpdateAccountantProjectProductInvoicingRequest;
                if (projectProductRequest != null)
                {

                    var projectProduct = repository.Retrieve<ProjectProduct>(projectProductRequest.Id);
                    if (projectProduct.Audit.VersionId != projectProductRequest.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProjectProduct { Id = request.Id }, projectProduct);
                        return;
                    }
                    var updateInvoiceDate = StatusCodeUpdatedToNotBillable(projectProductRequest, projectProduct);
                    Mapper.DynamicMap(projectProductRequest, projectProduct);
                    if (updateInvoiceDate) projectProduct.InvoicedDate = DateTime.Now;
                    repository.Save(projectProduct);
                    return;
                }

                var acdcProjectFixedPriceRequest = request as UpdateAccountantAcdcProjectFixedPriceInvoicingRequest;
                if (acdcProjectFixedPriceRequest != null)
                {
                    var projectFixedPrice = repository.Retrieve<ProjectFixedPrice>(acdcProjectFixedPriceRequest.Id);
                    if (projectFixedPrice.Audit.VersionId != acdcProjectFixedPriceRequest.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProjectFixedPrice { Id = request.Id }, projectFixedPrice);
                        return;
                    }
                    var updateInvoiceDate = StatusCodeUpdatedToNotBillable(acdcProjectFixedPriceRequest, projectFixedPrice);
                    Mapper.DynamicMap(acdcProjectFixedPriceRequest, projectFixedPrice);
                    if (updateInvoiceDate) projectFixedPrice.InvoicedDate = DateTime.Now;
                    repository.Save(projectFixedPrice);
                }

                var consultancyProjectFixedPriceRequest = request as UpdateAccountantConsultancyProjectFixedPriceInvoicingRequest;
                if (consultancyProjectFixedPriceRequest != null)
                {
                    var projectFixedPrice = repository.Retrieve<ProjectFixedPrice>(consultancyProjectFixedPriceRequest.Id);
                    if (projectFixedPrice.Audit.VersionId != consultancyProjectFixedPriceRequest.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProjectFixedPrice { Id = request.Id }, projectFixedPrice);
                        return;
                    }
                    var updateInvoiceDate = StatusCodeUpdatedToNotBillable(consultancyProjectFixedPriceRequest, projectFixedPrice);
                    Mapper.DynamicMap(consultancyProjectFixedPriceRequest, projectFixedPrice);
                    if (updateInvoiceDate) projectFixedPrice.InvoicedDate = DateTime.Now;
                    repository.Save(projectFixedPrice);
                }

                var productSheetEntryRequest = request as UpdateAccountantProductSheetEntryInvoicingRequest;
                if (productSheetEntryRequest != null)
                {
                    var productSheetEntry = repository.Retrieve<ProductSheetEntry>(productSheetEntryRequest.Id);
                    if (productSheetEntry.Audit.VersionId != productSheetEntryRequest.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new ProductSheetEntry { Id = request.Id }, productSheetEntry);
                        return;
                    }
                    var updateInvoiceDate = StatusCodeUpdatedToNotBillable(productSheetEntryRequest, productSheetEntry);
                    Mapper.DynamicMap(productSheetEntryRequest, productSheetEntry);
                    if (updateInvoiceDate) productSheetEntry.InvoicedDate = DateTime.Now;
                    repository.Save(productSheetEntry);
                }

                var timesheetEntryRequest = request as UpdateAccountantTimesheetEntryInvoicingRequest;
                if (timesheetEntryRequest != null)
                {
                    var timesheetEntry = repository.Retrieve<TimesheetEntry>(timesheetEntryRequest.Id);
                    if (timesheetEntry.Audit.VersionId != timesheetEntryRequest.AuditVersionid)
                    {
                        ValidationContainer.RegisterVersionMismatchEntry(new TimesheetEntry { Id = request.Id }, timesheetEntry);
                        return;
                    }
                    var updateInvoiceDate = StatusCodeUpdatedToNotBillable(timesheetEntryRequest, timesheetEntry);
                    Mapper.DynamicMap(timesheetEntryRequest, timesheetEntry);
                    if (updateInvoiceDate) timesheetEntry.InvoicedDate = DateTime.Now;
                    repository.Save(timesheetEntry);
                }
            });
        }

        public void CreateNewProjectDnaCommercialTranslations(CreateNewProjectDnaCommercialTranslationsRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                foreach (var languageId in request.LanguageIds)
                {
                    var projectDnaCommercialTranslation = repository.Prepare<ProjectDnaCommercialTranslation>();

                    projectDnaCommercialTranslation.ProjectDnaId = request.ProjectDnaId;
                    projectDnaCommercialTranslation.LanguageId = languageId;

                    repository.Save(projectDnaCommercialTranslation);
                }
            });
        }

        public Guid CreateNewWonProposal(CreateNewWonProposalRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var crmQueryService = Container.Resolve<ICustomerRelationshipManagementQueryService>();
                var sQueryService = Container.Resolve<ISecurityQueryService>();

                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var proposal = repository.PrepareProposal();

                Mapper.DynamicMap(request, proposal);
                proposal.StatusCode = (int)ProposalStatusType.Won;

                if (request.ContactId != null)
                {
                    var contact = crmQueryService.RetrieveContactDetailInformation(request.ContactId.Value);
                    var user = sQueryService.RetrieveUserByCrmAssociateId(contact.AssociateId);
                    proposal.BusinessDeveloperId = user.Id;
                }        

                repository.Save(proposal);

                return proposal.Id;
            });
        }

        public void EnsureProjectRevenueDistributions(Guid projectId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();
                var queryService = Container.Resolve<IProjectManagementQueryService>();

                var project = queryService.RetrieveProjectWithCrmProjects(projectId);

                var crmProjects = project.CrmProjects;
                var projectRevenueDistributions = project.ProjectRevenueDistributions;

                var missingRevenueDistributionIds = crmProjects.Select(crmProject => crmProject.Id).Except(projectRevenueDistributions.Select(prd => prd.CrmProjectId));
                var excessRevenueDistributionIds = projectRevenueDistributions.Select(prd => prd.CrmProjectId).Except(crmProjects.Select(crmProject => crmProject.Id));

                foreach (var missingRevenueDistribution in missingRevenueDistributionIds)
                    repository.CreateProjectRenevueDistribution(projectId, missingRevenueDistribution);

                foreach (var excessRevenueDistributionId in excessRevenueDistributionIds)
                    repository.DeleteProjectRevenueDistribution(projectId, excessRevenueDistributionId);
            });
        }

        public void UpdateProjectRevenueDistributions(List<UpdateProjectRevenueDistributionRequest> requests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var distributions = new List<ProjectRevenueDistribution>();
                foreach (var request in requests)
                {
                    var distribution = repository.Retrieve<ProjectRevenueDistribution>(request.Id);

                    if (distribution == null)
                        continue;

                    Mapper.DynamicMap(request, distribution);

                    distributions.Add(distribution);
                }

                repository.SaveList(distributions);
            });
        }

        public void UpdateProjectRole(UpdateProjectRoleRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var projectRole = repository.Retrieve<ProjectRole>(request.Id);

                Mapper.DynamicMap(request, projectRole);

                repository.Save(projectRole);

                foreach (var translationRequest in request.ProjectRoleTranslations)
                {
                    var projectRoleTranslation = repository.Retrieve<ProjectRoleTranslation>(translationRequest.Id);

                    Mapper.DynamicMap(translationRequest, projectRoleTranslation);

                    repository.Save(projectRoleTranslation);
                }
            });
        }

        public Guid CreateCulturalFitCandidateRequest(CreateCulturalFitCandidateRequestRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IProjectManagementCommandRepository>();

                var queryService = Container.Resolve<IProjectManagementQueryService>();
                var projectCandidate = queryService.RetrieveProjectCandidateDetail(request.ProjectCandidateId);
                var deadline = projectCandidate.ProjectCandidateDetail.AssessmentStartDate;
                var contactId = projectCandidate.Project.ContactId;
                var id = repository.CreateTheoremListRequestCandidate(contactId, projectCandidate.ProjectId, projectCandidate.CandidateId, deadline.GetValueOrDefault());

                return id;
            });
        }

        private void ValidateStatusUpdate(ProjectStatusCodeType previousStatus, Project updatedProject)
        {
            var repository = Container.Resolve<IProjectManagementCommandRepository>();

            var updateStatus = (ProjectStatusCodeType)updatedProject.StatusCode;
            switch (updateStatus)
            {
                case ProjectStatusCodeType.Ready:
                case ProjectStatusCodeType.Started:
                case ProjectStatusCodeType.Finished:
                    if (!repository.ListProject2CrmProject(updatedProject.Id).Any())
                        ValidationContainer.RegisterEntityValidationFaultEntry(string.Format("Unable to change status from '{0}' to '{1}' because there are no linked CRM projects.", previousStatus, updateStatus));
                    break;
            }
        }

        private void Validate(Project project)
        {
            ValidationContainer.ValidateObject(project);

            if (project.ProjectManagerId == Guid.Empty)
                ValidationContainer.RegisterEntityValidationFaultEntry("No project manager specified", "ProjectManagerId");

            //TODO: verify that an AssessmentDevelopmentProject project with a status !Draft has one ProjectCategoryDetail that is Main for that projecttype
            //if (project is AssessmentDevelopmentProject && project.ProjectCategoryDetailId == null && ((ProjectStatusCodeType)project.StatusCode) != ProjectStatusCodeType.Draft)
            //    ValidationContext.RegisterEntityValidationFaultEntry(string.Format("Unable to save this project with no main category when status isn't '{0}.'", ProjectStatusCodeType.Draft));
        }

        private bool StatusCodeUpdatedToNotBillable(UpdateInvoicingBaseRequest projectCandidateInvoicingRequest, IInvoiceInfo entry)
        {
            return (projectCandidateInvoicingRequest.InvoiceStatusCode == (int) InvoiceStatusType.NotBillable && projectCandidateInvoicingRequest.InvoiceStatusCode != entry.InvoiceStatusCode);
        }       
    }
}
