using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.Infrastructure.Validation;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectCandidateOverview;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Controllers
{
    public class ProjectCandidateOverviewController : ProjectController
    {
        public ActionResult Index()
        {
            using (DurationLog.Create())
            {
                try
                {
                    ViewBag.User = GetAuthenticationToken().User.UserName;
                    var model = new ProjectCandidateOverviewIndexActionModel
                        {
                            StartDate = DateTime.Now.AddDays(-14),
                            EndDate = DateTime.Now,
                            CustomerAssistants =
                                this.InvokeService<ISecurityQueryService, List<UserView>>(service => service.ListCustomerAssistants()),
                            CustomerAssistantId = GetAuthenticationToken().User.Id
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

        public ActionResult ProjectCandidateOverview(FilterProjectCandidateOverviewModel filterModel)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<ListProjectCandidateOverviewRequest>(filterModel);
                    var response = this.InvokeService<IProjectManagementQueryService, ListProjectCandidateOverviewResponse>(service => service.ListProjectCandidateOverviewEntries(request));
                    var projectCandidateOverviewEntries = response.Entries;
                    var model = new ProjectCandidateOverviewActionModel
                        {
                            ProjectCandidateOverviewEntries = projectCandidateOverviewEntries.OrderBy(pcoe => pcoe.Date).ToList(),
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
        public ActionResult UpdateProjectCandidateField(EditProjectCandidateOverviewEntryFieldModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request =
                        Mapper.DynamicMap<UpdateProjectCandidateOverviewEntryProjectCandidateFieldRequest>(model);

                    this.InvokeService<IProjectManagementCommandService>(
                        service => service.UpdateProjectCandidateOverviewEntryProjectCandidateField(request));

                    var projectCandidateOverviewEntryView = this.InvokeService<IProjectManagementQueryService, ProjectCandidateOverviewEntryView>(
                        service => service.RetrieveProjectCandidateOverviewEntry(model.Id, ProjectCandidateOverviewEntryType.ProjectCandidate));
                    return PartialView("ProjectCandidateOverviewEntry", projectCandidateOverviewEntryView);
                }
                catch (Exception exception)
                {
                    var faultException = exception as FaultException<ValidationContainer>;
                    if (faultException != null && faultException.Detail.FaultDetail.FaultEntries.OfType<VersionMismatchFaultEntry>().Any())
                    {
                        //Version mismatch
                        var projectCandidateOverviewEntryView = this.InvokeService<IProjectManagementQueryService, ProjectCandidateOverviewEntryView>(
                                service => service.RetrieveProjectCandidateOverviewEntry(model.Id, ProjectCandidateOverviewEntryType.ProjectCandidate));

                        var mismatchModel = new ProjectCandidateOverviewVersionMismatchActionModel
                            {
                                CurrentVersion = model,
                                ServerVersion = projectCandidateOverviewEntryView
                            };
                        Response.StatusCode = (int)HttpStatusCode.Conflict;
                        return PartialView("ProjectCandidateOverviewEntryProjectCandidateVersionMismatch", mismatchModel);
                    }
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateProjectCandidateCategoryField(EditProjectCandidateOverviewEntryFieldModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request =
                        Mapper.DynamicMap<UpdateProjectCandidateOverviewEntryProjectCandidateCategoryFieldRequest>(model);

                    this.InvokeService<IProjectManagementCommandService>(
                        service => service.UpdateProjectCandidateOverviewEntryProjectCandidateCategoryField(request));

                    var projectCandidateOverviewEntryView = this.InvokeService<IProjectManagementQueryService, ProjectCandidateOverviewEntryView>(
                        service => service.RetrieveProjectCandidateOverviewEntry(model.Id, ProjectCandidateOverviewEntryType.ProjectCandidateCategory));
                    return PartialView("ProjectCandidateOverviewEntry", projectCandidateOverviewEntryView);
                }
                catch (Exception exception)
                {
                    var faultException = exception as FaultException<ValidationContainer>;
                    if (faultException != null && faultException.Detail.FaultDetail.FaultEntries.OfType<VersionMismatchFaultEntry>().Any())
                    {
                        //Version mismatch
                        var projectCandidateOverviewEntryView = this.InvokeService<IProjectManagementQueryService, ProjectCandidateOverviewEntryView>(
                                service => service.RetrieveProjectCandidateOverviewEntry(model.Id, ProjectCandidateOverviewEntryType.ProjectCandidateCategory));

                        var mismatchModel = new ProjectCandidateOverviewVersionMismatchActionModel
                        {
                            CurrentVersion = model,
                            ServerVersion = projectCandidateOverviewEntryView
                        };
                        Response.StatusCode = (int)HttpStatusCode.Conflict;
                        return PartialView("ProjectCandidateOverviewEntryProjectCandidateCategoryVersionMismatch", mismatchModel);
                    }
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }
    }
}
