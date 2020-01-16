using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public int? ContactId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool Current { get; set; }

        [DataMember]
        public bool IsLive { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public CrmContactView Contact { get; set; }

        [DataMember]
        public List<DictionaryClusterView> DictionaryClusters { get; set; }

        #endregion
    }
}
