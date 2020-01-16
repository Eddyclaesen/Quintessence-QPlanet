using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateIndicatorSimulationScoreModel : BaseEntityModel
    {
        public Guid ProjectCandidateId { get; set; }

        public Guid DictionaryIndicatorId { get; set; }

        public string DictionaryIndicatorName { get; set; }

        public Guid DictionaryLevelId { get; set; }

        public string DictionaryLevelName { get; set; }

        public Guid DictionaryCompetenceId { get; set; }

        public Guid DictionaryClusterId { get; set; }

        public Guid SimulationSetId { get; set; }

        public Guid? SimulationDepartmentId { get; set; }

        public Guid? SimulationLevelId { get; set; }

        public Guid SimulationId { get; set; }

        public bool IsChanged { get; set; }

        [DisplayFormat(DataFormatString = "{0:G29}", ApplyFormatInEditMode = true)]
        public decimal? Score { get; set; }
    }
}