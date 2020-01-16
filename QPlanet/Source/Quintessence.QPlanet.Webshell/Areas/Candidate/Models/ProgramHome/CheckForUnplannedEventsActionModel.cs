using System;
using System.Collections.Generic;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramHome
{
    public class CheckForUnplannedEventsActionModel
    {
        public DateTime Date { get; set; }

        public Dictionary<string, List<string>> Messages { get; set; }

        public int OfficeId { get; set; }
    }
}