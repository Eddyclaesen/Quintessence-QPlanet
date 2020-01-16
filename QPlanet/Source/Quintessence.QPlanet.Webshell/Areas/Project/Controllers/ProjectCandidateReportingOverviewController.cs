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
using Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectCandidateReportingOverview;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Controllers
{
    public class ProjectCandidateReportingOverviewController : ProjectController
    {
        public ActionResult Index()
        {
            using (DurationLog.Create())
            {
                try
                {
                    ViewBag.User = GetAuthenticationToken().User.UserName;
                    var model = new ProjectCandidateReportingOverviewIndexActionModel
                        {
                            CustomerAssistants =
                                this.InvokeService<ISecurityQueryService, List<UserView>>(service => service.ListCustomerAssistants()),
                            CustomerAssistantId = GetAuthenticationToken().User.Id,
                            StartDate = DateTime.Now
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
        public ActionResult ProjectCandidateReportingOverview(ProjectCandidateReportingFiltersModel filterModel)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var m = filterModel;
                    var request = Mapper.DynamicMap<ListProjectCandidateReportingOverviewEntriesRequest>(filterModel);
                    var reportingOverviewEntries = this.InvokeService<IProjectManagementQueryService, List<ProjectCandidateReportingOverviewEntryView>>(service => service.ListProjectCandidateReportingOverviewEntries(request));
                    var model = new ProjectCandidateReportingOverviewActionModel
                        {
                            ReportStatuses = this.InvokeService<IProjectManagementQueryService, List<ReportStatusView>>(service => service.ListReportStatuses()),
                            Users = this.InvokeService<ISecurityQueryService, List<UserView>>(service => service.ListUsers()),
                            ReportingOverviewEntries = reportingOverviewEntries.OrderBy(pcoe => pcoe.ReportDeadline).ToList()
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
        public ActionResult UpdateProjectCandidateReportingField(EditProjectCandidateReportingOverviewEntryFieldModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request =
                        Mapper.DynamicMap<UpdateProjectCandidateReportingOverviewEntryProjectCandidateFieldRequest>(model);

                    this.InvokeService<IProjectManagementCommandService>(
                        service => service.UpdateProjectCandidateReportingOverviewEntryProjectCandidateField(request));

                    var updatedModel = new ProjectCandidateReportingOverviewRowModel
                        {
                            ReportStatuses =
                                this.InvokeService<IProjectManagementQueryService, List<ReportStatusView>>(
                                    service => service.ListReportStatuses()),
                            Users = this.InvokeService<ISecurityQueryService, List<UserView>>(service => service.ListUsers()),
                            ReportingOverviewEntry =
                                this
                                    .InvokeService
                                    <IProjectManagementQueryService, ProjectCandidateReportingOverviewEntryView>(
                                        service => service.RetrieveProjectCandidateReportingOverviewEntry(model.Id))
                        };
                    return PartialView("ProjectCandidateReportingOverviewEntry", updatedModel);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    var faultException = exception as FaultException<ValidationContainer>;
                    if (faultException != null && faultException.Detail.FaultDetail.FaultEntries.OfType<VersionMismatchFaultEntry>().Any())
                    {
                        //Version mismatch
                        var projectCandidateReportingOverviewEntryView = this
                            .InvokeService<IProjectManagementQueryService, ProjectCandidateReportingOverviewEntryView>(
                                service => service.RetrieveProjectCandidateReportingOverviewEntry(model.Id));

                        var mismatchModel = new ProjectCandidateReportingOverviewVersionMismatchActionModel
                            {
                                CurrentVersion = model,
                                ServerVersion = projectCandidateReportingOverviewEntryView
                            };
                        Response.StatusCode = (int)HttpStatusCode.Conflict;
                        return PartialView("ProjectCandidateReportingOverviewEntryVersionMismatch", mismatchModel);
                    }
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateProjectCandidateProjectReportingField(EditProjectCandidateOverviewEntryFieldModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request =
                        Mapper.DynamicMap<UpdateProjectCandidateReportingOverviewEntryProjectFieldRequest>(model);

                    this.InvokeService<IProjectManagementCommandService>(
                        service => service.UpdateProjectCandidateReportingOverviewEntryProjectField(request));

                    var updatedModel = new ProjectCandidateReportingOverviewRowModel
                    {
                        ReportStatuses =
                            this.InvokeService<IProjectManagementQueryService, List<ReportStatusView>>(
                                service => service.ListReportStatuses()),
                        ReportingOverviewEntry =
                            this
                                .InvokeService
                                <IProjectManagementQueryService, ProjectCandidateReportingOverviewEntryView>(
                                    service => service.RetrieveProjectCandidateReportingOverviewEntry(model.Id))
                    };
                    return PartialView("ProjectCandidateReportingOverviewEntry", updatedModel);
                }
                catch (Exception exception)
                {
                    //var faultException = exception as FaultException<ValidationContainer>;
                    //if (faultException != null && faultException.Detail.FaultDetail.FaultEntries.OfType<VersionMismatchFaultEntry>().Any())
                    //{
                    //    //Version mismatch
                    //    var projectCandidateOverviewEntryView = this.InvokeService<IProjectManagementQueryService, ProjectCandidateOverviewEntryView>(
                    //            service => service.RetrieveProjectCandidateOverviewEntry(model.Id, ProjectCandidateOverviewEntryType.ProjectCandidate));

                    //    var mismatchModel = new ProjectCandidateOverviewVersionMismatchActionModel
                    //        {
                    //            CurrentVersion = model,
                    //            ServerVersion = projectCandidateOverviewEntryView
                    //        };
                    //    Response.StatusCode = (int) HttpStatusCode.Conflict;
                    //    return PartialView("ProjectCandidateOverviewEntryProjectCandidateVersionMismatch", mismatchModel);
                    //}
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }
        
        public ActionResult RetrieveProjectCandidateRemarks(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectCandidate = this.InvokeService<IProjectManagementQueryService, ProjectCandidateView>(
                        service => service.RetrieveProjectCandidate(id));

                    var model = Mapper.DynamicMap<EditProjectCandidateRemarksModel>(projectCandidate);

                    return PartialView("ProjectCandidateRemarks", model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateProjectCandidateRemarks(EditProjectCandidateRemarksModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProjectCandidateRemarksRequest>(model);

                    this.InvokeService<IProjectManagementCommandService>(
                        service => service.UpdateProjectCandidateRemarks(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
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
