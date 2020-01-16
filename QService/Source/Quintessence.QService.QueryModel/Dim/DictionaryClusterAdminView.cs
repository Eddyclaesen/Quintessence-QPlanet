using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryClusterAdminView : ViewEntityBase
    {
        #region Simple

        [DataMember]
        public Guid DictionaryAdminId { get; set; }

        [DataMember]
        public Guid DictionaryId { get; set; }

        [DataMember]
        public string DictionaryName { get; set; }

        [DataMember]
        public int DictionaryNumberOfUsages { get; set; }

        [DataMember]
        public bool DictionaryIsLive { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public string ImageName { get; set; }

        #endregion

        #region Associations

        [DataMember]
        public DictionaryAdminView Dictionary { get; set; }

        [DataMember]
        public List<DictionaryCompetenceAdminView> DictionaryCompetences { get; set; }

        [DataMember]
        public List<DictionaryClusterTranslationAdminView> DictionaryClusterTranslations { get; set; }

        #endregion
    }
}
