using System;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class UnplannedProjectCandidateEventModel
    {
        private Guid? _id;
        public Guid Id { get { return _id ?? (_id = Guid.NewGuid()).Value; } }
        public Guid ProjectCandidateId { get; set; }
        public Guid? SimulationCombinationId { get; set; }
        public string SimulationSetName { get; set; }
        public string SimulationDepartmentName { get; set; }
        public string SimulationLevelName { get; set; }
        public string SimulationName { get; set; }
        public string SimulationCombinationName { get; set; }
        public Guid? ProjectCandidateCategoryDetailTypeId { get; set; }
        public string ProjectCategoryDetailTypeName { get; set; }
        public Guid AssessmentRoomId { get; set; }
        public string ProjectCandidateFullName { get; set; }
        public Guid? ProgramComponentSpecialId { get; set; }
        public string ProgramComponentSpecialName { get; set; }
        public string ContactName { get; set; }
    }
}