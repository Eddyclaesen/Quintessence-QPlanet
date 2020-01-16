using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryIndicatorView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public Guid DictionaryLevelId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool? IsStandard { get; set; }

        [DataMember]
        public bool? IsDistinctive { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public string Color { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryLevelView DictionaryLevel { get; set; }

        [DataMember]
        public List<DictionaryIndicatorTranslationView> DictionaryIndicatorTranslations { get; set; }

        #endregion
    }
}
