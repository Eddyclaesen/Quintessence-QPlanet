using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement
{
    [DataContract]
    public class UpdateContactDetailRequest : UpdateRequestBase
    {
        [DataMember]
        public string Remarks { get; set; }
    }
}
