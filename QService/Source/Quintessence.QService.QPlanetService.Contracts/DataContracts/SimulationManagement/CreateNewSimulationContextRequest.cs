using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement
{
    [DataContract]
    public class CreateNewSimulationContextRequest
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string UserNameBase { get; set; }

        [DataMember]
        public int PasswordLength { get; set; }
    }
}