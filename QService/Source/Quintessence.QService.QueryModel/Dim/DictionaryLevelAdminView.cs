using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryLevelAdminView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public Guid DictionaryCompetenceAdminId { get; set; }

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
        public Guid DictionaryCompetenceId { get; set; }

        [DataMember]
        public string DictionaryCompetenceName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Level { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryCompetenceAdminView DictionaryCompetence { get; set; }

        [DataMember]
        public List<DictionaryIndicatorAdminView> DictionaryIndicators { get; set; }

        [DataMember]
        public List<DictionaryLevelTranslationAdminView> DictionaryLevelTranslations { get; set; }

        #endregion
    }
}
