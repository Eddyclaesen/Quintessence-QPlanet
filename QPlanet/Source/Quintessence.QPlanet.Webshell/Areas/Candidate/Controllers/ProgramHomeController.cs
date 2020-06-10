using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramHome;
using Quintessence.QPlanet.Webshell.Infrastructure;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Controllers
{
    //[QPlanetAuthenticateController("CAM")]
    public class ProgramHomeController : QPlanetControllerBase
    {
        public ActionResult IndexQa()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var office = this.InvokeService<IInfrastructureQueryService, OfficeView>(service => service.RetrieveOffice(new RetrieveOfficeRequest { ShortName = "QA" }));
                    return View("ProgramHomeIndex", office);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult IndexQb()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var office = this.InvokeService<IInfrastructureQueryService, OfficeView>(service => service.RetrieveOffice(new RetrieveOfficeRequest { ShortName = "QB" }));
                    return View("ProgramHomeIndex", office);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult IndexQg()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var office = this.InvokeService<IInfrastructureQueryService, OfficeView>(service => service.RetrieveOffice(new RetrieveOfficeRequest { ShortName = "QG" }));
                    return View("ProgramHomeIndex", office);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        //ADDED PLANNING FOR CANDIDATES ON LOCATION
        public ActionResult IndexEx()
        {
            using (DurationLog.Create())
            {
                try
                {
                    var office = this.InvokeService<IInfrastructureQueryService, OfficeView>(service => service.RetrieveOffice(new RetrieveOfficeRequest { ShortName = "EX" }));
                    return View("ProgramHomeIndex", office);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult IndexOn()
        {
            using(DurationLog.Create())
            {
                try
                {
                    var office = this.InvokeService<IInfrastructureQueryService, OfficeView>(service => service.RetrieveOffice(new RetrieveOfficeRequest { ShortName = "ON" }));
                    return View("ProgramHomeIndex", office);
                }
                catch(Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult DayProgram(int officeId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var assessmentRooms = this.InvokeService<IInfrastructureQueryService, List<AssessmentRoomView>>(service => service.ListAssessmentRooms(new ListAssessmentRoomsRequest { OfficeId = officeId }));
                    var colors = this.InvokeService<IInfrastructureQueryService, List<AssessorColorView>>(service => service.ListAssessorColors());
                    var assessors = this.InvokeService<IProjectManagementQueryService, List<DayPlanAssessorView>>(service => service.ListProjectCandidateAssessorsForPlanning(officeId, new DateTime(year, month, day)));

                    var model = new DayProgramActionModel();

                    model.AssessmentRooms = assessmentRooms;
                    model.Date = new DateTime(year, month, day);
                    model.Assessors = assessors;
                    model.Colors = colors;

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult AssessmentRoomProgram(Guid assessmentRoomId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var programComponents = this.InvokeService<ICandidateManagementQueryService, List<ProgramComponentView>>(service => service.RetrieveAssessmentRoomProgram(assessmentRoomId, new DateTime(year, month, day)));
                    
                    var events = programComponents.OrderBy(pc => pc.Start).Select(pc => new
                    {
                        id = pc.Id,
                        title = CreateProgramComponentTitle(pc),
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
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult CheckForUnplannedEvents(int officeId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var messages = this.InvokeService<ICandidateManagementQueryService, Dictionary<string, List<string>>>(service => service.CheckForUnplannedEvents(officeId, new DateTime(year, month, day)));

                    var model = new CheckForUnplannedEventsActionModel();
                    model.Messages = messages;
                    model.OfficeId = officeId;
                    model.Date = new DateTime(year, month, day);

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult Candidates(int officeId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var programComponents = this.InvokeService<ICandidateManagementQueryService, List<ProgramComponentView>>(service => service.ListProgramComponentsByDate(new DateTime(year, month, day)));

                    var model = new CandidatesActionModel();
                    model.Candidates = new List<ProjectCandidateActionModel>();
                    model.Date = new DateTime(year, month, day);

                    foreach (var programComponent in programComponents)
                    {
                        if (model.Candidates.Any(c => c.ProjectCandidateId == programComponent.ProjectCandidateId))
                            continue;
                        
                        model.Candidates.Add(new ProjectCandidateActionModel
                            {
                                FirstName = programComponent.CandidateFirstName,
                                LastName = programComponent.CandidateLastName,
                                ProjectCandidateId = programComponent.ProjectCandidateId
                            });
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

        public ActionResult GenerateDayProgram(Guid projectCandidateId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new GenerateReportRequest
                    {
                        OutputFormat = ReportOutputFormat.Word,
                        ReportName = "/QPlanet/CandidateReporting/CandidateDayProgram",
                        Parameters = new Dictionary<string, string>
                            {
                                {"Date", new DateTime(year, month, day).ToString()},
                                {"ProjectCandidateId", projectCandidateId.ToString()}
                            }
                    };
                    var response = this.InvokeService<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));
                    return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}{1}{2}.docx", year, month, day));
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public static string CreateProgramComponentTitle(ProgramComponentView programComponent)
        {
            var builder = new StringBuilder();

            builder.Append(programComponent.CandidateFullName).Append("<br />").Append(programComponent.ContactName).Append("<br />");

            if (programComponent.SimulationCombinationId.HasValue)
            {
                builder.Append(programComponent.SimulationName)
                    .Append(programComponent.SimulationCombinationTypeCode == 1 ? " (Preparation)" : "(Execution)")
                    .Append("<br/>");
            }

            if (programComponent.ProjectCandidateCategoryDetailTypeId.HasValue)
                builder.Append(programComponent.ProjectCategoryDetailTypeName).Append("<br/>");

            if (programComponent.LeadAssessorUserId.HasValue)
                builder.Append(programComponent.LeadAssessorUserFullName).Append(" (Lead)<br/>");

            if (programComponent.CoAssessorUserId.HasValue)
                builder.Append(programComponent.CoAssessorUserFullName).Append(" (Co)<br/>");

            builder.Append(programComponent.Description);

            return builder.ToString();
        }

        public static string CreateUserProgramComponentTitle(ProgramComponentView programComponent)
        {
            var builder = new StringBuilder();

            builder.Append(programComponent.CandidateFullName).Append("<br />");

            if (programComponent.SimulationCombinationId.HasValue)
            {
                builder.Append(programComponent.SimulationName)
                    .Append(programComponent.SimulationCombinationTypeCode == 1 ? " (Preparation)" : "(Execution)")
                    .Append("<br/>");
            }

            if (programComponent.ProjectCandidateCategoryDetailTypeId.HasValue)
                builder.Append(programComponent.ProjectCategoryDetailTypeName).Append("<br/>");

            if (programComponent.LeadAssessorUserId.HasValue)
                builder.Append(programComponent.LeadAssessorUserFullName).Append(" (Lead)<br/>");

            if (programComponent.CoAssessorUserId.HasValue)
                builder.Append(programComponent.CoAssessorUserFullName).Append(" (Co)<br/>");

            builder.Append(programComponent.Description);

            builder.Append(programComponent.AssessmentRoomOfficeShortName).Append(" - ")
                .Append(programComponent.AssessmentRoomName).Append("<br/>");

            return builder.ToString();
        }

        public static string CreateCandidateProgramComponentTitle(ProgramComponentView programComponent)
        {
            var builder = new StringBuilder();

            if (programComponent.SimulationCombinationId.HasValue)
            {
                builder.Append(programComponent.SimulationName)
                    .Append(programComponent.SimulationCombinationTypeCode == 1 ? " (Preparation)" : "(Execution)")
                    .Append("<br/>");
            }

            if (programComponent.ProjectCandidateCategoryDetailTypeId.HasValue)
                builder.Append(programComponent.ProjectCategoryDetailTypeName).Append("<br/>");

            if (programComponent.LeadAssessorUserId.HasValue)
                builder.Append(programComponent.LeadAssessorUserFullName).Append(" (Lead)<br/>");

            if (programComponent.CoAssessorUserId.HasValue)
                builder.Append(programComponent.CoAssessorUserFullName).Append(" (Co)<br/>");

            builder.Append(programComponent.Description);

            builder.Append(programComponent.AssessmentRoomOfficeShortName).Append(" - ")
                .Append(programComponent.AssessmentRoomName).Append("<br/>");

            return builder.ToString();
        }
    }
}
