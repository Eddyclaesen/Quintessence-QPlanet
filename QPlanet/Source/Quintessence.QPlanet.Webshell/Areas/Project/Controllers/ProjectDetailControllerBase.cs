using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Practices.ObjectBuilder2;
using Quintessence.Infrastructure.Validation;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail;
using Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment;
using Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectDetailControllerBase;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Rep;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Controllers
{
    public abstract class ProjectDetailControllerBase : ProjectController
    {
        public abstract ActionResult Edit(Guid id);

        [HttpPost]
        public ActionResult EditContactDetail(EditProjectModelBase model)
        {
            using (DurationLog.Create())
            {
                var updateContactDetailRequest = Mapper.Map<UpdateContactDetailRequest>(model.ContactDetail);

                try
                {
                    this.InvokeService<ICustomerRelationshipManagementCommandService>(
                        service => service.UpdateContactDetailModel(updateContactDetailRequest));
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("Edit", new { id = model.Id });
            }
        }

        public virtual ActionResult EditProjectCandidates(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectView = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(id));

                    if (projectView == null)
                        throw new ObjectNotFoundException(string.Format("Project with id '{0}' was not found.", id));

                    var model = Mapper.Map<EditProjectCandidatesModel>(projectView);

                    switch (projectView.ProjectType.Code)
                    {
                        case "ACDC":
                            var assessmentDevelopmentProjectView = projectView as AssessmentDevelopmentProjectView;

                            if (assessmentDevelopmentProjectView == null)
                                throw new InvalidCastException(string.Format("Project was of type {0}. Expected type was 'ACDC'", projectView.ProjectType.Code));

                            if (assessmentDevelopmentProjectView.MainProjectCategoryDetail != null)
                            {
                                model.ProjectTypeCategoryId = assessmentDevelopmentProjectView.MainProjectCategoryDetail.ProjectTypeCategoryId;
                                model.ProjectTypeCategoryName = assessmentDevelopmentProjectView.MainProjectCategoryDetail.ProjectTypeCategory.Name;
                            }


                            break;

                        default:
                            return new HttpNotFoundResult();
                    }

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult ProjectCandidates(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectCandidates = this.InvokeService<IProjectManagementQueryService, List<ProjectCandidateView>>(service => service.ListProjectCandidates(id));
                    var project = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(id));

                    //var listProjectCandidatesResponse = this.InvokeService<IProjectManagementQueryService, ListProjectCandidatesResponse>(service => service.ListProjectCandidateDetailsByProjectId(id));

                    projectCandidates.Sort((pc1, pc2) => string.Compare(pc1.CandidateFullName, pc2.CandidateFullName, StringComparison.InvariantCulture));
                    var model = new ProjectCandidatesActionModel
                        {
                            Candidates = projectCandidates,
                            Project = (AssessmentDevelopmentProjectView)project,
                            //Languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages()),
                            //ReportStatuses = this.InvokeService<IProjectManagementQueryService, List<ReportStatusView>>(service => service.ListReportStatuses()),
                            //CustomerAssistants = this.InvokeService<ISecurityQueryService, List<UserView>>(service => service.ListCustomerAssistants()).OrderBy(ca => ca.FullName).ToList(),
                            //ProjectCategoryDetails = listProjectCandidatesResponse.Project.ProjectCategoryDetails.Select(Mapper.DynamicMap<EditProjectCategoryDetailModel>).OrderByDescending(pcd => pcd.ProjectTypeCategoryIsMain).ToList(),
                            //AssessmentRooms = this.InvokeService<IInfrastructureQueryService, List<AssessmentRoomView>>(service => service.ListAssessmentRooms(new ListAssessmentRoomsRequest())).OrderBy(r => r.Id).ToList(),
                            //ReportDefinitions = this.InvokeService<IReportManagementQueryService, List<ReportDefinitionView>>(service => service.ListReportDefinitions(new ListReportDefinitionsRequest { Code = "CANDIDATE" })).Where(rd => rd.IsActive).ToList(),
                        };

                    //var mainProjectCategoryDetail = project.ProjectCategoryDetails.FirstOrDefault(pcd => pcd.ProjectTypeCategory.IsMain);

                    //if (mainProjectCategoryDetail != null)
                    //{
                    //    if (mainProjectCategoryDetail is ProjectCategoryAcDetailView)
                    //        model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryAcDetailModel>(mainProjectCategoryDetail);
                    //    if (mainProjectCategoryDetail is ProjectCategoryCaDetailView)
                    //        model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryCaDetailModel>(mainProjectCategoryDetail);
                    //    if (mainProjectCategoryDetail is ProjectCategoryCuDetailView)
                    //        model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryCuDetailModel>(mainProjectCategoryDetail);
                    //    if (mainProjectCategoryDetail is ProjectCategoryDcDetailView)
                    //        model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryDcDetailModel>(mainProjectCategoryDetail);
                    //    if (mainProjectCategoryDetail is ProjectCategoryEaDetailView)
                    //        model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryEaDetailModel>(mainProjectCategoryDetail);
                    //    if (mainProjectCategoryDetail is ProjectCategoryFaDetailView)
                    //        model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryFaDetailModel>(mainProjectCategoryDetail);
                    //    if (mainProjectCategoryDetail is ProjectCategoryFdDetailView)
                    //        model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryFdDetailModel>(mainProjectCategoryDetail);
                    //    if (mainProjectCategoryDetail is ProjectCategoryPsDetailView)
                    //        model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryPsDetailModel>(mainProjectCategoryDetail);
                    //}

                    //var offices = this.InvokeService<IInfrastructureQueryService, List<OfficeView>>(service => service.ListOffices());

                    model.DeletedAppointments = model.Candidates.Where(c => c.ProjectCandidateDetail.IsSuperofficeAppointmentDeleted).ToList();

                    //model.Candidates
                    //    .SelectMany(c => c.ProjectCandidateCategoryDetailTypes)
                    //    .OfType<EditProjectCandidateCategoryDetailType1Model>()
                    //    .ForEach(c => c.Offices.AddRange(offices
                    //        .Select(o => new SelectListItem
                    //            {
                    //                Selected = c.OfficeId == o.Id,
                    //                Text = o.FullName,
                    //                Value = o.Id.ToString(CultureInfo.InvariantCulture)
                    //            })));

                    //model.Candidates.ForEach(c => c.ProjectCandidateCategoryDetailTypes.Sort((t1, t2) => t1.ProjectCategoryDetailTypeName.CompareTo(t2.ProjectCategoryDetailTypeName)));

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult ProjectCandidates(ProjectCandidatesActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (model.Candidates != null && model.Candidates.Count > 0)
                    {
                        //Update candidates
                        var candidates = model.Candidates.Where(c => !c.ProjectCandidateDetail.IsSuperofficeAppointmentDeleted);
                        var candidateRequests =
                            candidates.Select(Mapper.DynamicMap<UpdateProjectCandidateDetailRequest>).ToList();
                        this.InvokeService<IProjectManagementCommandService>(
                            service => service.UpdateProjectCandidatesDetails(candidateRequests));

                        //Update detail types
                        var detailTypes =
                            model.Candidates.Where(c => c.ProjectCandidateCategoryDetailTypes != null).SelectMany(
                                c => c.ProjectCandidateCategoryDetailTypes);
                        var detailTypeRequests = detailTypes.Select(Mapper.Map<UpdateProjectCandidateCategoryDetailTypeRequest>).ToList();

                        if (detailTypeRequests.Count > 0)
                        {
                            this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCandidateCategoryDetailTypes(detailTypeRequests));
                        }
                    }

                    //Update cancelled candidates
                    var cancelledCandidates = model.DeletedAppointments;
                    if (cancelledCandidates != null)
                    {
                        var cancelledRequests =
                        cancelledCandidates.Select(Mapper.DynamicMap<UpdateCancelledProjectCandidateRequest>).ToList();
                        this.InvokeService<IProjectManagementCommandService>(
                            service => service.UpdateCancelledProjectCandidates(cancelledRequests));
                    }

                    //Update unit prices
                    if (model.ProjectCategoryDetails != null && model.ProjectCategoryDetails.Count > 0)
                    {
                        var unitPriceRequests = model.ProjectCategoryDetails.Select(Mapper.DynamicMap<UpdateUnitPriceRequest>).ToList();
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCategoryDetailUnitPrices(unitPriceRequests));
                    }

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult CreateCulturalFitCandidateRequest(CreateCulturalFitCandidateRequestModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectCandidate = this.InvokeService<IProjectManagementQueryService, ProjectCandidateView>(service => service.RetrieveProjectCandidate(model.ProjectCandidateId));
                    var theoremListRequest = this.InvokeService<IProjectManagementQueryService, TheoremListRequestView>(service => service.RetrieveTheoremListRequestByProjectAndCandidate(projectCandidate.ProjectId, projectCandidate.CandidateId));

                    //No cultural fit for this project and this candidate => create one.
                    if (theoremListRequest == null)
                    {
                        var request = Mapper.DynamicMap<CreateCulturalFitCandidateRequestRequest>(model);
                        this.InvokeService<IProjectManagementCommandService>(service => service.CreateCulturalFitCandidateRequest(request));
                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }

                    return Json(new {message = "This candidate already has a cultural fit for this project."},
                                JsonRequestBehavior.AllowGet);

                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public virtual ActionResult SearchProjectCandidates()
        {
            return View(new SearchProjectCandidatesModel());
        }

        public virtual ActionResult ValidateProject(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementQueryService>(service => service.ValidateProject(id));
                    return Json(new[] { new { id = 0, message = "ok" } }, JsonRequestBehavior.AllowGet);
                }
                catch (FaultException<ValidationContainer> exception)
                {
                    var validationContext = exception.Detail;

                    return Json(validationContext.FaultDetail.FaultEntries.OfType<ValidationFaultEntry>().Select(e => new { code = e.Code, message = e.Message }), JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);

                    return Json(new[] { new { id = 0, message = exception.Message } }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public virtual ActionResult CheckUnregisteredCandidatesForProject(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ICustomerRelationshipManagementQueryService>(service => service.CheckUnregisteredCandidatesForProject(id));
                    return Json(new[] { new { id = 0, message = "ok" } }, JsonRequestBehavior.AllowGet);
                }
                catch (FaultException<ValidationContainer> exception)
                {
                    var validationContext = exception.Detail;

                    return Json(validationContext.FaultDetail.FaultEntries.OfType<ValidationFaultEntry>().Select(e => new { code = e.Code, message = e.Message }), JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);

                    return Json(new[] { new { id = 0, message = exception.Message } }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult ProjectPriceIndex(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var indices = this.InvokeService<IProjectManagementQueryService, List<ProjectPriceIndexView>>(service => service.ListProjectPriceIndices(id));
                    return PartialView(indices);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult AddProjectPriceIndex(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new CreateNewProjectPriceIndexModel();
                    model.ProjectId = id;
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProjectPriceIndex(CreateNewProjectPriceIndexModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewProjectPriceIndexRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.CreateNewProjectPriceIndex(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult EditProjectPriceIndex(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectPriceIndex = this.InvokeService<IProjectManagementQueryService, ProjectPriceIndexView>(service => service.RetrieveProjectPriceIndex(id));
                    var model = Mapper.DynamicMap<EditProjectPriceIndexModel>(projectPriceIndex);
                    model.StartDate = model.StartDate;
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditProjectPriceIndex(EditProjectPriceIndexModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectPriceIndexRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectPriceIndex(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult DeleteProjectPriceIndex(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteProjectPriceIndex(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult ProjectFixedPrice(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var indices = this.InvokeService<IProjectManagementQueryService, List<ProjectFixedPriceView>>(service => service.ListProjectFixedPrices(id));
                    return PartialView(indices);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult AddProjectFixedPrice(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new CreateNewProjectFixedPriceModel();
                    model.ProjectId = id;
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProjectFixedPrice(CreateNewProjectFixedPriceModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateNewProjectFixedPriceRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.CreateNewProjectFixedPrice(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult EditProjectFixedPrice(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectFixedPrice = this.InvokeService<IProjectManagementQueryService, ProjectFixedPriceView>(service => service.RetrieveProjectFixedPrice(id));
                    var model = Mapper.DynamicMap<EditProjectFixedPriceModel>(projectFixedPrice);
                    model.Deadline = model.Deadline;
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditProjectFixedPrice(EditProjectFixedPriceModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectFixedPriceRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectFixedPrice(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DeleteProjectFixedPrice(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteProjectFixedPrice(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public virtual ActionResult SubProjects(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projects = this.InvokeService<IProjectManagementQueryService, List<SubProjectView>>(service => service.ListSubProjects(id));
                    return PartialView(projects);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult EditProjectCandidateSimulationScores(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectCandidate = this.InvokeService<IProjectManagementQueryService, ProjectCandidateView>(service => service.RetrieveProjectCandidate(id));
                    var projectCategoryDetail = this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveProjectMainCategoryDetail(projectCandidate.ProjectId));

                    switch (projectCategoryDetail.ProjectTypeCategory.Code)
                    {
                        case "FD":
                        case "FA":
                            return RedirectToAction("EditProjectCandidateFocusedSimulationScores", new { id = id });

                        default:
                            return RedirectToAction("EditProjectCandidateStandardSimulationScores", new { id = id });
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);

                    return new HttpNotFoundResult("No scores possible for this type of project.");
                }
            }
        }

        public ActionResult EditProjectCandidateStandardSimulationScores(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var response = (ListProjectCandidateStandardSimulationScoresResponse)this.InvokeService<IProjectManagementQueryService, ListProjectCandidateSimulationScoresResponse>(service => service.ListProjectCandidateSimulationScores(id));

                    var model = new EditProjectCandidateStandardSimulationScoresActionModel();
                    model.ProjectCandidate = response.ProjectCandidate;

                    var projectCategoryDetail = this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveProjectMainCategoryDetail(model.ProjectCandidate.ProjectId));

                    if (projectCategoryDetail is ProjectCategoryAcDetailView
                        || projectCategoryDetail is ProjectCategoryDcDetailView
                        || projectCategoryDetail is ProjectCategoryEaDetailView
                        || projectCategoryDetail is ProjectCategoryFaDetailView
                        || projectCategoryDetail is ProjectCategoryFdDetailView
                        || projectCategoryDetail is ProjectCategoryPsDetailView
                        || projectCategoryDetail is ProjectCategorySoDetailView
                        || projectCategoryDetail is ProjectCategoryCaDetailView)
                    {
                        model.IsIndicatorScoringEnabled = ((ScoringTypeCodeType)(int)((dynamic)projectCategoryDetail).ScoringTypeCode) == ScoringTypeCodeType.WithIndicators;
                    }

                    foreach (var simulationGroup in response.ProjectCandidateIndicatorSimulationScores
                        .OrderBy(pcs => pcs.DictionaryClusterOrder)
                        .ThenBy(pcs => pcs.DictionaryCompetenceOrder)
                        .ThenBy(pcs => pcs.DictionaryLevelLevel)
                        .ThenBy(pcs => pcs.DictionaryIndicatorOrder)
                        .GroupBy(pcs => new { pcs.SimulationId, pcs.SimulationName, pcs.SimulationLevelId, pcs.SimulationLevelName, pcs.SimulationDepartmentId, pcs.SimulationDepartmentName, pcs.SimulationSetId, pcs.SimulationSetName }))
                    {
                        var simulationCombination = new StandardScoresSimulationCombinationModel
                            {
                                Id = Guid.NewGuid(),
                                SimulationId = simulationGroup.Key.SimulationId,
                                SimulationName = simulationGroup.Key.SimulationName,
                                SimulationLevelId = simulationGroup.Key.SimulationLevelId,
                                SimulationLevelName = simulationGroup.Key.SimulationLevelName,
                                SimulationDepartmentId = simulationGroup.Key.SimulationDepartmentId,
                                SimulationDepartmentName = simulationGroup.Key.SimulationDepartmentName,
                                SimulationSetId = simulationGroup.Key.SimulationSetId,
                                SimulationSetName = simulationGroup.Key.SimulationSetName
                            };
                        foreach (var competenceGroup in simulationGroup.GroupBy(pcs => new { pcs.DictionaryClusterId, pcs.DictionaryClusterName, pcs.DictionaryCompetenceId, pcs.DictionaryCompetenceName }))
                        {
                            var competenceSimulationScore = response.ProjectCandidateCompetenceSimulationScores
                                .FirstOrDefault(
                                    pccss => pccss.DictionaryClusterId == competenceGroup.Key.DictionaryClusterId
                                             && pccss.DictionaryCompetenceId == competenceGroup.Key.DictionaryCompetenceId
                                             && pccss.SimulationDepartmentId == simulationCombination.SimulationDepartmentId
                                             && pccss.SimulationId == simulationCombination.SimulationId
                                             && pccss.SimulationLevelId == simulationCombination.SimulationLevelId
                                             && pccss.SimulationSetId == simulationCombination.SimulationSetId);
                            var competence = Mapper.DynamicMap<EditProjectCandidateCompetenceSimulationScoreModel>(competenceSimulationScore);

                            foreach (var indicator in competenceGroup)
                            {
                                var editProjectCandidateIndicatorSimulationScoreModel = Mapper.DynamicMap<EditProjectCandidateIndicatorSimulationScoreModel>(indicator);

                                competence.Indicators.Add(editProjectCandidateIndicatorSimulationScoreModel);
                            }

                            simulationCombination.Competences.Add(competence);
                        }
                        model.SimulationCombinations.Add(simulationCombination);
                    }

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult EditProjectCandidateFocusedSimulationScores(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var response = (ListProjectCandidateFocusedSimulationScoresResponse)this.InvokeService<IProjectManagementQueryService, ListProjectCandidateSimulationScoresResponse>(service => service.ListProjectCandidateSimulationScores(id));

                    var model = new EditProjectCandidateFocusedSimulationScoresActionModel();
                    model.ProjectCandidate = response.ProjectCandidate;

                    var projectCategoryDetail = this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveProjectMainCategoryDetail(model.ProjectCandidate.ProjectId));

                    if (projectCategoryDetail is ProjectCategoryAcDetailView
                        || projectCategoryDetail is ProjectCategoryDcDetailView
                        || projectCategoryDetail is ProjectCategoryEaDetailView
                        || projectCategoryDetail is ProjectCategoryFaDetailView
                        || projectCategoryDetail is ProjectCategoryFdDetailView
                        || projectCategoryDetail is ProjectCategoryPsDetailView
                        || projectCategoryDetail is ProjectCategorySoDetailView
                        || projectCategoryDetail is ProjectCategoryCaDetailView)
                        {
                        model.IsIndicatorScoringEnabled = ((ScoringTypeCodeType)(int)((dynamic)projectCategoryDetail).ScoringTypeCode) == ScoringTypeCodeType.WithIndicators;
                    }

                    foreach (var simulationGroup in response.ProjectCandidateIndicatorSimulationFocusedScores
                        .OrderBy(pcs => pcs.DictionaryClusterOrder)
                        .ThenBy(pcs => pcs.DictionaryCompetenceOrder)
                        .ThenBy(pcs => pcs.DictionaryLevelLevel)
                        .ThenBy(pcs => pcs.DictionaryIndicatorOrder)
                        .GroupBy(pcs => new { pcs.SimulationId, pcs.SimulationName, pcs.SimulationLevelId, pcs.SimulationLevelName, pcs.SimulationDepartmentId, pcs.SimulationDepartmentName, pcs.SimulationSetId, pcs.SimulationSetName }))
                    {
                        var simulationCombination = new FocusedScoresSimulationCombinationModel
                        {
                            Id = Guid.NewGuid(),
                            SimulationId = simulationGroup.Key.SimulationId,
                            SimulationName = simulationGroup.Key.SimulationName,
                            SimulationLevelId = simulationGroup.Key.SimulationLevelId,
                            SimulationLevelName = simulationGroup.Key.SimulationLevelName,
                            SimulationDepartmentId = simulationGroup.Key.SimulationDepartmentId,
                            SimulationDepartmentName = simulationGroup.Key.SimulationDepartmentName,
                            SimulationSetId = simulationGroup.Key.SimulationSetId,
                            SimulationSetName = simulationGroup.Key.SimulationSetName
                        };
                        foreach (var competenceGroup in simulationGroup.GroupBy(pcs => new { pcs.DictionaryClusterId, pcs.DictionaryClusterName, pcs.DictionaryCompetenceId, pcs.DictionaryCompetenceName }))
                        {
                            var competenceSimulationScore = response.ProjectCandidateCompetenceSimulationScores
                                .FirstOrDefault(
                                    pccss => pccss.DictionaryClusterId == competenceGroup.Key.DictionaryClusterId
                                             && pccss.DictionaryCompetenceId == competenceGroup.Key.DictionaryCompetenceId
                                             && pccss.SimulationDepartmentId == simulationCombination.SimulationDepartmentId
                                             && pccss.SimulationId == simulationCombination.SimulationId
                                             && pccss.SimulationLevelId == simulationCombination.SimulationLevelId
                                             && pccss.SimulationSetId == simulationCombination.SimulationSetId);

                            if (competenceSimulationScore == null)
                                continue;

                            var competence = Mapper.DynamicMap<EditProjectCandidateCompetenceSimulationFocusedScoreModel>(competenceSimulationScore);

                            foreach (var indicator in competenceGroup)
                            {
                                var editProjectCandidateIndicatorSimulationScoreModel = Mapper.DynamicMap<EditProjectCandidateIndicatorSimulationScoreModel>(indicator);

                                if (indicator.IsStandard)
                                    competence.StandardIndicators.Add(editProjectCandidateIndicatorSimulationScoreModel);
                                else if (indicator.IsDistinctive)
                                    competence.DistinctiveIndicators.Add(editProjectCandidateIndicatorSimulationScoreModel);
                            }

                            simulationCombination.Competences.Add(competence);
                        }
                        model.SimulationCombinations.Add(simulationCombination);
                    }

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateCompetenceSimulationStandarScores(EditProjectCandidateStandardSimulationScoresActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var requests = new List<UpdateProjectCandidateCompetenceSimulationScoreRequest>();
                    foreach (var competenceSimulation in model.SimulationCombinations.SelectMany(sc => sc.Competences).Where(sc => sc.IsChanged))
                    {
                        var indicatorsToUpdate = competenceSimulation.Indicators.Where(i => i.IsChanged).ToList();
                        competenceSimulation.Indicators = indicatorsToUpdate;

                        requests.Add(Mapper.Map<UpdateProjectCandidateCompetenceSimulationScoreRequest>(competenceSimulation));
                    }

                    Task.WaitAll(requests.Select(request => Task.Run(() => this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCandidateCompetenceSimulationScore(request)))).ToArray());

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateCompetenceSimulationFocusedScores(EditProjectCandidateFocusedSimulationScoresActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    foreach (var competenceSimulation in model.SimulationCombinations.SelectMany(sc => sc.Competences).Where(sc => sc.IsChanged))
                    {
                        var distinctiveIndicatorsToUpdate =
                            competenceSimulation.DistinctiveIndicators.Where(di => di.IsChanged).ToList();
                        var standardIndicatorsToUpdate =
                            competenceSimulation.StandardIndicators.Where(di => di.IsChanged).ToList();

                        competenceSimulation.StandardIndicators = standardIndicatorsToUpdate;
                        competenceSimulation.DistinctiveIndicators = distinctiveIndicatorsToUpdate;

                        var request = Mapper.Map<UpdateProjectCandidateCompetenceSimulationScoreRequest>(competenceSimulation);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCandidateCompetenceSimulationScore(request));
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult EditProjectCandidateScores(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new EditProjectCandidateScoresActionModel();

                    var response = this.InvokeService<IProjectManagementQueryService, ListProjectCandidateScoresResponse>(service => service.ListProjectCandidateScores(id));

                    model.Project = response.Project;
                    model.ProjectCandidate = response.ProjectCandidate;
                    model.ReportTypes = response.ReportTypes;
                    model.ReportType = model.ReportTypes.FirstOrDefault(rt => rt.Id == model.Project.CandidateScoreReportTypeId);
                    model.ProjectCandidateIndicatorSimulationFocusedScores = response.ProjectCandidateIndicatorSimulationFocusedScores;
                    model.ProjectCandidateIndicatorSimulationScores = response.ProjectCandidateIndicatorSimulationScores;
                    model.ProjectCandidateCompetenceSimulationScores = response.ProjectCandidateCompetenceSimulationScores;

                    var candidateReportDefinitionId = response.Project.CandidateReportDefinitionId.GetValueOrDefault();
                    var candidateReportDefinition = this.InvokeService<IReportManagementQueryService, CandidateReportDefinitionView>(service => service.RetrieveCandidateReportDefinition(candidateReportDefinitionId));
                    model.ReportDefinition = candidateReportDefinition;
                    
                    var projectCategoryDetail = this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveProjectMainCategoryDetail(model.ProjectCandidate.ProjectId));

                    if (projectCategoryDetail is ProjectCategoryAcDetailView
                        || projectCategoryDetail is ProjectCategoryDcDetailView 
                        || projectCategoryDetail is ProjectCategoryEaDetailView
                        || projectCategoryDetail is ProjectCategoryFaDetailView
                        || projectCategoryDetail is ProjectCategoryFdDetailView
                        || projectCategoryDetail is ProjectCategoryPsDetailView
                        || projectCategoryDetail is ProjectCategorySoDetailView
                        || projectCategoryDetail is ProjectCategoryCaDetailView)
                    {
                        model.IsIndicatorScoringEnabled = ((ScoringTypeCodeType)(int)((dynamic)projectCategoryDetail).ScoringTypeCode) == ScoringTypeCodeType.WithIndicators;
                    }

                    switch (model.ReportType.Code)
                    {
                        case "CO":
                            model.Competences = ((ListProjectCandidateCompetenceScoresResponse)response)
                                .ProjectCandidateCompetenceScores
                                .OrderBy(pccs => pccs.DictionaryCompetence.DictionaryCluster.Order)
                                .ThenBy(pccs => pccs.DictionaryCompetence.Order)
                                .Select(Mapper.Map<EditProjectCandidateCompetenceScoreModel>).ToList();

                            model.Competences
                                .ForEach(competence => competence.ProjectCandidateIndicatorScores
                                    .Sort((a, b) => a.DictionaryIndicator.Order.CompareTo(b.DictionaryIndicator.Order)));
                            break;

                        case "CL":
                            model.Clusters = ((ListProjectCandidateClusterScoresResponse)response)
                                .ProjectCandidateClusterScores
                                .OrderBy(pccs => pccs.DictionaryCluster.Order)
                                .Select(Mapper.Map<EditProjectCandidateClusterScoreModel>)
                                .ToList();

                            model.Clusters
                                .SelectMany(cluster => cluster.ProjectCandidateCompetenceScores)
                                .ForEach(competence => competence.ProjectCandidateIndicatorScores
                                    .Sort((a, b) => a.DictionaryIndicator.Order.CompareTo(b.DictionaryIndicator.Order)));
                            break;

                        default:
                            return new HttpNotFoundResult();
                    }

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);

                    return new HttpNotFoundResult("No scores possible for this type of project.");
                }
            }
        }

        [HttpPost]
        public ActionResult EditProjectCandidateScores(EditProjectCandidateScoresActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (model.Competences != null)
                    {
                        var request = model.Competences.Select(Mapper.Map<UpdateProjectCandidateCompetenceScoreRequest>).ToList();
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCandidateCompetenceScores(request));
                    }

                    if (model.Clusters != null)
                    {
                        var request = model.Clusters.Select(Mapper.Map<UpdateProjectCandidateClusterScoreRequest>).ToList();
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCandidateClusterScores(request));
                    }

                    return RedirectToAction("EditProjectCandidateScores", new { id = model.ProjectCandidate.Id });
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);

                    return new HttpNotFoundResult("No scores possible for this type of project.");
                }
            }
        }

        [HttpPost]
        public ActionResult EditProjectCandidateRoiScores(Guid id, int? score)
        {
            using (DurationLog.Create())
            {
                try
                {                  
                    this.InvokeService<IProjectManagementCommandService>(service => service.SaveRoiScores(id, score));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult EditProjectCandidateResume(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = new EditProjectCandidateResumeActionModel();

                    var response = this.InvokeService<IProjectManagementQueryService, RetrieveProjectCandidateResumeResponse>(service => service.RetrieveProjectCandidateResume(id));

                    model.ProjectCandidate = response.ProjectCandidate;
                    model.ProjectCandidateResume = Mapper.Map<EditProjectCandidateResumeModel>(response.ProjectCandidateResume);
                    var orderedList = model.ProjectCandidateResume.ProjectCandidateResumeFields.OrderBy(x => x.AuditCreatedOn);
                    model.ProjectCandidateResume.ProjectCandidateResumeFields = orderedList.ToList();
                    model.Project = response.Project;

                    model.ProjectCandidateCompetenceSimulationScores = response.ProjectCandidateCompetenceSimulationScores;

                    model.ProjectCandidateCompetenceScores = response.ProjectCandidateCompetenceScores
                        .OrderBy(pccs => pccs.DictionaryCompetence.DictionaryCluster.Order)
                        .ThenBy(pccs => pccs.DictionaryCompetence.Order)
                        .ToList();

                    model.ProjectCandidateClusterScores = response.ProjectCandidateClusterScores
                        .OrderBy(pccs => pccs.DictionaryCluster.Order)
                        .ToList();

                    var candidateReportDefinitionId = response.Project.CandidateReportDefinitionId.GetValueOrDefault();
                    var candidateReportDefinition = this.InvokeService<IReportManagementQueryService, CandidateReportDefinitionView>(service => service.RetrieveCandidateReportDefinition(candidateReportDefinitionId));
                    model.ReportDefinition = candidateReportDefinition;

                    model.Advices = response.Advices;

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditProjectCandidateResume(EditProjectCandidateResumeActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.Map<UpdateProjectCandidateResumeRequest>(model.ProjectCandidateResume);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCandidateResume(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult ProjectCandidateProductScores(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new ListProductScoresRequest { ProjectCandidateId = id };

                    var response = this.InvokeService<IProjectManagementQueryService, ListProductScoresResponse>(service => service.ListProductScores(request));

                    var model = Mapper.DynamicMap<ProjectCandidateProductScoresActionModel>(response);

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult ProjectCandidateReporting(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new ListProductScoresRequest { ProjectCandidateId = id };

                    var scores = this.InvokeService<IProjectManagementQueryService, ListProductScoresResponse>(service => service.ListProductScores(request));

                    var motivationscores = true;

                    if (scores.MotivationInterview)
                    {
                        for (int i = 0; i < scores.RoiScores.Count; i++)
                        {
                            if (scores.RoiScores[i].Score == null) motivationscores = false;
                        }
                    }                    

                    var response = this.InvokeService<IProjectManagementQueryService, RetrieveProjectCandidateReportingResponse>(service => service.RetrieveProjectCandidateReporting(id));

                    var model = Mapper.DynamicMap<ProjectCandidateReportingActionModel>(response);
                    model.MotivationScores = motivationscores;

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult UnmarkDocumentAsImportant(Guid projectId, Guid uniqueId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.UnmarkDocumentAsImportant(projectId, uniqueId));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult MarkDocumentAsImportant(Guid projectId, Guid uniqueId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.MarkDocumentAsImportant(projectId, uniqueId));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }
    }
}