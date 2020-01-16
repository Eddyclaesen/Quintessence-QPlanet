using System;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditActivityDetailTrainingCandidateModel : BaseEntityModel
    {
        public int ContactId { get; set; }
        public int CrmAppointmentId { get; set; }
        public Guid ActivityDetailTrainingId { get; set; }
        public Guid? CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ActivityDetailTrainingName { get; set; }
        public string AppointmentDescription { get; set; }
    }
}