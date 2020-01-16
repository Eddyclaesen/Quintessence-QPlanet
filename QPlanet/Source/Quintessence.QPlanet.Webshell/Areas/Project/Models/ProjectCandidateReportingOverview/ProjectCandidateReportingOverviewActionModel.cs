using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectCandidateReportingOverview
{
    public class ProjectCandidateReportingOverviewActionModel
    {
        public List<ProjectCandidateReportingOverviewEntryView> ReportingOverviewEntries { get; set; }

        public List<ReportStatusView> ReportStatuses { get; set; }

        public List<UserView> Users { get; set; }

    }
}