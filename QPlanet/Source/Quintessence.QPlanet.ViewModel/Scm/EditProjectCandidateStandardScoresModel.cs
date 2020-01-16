using System;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditProjectCandidateStandardScoresModel : BaseEntityModel
    {
        public Guid SimulationCombinationId { get; set; }
        public string SimulationSetName { get; set; }
        public string SimulationDepartmentName { get; set; }
        public string SimulationLevelName { get; set; }
        public string SimulationName { get; set; }
        public Guid DictionaryIndicatorId { get; set; }
        public string DictionaryIndicatorName { get; set; }
        public Guid DictionaryCompetenceId { get; set; }
        public string DictionaryCompetenceName { get; set; }
        public Guid DictionaryClusterId { get; set; }
        public string DictionaryClusterName { get; set; }
        public decimal Score { get; set; }
    }
}
