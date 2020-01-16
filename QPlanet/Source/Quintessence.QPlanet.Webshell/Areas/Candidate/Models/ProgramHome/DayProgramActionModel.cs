using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramHome
{
    public class DayProgramActionModel
    {
        public DateTime Date { get; set; }

        public List<AssessmentRoomView> AssessmentRooms { get; set; }

        public List<DayPlanAssessorView> Assessors { get; set; }

        public List<AssessorColorView> Colors { get; set; }
    }
}