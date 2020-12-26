using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCategoryDetailSimulationCombinationView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ProjectCategoryDetailId { get; set; }

        [DataMember]
        public Guid SimulationCombinationId { get; set; }

        [DataMember]
        public Guid SimulationSetId { get; set; }

        [DataMember]
        public string SimulationSetName { get; set; }

        [DataMember]
        public Guid? SimulationDepartmentId { get; set; }

        [DataMember]
        public string SimulationDepartmentName { get; set; }

        [DataMember]
        public Guid? SimulationLevelId { get; set; }

        [DataMember]
        public string SimulationLevelName { get; set; }

        [DataMember]
        public Guid SimulationId { get; set; }

        [DataMember]
        public string SimulationName { get; set; }

        [DataMember]
        public int Preparation { get; set; }

        [DataMember]
        public int Execution { get; set; }

        [DataMember]
        public string LanguageNames { get; set; }

        [DataMember]
        public string QCandidateLayout { get; set; }
    }
}
