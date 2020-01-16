using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateCompetenceSimulationFocusedScoreModel : BaseEntityModel
    {
        private List<EditProjectCandidateIndicatorSimulationScoreModel> _standardIndicators;
        private List<EditProjectCandidateIndicatorSimulationScoreModel> _distinctiveIndicators;

        public Guid DictionaryClusterId { get; set; }

        public string DictionaryClusterName { get; set; }

        public Guid DictionaryCompetenceId { get; set; }

        public string DictionaryCompetenceName { get; set; }

        public List<EditProjectCandidateIndicatorSimulationScoreModel> StandardIndicators
        {
            get { return _standardIndicators ?? (_standardIndicators = new List<EditProjectCandidateIndicatorSimulationScoreModel>()); }
            set { _standardIndicators = value; }
        }

        public List<EditProjectCandidateIndicatorSimulationScoreModel> DistinctiveIndicators
        {
            get { return _distinctiveIndicators ?? (_distinctiveIndicators = new List<EditProjectCandidateIndicatorSimulationScoreModel>()); }
            set { _distinctiveIndicators = value; }
        }

        public List<EditProjectCandidateIndicatorSimulationScoreModel> Indicators
        {
            get { return StandardIndicators.Concat(DistinctiveIndicators).ToList(); }
        }

        [DisplayFormat(DataFormatString = "{0:G29}", ApplyFormatInEditMode = true)]
        public decimal? Score { get; set; }

        public bool IsChanged { get; set; }

        [AllowHtml]
        public string Remarks { get; set; }
    }
}