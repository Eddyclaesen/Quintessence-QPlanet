using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Sim
{
    [DataContract(IsReference = true)]
    public class SimulationCombinationView : ViewEntityBase
    {
        [DataMember]
        public Guid SimulationSetId { get; set; }

        [DataMember]
        public Guid? SimulationDepartmentId { get; set; }

        [DataMember]
        public Guid? SimulationLevelId { get; set; }

        [DataMember]
        public Guid SimulationId { get; set; }

        [DataMember]
        public int Preparation { get; set; }

        [DataMember]
        public int Execution { get; set; }

        [DataMember]
        public string LanguageNames { get; set; }

        [DataMember] 
        public int QCandidateLayoutId { get; set; }

        [DataMember] 
        public Guid? PredecessorId { get; set; }
    }
}
