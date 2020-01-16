using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement
{
    [DataContract]
    public class RetrieveOfficeRequest
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string ShortName { get; set; }
    }
}
