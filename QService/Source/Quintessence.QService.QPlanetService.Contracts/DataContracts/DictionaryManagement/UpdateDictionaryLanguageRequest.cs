using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class UpdateDictionaryLanguageRequest : UpdateRequestBase
    {
        [DataMember]
        public List<UpdateDictionaryClusterRequest> DictionaryClusters { get; set; }
    }
}