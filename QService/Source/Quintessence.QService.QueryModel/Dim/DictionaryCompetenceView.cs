using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryCompetenceView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public Guid DictionaryClusterId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Order { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryClusterView DictionaryCluster { get; set; }

        [DataMember]
        public List<DictionaryCompetenceTranslationView> DictionaryCompetenceTranslations { get; set; }

        [DataMember]
        public List<DictionaryLevelView> DictionaryLevels { get; set; }

        #endregion
    }
}
