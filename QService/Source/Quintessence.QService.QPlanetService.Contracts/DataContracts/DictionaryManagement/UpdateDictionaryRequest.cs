using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class UpdateDictionaryRequest : UpdateRequestBase
    {
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool Current { get; set; }

        [DataMember]
        public bool IsLive { get; set; }
    }
}