using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryCompetenceTranslationAdminView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public Guid DictionaryCompetenceAdminId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Description { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryCompetenceAdminView DictionaryCompetenceAdmin { get; set; }

        #endregion
    }
}
