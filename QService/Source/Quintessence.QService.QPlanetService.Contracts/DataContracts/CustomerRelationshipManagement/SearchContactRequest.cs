using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement
{
    [DataContract]
    public class SearchContactRequest
    {
        [DataMember]
        public string CustomerName { get; set; }
    }
}
