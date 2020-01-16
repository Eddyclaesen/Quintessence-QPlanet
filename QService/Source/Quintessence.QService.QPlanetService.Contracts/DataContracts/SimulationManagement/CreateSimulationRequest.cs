using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement
{
    [DataContract]
    public class CreateSimulationRequest
    {
        [DataMember]
        public string Name { get; set; }
    }
}