using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCategoryDetailDictionaryIndicatorView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ProjectCategoryDetailId { get; set; }

        [DataMember]
        public Guid DictionaryIndicatorId { get; set; }

        [DataMember]
        public string DictionaryIndicatorName { get; set; }

        [DataMember]
        public int DictionaryIndicatorOrder { get; set; }

        [DataMember]
        public string DictionaryLevelName { get; set; }

        [DataMember]
        public Guid DictionaryCompetenceId { get; set; }

        [DataMember]
        public string DictionaryCompetenceName { get; set; }

        [DataMember]
        public int DictionaryCompetenceOrder { get; set; }

        [DataMember]
        public string DictionaryClusterName { get; set; }

        [DataMember]
        public int DictionaryClusterOrder { get; set; }

        [DataMember]
        public Guid DictionaryClusterId { get; set; }

        [DataMember]
        public Guid DictionaryLevelId { get; set; }

        [DataMember]
        public int DictionaryLevelLevel { get; set; }

        [DataMember]
        public bool IsDefinedByRole { get; set; }

        [DataMember]
        public bool? IsStandard { get; set; }

        [DataMember]
        public bool? IsDistinctive { get; set; }
    }
}
