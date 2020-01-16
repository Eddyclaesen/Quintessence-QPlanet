using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryCompetenceAdminView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public Guid DictionaryClusterAdminId { get; set; }

        [DataMember]
        public Guid DictionaryId { get; set; }

        [DataMember]
        public string DictionaryName { get; set; }

        [DataMember]
        public int DictionaryNumberOfUsages { get; set; }

        [DataMember]
        public bool DictionaryIsLive { get; set; }

        [DataMember]
        public Guid DictionaryClusterId { get; set; }

        [DataMember]
        public string DictionaryClusterName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Order { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryClusterAdminView DictionaryCluster { get; set; }

        [DataMember]
        public List<DictionaryLevelAdminView> DictionaryLevels { get; set; }

        [DataMember]
        public List<DictionaryCompetenceTranslationAdminView> DictionaryCompetenceTranslations { get; set; }

        #endregion
    }
}
