using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryIndicatorTranslationView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public Guid DictionaryIndicatorId { get; set; }

        [DataMember]
        public string Text { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public LanguageView Language { get; set; }

        [DataMember]
        public DictionaryIndicatorView DictionaryIndicator { get; set; }

        #endregion
    }
}
