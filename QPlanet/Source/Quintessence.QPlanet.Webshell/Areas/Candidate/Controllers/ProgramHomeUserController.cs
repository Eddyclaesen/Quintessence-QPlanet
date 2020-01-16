using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramHomeUser;
using Quintessence.QPlanet.Webshell.Infrastructure;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Controllers
{
    //[QPlanetAuthenticateController("CAM")]
    public class ProgramHomeUserController : QPlanetControllerBase
    {
        public ActionResult Index(Guid userId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var user = this.InvokeService<ISecurityQueryService, UserView>(service => service.RetrieveUser(userId));

                    var model = new IndexActionModel();

                    model.User = user;
                    model.Date = new DateTime(year, month, day);

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult UserDayProgram(Guid userId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var programComponents = this.InvokeService<ICandidateManagementQueryService, List<ProgramComponentView>>(service => service.ListProgramComponentsByDate(new DateTime(year, month, day)));
                    var colors = this.InvokeService<IInfrastructureQueryService, List<AssessorColorView>>(service => service.ListAssessorColors());

                    var model = new UserDayProgramActionModel();

                    model.UserId = userId;
                    model.Date = new DateTime(year, month, day);

                    model.UserProgramComponents = programComponents.Where(pc => pc.LeadAssessorUserId == userId || pc.CoAssessorUserId == userId).ToList();
                    model.Colors = colors;

                    var officeIds = model.UserProgramComponents.Select(pc => pc.AssessmentRoomOfficeId).Distinct();

                    var filteredProgramComponents = programComponents.Where(pc => officeIds.Contains(pc.AssessmentRoomOfficeId)).ToList();

                    model.Candidates = new List<DayProgramCandidateModel>();
                    foreach (var programComponent in filteredProgramComponents
                        .Where(programComponent => model.Candidates.All(c => c.ProjectCandidateId != programComponent.ProjectCandidateId)))
                    {
                        model.Candidates.Add(new DayProgramCandidateModel
                            {
                                ProjectCandidateId = programComponent.ProjectCandidateId,
                                FullName = programComponent.CandidateFullName
                            });
                    }

                    model.Assessors = new Dictionary<Guid, string>();
                    foreach (var pc in filteredProgramComponents.Where(pc => pc.LeadAssessorUserId.HasValue).Select(pc => new { pc.LeadAssessorUserId, pc.LeadAssessorUserFullName }))
                        if (!model.Assessors.ContainsKey(pc.LeadAssessorUserId.Value))
                            model.Assessors.Add(pc.LeadAssessorUserId.Value, pc.LeadAssessorUserFullName);
                    foreach (var pc in filteredProgramComponents.Where(pc => pc.CoAssessorUserId.HasValue).Select(pc => new { pc.CoAssessorUserId, pc.CoAssessorUserFullName }))
                        if (!model.Assessors.ContainsKey(pc.CoAssessorUserId.Value))
                            model.Assessors.Add(pc.CoAssessorUserId.Value, pc.CoAssessorUserFullName);

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult UserEvents(Guid userId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var start = new DateTime(year, month, day);
                    var end = start.AddDays(1).AddSeconds(-1);

                    var programComponents = this.InvokeService<ICandidateManagementQueryService, List<ProgramComponentView>>(service => service.ListProgramComponentsByUser(userId, start, end));

                    var events = programComponents.OrderBy(pc => pc.Start).Select(pc => new
                    {
                        id = pc.Id,
                        title = ProgramHomeController.CreateUserProgramComponentTitle(pc),
                        start = pc.Start.ToString("s"),
                        end = pc.End.ToString("s"),
                        allDay = false,
                        leadAssessorId = pc.LeadAssessorUserId,
                        coAssessorId = pc.CoAssessorUserId
                    }).ToArray();

                    return Json(events, JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult CandidateEvents(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var programComponents = this.InvokeService<ICandidateManagementQueryService, List<ProgramComponentView>>(service => service.ListProgramComponentsByCandidate(id));

                    var events = programComponents.OrderBy(pc => pc.Start).Select(pc => new
                    {
                        id = pc.Id,
                        title = ProgramHomeController.CreateCandidateProgramComponentTitle(pc),
                        start = pc.Start.ToString("s"),
                        end = pc.End.ToString("s"),
                        allDay = false,
                        leadAssessorId = pc.LeadAssessorUserId,
                        coAssessorId = pc.CoAssessorUserId
                    }).ToArray();

                    return Json(events, JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult GenerateDayProgramReport(Guid userId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new GenerateReportRequest
                    {
                        OutputFormat = ReportOutputFormat.Word,
                        ReportName = "/QPlanet/UserReporting/UserDayProgram",
                        Parameters = new Dictionary<string, string>
                            {
                                {"Date", new DateTime(year, month, day).ToString()},
                                {"UserId", userId.ToString()}
                            }
                    };
                    var response = this.InvokeService<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));
                    return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}_{1}.docx", userId, string.Format("{0}{1}{2}", year, month, day)));
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
