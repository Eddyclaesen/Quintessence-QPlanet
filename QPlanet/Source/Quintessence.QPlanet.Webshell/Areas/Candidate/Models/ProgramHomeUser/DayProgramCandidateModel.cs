using System;
using System.Web.Mvc;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramHomeUser
{
    public class DayProgramCandidateModel
    {
        public Guid ProjectCandidateId { get; set; }

        public string FullName { get; set; }

        public JsonResult Events { get; set; }
    }
}