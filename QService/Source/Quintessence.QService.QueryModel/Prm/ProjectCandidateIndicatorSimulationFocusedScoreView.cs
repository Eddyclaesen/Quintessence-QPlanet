using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateIndicatorSimulationFocusedScoreView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public Guid SimulationCombinationId { get; set; }

        [DataMember]
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
        public Guid SimulationSetId { get; set; }

        [DataMember]
        public string SimulationSetName { get; set; }

        [DataMember]
        public Guid? SimulationDepartmentId { get; set; }

        [DataMember]
        public string SimulationDepartmentName { get; set; }

        [DataMember]
        public Guid? SimulationLevelId { get; set; }

        [DataMember]
        public string SimulationLevelName { get; set; }

        [DataMember]
        public Guid SimulationId { get; set; }

        [DataMember]
        public string SimulationName { get; set; }

        [DataMember]
        public decimal? Score { get; set; }

        [DataMember]
        public bool IsStandard { get; set; }

        [DataMember]
        public bool IsDistinctive { get; set; }
    }
}