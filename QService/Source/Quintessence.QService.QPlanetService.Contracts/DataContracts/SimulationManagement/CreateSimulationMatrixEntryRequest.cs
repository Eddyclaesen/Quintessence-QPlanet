using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement
{
    [DataContract]
    public class CreateSimulationMatrixEntryRequest
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
        public int QCandidateLayoutId { get; set; }
    }
}