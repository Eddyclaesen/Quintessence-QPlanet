using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Webshell.Areas.Candidate.Controllers;
using Quintessence.QPlanet.Webshell.Infrastructure;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.WorkspaceManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Cam;

namespace Quintessence.QPlanet.Webshell.Areas.Workspace.Controllers
{
    public class WorkspaceDayProgramController : QPlanetControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Events(int? start, int? end)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var authenticationToken = GetAuthenticationToken();

                    var startDate = start.HasValue ? new DateTime(1970, 1, 1).AddSeconds(start.Value) : DateTime.Now.Date;
                    var endDate = end.HasValue ? new DateTime(1970, 1, 1).AddSeconds(end.Value) : DateTime.Now.Date.AddDays(1).AddSeconds(-1);

                    var request = new ListProgramComponentsRequest
                        {
                            UserId = authenticationToken.UserId,
                            Start = startDate,
                            End = endDate
                        };

                    var programComponents = this.InvokeService<IWorkspaceManagementQueryService, List<ProgramComponentView>>(service => service.ListProgramComponents(request));

                    //Determine the view by the range
                    if ((endDate - startDate).Days > 7)
                    {
                        //if (month view only show all day events
                        var events = programComponents
                            .Select(pc => new { pc.Start.Date, pc.AssessmentRoomOfficeShortName })
                            .Distinct()
                            .Select(d => new
                                {
                                    id = d.Date.ToShortDateString(),
                                    start = d.Date.ToString("s"),
                                    title = string.Format("Assessment {0}", d.AssessmentRoomOfficeShortName),
                                    allDay = true,
                                    url = Url.Action("Index", "ProgramHomeUser", new { area = "Candidate", userId = authenticationToken.UserId.ToString(), year = d.Date.Year, month = d.Date.Month, day = d.Date.Day }),
                                    urlTarget = string.Format("ProgramHomeUser{0}{1}{2}", d.Date.Year, d.Date.Month, d.Date.Day)
                                });

                        events = events.Concat(programComponents.Select(pc => new { pc.Start.Date, pc.ProjectCandidateId, pc.CandidateFullName })
                            .Distinct()
                            .Select(d => new
                                {
                                    id = d.Date.ToShortDateString(),
                                    start = d.Date.ToString("s"),
                                    title = d.CandidateFullName,
                                    allDay = true,
                                    url = Url.Action("EditProjectCandidateSimulationScores", "ProjectAssessmentDevelopment", new { area = "Project", id = d.ProjectCandidateId }),
                                    urlTarget = d.ProjectCandidateId.ToString()
                                }));

                        return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //Show detailed events
                        var events = programComponents.Select(pc => new { pc.Start.Date, pc.ProjectCandidateId, pc.CandidateFullName })
                            .Distinct()
                            .Select(d => new
                            {
                                id = Guid.NewGuid(),
                                title = d.CandidateFullName,
                                start = d.Date.ToString("s"),
                                end = string.Empty,
                                allDay = true,
                                url = Url.Action("EditProjectCandidateSimulationScores", "ProjectAssessmentDevelopment", new { area = "Project", id = d.ProjectCandidateId }),
                                urlTarget = d.ProjectCandidateId.ToString()
                            });

                        events = events.Concat(programComponents.Select(pc => new
                        {
                            id = pc.Id,
                            title = ProgramHomeController.CreateProgramComponentTitle(pc),
                            start = pc.Start.ToString("s"),
                            end = pc.End.ToString("s"),
                            allDay = false,
                            url = Url.Action("Index", "ProgramHomeUser", new { area = "Candidate", userId = authenticationToken.UserId.ToString(), year = pc.Start.Year, month = pc.Start.Month, day = pc.Start.Day }),
                            urlTarget = string.Format("ProgramHomeUser{0}{1}{2}", pc.Start.Year, pc.Start.Month, pc.Start.Day)
                        }));

                        return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }
    }
}