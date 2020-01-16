using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract]
    public class DictionaryIndicatorMatrixEntryView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public int DictionaryIndicatorOrder { get; set; }

        [DataMember]
        public string DictionaryIndicatorName { get; set; }

        [DataMember]
        public Guid DictionaryLevelId { get; set; }

        [DataMember]
        public int DictionaryLevelLevel { get; set; }

        [DataMember]
        public string DictionaryLevelName { get; set; }

        [DataMember]
        public Guid DictionaryCompetenceId { get; set; }

        [DataMember]
        public string DictionaryCompetenceName { get; set; }

        [DataMember]
        public int DictionaryCompetenceOrder { get; set; }

        [DataMember]
        public Guid DictionaryClusterId { get; set; }

        [DataMember]
        public string DictionaryClusterName { get; set; }

        [DataMember]
        public int DictionaryClusterOrder { get; set; }

        [DataMember]
        public Guid DictionaryId { get; set; }

        [DataMember]
        public string DictionaryName { get; set; }

        [DataMember]
        public string DictionaryIndicatorColor { get; set; }
    }
}