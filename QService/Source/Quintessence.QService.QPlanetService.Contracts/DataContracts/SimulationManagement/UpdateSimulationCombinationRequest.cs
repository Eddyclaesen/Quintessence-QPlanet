using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement
{
    [DataContract]
    public class UpdateSimulationCombinationRequest : UpdateRequestBase
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

        [DataMember]
        public List<int> AvailableLanguageIds { get; set; }
    }
}