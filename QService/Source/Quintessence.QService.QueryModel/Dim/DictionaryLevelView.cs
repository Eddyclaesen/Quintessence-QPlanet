using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryLevelView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public Guid DictionaryCompetenceId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Level { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryCompetenceView DictionaryCompetence { get; set; }

        [DataMember]
        public List<DictionaryLevelTranslationView> DictionaryLevelTranslations { get; set; }

        [DataMember]
        public List<DictionaryIndicatorView> DictionaryIndicators { get; set; }

        #endregion
    }
}
