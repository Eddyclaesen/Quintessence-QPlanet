using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public class ProjectRoleDictionaryLevelView
    {
        [DataMember, Key, Column(Order = 0)]
        public Guid ProjectRoleId { get; set; }

        [DataMember]
        public string ProjectRoleName { get; set; }

        [DataMember]
        public int? ContactId { get; set; }

        [DataMember, Key, Column(Order = 1)]
        public Guid DictionaryIndicatorId { get; set; }

        [DataMember]
        public string DictionaryIndicatorName { get; set; }

        [DataMember]
        public int DictionaryIndicatorOrder { get; set; }

        [DataMember]
        public Guid DictionaryLevelId { get; set; }

        [DataMember]
        public string DictionaryLevelName { get; set; }

        [DataMember]
        public int DictionaryLevelLevel { get; set; }

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
        public bool IsStandard { get; set; }

        [DataMember]
        public bool IsDistinctive { get; set; }
    }
}
