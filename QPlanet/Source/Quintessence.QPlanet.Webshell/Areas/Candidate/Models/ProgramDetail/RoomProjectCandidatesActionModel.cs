using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramDetail
{
    public class RoomProjectCandidatesActionModel
    {
        public Dictionary<Guid, string> Candidates { get; set; }

        public AssessmentRoomView AssessmentRoom { get; set; }
    }
}