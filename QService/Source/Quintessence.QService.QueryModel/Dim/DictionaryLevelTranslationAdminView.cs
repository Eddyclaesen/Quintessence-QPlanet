using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryLevelTranslationAdminView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public Guid DictionaryLevelAdminId { get; set; }

        [DataMember]
        public string Text { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryLevelAdminView DictionaryLevelAdmin { get; set; }

        #endregion
    }
}