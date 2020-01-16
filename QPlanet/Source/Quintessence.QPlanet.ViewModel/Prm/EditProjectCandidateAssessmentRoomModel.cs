using System;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateAssessmentRoomModel : BaseEntityModel
    {
        public Guid? AssessmentRoomId { get; set; }

        public string CandidateFullName { get; set; }
    }
}