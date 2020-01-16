using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Infrastructure.Security;
using Quintessence.QPlanet.Infrastructure.Services;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectGeneral;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.DataModel.Prm;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DocumentManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Rep;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Controllers
{
    public class ProjectGeneralController : ProjectController
    {
        public ActionResult SearchContact(string term)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var serviceInvoker = new ServiceInvoker<ICustomerRelationshipManagementQueryService>();
                    var contacts = serviceInvoker.Execute(ControllerContext.HttpContext.ApplicationInstance.Context,
                                                          service =>
                                                          service.SearchContacts(new SearchContactRequest { CustomerName = term }));

                    contacts.Sort((c1, c2) => String.Compare(c1.FullName, c2.FullName, StringComparison.InvariantCultureIgnoreCase));

                    var filteredContacts = contacts.Where(c => String.Equals(c.FullName, term, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    filteredContacts.AddRange(contacts.Except(filteredContacts));

                    var contactJson = filteredContacts.Select(u => new { label = u.FullName, value = u.Id }).ToList();

                    var limitedList = contactJson.ToList();

                    var result = Json(limitedList, JsonRequestBehavior.AllowGet);
                    return result;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult SearchUser(string term)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var serviceInvoker = new ServiceInvoker<ISecurityQueryService>();
                    var response = serviceInvoker.Execute(ControllerContext.HttpContext.ApplicationInstance.Context, service => service.SearchUser(new SearchUserRequest { Name = term }));

                    var contactJson = response.Users.Select(u => new { label = u.FullName, value = u.Id }).ToList();

                    var limitedList = contactJson.ToList();

                    var result = Json(limitedList, JsonRequestBehavior.AllowGet);
                    return result;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult SearchProjectManager(string term)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var serviceInvoker = new ServiceInvoker<ISecurityQueryService>();
                    var response = serviceInvoker.Execute(ControllerContext.HttpContext.ApplicationInstance.Context, service => service.SearchUser(new SearchUserRequest { Name = term }));

                    var contactJson = response.Users.Select(u => new { label = u.FullName, value = u.Id }).ToList();

                    var limitedList = contactJson.ToList();

                    var result = Json(limitedList, JsonRequestBehavior.AllowGet);
                    return result;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult SearchCustomerAssistant(string term)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var serviceInvoker = new ServiceInvoker<ISecurityQueryService>();
                    var response = serviceInvoker.Execute(ControllerContext.HttpContext.ApplicationInstance.Context, service => service.SearchUser(new SearchUserRequest { Name = term }));

                    var contactJson = response.Users.Select(u => new { label = u.FullName, value = u.Id }).ToList();

                    var limitedList = contactJson.ToList();

                    var result = Json(limitedList, JsonRequestBehavior.AllowGet);
                    return result;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult ContactCustomerAssistant(int id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var contact = this.InvokeService<ICustomerRelationshipManagementQueryService, CrmContactView>(service => service.RetrieveContactDetailInformation(id));

                    if (contact == null)
                        return new HttpNotFoundResult();

                    return Json(contact.CustomerAssistant, JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult CrmActiveProjects(int id)
        {
            using (DurationLog.Create())
            {
                var crmActiveProjects = this.InvokeService<ICustomerRelationshipManagementQueryService, List<CrmActiveProjectView>>(service => service.ListActiveProjects(id));

                if (crmActiveProjects.Count == 0)
                    return new HttpNotFoundResult();

                return PartialView(crmActiveProjects.OrderBy(p => p.Name));
            }
        }

        public ActionResult Edit(Guid id)
        {
            using (DurationLog.Create())
            {
                var serviceInvoker = new ServiceInvoker<IProjectManagementQueryService>();
                var project = serviceInvoker.Execute(ControllerContext.HttpContext.ApplicationInstance.Context,
                                                     service => service.RetrieveProject(id));

                switch (project.ProjectType.Code)
                {
                    case "ACDC":
                        return RedirectToAction("Edit", "ProjectAssessmentDevelopment", new { area = "Project", id });

                    case "CONS":
                        return RedirectToAction("Edit", "ProjectConsultancy", new { area = "Project", id });
                }

                return new HttpNotFoundResult();
            }
        }

        public ActionResult EditProjectCategoryDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                var projectView = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProject(id));
                var projectCategoryDetailView = this.InvokeService<IProjectManagementQueryService, ProjectCategoryDetailView>(service => service.RetrieveMainProjectCategoryDetail(id));

                switch (projectView.ProjectType.Code)
                {
                    case "ACDC":
                        switch (projectCategoryDetailView.ProjectTypeCategory.Code)
                        {
                            case "AC":
                                return RedirectToAction("EditProjectCategoryAcDetail", "ProjectAssessmentDevelopment", new { area = "Project", id });

                            case "FA":
                                return RedirectToAction("EditProjectCategoryFaDetail", "ProjectAssessmentDevelopment", new { area = "Project", id });

                            case "DC":
                                return RedirectToAction("EditProjectCategoryDcDetail", "ProjectAssessmentDevelopment", new { area = "Project", id });

                            case "FD":
                                return RedirectToAction("EditProjectCategoryFdDetail", "ProjectAssessmentDevelopment", new { area = "Project", id });

                            case "EA":
                                return RedirectToAction("EditProjectCategoryEaDetail", "ProjectAssessmentDevelopment", new { area = "Project", id });

                            case "PS":
                                return RedirectToAction("EditProjectCategoryPsDetail", "ProjectAssessmentDevelopment", new { area = "Project", id });

                            case "SO":
                                return RedirectToAction("EditProjectCategorySoDetail", "ProjectAssessmentDevelopment", new { area = "Project", id });

                            case "CA":
                                return RedirectToAction("EditProjectCategoryCaDetail", "ProjectAssessmentDevelopment", new { area = "Project", id });

                            case "CU":
                                return RedirectToAction("EditProjectCategoryCuDetail", "ProjectAssessmentDevelopment", new { area = "Project", id });

                            case "CO":
                                return RedirectToAction("EditProjectCategoryCuDetail", "ProjectAssessmentDevelopment", new { area = "Project", id });
                        }
                        break;

                }

                return new HttpNotFoundResult();
            }
        }

        /// <summary>
        /// Retrieves the project with its linked CrmProjects
        /// </summary>
        /// <param name="id">The id of the project</param>
        /// <returns></returns>
        public ActionResult ListCrmProjects(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.EnsureProjectRevenueDistributions(id));
                    var crmProjectView = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectWithCrmProjects(id));
                    var crmProjects = crmProjectView.CrmProjects;

                    var model = new ListCrmProjectsActionModel();
                    model.CrmProjects = crmProjects;
                    model.ProjectRevenueDistribution = crmProjectView.ProjectRevenueDistributions;

                    model.CrmProjectRevenueDistributions = crmProjectView.ProjectRevenueDistributions.Select(Mapper.DynamicMap<CrmProjectRevenueDistributionModel>).ToList();

                    foreach (var crmProjectRevenueDistribution in model.CrmProjectRevenueDistributions)
                    {
                        var crmProject = crmProjectView.CrmProjects.FirstOrDefault(cp => cp.Id == crmProjectRevenueDistribution.CrmProjectId);

                        if (crmProject == null)
                            continue;

                        crmProjectRevenueDistribution.CrmProjectName = crmProject.Name;
                        crmProjectRevenueDistribution.CrmProjectStatusName = crmProject.ProjectStatus.Name;
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
        public ActionResult ListCrmProjects(ListCrmProjectsActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var requests = model.CrmProjectRevenueDistributions.Select(Mapper.DynamicMap<UpdateProjectRevenueDistributionRequest>).ToList();
                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectRevenueDistributions(requests));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        /// <summary>
        /// Retrieves the project invoice amount overview
        /// </summary>
        /// <param name="id">The id of the project</param>
        /// <returns></returns>
        public ActionResult ListProjectInvoiceAmountOverview(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectInvoiceAmountOverviewEntries = this.InvokeService<IProjectManagementQueryService, List<ProjectInvoiceAmountOverviewEntryView>>(service => service.ListProjectInvoiceAmountOverviewEntries(id));

                    var model = new ListProjectInvoiceAmountEntriesModel
                        {
                            InvoiceAmountDictionary = projectInvoiceAmountOverviewEntries
                                                        .GroupBy(entry => entry.FiscalYear)
                                                        .OrderByDescending(entry => entry.Key)
                                                        .ToDictionary(group => group.Key, group => group.Sum(entry => entry.InvoiceAmount.GetValueOrDefault()))
                        };

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        /// <summary>
        /// Searches crm projects for the linked contact of the project
        /// </summary>
        /// <param name="id">The id of the project</param>
        /// <returns></returns>
        public ActionResult SearchCrmProjects(Guid id)
        {
            using (DurationLog.Create())
            {
                var model = new SearchCrmProjectsModel();
                model.ProjectId = id;
                model.IsStatusPlannedChecked = true;
                model.IsStatusRunningChecked = true;
                return PartialView(model);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchCrmProjects(SearchCrmProjectsModel model)
        {
            using (DurationLog.Create())
            {
                var request = new SearchCrmProjectsRequest
                    {
                        ProjectId = model.ProjectId,
                        ProjectName = model.ProjectName,
                        WithPlannedStatus = model.IsStatusPlannedChecked,
                        WithRunningStatus = model.IsStatusRunningChecked,
                        WithDoneStatus = model.IsStatusDoneChecked,
                        WithStoppedStatus = model.IsStatusStoppedChecked
                    };

                var links = this.InvokeService<IProjectManagementQueryService, List<SearchCrmProjectResultItemView>>(service => service.SearchCrmProjects(request));

                return PartialView("SearchCrmProjectsResult", links.Select(Mapper.Map<SearchCrmProjectResultItemModel>).ToList());
            }
        }

        [HttpPost]
        public ActionResult LinkCrmProjects(Guid id, List<SearchCrmProjectResultItemModel> results)
        {
            using (DurationLog.Create())
            {
                if (results == null)
                    return new HttpStatusCodeResult(200);

                this.InvokeService<IProjectManagementCommandService>(service =>
                    {
                        foreach (var searchCrmProjectResultItemModel in results.Where(item => item.IsSelected))
                            service.LinkProject2CrmProject(id, searchCrmProjectResultItemModel.CrmProjectId);
                    });
                return new HttpStatusCodeResult(200);
            }
        }

        public ActionResult UnlinkCrmProject(Guid id, int crmId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(service => service.UnlinkProject2CrmProject(id, crmId));
                    return new HttpStatusCodeResult(200);
                }
                catch (Exception exception)
                {
                    return HandleStatusCodeError(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult ChangeProjectManager(Guid id, FormCollection formCollection)
        {
            using (DurationLog.Create())
            {
                var projectManagerId = new Guid(formCollection["SelectedProjectManagerUserId"]);
                var user = this.InvokeService<ISecurityQueryService, UserView>(service => service.RetrieveUser(projectManagerId));
                return Json(user);
            }
        }

        [HttpPost]
        public ActionResult ChangeCustomerAssistant(Guid id, FormCollection formCollection)
        {
            using (DurationLog.Create())
            {
                var customerAssistantId = new Guid(formCollection["SelectedCustomerAssistantUserId"]);
                var user = this.InvokeService<ISecurityQueryService, UserView>(service => service.RetrieveUser(customerAssistantId));
                return Json(user);
            }
        }

        public ActionResult Create(Guid? id, Guid? projectCandidateId)
        {
            using (DurationLog.Create())
            {
                var model = new CreateProjectModel();

                model.ProjectTypes =
                    MapperExtensions.MapList<ProjectTypeSelectListItemModel>(
                        this.InvokeService<IProjectManagementQueryService, List<ProjectTypeView>>(
                            service => service.ListProjectTypes()));

                model.MainProjectId = id;

                //Create subproject
                if (id.HasValue)
                {
                    model.MainProject = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(id.Value));
                    model.ProjectCandidateId = projectCandidateId;
                }
                else
                {
                    var identity = IdentityHelper.RetrieveIdentity(HttpContext.ApplicationInstance.Context);

                    if (identity == null)
                    {
                        return new HttpUnauthorizedResult("Unable to retrieve identity information");
                    }
                    var token = this.InvokeService<IAuthenticationQueryService, AuthenticationTokenView>(service => service.RetrieveAuthenticationTokenDetail(new Guid(identity.Ticket.UserData)));

                    model.ProjectManagerUserId = token.UserId;
                    model.ProjectManagerFullName = token.User.FullName;
                }

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Create(CreateProjectModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var createNewProjectRequest = Mapper.Map<CreateNewProjectRequest>(model);

                    var projectId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProject(createNewProjectRequest));

                    return Json(projectId.ToString());
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        /// <summary>
        /// Lists the share point documents.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult ListSharePointDocuments(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new ListProjectDocumentsRequest { ProjectId = id };
                    var response = this.InvokeService<IDocumentManagementQueryService, ListProjectDocumentsResponse>(service => service.ListProjectDocuments(request));
                    var documents = response.Documents.OrderBy(d => d.IsImportant).ThenBy(d => d.Name).ToList();

                    return PartialView(documents);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult ListProjectDocuments(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new ListProjectDocumentsRequest { ProjectId = id };
                    var response = this.InvokeService<IDocumentManagementQueryService, ListProjectDocumentsResponse>(service => service.ListProjectDocuments(request));
                    var documents = response.Documents.OrderBy(d => d.IsImportant).ThenBy(d => d.Name).ToList();

                    return PartialView(documents);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="reportDefinitionId">The report definition id.</param>
        /// <param name="outputFormat">The output format.</param>
        /// <returns></returns>
        public ActionResult GenerateReport(Guid id, string code, Guid reportDefinitionId, string outputFormat)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var reportOutputFormat = (ReportOutputFormat)Enum.Parse(typeof(ReportOutputFormat), outputFormat, true);
                    var reportDefinition = this.InvokeService<IReportManagementQueryService, ReportDefinitionView>(service => service.RetrieveReportDefinition(reportDefinitionId));

                    var request = new GenerateReportRequest
                        {
                            OutputFormat = reportOutputFormat,
                            ReportName = reportDefinition.Location
                        };

                    switch (code.ToUpperInvariant())
                    {
                        case "PROJECT":
                            request.Parameters = new Dictionary<string, string> { { "ProjectId", id.ToString() } };
                            break;

                        case "CANDIDATE":
                            request.Parameters = new Dictionary<string, string> { { "ProjectCandidateId", id.ToString() } };
                            break;
                    }

                    var response = this.InvokeService<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));

                    switch (reportOutputFormat)
                    {
                        case ReportOutputFormat.Csv:
                            return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}_{1}.csv", reportDefinition.Name, id));

                        case ReportOutputFormat.Excel:
                            return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}_{1}.xls", reportDefinition.Name, id));

                        case ReportOutputFormat.Html32:
                        case ReportOutputFormat.Html4:
                        case ReportOutputFormat.MHtml:
                            return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}_{1}.htm", reportDefinition.Name, id));

                        case ReportOutputFormat.Image:
                            return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}_{1}.tiff", reportDefinition.Name, id));

                        case ReportOutputFormat.Pdf:
                            return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}_{1}.pdf", reportDefinition.Name, id));

                        case ReportOutputFormat.Word:
                            return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}_{1}.docx", reportDefinition.Name, id));

                        case ReportOutputFormat.Xml:
                            return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}_{1}.xml", reportDefinition.Name, id));

                        default:
                            throw new ArgumentOutOfRangeException("outputFormat", "Illegal output format");
                    }

                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult GenerateCandidateReport(Guid candidateReportDefinitionId, Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var candidateReportDefinition = this.InvokeService<IReportManagementQueryService, CandidateReportDefinitionView>(service => service.RetrieveCandidateReportDefinition(candidateReportDefinitionId));
                    var projectCandidate = this.InvokeService<IProjectManagementQueryService, ProjectCandidateView>(service => service.RetrieveProjectCandidate(projectCandidateId));
                    var project = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(projectCandidate.ProjectId));

                    var request = new GenerateReportRequest
                        {
                            ReportName = candidateReportDefinition.Location,
                            Parameters = new Dictionary<string, string> { { "ProjectCandidateId", projectCandidateId.ToString() } },
                            OutputFormat = ReportOutputFormat.Word
                        };
                    var response = this.InvokeService<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));

                    var filename = string.Format("{0} - {1}.docx", project.Contact.FullName, projectCandidate.CandidateFullName);
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

        [HttpPost]
        public ActionResult GenerateCandidateReport(GenerateCandidateReportModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var candidateReportDefinition = this.InvokeService<IReportManagementQueryService, CandidateReportDefinitionView>(service => service.RetrieveCandidateReportDefinition(model.CandidateReportDefinitionId));
                    var projectCandidate = this.InvokeService<IProjectManagementQueryService, ProjectCandidateView>(service => service.RetrieveProjectCandidate(model.Id));
                    var project = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProjectDetail(projectCandidate.ProjectId));

                    var updateRequest = Mapper.DynamicMap<UpdateProjectCandidateScoringCoAssessorIdRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(
                        service => service.UpdateProjectCandidateScoringCoAssessorId(updateRequest));

                    var request = new GenerateReportRequest
                    {
                        ReportName = candidateReportDefinition.Location,
                        Parameters = new Dictionary<string, string> { { "ProjectCandidateId", model.Id.ToString() } },
                        OutputFormat = ReportOutputFormat.Word
                    };
                    var response = this.InvokeService<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));

                    var filename = string.Format("{0} - {1}.docx", project.Contact.FullName, projectCandidate.CandidateFullName);
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

        public ActionResult Copy(Guid id)
        {
            using (DurationLog.Create())
            {
                var model = new CreateProjectModel();

                model.ProjectTypes =
                    MapperExtensions.MapList<ProjectTypeSelectListItemModel>(
                        this.InvokeService<IProjectManagementQueryService, List<ProjectTypeView>>(
                            service => service.ListProjectTypes()));

                model.CopyProjectId = id;
                model.CopyProject = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProject(id));

                var identity = IdentityHelper.RetrieveIdentity(HttpContext.ApplicationInstance.Context);

                if (identity == null)
                {
                    return new HttpUnauthorizedResult("Unable to retrieve identity information");
                }
                var token = this.InvokeService<IAuthenticationQueryService, AuthenticationTokenView>(service => service.RetrieveAuthenticationTokenDetail(new Guid(identity.Ticket.UserData)));

                model.ProjectManagerUserId = token.UserId;
                model.ProjectManagerFullName = token.User.FullName;

                return View("Create", model);
            }
        }

        [HttpPost]
        public ActionResult Copy(CreateProjectModel model)
        {
            using (DurationLog.Create())
            {
                //If main project, create new subproject
                if (model.MainProjectId.HasValue)
                {
                    var createNewProjectRequest = Mapper.Map<CreateNewProjectRequest>(model);

                    var projectId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProject(createNewProjectRequest));

                    return RedirectToAction("Edit", new { id = projectId });
                }

                //Create normal project
                if (ModelState.IsValid)
                {
                    var createNewProjectRequest = Mapper.Map<CreateNewProjectRequest>(model);

                    var projectId = this.InvokeService<IProjectManagementCommandService, Guid>(service => service.CreateNewProject(createNewProjectRequest));

                    return RedirectToAction("Edit", new { id = projectId });
                }

                model.ProjectTypes = MapperExtensions.MapList<ProjectTypeSelectListItemModel>(this.InvokeService<IProjectManagementQueryService, List<ProjectTypeView>>(service => service.ListProjectTypes()));
                model.CopyProject = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProject(model.CopyProject.Id));

                return View("Create", model);
            }
        }

        public ActionResult ProjectDna(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectDna = this.InvokeService<IProjectManagementQueryService, ProjectDnaView>(service => service.RetrieveProjectDna(crmProjectId));

                    var model = Mapper.Map<EditProjectDnaModel>(projectDna);

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
        public ActionResult ProjectDna(EditProjectDnaModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectDnaRequest>(model);

                    request.SelectedProjectDnaTypeIds = model.ProjectDnaTypes.Where(pdt => pdt.IsSelected).Select(pdt => pdt.Id).ToList();
                    request.SelectedProjectDnaContactPersonIds = model.ProjectDnaContactPersons.Where(pdc => pdc.IsSelected).Select(pdc => pdc.Id).ToList();

                    this.InvokeService<IProjectManagementCommandService>(service => service.UpdateProjectDna(request));

                    return RedirectToAction("ProjectDna", new { crmProjectId = model.CrmProjectId });
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult EvaluationOverview(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectEvaluation = this.InvokeService<IProjectManagementQueryService, ProjectEvaluationView>(service => service.RetrieveProjectEvaluationByCrmProject(crmProjectId));

                    var model = Mapper.DynamicMap<EditProjectEvaluationModel>(projectEvaluation);

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
        public ActionResult EvaluationOverview(EditProjectEvaluationModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var request = Mapper.DynamicMap<UpdateProjectEvaluationRequest>(model);
                        this.InvokeService<IProjectManagementCommandService>(
                            service => service.UpdateProjectEvaluation(request));
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

        public ActionResult ListEvaluationForms(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var evaluationForms = this.InvokeService<IProjectManagementQueryService, List<EvaluationFormView>>(service => service.ListEvaluationFormsByCrmProject(crmProjectId));
                    var orderedForms = evaluationForms.OrderByDescending(ef => ef.Audit.CreatedOn).ToList();
                    return PartialView(orderedForms);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult ListProjectComplaints(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectComplaints = this.InvokeService<IProjectManagementQueryService, List<ProjectComplaintView>>(service => service.ListProjectComplaintsByCrmProject(crmProjectId));
                    var orderedForms = projectComplaints.OrderByDescending(pc => pc.Audit.CreatedOn).ToList();
                    return PartialView(orderedForms);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult CreateEvaluationForm(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var languages = this.InvokeService<IInfrastructureQueryService, List<LanguageView>>(service => service.ListLanguages());
                    var evaluationFormTypes = this.InvokeService<IProjectManagementQueryService, List<EvaluationFormTypeView>>(service => service.ListEvaluationFormTypes());

                    var model = new CreateEvaluationFormActionModel
                        {
                            CrmProjectId = crmProjectId,
                            LanguageId = languages.FirstOrDefault().Id,
                            EvaluationFormTypeId = evaluationFormTypes.FirstOrDefault().Id,
                            Gender = "M",
                            EvaluationFormTypes = evaluationFormTypes,
                            Languages = languages
                        };

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
        public ActionResult CreateEvaluationForm(CreateEvaluationFormActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var request = Mapper.DynamicMap<CreateEvaluationFormRequest>(model);
                        this.InvokeService<IProjectManagementCommandService>(
                            service => service.CreateEvaluationForm(request));
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

        public ActionResult UpdateEvaluationForm(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<IProjectManagementCommandService>(
                        service => service.UpdateEvaluationForm(id));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }

        }

        public ActionResult CreateProjectComplaint(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var complaintTypes = this.InvokeService<IProjectManagementQueryService, List<ComplaintTypeView>>(service => service.ListComplaintTypes());
                    var identity = IdentityHelper.RetrieveIdentity(HttpContext.ApplicationInstance.Context);

                    if (identity == null)
                    {
                        return new HttpUnauthorizedResult("Unable to retrieve identity information");
                    }
                    var token = this.InvokeService<IAuthenticationQueryService, AuthenticationTokenView>(service => service.RetrieveAuthenticationTokenDetail(new Guid(identity.Ticket.UserData)));

                    var model = new ProjectComplaintActionModel
                    {
                        CrmProjectId = crmProjectId,
                        Complaint = complaintTypes,
                        ComplaintDate = DateTime.Now,
                        ComplaintSeverityTypeId = 10,
                        ComplaintStatusTypeId = 10,
                        ComplaintTypeId = complaintTypes.FirstOrDefault().Id,
                        SubmitterId = token.UserId,
                        SubmitterName = token.User.FullName
                    };

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
        public ActionResult CreateProjectComplaint(ProjectComplaintActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {

                    var request = Mapper.DynamicMap<CreateProjectComplaintRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(
                        service => service.CreateProjectComplaint(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult UpdateProjectComplaint(Guid ComplaintId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var complaintTypes = this.InvokeService<IProjectManagementQueryService, List<ComplaintTypeView>>(service => service.ListComplaintTypes());
                    var projectComplaint = this.InvokeService<IProjectManagementQueryService, ProjectComplaintView>(service => service.RetrieveProjectComplaint(ComplaintId));
                    var model = Mapper.DynamicMap<ProjectComplaintActionModel>(projectComplaint);
                    model.ComplaintSeverityTypeId = Convert.ToInt32((Enum.GetValues(typeof(ComplaintSeverityType)).OfType<ComplaintSeverityType>().Select(g =>
                                                                                            new SelectListItem
                                                                                            {
                                                                                                Value = Convert.ToString((int)g),
                                                                                                Text = EnumMemberNameAttribute.GetName(g)
                                                                                            }).Where(g => g.Text == projectComplaint.ComplaintSeverityTypeName).FirstOrDefault()).Value);
                    model.ComplaintStatusTypeId = Convert.ToInt32((Enum.GetValues(typeof(ComplaintStatusType)).OfType<ComplaintStatusType>().Select(g =>
                                                                        new SelectListItem
                                                                        {
                                                                            Value = Convert.ToString((int)g),
                                                                            Text = EnumMemberNameAttribute.GetName(g)
                                                                        }).Where(g => g.Text == projectComplaint.ComplaintStatusTypeName).FirstOrDefault()).Value);
                    model.ComplaintTypeId = (complaintTypes.Where(g => g.Name == projectComplaint.ComplaintTypeName).FirstOrDefault()).Id;
                    model.Complaint = complaintTypes;
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
        public ActionResult UpdateProjectComplaint(ProjectComplaintActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {

                    var request = Mapper.DynamicMap<UpdateProjectComplaintRequest>(model);
                    this.InvokeService<IProjectManagementCommandService>(
                        service => service.UpdateProjectComplaint(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult RetrieveEvaluationFormEmail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    try
                    {
                        var response = this.InvokeService<IInfrastructureQueryService, CreateEvaluationFormMailResponse>(service => service.CreateEvaluationFormMail(id));
                        return Json((new { to = response.To, subject = response.Subject, body = response.Body, bcc = response.Bcc }), JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception exception)
                    {
                        LogManager.LogError(exception);
                        return HandleStatusCodeError(exception);
                    }

                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult GenerateEvaluationReport(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new GenerateReportRequest
                    {
                        OutputFormat = ReportOutputFormat.Pdf,
                        ReportName = "/Qcare/QCareEvaluationForm",
                        Parameters = new Dictionary<string, string>
                            {
                                {"EvaluationFormId", id.ToString()},
                            }
                    };

                    var response = this.InvokeService<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));
                    return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}.pdf", id));
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