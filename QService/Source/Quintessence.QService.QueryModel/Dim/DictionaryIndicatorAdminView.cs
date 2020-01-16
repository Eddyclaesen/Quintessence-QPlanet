using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryIndicatorAdminView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public Guid DictionaryLevelAdminId { get; set; }

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
        public Guid DictionaryLevelId { get; set; }

        [DataMember]
        public string DictionaryLevelName { get; set; }

        [DataMember]
        public int DictionaryLevelLevel { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool? IsStandard { get; set; }

        [DataMember]
        public bool? IsDistinctive { get; set; }

        [DataMember]
        public int Order { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryLevelAdminView DictionaryLevel { get; set; }

        [DataMember]
        public List<DictionaryIndicatorTranslationAdminView> DictionaryIndicatorTranslations { get; set; }

        #endregion
    }
}
