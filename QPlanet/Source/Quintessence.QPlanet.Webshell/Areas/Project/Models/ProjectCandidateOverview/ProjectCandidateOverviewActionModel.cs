using System.Collections.Generic;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectCandidateOverview
{
    public class ProjectCandidateOverviewActionModel
    {
        public List<ProjectCandidateOverviewEntryView> ProjectCandidateOverviewEntries { get; set; }

        public int TotalEntries { get; set; }
    }
}