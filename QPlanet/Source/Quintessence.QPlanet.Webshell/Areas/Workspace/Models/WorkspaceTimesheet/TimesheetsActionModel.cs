using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QPlanet.Webshell.Areas.Workspace.Models.WorkspaceTimesheet
{
    public class TimesheetsActionModel
    {
        public bool IsProjectManager { get; set; }
        public List<TimesheetEntryView> RegisteredTimesheets { get; set; }

        public List<CrmTimesheetUnregisteredEntryView> UnregisteredTimesheets { get; set; }

        public List<ProjectView> PromaProjects { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public Guid UserId { get; set; }
    }
}