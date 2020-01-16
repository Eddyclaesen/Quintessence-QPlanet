using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.ViewModel.Cam;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramDetail;
using Quintessence.QPlanet.Webshell.Infrastructure;
using Quintessence.QPlanet.Webshell.Infrastructure.Controllers;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Controllers
{
    public class ProgramDetailController : QPlanetControllerBase
    {
        public ActionResult Edit(int officeId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var rooms = this.InvokeService<IInfrastructureQueryService, List<AssessmentRoomView>>(service => service.ListAssessmentRooms(new ListAssessmentRoomsRequest()));
                    var colors = this.InvokeService<IInfrastructureQueryService, List<AssessorColorView>>(service => service.ListAssessorColors());
                    var assessors = this.InvokeService<IProjectManagementQueryService, List<DayPlanAssessorView>>(service => service.ListProjectCandidateAssessorsForPlanning(officeId, new DateTime(year, month, day)));

                    var model = new EditActionModel();

                    model.Offices = rooms.Select(room => new { room.OfficeId, room.OfficeFullName, room.OfficeShortName }).Distinct().ToDictionary(office => office.OfficeId, office => string.Format("{0} ({1})", office.OfficeFullName, office.OfficeShortName));
                    model.OfficeId = officeId;
                    model.Date = new DateTime(year, month, day);
                    model.Colors = colors;
                    model.Assessors = assessors;

                    return View(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception);
                }
            }
        }

        public ActionResult EditDayProgram(int officeId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var date = new DateTime(year, month, day);

                    var assessmentRooms = this.InvokeService<IInfrastructureQueryService, List<AssessmentRoomView>>(service => service.ListAssessmentRooms(new ListAssessmentRoomsRequest { OfficeId = officeId }));

                    var model = new EditDayProgramActionModel();
                    model.AssessmentRooms = assessmentRooms;
                    model.Date = date;

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult Events(Guid roomId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var programComponents = this.InvokeService<ICandidateManagementQueryService, List<ProgramComponentView>>(service => service.RetrieveAssessmentRoomProgram(roomId, new DateTime(year, month, day)));

                    var events = programComponents.OrderBy(pc => pc.Start).Select(pc => new
                    {
                        id = pc.Id,
                        title = ProgramHomeController.CreateProgramComponentTitle(pc),
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

        public ActionResult ProjectCandidateEvents(Guid roomId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var unplannedProjectCandidateEvents = this.InvokeService<IProjectManagementQueryService, List<UnplannedProjectCandidateEvent>>(service => service.ListAvailableEvents(roomId, new DateTime(year, month, day)));

                    var model = new ProjectCandidateEventsActionModel();

                    model.UnplannedProjectCandidateEvents = unplannedProjectCandidateEvents.Select(Mapper.DynamicMap<UnplannedProjectCandidateEventModel>).ToList();
                    model.Date = new DateTime(year, month, day);

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult PlanSimulation(Guid projectCandidateId, Guid simulationCombinationId, Guid roomId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new CreateProgramComponentRequest
                        {
                            ProjectCandidateId = projectCandidateId,
                            SimulationCombinationId = simulationCombinationId,
                            AssessmentRoomId = roomId
                        };
                    this.InvokeService<ICandidateManagementCommandService>(service => service.CreateProgramComponent(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult PlanCategoryDetail(Guid projectCandidateId, Guid projectCandidateCategoryDetailTypeId, Guid roomId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new CreateProgramComponentRequest
                        {
                            ProjectCandidateId = projectCandidateId,
                            ProjectCandidateCategoryDetailTypeId = projectCandidateCategoryDetailTypeId,
                            AssessmentRoomId = roomId
                        };
                    this.InvokeService<ICandidateManagementCommandService>(service => service.CreateProgramComponent(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult PlanSpecial(Guid projectCandidateId, Guid programComponentSpecialId, Guid roomId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new CreateProgramComponentRequest
                        {
                            ProjectCandidateId = projectCandidateId,
                            ProgramComponentSpecialId = programComponentSpecialId,
                            AssessmentRoomId = roomId
                        };
                    this.InvokeService<ICandidateManagementCommandService>(service => service.CreateProgramComponent(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult UpdateProgramComponentEnd(UpdateProgramComponentEnd model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProgramComponentEndRequest>(model);
                    this.InvokeService<ICandidateManagementCommandService>(service => service.UpdateProgramComponentEnd(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult UpdateProgramComponentStart(UpdateProgramComponentStart model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProgramComponentStartRequest>(model);
                    this.InvokeService<ICandidateManagementCommandService>(service => service.UpdateProgramComponentStart(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult EditProgramComponent(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var programComponent = this.InvokeService<ICandidateManagementQueryService, ProgramComponentView>(service => service.RetrieveProgramComponent(id));
                    var assessors = this.InvokeService<IProjectManagementQueryService, List<DayPlanAssessorView>>(service => service.ListProjectCandidateAssessorsForPlanning(programComponent.AssessmentRoomOfficeId, programComponent.Start.Date));

                    var projectcandidate = this.InvokeService<IProjectManagementQueryService, ProjectCandidateView>(service => service.RetrieveProjectCandidate(programComponent.ProjectCandidateId));
                    var project = this.InvokeService<IProjectManagementQueryService, ProjectView>(service => service.RetrieveProject(projectcandidate.ProjectId));

                    var contact = this.InvokeService<ICustomerRelationshipManagementQueryService, CrmContactView>(service => service.RetrieveContactDetailInformation(project.ContactId));

                    var model = new EditProgramComponentActionModel();

                    model.ProgramComponent = Mapper.DynamicMap<EditProgramComponentModel>(programComponent);
                    model.Assessors = assessors;
                    model.ProgramComponent.ContactName = contact.FullName;

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
        public ActionResult EditProgramComponent(EditProgramComponentActionModel model)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = Mapper.DynamicMap<UpdateProgramComponentRequest>(model.ProgramComponent);
                    this.InvokeService<ICandidateManagementCommandService>(service => service.UpdateProgramComponent(request));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult CheckForCollisions(int officeid, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var programComponentIds = this.InvokeService<ICandidateManagementQueryService, List<Guid>>(service => service.CheckForProgramComponentCollisions(officeid, new DateTime(year, month, day)));

                    return Json(programComponentIds, JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult DeleteEvent(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ICandidateManagementCommandService>(service => service.DeleteProgramComponent(id));

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeError(exception);
                }
            }
        }

        public ActionResult RoomProjectCandidates(Guid roomId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var programComponents = this.InvokeService<ICandidateManagementQueryService, List<ProgramComponentView>>(service => service.ListProgramComponentsByRoom(roomId, new DateTime(year, month, day)));
                    var room = this.InvokeService<IInfrastructureQueryService, AssessmentRoomView>(service => service.RetrieveAssessmentRoom(roomId));

                    var model = new RoomProjectCandidatesActionModel();
                    model.Candidates = programComponents.Select(pc => new { pc.ProjectCandidateId, pc.CandidateFullName }).Distinct().ToDictionary(c => c.ProjectCandidateId, c => c.CandidateFullName);
                    model.AssessmentRoom = room;

                    return PartialView(model);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }

        public ActionResult UnplanCandidate(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ICandidateManagementCommandService>(service => service.DeleteProjectCandidateProgramComponents(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult ClearRoom(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    this.InvokeService<ICandidateManagementCommandService>(service => service.DeleteRoomProgramComponents(id));
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleStatusCodeErrorHtml(exception);
                }
            }
        }

        public ActionResult GenerateDayplan(Guid userId, int year, int month, int day)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new GenerateReportRequest
                    {
                        OutputFormat = ReportOutputFormat.Pdf,
                        ReportName = "/QPlanet/UserReporting/UserDayProgram",
                        Parameters = new Dictionary<string, string>
                            {
                                {"Date", new DateTime(year, month, day).ToString()},
                                {"UserId", userId.ToString()}
                            }
                    };
                    
                    var response = this.InvokeService<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));
                    return File(Convert.FromBase64String(response.Output), response.ContentType, string.Format("{0}{1}{2}.pdf", year, month, day));
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
