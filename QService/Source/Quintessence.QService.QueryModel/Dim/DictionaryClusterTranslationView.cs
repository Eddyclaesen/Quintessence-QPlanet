using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryClusterTranslationView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public Guid DictionaryClusterId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Description { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryClusterView DictionaryCluster { get; set; }

        [DataMember]
        public LanguageView Language { get; set; }

        #endregion
    }
}
