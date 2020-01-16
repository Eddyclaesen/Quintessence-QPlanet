using System.Web.Mvc;

namespace Quintessence.QPlanet.Webshell.Areas.Workspace
{
    public class WorkspaceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Workspace"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Workspace_WorkspaceTimesheet_Timesheets",
                "Workspace/WorkspaceTimesheet/Timesheets/{year}/{month}/{userId}",
                new { controller = "WorkspaceTimesheet", action = "Timesheets" });

            context.MapRoute(
                "Workspace_WorkspaceDayProgram_Events",
                "Workspace/WorkspaceDayProgram/Events/{start}/{end}",
                new { controller = "WorkspaceDayProgram", action = "Events" });

            context.MapRoute(
                "Workspace_default",
                "Workspace/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
