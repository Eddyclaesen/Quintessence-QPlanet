using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class ImportDictionaryRequest
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public List<ImportDictionaryClusterRequest> DictionaryClusters { get; set; }
    }
}