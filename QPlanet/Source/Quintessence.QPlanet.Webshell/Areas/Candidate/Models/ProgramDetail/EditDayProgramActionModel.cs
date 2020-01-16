using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramDetail
{
    public class EditDayProgramActionModel
    {
        public List<AssessmentRoomView> AssessmentRooms { get; set; }

        public DateTime Date { get; set; }
    }
}