using System;
using System.Collections.Generic;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramHome
{
    public class CandidatesActionModel
    {
        public List<ProjectCandidateActionModel> Candidates { get; set; }

        public DateTime Date { get; set; }
    }
}