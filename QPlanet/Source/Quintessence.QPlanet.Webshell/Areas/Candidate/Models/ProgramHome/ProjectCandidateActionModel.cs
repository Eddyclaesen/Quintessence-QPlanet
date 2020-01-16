using System;

namespace Quintessence.QPlanet.Webshell.Areas.Candidate.Models.ProgramHome
{
    public class ProjectCandidateActionModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ProjectCandidateId { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
    }
}