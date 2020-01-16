using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryImportView
    {
        [DataMember]
        public int? ContactId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<DictionaryClusterImportView> DictionaryClusters { get; set; }
    }
}
