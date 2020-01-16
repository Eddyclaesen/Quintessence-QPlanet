using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement
{
    [DataContract]
    public class ListAssessmentRoomsRequest
    {
        [DataMember]
        public int? OfficeId { get; set; }
    }
}