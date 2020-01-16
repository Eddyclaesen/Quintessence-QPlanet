﻿using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryCompetenceTranslationView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public Guid DictionaryCompetenceId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Description { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryCompetenceView DictionaryCompetence { get; set; }

        [DataMember]
        public LanguageView Language { get; set; }

        #endregion
    }
}
