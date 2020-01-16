using System;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class AddProjectCandidateModel
    {
        public Guid ProjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Code { get; set; }
        public int AppointmentId { get; set; }
        public int LanguageId { get; set; }
    }
}
