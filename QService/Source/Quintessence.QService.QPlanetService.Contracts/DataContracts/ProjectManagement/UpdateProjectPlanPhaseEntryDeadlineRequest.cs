using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectPlanPhaseEntryDeadlineRequest : UpdateRequestBase
    {
        [DataMember]
        public DateTime Deadline { get; set; }

        [DataMember]
        public string Notes { get; set; }
    }
}