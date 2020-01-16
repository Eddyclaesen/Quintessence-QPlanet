using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryIndicatorTranslationAdminView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public Guid DictionaryIndicatorAdminId { get; set; }

        [DataMember]
        public string Text { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryIndicatorAdminView DictionaryIndicatorAdmin { get; set; }

        #endregion
    }
}
