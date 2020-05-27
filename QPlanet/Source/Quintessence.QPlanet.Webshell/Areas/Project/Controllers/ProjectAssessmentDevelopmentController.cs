using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Practices.ObjectBuilder2;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Infrastructure.Web;
using Quintessence.QPlanet.ViewModel.Crm;
using Quintessence.QPlanet.ViewModel.Dim;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail;
using Quintessence.QPlanet.ViewModel.Sim;
using Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment;
using Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectDetailControllerBase;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Dim;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Rep;
using Quintessence.QService.QueryModel.Scm;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QueryModel.Sim;
using Quintessence.QService.QueryModel.Sof;
using System.IO;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Controllers
{
    public class ProjectAssessmentDevelopmentController : ProjectDetailControllerBase
    {
        public override ActionResult Edit(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var token = GetAuthenticationToken();

                    Session["UserId"] = token.UserId.ToString();
                    
                    var projectView = this.InvokeService<IProjectManagementQueryService, AssessmentDevelopmentProjectView>(service => service.RetrieveAssessmentDevelopmentProjectDetail(id));
                    var contactDetailView = this.InvokeService<ICustomerRelationshipManagementQueryService, ContactDetailView>(service => service.RetrieveContactDetail(projectView.ContactId));

                    var mainProjectView = this.InvokeService<IProjectManagementQueryService, MainProjectView>(service => service.RetrieveMainProject(id));
                    var projectModel = Mapper.Map<EditProjectAssessmentDevelopmentModel>(projectView);

                    if (mainProjectView != null)
                    {
                        projectModel.ParentProjectId = mainProjectView.Id;
                        projectModel.ParentProjectName = mainProjectView.Name;
                        projectModel.ParentProjectTypeName = mainProjectView.ProjectTypeName;
                    }

                    projectModel.ContactDetail = Mapper.Map<ContactDetailModel>(contactDetailView);
                    projectModel.ContactDetail.ProjectId = projectView.Id;

                    var possibleStatusses = this.InvokeService<IProjectManagementQueryService, List<ProjectStatusCodeViewType>>(service => service.ListPossibleStatusses(projectModel.StatusCode));

                    projectModel.ProjectStatusses = possibleStatusses.Select(s => new ProjectStatusTypeSelectListItemModel { Id = (int)s, Name = s.ToString() }).ToList();

                    var possibleDictionaries = this.InvokeService<IDictionaryManagementQueryService, List<AvailableDictionaryView>>(service => service.ListAvailableDictionaries(projectModel.ContactId));

                    projectModel.Dictionaries.AddRange(possibleDictionaries.Select(Mapper.Map<DictionarySelectListItemModel>));

                    var availableProjectCategories = this.InvokeService<IProjectManagementQueryService, List<ProjectTypeCategoryView>>(service => service.ListAvailableProjectCategories(projectView.ProjectTypeId));

                    //If !Draft => Main category can't be changed anymore
                    if (projectView.MainProjectCategoryDetail != null && (ProjectStatusCodeViewType)projectView.StatusCode != ProjectStatusCodeViewType.Draft)
                    {
                        projectModel.AvailableMainProjectCategories.Add(new ProjectCategorySelectListItemModel { Id = projectView.MainProjectCategoryDetail.ProjectTypeCategoryId, Name = projectView.MainProjectCategoryDetail.ProjectTypeCategory.Name });
                        projectModel.ProjectTypeCategoryId = projectView.MainProjectCategoryDetail.ProjectTypeCategoryId;
                        projectModel.ProjectTypeCategoryName = projectView.MainProjectCategoryDetail.ProjectTypeCategory.Name;
                    }
                    else
                    {
                        if (projectView.MainProjectCategoryDetail != null)
                        {
                            projectModel.ProjectTypeCategoryId = projectView.MainProjectCategoryDetail.ProjectTypeCategoryId;
                            projectModel.ProjectTypeCategoryName = projectView.MainProjectCategoryDetail.ProjectTypeCategory.Name;
                        }

                        projectModel.AvailableMainProjectCategories.Add(new ProjectCategorySelectListItemModel { Id = null, Name = string.Empty });
                        projectModel.AvailableMainProjectCategories.AddRange(availableProjectCategories.Where(pc => pc.IsMain).Select(pc => new ProjectCategorySelectListItemModel { Id = pc.Id, Name = pc.Name }));
                    }

                    var checkedProjectTypeSubcategoryIds = new List<Guid>(projectView.ProjectCategoryDetails.Where(pcd => !pcd.ProjectTypeCategory.IsMain).Select(pcd => pcd.ProjectTypeCategoryId));
                    projectModel.AvailableProjectSubCategories
                        .AddRange(availableProjectCategories
                        .Where(pc => !pc.IsMain)
                        .Select(pc => new ProjectSubCategoryCheckboxItemModel
                            {
                                Id = pc.Id,
                                Name = pc.Name,
                                IsChecked = checkedProjectTypeSubcategoryIds.Contains(pc.Id),
                                Status = ((ProjectStatusCodeViewType)projectView.StatusCode)
                            }));
                    projectModel.HasSubProjectCategoryDetails = projectView.ProjectCategoryDetails.Any(pcd => !pcd.ProjectTypeCategory.IsMain);

                    projectModel.AvailableProjectSubCategories.Sort((cat1, cat2) => String.Compare(cat1.Name, cat2.Name, StringComparison.Ordinal));

                    return View(projectModel);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public FileResult Download(string FileName, string FullPath)
        {
            //return File("~/Uploads/" + DirectoryName + "/" + FileName, System.Net.Mime.MediaTypeNames.Application.Octet);
            return File(FullPath, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
        }

        public ActionResult UploadFile(Guid id)
        {
            var filePath = Path.Combine(Server.MapPath("~/Uploads/"), id.ToString());

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            DirectoryInfo dirinfo = new DirectoryInfo(filePath);
            List<FileInfo> filelist = dirinfo.GetFiles().ToList();

            return PartialView(filelist);
        }

        [HttpPost]
        public ActionResult UploadFile(Guid id, IEnumerable<HttpPostedFileBase> files)
        {
            var filePath = Path.Combine(Server.MapPath("~/Uploads/"), id.ToString());

            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    file.SaveAs(Path.Combine(filePath, Path.GetFileName(file.FileName)));
                }
            }

            DirectoryInfo dirinfo = new DirectoryInfo(filePath);
            List<FileInfo> filelist = dirinfo.GetFiles().ToList();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult Edit(EditProjectAssessmentDevelopmentModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateProjectRequest = Mapper.Map<UpdateAssessmentDevelopmentProjectRequest>(model);

                    updateProjectRequest.SelectedProjectTypeCategoryIds.AddRange(model.AvailableProjectSubCategories.Where(apsc => apsc.IsChecked).Select(apsc => apsc.Id));

                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateAssessmentDevelopmentProject(updateProjectRequest));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult SubProjectCategoryDetails(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var project = this.InvokeService<IProjectManagementQueryService, AssessmentDevelopmentProjectView>(service => service.RetrieveAssessmentDevelopmentProjectDetail(id));

                    var model = new SubProjectCategoryDetailsActionModel();

                    if (project.MainProjectCategoryDetail != null)
                    {
                        model.ProjectTypeCategoryId = project.MainProjectCategoryDetail.ProjectTypeCategoryId;
                        model.ProjectTypeCategoryName = project.MainProjectCategoryDetail.ProjectTypeCategory.Name;
                    }

                    model.Project = project;
                    model.ProjectSubCategoryDetails = project.ProjectCategoryDetails
                        .Where(pcd => !pcd.ProjectTypeCategory.IsMain)
                        .Select(Mapper.Map<EditProjectSubCategoryDetailModelBase>)
                        .OrderBy(pcd => pcd.Name)
                        .ToList();

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult EditProjectCategoryAcDetail(Guid id)
        {
            var projectCategoryDetail = (ProjectCategoryAcDetailView)this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveMainProjectCategoryDetailDetail(id));
            var model = Mapper.Map<EditProjectCategoryAcDetailModel>(projectCategoryDetail);
            model.SimulationContexts = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextView>>(service => service.ListSimulationContexts());
            return View("EditProjectMainCategoryDetail", model);
        }

        public ActionResult EditProjectCategoryFaDetail(Guid id)
        {
            var projectCategoryDetail = (ProjectCategoryFaDetailView)this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveMainProjectCategoryDetailDetail(id));
            var model = Mapper.Map<EditProjectCategoryFaDetailModel>(projectCategoryDetail);
            model.SimulationContexts = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextView>>(service => service.ListSimulationContexts());
            var projectRoles = this.InvokeService<IProjectManagementQueryService, List<ProjectRoleView>>(service => service.ListProjectRolesForQuintessenceAndContact(model.ProjectContactId));

            model.ProjectRoles = new List<DropDownListGroupItem>();

            var projectRolesGroups = projectRoles.GroupBy(pr => pr.ContactName);
            foreach (var projectRoleGroup in projectRolesGroups)
            {
                model.ProjectRoles.Add(new DropDownListGroupItem
                    {
                        Name = projectRoleGroup.Key ?? "Quintessence",
                        Items =
                            projectRoleGroup.Select(
                                prq => new DropDownListItem { Text = prq.Name, Value = prq.Id.ToString() }).ToList()
                    });
            }

            return View("EditProjectMainCategoryDetail", model);
        }

        public ActionResult EditProjectCategoryDcDetail(Guid id)
        {
            var projectCategoryDetail = (ProjectCategoryDcDetailView)this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveMainProjectCategoryDetailDetail(id));
            var model = Mapper.Map<EditProjectCategoryDcDetailModel>(projectCategoryDetail);
            model.SimulationContexts = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextView>>(service => service.ListSimulationContexts());
            return View("EditProjectMainCategoryDetail", model);
        }

        public ActionResult EditProjectCategoryFdDetail(Guid id)
        {
            var projectCategoryDetail = (ProjectCategoryFdDetailView)this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveMainProjectCategoryDetailDetail(id));
            var model = Mapper.Map<EditProjectCategoryFdDetailModel>(projectCategoryDetail);
            model.SimulationContexts = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextView>>(service => service.ListSimulationContexts());
            var projectRoles = this.InvokeService<IProjectManagementQueryService, List<ProjectRoleView>>(service => service.ListProjectRolesForQuintessenceAndContact(model.ProjectContactId));

            model.ProjectRoles = new List<DropDownListGroupItem>();
            var projectRolesGroups = projectRoles.GroupBy(pr => pr.ContactName);
            foreach (var projectRoleGroup in projectRolesGroups)
            {
                model.ProjectRoles.Add(new DropDownListGroupItem
                {
                    Name = projectRoleGroup.Key ?? "Quintessence",
                    Items =
                        projectRoleGroup.Select(
                            prq => new DropDownListItem { Text = prq.Name, Value = prq.Id.ToString() }).ToList()
                });
            }
            return View("EditProjectMainCategoryDetail", model);
        }

        public ActionResult EditProjectCategoryEaDetail(Guid id)
        {
            var projectCategoryDetail = (ProjectCategoryEaDetailView)this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveMainProjectCategoryDetailDetail(id));
            var model = Mapper.Map<EditProjectCategoryEaDetailModel>(projectCategoryDetail);
            model.SimulationContexts = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextView>>(service => service.ListSimulationContexts());
            return View("EditProjectMainCategoryDetail", model);
        }

        public ActionResult EditProjectCategoryPsDetail(Guid id)
        {
            var projectCategoryDetail = (ProjectCategoryPsDetailView)this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveMainProjectCategoryDetailDetail(id));
            var model = Mapper.Map<EditProjectCategoryPsDetailModel>(projectCategoryDetail);
            model.SimulationContexts = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextView>>(service => service.ListSimulationContexts());
            return View("EditProjectMainCategoryDetail", model);
        }

        public ActionResult EditProjectCategorySoDetail(Guid id)
        {
            var projectCategoryDetail = (ProjectCategorySoDetailView)this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveMainProjectCategoryDetailDetail(id));
            var model = Mapper.Map<EditProjectCategorySoDetailModel>(projectCategoryDetail);
            model.SimulationContexts = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextView>>(service => service.ListSimulationContexts());
            return View("EditProjectMainCategoryDetail", model);
        }

        public ActionResult EditProjectCategoryCaDetail(Guid id)
        {
            var projectCategoryDetail = (ProjectCategoryCaDetailView)this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveMainProjectCategoryDetailDetail(id));
            var model = Mapper.Map<EditProjectCategoryCaDetailModel>(projectCategoryDetail);
            model.SimulationContexts = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextView>>(service => service.ListSimulationContexts());
            return View("EditProjectMainCategoryDetail", model);
        }

        public ActionResult EditProjectCategoryCuDetail(Guid id)
        {
            var projectCategoryDetail = (ProjectCategoryCuDetailView)this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveMainProjectCategoryDetailDetail(id));
            var model = Mapper.Map<EditProjectCategoryCuDetailModel>(projectCategoryDetail);
            model.SimulationContexts = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextView>>(service => service.ListSimulationContexts());
            return View("EditProjectMainCategoryDetail", model);
        }

        public ActionResult EditProjectCategoryCoDetail(Guid id)
        {
            var projectCategoryDetail = (ProjectCategoryCuDetailView)this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveMainProjectCategoryDetailDetail(id));
            var model = Mapper.Map<EditProjectCategoryCuDetailModel>(projectCategoryDetail);
            model.SimulationContexts = this.InvokeService<ISimulationManagementQueryService, List<SimulationContextView>>(service => service.ListSimulationContexts());
            return View("EditProjectMainCategoryDetail", model);
        }

        public ActionResult SelectedIndicators(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var token = GetAuthenticationToken();
                    var request = new ListProjectCategoryDetailDictionaryIndicatorsRequest
                        {
                            ProjectCategoryDetailId = id,
                            LanguageId = token.User.LanguageId
                        };
                    var entries = this.InvokeService<IProjectManagementQueryService, List<ProjectCategoryDetailDictionaryIndicatorView>>(service => service.ListProjectCategoryDetailDictionaryIndicators(request));
                    var model = entries
                        .OrderBy(entry => entry.DictionaryClusterOrder)
                        .ThenBy(entry => entry.DictionaryCompetenceOrder)
                        .ThenBy(entry => entry.DictionaryLevelLevel)
                        .ThenBy(entry => entry.DictionaryIndicatorOrder);

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult DictionaryIndicators(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var token = GetAuthenticationToken();
                    var request = new ListDictionaryIndicatorMatrixEntriesRequest
                        {
                            DictionaryId = id,
                            LanguageId = token.User.LanguageId
                        };
                    var selectedIndicators = this.InvokeService<IDictionaryManagementQueryService, List<DictionaryIndicatorMatrixEntryView>>(service => service.ListDictionaryIndicatorMatrixEntries(request));

                    return PartialView(selectedIndicators.Select(Mapper.DynamicMap<DictionaryIndicatorMatrixEntryModel>).ToList());
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DictionaryLevels(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var token = GetAuthenticationToken();
                    var request = new ListDictionaryIndicatorMatrixEntriesRequest
                    {
                        DictionaryId = id,
                        LanguageId = token.User.LanguageId
                    };
                    var entries = this.InvokeService<IDictionaryManagementQueryService, List<DictionaryIndicatorMatrixEntryView>>(service => service.ListDictionaryIndicatorMatrixEntries(request));

                    var model = entries
                        .Select(Mapper.DynamicMap<DictionaryIndicatorMatrixEntryModel>)
                        .OrderBy(dimem => dimem.DictionaryClusterName)
                        .ThenBy(dimem => dimem.DictionaryCompetenceName)
                        .ThenBy(dimem => dimem.DictionaryLevelName)
                        .ThenBy(dimem => dimem.DictionaryIndicatorName)
                        .ToList();

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult SelectedLevels(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var token = GetAuthenticationToken();
                    var request = new ListProjectCategoryDetailDictionaryIndicatorsRequest
                        {
                            ProjectCategoryDetailId = id,
                            LanguageId = token.User.LanguageId
                        };
                    var selectedIndicators = this.InvokeService<IProjectManagementQueryService, List<ProjectCategoryDetailDictionaryIndicatorView>>(service => service.ListProjectCategoryDetailDictionaryIndicators(request));

                    return PartialView(selectedIndicators);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddDictionaryIndicators(Guid id, List<Guid> selectedIndicatorIds)
        {
            using (DurationLog.Create())
            {
                try
                {
                    //We can't upload an enormous amount of data, so we slice the array up into little pieces.
                    //For each piece, we make an ajax-call, but the default model binder can't handle subsequent calls (since the array of items starts for instance with "[50].Id"
                    if (selectedIndicatorIds.Count > 0)
                    {
                        var request = new LinkProjectCategoryDetail2DictionaryIndicatorsRequest
                            {
                                ProjectCategoryDetailId = id,
                                DictionaryIndicatorIds = selectedIndicatorIds
                            };
                        this.InvokeService<IProjectManagementCommandService>(service => service.LinkProjectCategoryDetail2DictionaryIndicators(request));
                    }
                    return new HttpStatusCodeResult(200);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddDictionaryLevels(Guid id, FormCollection model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var dictionaryLevelIds = model[0].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(entry => new Guid(entry)).ToList();

                    if (dictionaryLevelIds.Count > 0)
                    {
                        var request = new LinkProjectCategoryDetail2DictionaryIndicatorsRequest
                            {
                                ProjectCategoryDetailId = id,
                                DictionaryLevelIds = dictionaryLevelIds
                            };
                        this.InvokeService<IProjectManagementCommandService>(service => service.LinkProjectCategoryDetail2DictionaryIndicators(request));
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

        public ActionResult RemoveDictionaryIndicator(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.UnlinkProjectCategoryDetail2DictionaryIndicator(id));
                    return new HttpStatusCodeResult(200);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult SelectedSimulations(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var selectedIndicators = this.InvokeService<IProjectManagementQueryService, List<ProjectCategoryDetailSimulationCombinationView>>(service => service.ListProjectCategoryDetailSimulationCombinations(id));
                    var model = selectedIndicators
                        .OrderBy(entry => entry.SimulationSetName)
                        .ThenBy(entry => entry.SimulationDepartmentName)
                        .ThenBy(entry => entry.SimulationLevelName)
                        .ThenBy(entry => entry.SimulationName);

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult Simulations()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var response = this.InvokeService<ISimulationManagementQueryService, ListSimulationMatrixEntriesResponse>(service => service.ListSimulationMatrixEntries(new ListSimulationMatrixEntriesRequest()));
                    var simulations = response.SimulationMatrixEntries;

                    return PartialView(simulations.Select(Mapper.DynamicMap<SimulationMatrixEntryModel>).ToList());
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult AddSimulationCombinations(Guid id, List<Guid> selectedSimulationCombinationIds)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (selectedSimulationCombinationIds.Count > 0)
                    {
                        this.InvokeService<IProjectManagementCommandService>(service => service.LinkProjectCategoryDetail2SimulationCombinations(id, selectedSimulationCombinationIds));
                    }
                    return new HttpStatusCodeResult(200);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult RemoveSimulationCombination(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.UnlinkProjectCategoryDetail2Combination(id));
                    return new HttpStatusCodeResult(200);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult CompetenceSimulations(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var token = GetAuthenticationToken();

                    var model = new IndicatorSimulationsModel();

                    model.DictionaryIndicators = this.InvokeService<IProjectManagementQueryService, List<ProjectCategoryDetailDictionaryIndicatorView>>(service => service.ListProjectCategoryDetailDictionaryIndicators(new ListProjectCategoryDetailDictionaryIndicatorsRequest(id, token.User.LanguageId)));
                    model.SimulationCombinations = this.InvokeService<IProjectManagementQueryService, List<ProjectCategoryDetailSimulationCombinationView>>(service => service.ListProjectCategoryDetailSimulationCombinations(id));
                    var indicatorSimulations = this.InvokeService<IProjectManagementQueryService, List<ProjectCategoryDetailCompetenceSimulationView>>(service => service.ListProjectCategoryDetailCompetenceSimulations(id));

                    if (model.DictionaryIndicators.Count > 0)
                    {
                        model.Matrix = new Dictionary<Guid, Dictionary<Guid, bool>>();

                        foreach (var simulationCombination in model.SimulationCombinations)
                        {
                            var dictionary = new Dictionary<Guid, bool>();

                            foreach (var group in model.DictionaryIndicators.GroupBy(di => di.DictionaryCompetenceId))
                            {
                                dictionary.Add(group.Key, indicatorSimulations.Any(insim => insim.SimulationCombinationId == simulationCombination.SimulationCombinationId && insim.DictionaryCompetenceId == group.Key));
                            }

                            model.Matrix.Add(simulationCombination.SimulationCombinationId, dictionary);
                        }
                    }

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        [HttpPost]
        public ActionResult AddSimulationCompetence(Guid projectCategoryDetailId, Guid competenceId, Guid combinationId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.LinkProjectCategoryDetail2Competence2Combination(projectCategoryDetailId, competenceId, combinationId));
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
        public ActionResult RemoveSimulationCompetence(Guid projectCategoryDetailId, Guid competenceId, Guid combinationId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.UnlinkProjectCategoryDetail2Competence2Combination(projectCategoryDetailId, competenceId, combinationId));
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
        public ActionResult RemoveDictionaryCompetence(Guid projectCategoryDetailId, Guid competenceId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new UnlinkProjectCategoryDetail2DictionaryLevelRequest
                        {
                            ProjectCategoryDetailId = projectCategoryDetailId,
                            DictionaryCompetenceId = competenceId
                        };
                    this.InvokeService<IProjectManagementCommandService>(service => service.UnlinkProjectCategoryDetail2DictionaryLevel(request));
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
        public ActionResult UnassignProjectRole(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.UnassignProjectRole(id));
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
        public ActionResult UpdateSimulationInformation(UpdateProjectCategoryDetailSimulationRemarks model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectCategoryDetailSimulationInformationRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCategoryDetailSimulationInformation(request));
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
        public ActionResult UpdateMatrixInformation(UpdateProjectCategoryDetailMatrixRemarks model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCategoryDetailMatrixRemarks(model.ProjectCategoryDetailId, model.MatrixRemarks, model.ScoringTypeCode));
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
        public ActionResult ChangeProjectRole(UpdateProjectRoleModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.AssignProjectRole(model.ProjectCategoryDetailId, model.ProjectRoleId));
                    return RedirectToAction("EditProjectCategoryDetail", "ProjectGeneral", new { id = model.ProjectId });
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult UnregisteredCandidates(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var unregisteredCandidates = this.InvokeService<ICustomerRelationshipManagementQueryService, List<CrmUnregisteredCandidateAppointmentView>>(service => service.ListCrmUnregisteredCandidateAppointments(id));
                    var model = new UnregisteredCandidatesModel
                        {
                            UnregisteredCandidates = unregisteredCandidates,
                            ProjectId = id
                        };
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
        public ActionResult AddCandidate(AddProjectCandidateModel model)
        {
            if (ModelState.IsValid)
            {
                using (DurationLog.Create())
                {
                    try
                    {

                        var listCandidatesByFullNameRequest = new ListCandidatesByFullNameRequest { FirstName = model.FirstName, LastName = model.LastName };
                        var candidates = this.InvokeService<ICandidateManagementQueryService, List<CandidateView>>(service => service.ListCandidatesByFullName(listCandidatesByFullNameRequest));
                        if (candidates.Count == 0) //Add entirely new candidate
                        {
                            var addProjectCandidateRequest = new AddProjectCandidateRequest
                                {
                                    FirstName = model.FirstName,
                                    LastName = model.LastName,
                                    Gender = model.Gender,
                                    Code = model.Code,
                                    AppointmentId = model.AppointmentId,
                                    ProjectId = model.ProjectId,
                                    CandidateId = Guid.Empty,
                                    LanguageId = model.LanguageId
                                };
                            this.InvokeService<IProjectManagementCommandService>(service => service.AddProjectCandidate(addProjectCandidateRequest));
                            return Content("Ok");
                        }

                        //Add existing candidate with crm appointment to project candidate
                        var duplicates = new DuplicateCandidatesModel
                            {
                                Candidates = candidates,
                                ProjectId = model.ProjectId,
                                AppointmentId = model.AppointmentId,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Gender = model.Gender,
                                Code = model.Code,
                                LanguageId = model.LanguageId
                            };

                        return PartialView("DuplicateCandidates", duplicates);

                    }
                    catch (Exception exception)
                    {
                        LogManager.LogError(exception);
                        return HandleError(exception);
                    }
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK); //TODO: return Content("Nok") and handle it in client-side javascript
        }

        [HttpPost]
        public ActionResult AddCandidateFromDuplicates(DuplicateCandidatesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DurationLog.Create())
                    {
                        AddProjectCandidateRequest addProjectCandidateRequest;
                        if (model.SelectedCandidateId == Guid.Empty) //Add as new candidate
                        {
                            addProjectCandidateRequest = new AddProjectCandidateRequest
                                {
                                    FirstName = model.FirstName,
                                    LastName = model.LastName,
                                    Gender = model.Gender,
                                    AppointmentId = model.AppointmentId,
                                    ProjectId = model.ProjectId,
                                    CandidateId = Guid.Empty,
                                    Code = model.Code,
                                    LanguageId = model.LanguageId
                                };

                        }
                        else //Add existing candidate to project candidate table
                        {
                            addProjectCandidateRequest = new AddProjectCandidateRequest
                            {
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Gender = model.Gender,
                                AppointmentId = model.AppointmentId,
                                ProjectId = model.ProjectId,
                                CandidateId = model.SelectedCandidateId,
                                Code = model.Code,
                                LanguageId = model.LanguageId
                            };
                        }

                        this.InvokeService<IProjectManagementCommandService>(
                            service => service.AddProjectCandidate(addProjectCandidateRequest));
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult DeleteProjectCandidate(Guid id)
        {
            try
            {
                using (DurationLog.Create())
                {
                    this.InvokeService<IProjectManagementCommandService>(
                        service => service.DeleteProjectCandidate(id));
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult ListProjectCandidateReportRecipients(Guid projectCandidateId)
        {
            try
            {
                using (DurationLog.Create())
                {
                    var reportRecipients = this.InvokeService<IProjectManagementQueryService, List<ProjectCandidateReportRecipientView>>(
                    service => service.ListProjectCandidateReportRecipientsByProjectCandidateId(projectCandidateId));

                    return PartialView(reportRecipients);
                }

            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }

        public ActionResult ListCrmEmails(int contactId)
        {
            try
            {
                using (DurationLog.Create())
                {
                    var crmEmails = this.InvokeService<ICustomerRelationshipManagementQueryService, List<CrmEmailView>>(
                    service => service.ListCrmEmailsByContactId(contactId));

                    var listCrmEmailsModel = crmEmails.Select(Mapper.DynamicMap<SelectCrmEmailModel>).ToList();

                    return PartialView(listCrmEmailsModel);
                }

            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleError(exception);
            }
        }

        [HttpPost]
        public ActionResult AddProjectCandidateReportRecipients(Guid projectCandidateId, List<SelectCrmEmailModel> model)
        {
            try
            {
                var selectedRecipients = model.Where(e => e.IsSelected).ToList();
                var reportRecipients =
                    this.InvokeService<IProjectManagementQueryService, List<ProjectCandidateReportRecipientView>>(
                        service => service.ListProjectCandidateReportRecipientsByProjectCandidateId(projectCandidateId));

                if (selectedRecipients.Count > 0)
                {
                    var requests = new List<CreateProjectCandidateReportRecipientRequest>();
                    foreach (var selectRecipient in selectedRecipients)
                    {
                        if (reportRecipients.All(rr => rr.CrmEmailId != selectRecipient.Id))
                            requests.Add(new CreateProjectCandidateReportRecipientRequest { CrmEmailId = selectRecipient.Id, ProjectCandidateId = projectCandidateId });
                    }

                    this.InvokeService<IProjectManagementCommandService>(service => service.CreateProjectCandidateReportRecipients(requests));
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        #region UpdateSubCategoryType
        [HttpPost]
        public ActionResult UpdateSubCategoryType1([Bind(Prefix = "Subcategory")]EditProjectSubCategoryDetailType1Model model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateProjectCategoryDetailTypeRequest = new UpdateProjectCategoryDetailTypeRequest
                    {
                        Id = model.Id,
                        Description = model.Description,
                        SurveyPlanningId = model.SurveyPlanningId,
                        AuditVersionid = model.AuditVersionId
                    };
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCategoryDetailType1(updateProjectCategoryDetailTypeRequest));


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
        public ActionResult UpdateSubCategoryType2([Bind(Prefix = "Subcategory")]EditProjectSubCategoryDetailType2Model model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateProjectCategoryDetailTypeRequest = new UpdateProjectCategoryDetailTypeRequest
                    {
                        Id = model.Id,
                        Description = model.Description,
                        SurveyPlanningId = model.SurveyPlanningId,
                        AuditVersionid = model.AuditVersionId
                    };
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCategoryDetailType2(updateProjectCategoryDetailTypeRequest));


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
        public ActionResult UpdateSubCategoryType3([Bind(Prefix = "Subcategory")]EditProjectSubCategoryDetailType3Model model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var updateProjectCategoryDetailTypeRequest = new UpdateProjectCategoryDetailTypeRequest
                    {
                        Id = model.Id,
                        Description = model.Description,
                        SurveyPlanningId = model.SurveyPlanningId,
                        IncludeInCandidateReport = model.IncludeInCandidateReport,
                        AuditVersionid = model.AuditVersionId
                    };
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCategoryDetailType3(updateProjectCategoryDetailTypeRequest));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }
        #endregion

        public ActionResult CustomerFeedbackReporting(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var project = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProject(id));
                    var acdcProject = (AssessmentDevelopmentProjectView)project;

                    var candidateReportDefinitions = this.InvokeService<IReportManagementQueryService, List<CandidateReportDefinitionView>>(service => service.ListCandidateReportDefinitionsForCustomer(project.ContactId));
                    var candidateScoreReportTypes = this.InvokeService<IReportManagementQueryService, List<CandidateScoreReportTypeView>>(service => service.ListCandidateScoreReportTypes());
                    var reportDefinitions = this.InvokeService<IReportManagementQueryService, List<ReportDefinitionView>>(service => service.ListReportDefinitions(new ListReportDefinitionsRequest { Code = "PROJECT" }));

                    var model = Mapper.DynamicMap<ReportingActionModel>(acdcProject);
                    model.CandidateReportDefinitions = candidateReportDefinitions.OrderBy(crd => crd.Name).ToList();
                    model.ReportTypes = candidateScoreReportTypes.OrderBy(csrt => csrt.Name).ToList();
                    foreach (CandidateScoreReportTypeView rtype in model.ReportTypes)
                    {
                        if (rtype.Name == "Cluster")
                        {
                        }
                    }
                    model.ReportDefinitions = reportDefinitions.Where(rd => rd.IsActive).OrderBy(rd => rd.Name).ToList();

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
        public ActionResult CustomerFeedbackReporting(ReportingActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectReportingRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectReporting(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult Reporting(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var project = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProject(id));
                    var acdcProject = (AssessmentDevelopmentProjectView)project;

                    var candidateReportDefinitions = this.InvokeService<IReportManagementQueryService, List<CandidateReportDefinitionView>>(service => service.ListCandidateReportDefinitionsForCustomer(project.ContactId));
                    var candidateScoreReportTypes = this.InvokeService<IReportManagementQueryService, List<CandidateScoreReportTypeView>>(service => service.ListCandidateScoreReportTypes());
                    var reportDefinitions = this.InvokeService<IReportManagementQueryService, List<ReportDefinitionView>>(service => service.ListReportDefinitions(new ListReportDefinitionsRequest { Code = "PROJECT" }));

                    var model = Mapper.DynamicMap<ReportingActionModel>(acdcProject);
                    model.CandidateReportDefinitions = candidateReportDefinitions.OrderBy(crd => crd.Name).ToList();
                    model.ReportTypes = candidateScoreReportTypes.OrderBy(csrt => csrt.Name).ToList();
                    foreach (CandidateScoreReportTypeView rtype in model.ReportTypes)
                    {
                        if (rtype.Name == "Cluster")
                        {
                        }
                    }
                    model.ReportDefinitions = reportDefinitions.Where(rd => rd.IsActive).OrderBy(rd => rd.Name).ToList();
                    
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
        public ActionResult Reporting(ReportingActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectReportingRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectReporting(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult ChangeCandidateLanguage(int languageId, Guid candidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ICandidateManagementCommandService>(service => service.ChangeCandidateLanguage(languageId, candidateId));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult DeleteProjectCandidateReportRecipient(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteProjectCandidateReportRecipient(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult CancelProjectCandidate(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = Mapper.DynamicMap<CancelProjectCandidateModel>(this.InvokeService<IProjectManagementQueryService, ProjectCandidateView>(service => service.RetrieveProjectCandidate(id)));
                    if (model.CancelledDate == DateTime.MinValue)
                        model.CancelledDate = DateTime.Now;
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
        public ActionResult CancelProjectCandidate(CancelProjectCandidateModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CancelProjectCandidateRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.CancelProjectCandidate(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult ListProjectTypeCategoryUnitPrices(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var model = this.InvokeService<IProjectManagementQueryService, List<ProjectTypeCategoryUnitPriceView>>(service => service.ListProjectTypeCategoryUnitPricesByCategory(id));
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult InvoiceOverview(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var project = this.InvokeService<IProjectManagementQueryService, AssessmentDevelopmentProjectView>(service => service.RetrieveAssessmentDevelopmentProjectDetail(id));
                    var model = new InvoiceOverviewActionModel
                    {
                        Project = project,
                    };

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
        public ActionResult SaveInvoices(ListInvoicesActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var requests = new List<UpdateBaseInvoicingRequest>();

                    //"Planned" list
                    if (model.PlannedList != null && model.PlannedList.Count > 0)
                    {
                        requests.AddRange(model.PlannedList.Where(tvl => tvl is EditProjectCandidateInvoiceModel).Select(Mapper.DynamicMap<UpdateProjectCandidateInvoicingRequest>));
                        requests.AddRange(model.PlannedList.Where(tvl => tvl is EditProjectCandidateCategoryInvoiceModel).Select(Mapper.DynamicMap<UpdateProjectCandidateCategoryInvoicingRequest>));
                        requests.AddRange(model.PlannedList.Where(tvl => tvl is EditProductInvoiceModel).Select(Mapper.DynamicMap<UpdateProductInvoicingRequest>));
                    }

                    //"To verify" list
                    if (model.ToVerifyList != null && model.ToVerifyList.Count > 0)
                    {
                        requests.AddRange(model.ToVerifyList.Where(tvl => tvl is EditProjectCandidateInvoiceModel).Select(Mapper.DynamicMap<UpdateProjectCandidateInvoicingRequest>));
                        requests.AddRange(model.ToVerifyList.Where(tvl => tvl is EditProjectCandidateCategoryInvoiceModel).Select(Mapper.DynamicMap<UpdateProjectCandidateCategoryInvoicingRequest>));
                        requests.AddRange(model.ToVerifyList.Where(tvl => tvl is EditProductInvoiceModel).Select(Mapper.DynamicMap<UpdateProductInvoicingRequest>));
                    }

                    //"Ready for invoicing" list
                    if (model.ReadyForInvoicingList != null && model.ReadyForInvoicingList.Count > 0)
                    {
                        requests.AddRange(model.ReadyForInvoicingList.Where(rfil => rfil is EditProjectCandidateInvoiceModel).Select(Mapper.DynamicMap<UpdateProjectCandidateInvoicingRequest>));
                        requests.AddRange(model.ReadyForInvoicingList.Where(rfil => rfil is EditProjectCandidateCategoryInvoiceModel).Select(Mapper.DynamicMap<UpdateProjectCandidateCategoryInvoicingRequest>));
                        requests.AddRange(model.ReadyForInvoicingList.Where(rfil => rfil is EditProductInvoiceModel).Select(Mapper.DynamicMap<UpdateProductInvoicingRequest>));
                    }

                    if (requests.Count > 0)
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateInvoicing(requests));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult ListInvoices(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var response = this.InvokeService<IProjectManagementQueryService, InvoiceOverviewResponse>(service => service.ListInvoiceOverview(id));

                    var model = new ListInvoicesActionModel
                    {
                        Project = response.Project,
                        ToVerifyList = new List<BaseEditInvoiceModel>(),
                        ReadyForInvoicingList = new List<BaseEditInvoiceModel>(),
                        InvoicedList = new List<BaseEditInvoiceModel>(),
                        PlannedList = new List<BaseEditInvoiceModel>()
                    };
                    if (response.Project.MainProjectCategoryDetail != null)
                    {
                        foreach (var projectCandidate in response.ProjectCandidates)
                        {
                            //Main category invoicing
                            var mainCategoryInvoiceModel = Mapper.DynamicMap<EditProjectCandidateInvoiceModel>(projectCandidate);
                            mainCategoryInvoiceModel.ProductName = response.Project.MainProjectCategoryDetail.ProjectTypeCategory.Name;
                            if (projectCandidate.InvoiceStatusCode == (int)InvoiceStatusType.ToVerify)
                            {
                                model.ToVerifyList.Add(mainCategoryInvoiceModel);
                            }
                            else if (projectCandidate.InvoiceStatusCode == (int)InvoiceStatusType.ReadyForInvoicing)
                            {
                                model.ReadyForInvoicingList.Add(mainCategoryInvoiceModel);
                            }
                            else if (projectCandidate.InvoiceStatusCode == (int)InvoiceStatusType.Invoiced)
                            {
                                model.InvoicedList.Add(mainCategoryInvoiceModel);
                            }
                            else if (projectCandidate.InvoiceStatusCode == (int)InvoiceStatusType.Planned)
                            {
                                model.PlannedList.Add(mainCategoryInvoiceModel);
                            }

                            //Sub categories invoicing
                            HandleProjectCandidateCategoryInvoicing(projectCandidate, model);

                        }

                    }

                    foreach (var projectProduct in response.ProjectProducts)
                    {
                        var projectProductInvoiceModel = Mapper.DynamicMap<EditProductInvoiceModel>(projectProduct);
                        projectProductInvoiceModel.ProductName = projectProduct.ProductTypeName;

                        switch (projectProduct.InvoiceStatusCode)
                        {
                            case (int)InvoiceStatusType.ToVerify:
                                model.ToVerifyList.Add(projectProductInvoiceModel);
                                break;
                            case (int)InvoiceStatusType.ReadyForInvoicing:
                                model.ReadyForInvoicingList.Add(projectProductInvoiceModel);
                                break;
                            case (int)InvoiceStatusType.Invoiced:
                                model.InvoicedList.Add(projectProductInvoiceModel);
                                break;
                            case (int)InvoiceStatusType.Planned:
                                model.PlannedList.Add(projectProductInvoiceModel);
                                break;
                        }
                    }

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        private static void HandleProjectCandidateCategoryInvoicing(ProjectCandidateView projectCandidate,
                                                                    ListInvoicesActionModel model)
        {
            foreach (var projectCandidateCategoryDetailType in projectCandidate.ProjectCandidateCategoryDetailTypes)
            {
                var projectCandidateCategoryDetailType1 =
                    projectCandidateCategoryDetailType as ProjectCandidateCategoryDetailType1View;
                if (projectCandidateCategoryDetailType1 != null) //Handle type 1
                {
                    var projectCandidateCategoryInvoiceModel =
                        Mapper.DynamicMap<EditProjectCandidateCategoryInvoiceModel>(
                            projectCandidateCategoryDetailType1);
                    projectCandidateCategoryInvoiceModel.CategoryDetailType = 1;
                    projectCandidateCategoryInvoiceModel.ProductName =
                        projectCandidateCategoryDetailType1.ProjectCategoryDetailTypeName;
                    if (projectCandidateCategoryDetailType1.InvoiceStatusCode == (int)InvoiceStatusType.ToVerify)
                        model.ToVerifyList.Add(projectCandidateCategoryInvoiceModel);
                    else if (projectCandidateCategoryDetailType1.InvoiceStatusCode == (int)InvoiceStatusType.ReadyForInvoicing)
                        model.ReadyForInvoicingList.Add(projectCandidateCategoryInvoiceModel);
                    else if (projectCandidateCategoryDetailType1.InvoiceStatusCode == (int)InvoiceStatusType.Invoiced)
                        model.InvoicedList.Add(projectCandidateCategoryInvoiceModel);
                    else if (projectCandidateCategoryDetailType1.InvoiceStatusCode == (int)InvoiceStatusType.Planned)
                        model.PlannedList.Add(projectCandidateCategoryInvoiceModel);

                    continue;
                }

                var projectCandidateCategoryDetailType2 =
                    projectCandidateCategoryDetailType as ProjectCandidateCategoryDetailType2View;
                if (projectCandidateCategoryDetailType2 != null) //Handle type 2
                {
                    var projectCandidateCategoryInvoiceModel =
                        Mapper.DynamicMap<EditProjectCandidateCategoryInvoiceModel>(
                            projectCandidateCategoryDetailType2);
                    projectCandidateCategoryInvoiceModel.CategoryDetailType = 2;
                    projectCandidateCategoryInvoiceModel.ProductName =
                        projectCandidateCategoryDetailType2.ProjectCategoryDetailTypeName;
                    if (projectCandidateCategoryDetailType2.InvoiceStatusCode == (int)InvoiceStatusType.ToVerify)
                        model.ToVerifyList.Add(projectCandidateCategoryInvoiceModel);
                    else if (projectCandidateCategoryDetailType2.InvoiceStatusCode == (int)InvoiceStatusType.ReadyForInvoicing)
                        model.ReadyForInvoicingList.Add(projectCandidateCategoryInvoiceModel);
                    else if (projectCandidateCategoryDetailType2.InvoiceStatusCode == (int)InvoiceStatusType.Invoiced)
                        model.InvoicedList.Add(projectCandidateCategoryInvoiceModel);
                    else if (projectCandidateCategoryDetailType2.InvoiceStatusCode == (int)InvoiceStatusType.Planned)
                        model.PlannedList.Add(projectCandidateCategoryInvoiceModel);

                    continue;
                }

                var projectCandidateCategoryDetailType3 =
                    projectCandidateCategoryDetailType as ProjectCandidateCategoryDetailType3View;
                if (projectCandidateCategoryDetailType3 != null) //Handle type 3
                {
                    var projectCandidateCategoryInvoiceModel =
                        Mapper.DynamicMap<EditProjectCandidateCategoryInvoiceModel>(
                            projectCandidateCategoryDetailType3);
                    projectCandidateCategoryInvoiceModel.CategoryDetailType = 3;
                    projectCandidateCategoryInvoiceModel.ProductName =
                        projectCandidateCategoryDetailType3.ProjectCategoryDetailTypeName;
                    if (projectCandidateCategoryDetailType3.InvoiceStatusCode == (int)InvoiceStatusType.ToVerify)
                        model.ToVerifyList.Add(projectCandidateCategoryInvoiceModel);
                    else if (projectCandidateCategoryDetailType3.InvoiceStatusCode == (int)InvoiceStatusType.ReadyForInvoicing)
                        model.ReadyForInvoicingList.Add(projectCandidateCategoryInvoiceModel);
                    else if (projectCandidateCategoryDetailType3.InvoiceStatusCode == (int)InvoiceStatusType.Invoiced)
                        model.InvoicedList.Add(projectCandidateCategoryInvoiceModel);
                    else if (projectCandidateCategoryDetailType3.InvoiceStatusCode == (int)InvoiceStatusType.Planned)
                        model.PlannedList.Add(projectCandidateCategoryInvoiceModel);

                }
            }
        }

        public ActionResult ListProjectProducts(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectProducts = this.InvokeService<IProjectManagementQueryService, List<ProjectProductView>>(service => service.ListProjectProducts(id));

                    return PartialView(projectProducts);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }


        public ActionResult CreateProjectProduct(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var productTypes = this.InvokeService<ISupplyChainManagementQueryService, List<ProductTypeView>>(service => service.ListProductTypes());
                    var model = new CreateProjectProductActionModel();
                    model.ProductTypes = productTypes.OrderBy(pt => pt.Name).ToList();
                    model.ProjectId = id;

                    var firstProductType = model.ProductTypes.FirstOrDefault();

                    if (firstProductType != null)
                    {
                        model.ProductTypeId = firstProductType.Id;
                        model.InvoiceAmount = firstProductType.UnitPrice;
                    }

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        [HttpPost]
        public ActionResult CreateProjectProduct(CreateProjectProductActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateProjectProductRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.CreateProjectProduct(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult EditProjectProduct(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectProduct = this.InvokeService<IProjectManagementQueryService, ProjectProductView>(service => service.RetrieveProjectProduct(id));

                    var model = Mapper.DynamicMap<EditProjectProductModel>(projectProduct);

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
        public ActionResult EditProjectProduct(EditProjectProductModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectProductRequest>(model);

                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectProduct(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult DeleteProjectProduct(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteProjectProduct(id));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult SendCandidateInvitationMail(Guid id, int languageId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var qCandidateUrl = ConfigurationManager.AppSettings["QCandidateUrl"];
                    var response = this.InvokeService<IInfrastructureQueryService, CreateProjectCandidateInvitationMailResponse>(service => service.CreateProjectCandidateInvitationMail(id, qCandidateUrl));
                    return Json((new { to = response.To, subject = response.Subject, body = response.Body, bcc = response.Bcc }), JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult SendCulturalFitInvitationMail(Guid id, int languageId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var response = this.InvokeService<IInfrastructureQueryService, CreateCulturalFitInvitationMailResponse>(service => service.CreateCulturalFitInvitationMail(id, languageId));
                    return Json((new { to = response.To, subject = response.Subject, body = response.Body, bcc = response.Bcc }), JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult CustomerFeedback(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var project = this.InvokeService<IProjectManagementQueryService, AssessmentDevelopmentProjectView>(service => service.RetrieveAssessmentDevelopmentProjectDetail(id));
                    var model = Mapper.DynamicMap<EditCustomerFeedbackModel>(project);
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult CustomerFeedback(EditCustomerFeedbackModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var request = Mapper.DynamicMap<UpdateCustomerFeedbackRequest>(model);
                        this.InvokeService<IProjectManagementCommandService>(service => service.UpdateCustomerFeedback(request));
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

        public ActionResult ProjectReportRecipients(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectReportRecipients = this.InvokeService<IProjectManagementQueryService, List<ProjectReportRecipientView>>(service => service.ListProjectReportRecipientsByProjectId(id));
                    return PartialView(projectReportRecipients);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        /// <summary>
        /// Action for project unit prices.
        /// </summary>
        /// <param name="id">The project id.</param>
        /// <returns></returns>
        public ActionResult ProjectUnitPrices(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectCategoryDetails = this.InvokeService<IProjectManagementQueryService, List<ProjectCategoryDetailView>>(service => service.ListProjectCategoryDetails(id));

                    var model = new ProjectUnitPricesActionModel
                        {
                            ProjectCategoryDetails =
                                projectCategoryDetails.Select(Mapper.DynamicMap<EditProjectCategoryDetailModel>)
                                                      .OrderByDescending(pcd => pcd.ProjectTypeCategoryIsMain)
                                                      .ToList()
                        };
                    model.ProjectId = id;

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        /// <summary>
        /// HttpPost Action for project unit prices.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProjectUnitPrices(ProjectUnitPricesActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var unitPriceRequests = model.ProjectCategoryDetails.Select(Mapper.DynamicMap<UpdateUnitPriceRequest>).ToList();
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCategoryDetailUnitPrices(unitPriceRequests));
                    var request = new UpdateProjectUnitPricesRequest
                        {
                            ProjectId = model.ProjectId,
                            UnitPriceRequests = unitPriceRequests
                        };
                    this.InvokeService<IProjectManagementCommandService>(
                        service => service.UpdateProjectUnitPrices(request));

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
        public ActionResult AddProjectReportRecipients(Guid id, List<SelectCrmEmailModel> model)
        {
            try
            {
                var selectedRecipients = model.Where(e => e.IsSelected).ToList();
                var reportRecipients =
                    this.InvokeService<IProjectManagementQueryService, List<ProjectReportRecipientView>>(
                        service => service.ListProjectReportRecipientsByProjectId(id));

                if (selectedRecipients.Count > 0)
                {
                    var requests = new List<CreateProjectReportRecipientRequest>();
                    foreach (var selectRecipient in selectedRecipients)
                    {
                        if (reportRecipients.All(rr => rr.CrmEmailId != selectRecipient.Id))
                            requests.Add(new CreateProjectReportRecipientRequest { CrmEmailId = selectRecipient.Id, ProjectId = id });
                    }

                    if (requests.Any())
                    {
                        this.InvokeService<IProjectManagementCommandService>(service => service.CreateProjectReportRecipients(requests));
                    }
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return HandleStatusCodeError(exception);
            }
        }

        public ActionResult DeleteProjectReportRecipient(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.DeleteProjectReportRecipient(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult ListCulturalFitContactRequests(int contactId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var requests = this.InvokeService<IProjectManagementQueryService, List<TheoremListRequestView>>(service => service.ListCulturalFitContactRequests(contactId));
                    var model = new ListCulturalFitContactRequestsModel
                        {
                            Requests = requests
                        };
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult CreateCulturalFitContactRequest(int contactId, Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var contactPersons = this.InvokeService<ICustomerRelationshipManagementQueryService, List<CrmEmailView>>(service => service.ListCrmEmailsByContactId(contactId));
                    var project = this.InvokeService<IProjectManagementQueryService, AssessmentDevelopmentProjectView>(service => service.RetrieveAssessmentDevelopmentProjectDetail(projectId));

                    var model = new CreateCulturalFitContactRequestModel()
                        {
                            ContactId = contactId,
                            ContactName = project.Contact.FullName,
                            ProjectId = projectId,
                            ProjectName = project.Name,
                            Deadline = DateTime.Now.AddMonths(1),
                            ContactPersons = contactPersons.OrderBy(cp => cp.LastName).ToList(),
                        };
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
        public ActionResult CreateCulturalFitContactRequest(CreateCulturalFitContactRequestModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<CreateCulturalFitContactRequestRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.CreateCulturalFitContactRequest(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult EditCulturalFitContactRequest(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var culturalFitRequest = this.InvokeService<IProjectManagementQueryService, TheoremListRequestView>(service => service.RetrieveTheoremListRequest(id));
                    var contactPersons = this.InvokeService<ICustomerRelationshipManagementQueryService, List<CrmEmailView>>(service => service.ListCrmEmailsByContactId(culturalFitRequest.ContactId));

                    var model = Mapper.DynamicMap<EditCulturalFitContactRequestModel>(culturalFitRequest);
                    model.ContactPersons = contactPersons;
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
        public ActionResult EditCulturalFitContactRequest(EditCulturalFitContactRequestModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateCulturalFitContactRequestRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateCulturalFitContactRequest(request));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult ReopenCulturalFitContactRequest(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.ReopenCulturalFitContactRequest(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult GenerateCulturalFitReport(Guid id, int languageId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var theoremListRequest = this.InvokeService<IProjectManagementQueryService, TheoremListRequestView>(service => service.RetrieveTheoremListRequest(id));
                    var contact = this.InvokeService<ICustomerRelationshipManagementQueryService, CrmContactView>(service => service.RetrieveContactDetailInformation(theoremListRequest.ContactId));

                    var request = new GenerateReportRequest
                    {
                        OutputFormat = ReportOutputFormat.Pdf,
                        ReportName = "/CulturalFit Reports/CulturalFitReport"
                    };

                    request.Parameters = new Dictionary<string, string> { { "TheoremListRequestId", id.ToString() }, { "LanguageId", languageId.ToString(CultureInfo.InvariantCulture) } };

                    var response = this.InvokeService<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));

                    var filename = string.Format("{0}_{1}.pdf", "CulturalFit", contact.FullName);
                    filename = HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8);

                    return File(Convert.FromBase64String(response.Output), response.ContentType, filename);

                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult MarkProjectCandidateDetailDictionaryIndicatorAsStandard(Guid id, bool isChecked)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.MarkProjectCategoryDetailDictionaryIndicator(id, isChecked, false));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult MarkProjectCandidateDetailDictionaryIndicatorAsDistinctive(Guid id, bool isChecked)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.MarkProjectCategoryDetailDictionaryIndicator(id, false, isChecked));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult EditProjectCandidateDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectCandidate = this.InvokeService<IProjectManagementQueryService, ProjectCandidateView>(service => service.RetrieveProjectCandidateDetailExtended(id));
                    var project = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(projectCandidate.ProjectId));

                    var model = Mapper.Map<EditProjectCandidateDetailModel>(projectCandidate);

                    model.Languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());
                    model.ReportStatuses = this.InvokeService<IProjectManagementQueryService, List<ReportStatusView>>(service => service.ListReportStatuses());
                    model.CustomerAssistants = this.InvokeService<ISecurityQueryService, List<UserView>>(service => service.ListCustomerAssistants()).OrderBy(ca => ca.FullName).ToList();
                    model.ProjectCategoryDetails = project.ProjectCategoryDetails.Select(Mapper.DynamicMap<EditProjectCategoryDetailModel>).OrderByDescending(pcd => pcd.ProjectTypeCategoryIsMain).ToList();
                    model.AssessmentRooms = this.InvokeService<IInfrastructureQueryService, List<AssessmentRoomView>>(service => service.ListAssessmentRooms(new ListAssessmentRoomsRequest())).OrderBy(r => r.Id).ToList();
                    model.ReportDefinitions = this.InvokeService<IReportManagementQueryService, List<ReportDefinitionView>>(service => service.ListReportDefinitions(new ListReportDefinitionsRequest { Code = "CANDIDATE" })).Where(rd => rd.IsActive).ToList();
                    model.Project = (AssessmentDevelopmentProjectView)project;

                    var mainProjectCategoryDetail = project.ProjectCategoryDetails.FirstOrDefault(pcd => pcd.ProjectTypeCategory.IsMain);

                    if (mainProjectCategoryDetail != null)
                    {
                        if (mainProjectCategoryDetail is ProjectCategoryAcDetailView)
                            model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryAcDetailModel>(mainProjectCategoryDetail);
                        if (mainProjectCategoryDetail is ProjectCategoryCaDetailView)
                            model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryCaDetailModel>(mainProjectCategoryDetail);
                        if (mainProjectCategoryDetail is ProjectCategoryCuDetailView)
                            model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryCuDetailModel>(mainProjectCategoryDetail);
                        if (mainProjectCategoryDetail is ProjectCategoryDcDetailView)
                            model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryDcDetailModel>(mainProjectCategoryDetail);
                        if (mainProjectCategoryDetail is ProjectCategoryEaDetailView)
                            model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryEaDetailModel>(mainProjectCategoryDetail);
                        if (mainProjectCategoryDetail is ProjectCategoryFaDetailView)
                            model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryFaDetailModel>(mainProjectCategoryDetail);
                        if (mainProjectCategoryDetail is ProjectCategoryFdDetailView)
                            model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryFdDetailModel>(mainProjectCategoryDetail);
                        if (mainProjectCategoryDetail is ProjectCategoryPsDetailView)
                            model.MainProjectCategoryDetail = Mapper.Map<EditProjectCategoryPsDetailModel>(mainProjectCategoryDetail);
                    }

                    var offices = this.InvokeService<IInfrastructureQueryService, List<OfficeView>>(service => service.ListOffices());

                    model.ProjectCandidateCategoryDetailTypes
                        .OfType<EditProjectCandidateCategoryDetailType1Model>()
                        .ForEach(c => c.Offices.AddRange(offices
                            .Select(o => new SelectListItem
                                {
                                    Selected = c.OfficeId == o.Id,
                                    Text = o.FullName,
                                    Value = o.Id.ToString(CultureInfo.InvariantCulture)
                                })));

                    model.ProjectCandidateCategoryDetailTypes.Sort((t1, t2) => String.Compare(t1.ProjectCategoryDetailTypeName, t2.ProjectCategoryDetailTypeName, StringComparison.Ordinal));

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditProjectCandidate(EditProjectCandidateDetailModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    //Update candidates
                    var request = Mapper.DynamicMap<UpdateProjectCandidateDetailRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCandidatesDetail(request));

                    //Update detail types
                    if (model.ProjectCandidateCategoryDetailTypes != null && model.ProjectCandidateCategoryDetailTypes.Count > 0)
                    {
                        var detailTypeRequests = model.ProjectCandidateCategoryDetailTypes.Select(Mapper.Map<UpdateProjectCandidateCategoryDetailTypeRequest>).ToList();

                        if (detailTypeRequests.Count > 0)
                        {
                            this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectCandidateCategoryDetailTypes(detailTypeRequests));
                        }
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
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult EditDeletedProjectCandidateDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectCandidate = this.InvokeService<IProjectManagementQueryService, ProjectCandidateView>(service => service.RetrieveProjectCandidateDetailExtended(id));
                    var model = Mapper.Map<EditProjectCandidateDetailModel>(projectCandidate);
                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult EditDeletedProjectCandidateDetail(EditProjectCandidateDetailModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateCancelledProjectCandidateRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateCancelledProjectCandidate(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult GenerateCandidateMap(Guid Id, Guid ProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new GenerateReportRequest
                    {
                        OutputFormat = ReportOutputFormat.Pdf,
                        ReportName = "/QPlanet/UserReporting/CandidateMap",
                        Parameters = new Dictionary<string, string>
                            {
                                {"Id", Id.ToString()},
                                {"ProjectId", ProjectId.ToString()}
                            }
                    };
                    var response = this.InvokeService<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));
                    return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("CandidateMap.pdf"));
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }
    }
}
