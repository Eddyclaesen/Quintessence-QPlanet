using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement
{
    [DataContract]
    public class UpdateSimulationLevelRequest : UpdateRequestBase
    {
        [DataMember]
        public string Name { get; set; }
    }
}