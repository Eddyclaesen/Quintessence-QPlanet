using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateCompetenceSimulationScoreModel : BaseEntityModel
    {
        private List<EditProjectCandidateIndicatorSimulationScoreModel> _indicators;

        public Guid DictionaryClusterId { get; set; }

        public string DictionaryClusterName { get; set; }

        public Guid DictionaryCompetenceId { get; set; }

        public string DictionaryCompetenceName { get; set; }

        public List<EditProjectCandidateIndicatorSimulationScoreModel> Indicators
        {
            get { return _indicators ?? (_indicators = new List<EditProjectCandidateIndicatorSimulationScoreModel>()); }
            set { _indicators = value; }
        }

        [DisplayFormat(DataFormatString = "{0:G29}", ApplyFormatInEditMode = true)]
        public decimal? Score { get; set; }

        public bool IsChanged { get; set; }

        [AllowHtml]
        public string Remarks { get; set; }
    }
}