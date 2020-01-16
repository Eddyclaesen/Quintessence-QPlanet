using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement
{
    [DataContract]
    public class SearchUserRequest
    {
        [DataMember]
        public string Name { get; set; }
    }
}