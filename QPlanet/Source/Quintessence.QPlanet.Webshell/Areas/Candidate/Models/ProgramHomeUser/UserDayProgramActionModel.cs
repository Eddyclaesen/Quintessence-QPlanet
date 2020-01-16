using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramHomeUser
{
    public class UserDayProgramActionModel
    {
        public List<ProgramComponentView> UserProgramComponents { get; set; }

        public Dictionary<Guid, string> Assessors { get; set; }

        public List<DayProgramCandidateModel> Candidates { get; set; }

        public List<AssessorColorView> Colors { get; set; }

        public JsonResult UserEvents { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
    }
}