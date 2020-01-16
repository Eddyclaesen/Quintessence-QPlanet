using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class UpdateDictionaryIndicatorRequest : UpdateRequestBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public bool? IsStandard { get; set; }

        [DataMember]
        public bool? IsDistinctive { get; set; }

        [DataMember]
        public List<UpdateDictionaryIndicatorTranslationRequest> DictionaryIndicatorTranslations { get; set; }
    }
}