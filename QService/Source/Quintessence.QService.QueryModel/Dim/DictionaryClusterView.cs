using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryClusterView : ViewEntityBase
    {
        #region Simple properties

        [DataMember]
        public Guid DictionaryId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public string ImageName { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryView Dictionary { get; set; }

        [DataMember]
        public List<DictionaryClusterTranslationView> DictionaryClusterTranslations { get; set; }

        [DataMember]
        public List<DictionaryCompetenceView> DictionaryCompetences { get; set; }

        #endregion
    }
}
