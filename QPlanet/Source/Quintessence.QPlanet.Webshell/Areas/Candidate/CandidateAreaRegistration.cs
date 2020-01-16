using System.Web.Mvc;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate
{
    public class CandidateAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Candidate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Candidate_ProgramHome_DayProgram",
                "Candidate/ProgramHome/DayProgram/{officeId}/{year}/{month}/{day}",
                new {controller = "ProgramHome", action = "DayProgram"});

            context.MapRoute(
                "Candidate_ProgramHome_AssessmentRoomProgram",
                "Candidate/ProgramHome/AssessmentRoomProgram/{assessmentRoomId}/{year}/{month}/{day}",
                new { controller = "ProgramHome", action = "AssessmentRoomProgram" });

            context.MapRoute(
                "Candidate_ProgramHome_CheckForUnplannedEvents",
                "Candidate/ProgramHome/CheckForUnplannedEvents/{officeId}/{year}/{month}/{day}",
                new { controller = "ProgramHome", action = "CheckForUnplannedEvents" });

            context.MapRoute(
                "Candidate_ProgramDetail_Edit",
                "Candidate/ProgramDetail/Edit/{officeId}/{year}/{month}/{day}",
                new { controller = "ProgramDetail", action = "Edit" });

            context.MapRoute(
                "Candidate_ProgramDetail_EditDayProgram",
                "Candidate/ProgramDetail/EditDayProgram/{officeId}/{year}/{month}/{day}",
                new { controller = "ProgramDetail", action = "EditDayProgram" });

            context.MapRoute(
                "Candidate_ProgramDetail_CheckForCollisions",
                "Candidate/ProgramDetail/CheckForCollisions/{officeId}/{year}/{month}/{day}",
                new { controller = "ProgramDetail", action = "CheckForCollisions" });

            context.MapRoute(
                "Candidate_ProgramDetail_Events",
                "Candidate/ProgramDetail/Events/{roomId}/{year}/{month}/{day}",
                new { controller = "ProgramDetail", action = "Events" });

            context.MapRoute(
                "Candidate_ProgramDetail_ProjectCandidateEvents",
                "Candidate/ProgramDetail/ProjectCandidateEvents/{roomId}/{year}/{month}/{day}",
                new { controller = "ProgramDetail", action = "ProjectCandidateEvents" });

            context.MapRoute(
                "Candidate_ProgramDetail_RoomProjectCandidates",
                "Candidate/ProgramDetail/RoomProjectCandidates/{roomId}/{year}/{month}/{day}",
                new { controller = "ProgramDetail", action = "RoomProjectCandidates" });

            context.MapRoute(
                "Candidate_ProgramDetail_EditRoomProgram",
                "Candidate/ProgramDetail/EditRoomProgram/{roomId}/{year}/{month}/{day}",
                new { controller = "ProgramDetail", action = "EditRoomProgram" });

            context.MapRoute(
                "Candidate_ProgramDetail_PlanSimulation",
                "Candidate/ProgramDetail/PlanSimulation/{projectCandidateId}/{simulationCombinationId}/{roomId}",
                new { controller = "ProgramDetail", action = "PlanSimulation" });

            context.MapRoute(
                "Candidate_ProgramDetail_PlanCategoryDetail",
                "Candidate/ProgramDetail/PlanCategoryDetail/{projectCandidateId}/{projectCandidateCategoryDetailTypeId}/{roomId}",
                new { controller = "ProgramDetail", action = "PlanCategoryDetail" });

            context.MapRoute(
                "Candidate_ProgramDetail_PlanSpecial",
                "Candidate/ProgramDetail/PlanSpecial/{projectCandidateId}/{programComponentSpecialId}/{roomId}",
                new { controller = "ProgramDetail", action = "PlanSpecial" });

            context.MapRoute(
                "Candidate_ProgramHomeUser_Index",
                "Candidate/ProgramHomeUser/Index/{userId}/{year}/{month}/{day}",
                new { controller = "ProgramHomeUser", action = "Index" });

            context.MapRoute(
                "Candidate_ProgramHomeUser_UserDayProgram",
                "Candidate/ProgramHomeUser/UserDayProgram/{userId}/{year}/{month}/{day}",
                new { controller = "ProgramHomeUser", action = "UserDayProgram" });

            context.MapRoute(
                "Candidate_ProgramHomeUser_UserEvents",
                "Candidate/ProgramHomeUser/UserEvents/{userId}/{year}/{month}/{day}",
                new { controller = "ProgramHomeUser", action = "UserEvents" });

            context.MapRoute(
                "Candidate_ProgramHomeUser_GenerateDayProgramReport",
                "Candidate/ProgramHomeUser/GenerateDayProgramReport/{userId}/{year}/{month}/{day}",
                new { controller = "ProgramHomeUser", action = "GenerateDayProgramReport" });

            context.MapRoute(
                "Candidate_ProgramDetail_GenerateDayplan",
                "Candidate/ProgramDetail/GenerateDayplan/{userId}/{year}/{month}/{day}/Program.pdf",
                new { controller = "ProgramDetail", action = "GenerateDayplan" });

            context.MapRoute(
                "Candidate_ProgramHome_Candidates",
                "Candidate/ProgramHome/Candidates/{officeId}/{year}/{month}/{day}",
                new { controller = "ProgramHome", action = "Candidates" });

            context.MapRoute(
                "Candidate_ProgramHome_GenerateDayplan",
                "Candidate/ProgramHome/GenerateDayplan/{projectCandidateId}/{year}/{month}/{day}",
                new { controller = "ProgramHome", action = "GenerateDayplan" });

            context.MapRoute(
                "Candidate_default",
                "Candidate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
