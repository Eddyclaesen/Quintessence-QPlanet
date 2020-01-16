using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramDetail
{
    public class EditActionModel
    {
        public Dictionary<int, string> Offices { get; set; }

        public int OfficeId { get; set; }

        public DateTime Date { get; set; }

        public List<AssessorColorView> Colors { get; set; }

        public List<DayPlanAssessorView> Assessors { get; set; }
    }
}