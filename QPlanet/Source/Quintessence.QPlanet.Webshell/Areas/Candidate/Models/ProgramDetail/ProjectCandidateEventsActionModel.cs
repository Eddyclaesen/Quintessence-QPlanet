using System;
using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramDetail
{
    public class ProjectCandidateEventsActionModel
    {
        public List<UnplannedProjectCandidateEventModel> UnplannedProjectCandidateEvents { get; set; }

        public DateTime Date { get; set; }
    }
}