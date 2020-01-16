using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Cam
{
    public class ProgramComponent : EntityBase
    {
        public Guid ProjectCandidateId { get; set; }
        public Guid AssessmentRoomId { get; set; }
        public Guid? SimulationCombinationId { get; set; }
        public int SimulationCombinationTypeCode { get; set; }
        public Guid? ProjectCandidateCategoryDetailTypeId { get; set; }
        public Guid? LeadAssessorUserId { get; set; }
        public Guid? CoAssessorUserId { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
