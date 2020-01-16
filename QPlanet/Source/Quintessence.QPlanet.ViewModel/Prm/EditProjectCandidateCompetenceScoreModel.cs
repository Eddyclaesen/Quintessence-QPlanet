using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateCompetenceScoreModel : BaseEntityModel
    {
        public DictionaryCompetenceView DictionaryCompetence { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:G29}", ApplyFormatInEditMode = true)]
        public decimal? Score { get; set; }

        [AllowHtml]
        public string Statement { get; set; }

        public List<EditProjectCandidateIndicatorScoreModel> ProjectCandidateIndicatorScores { get; set; }

        public string GetDictionaryCompetenceName(int languageId)
        {
            var translation = DictionaryCompetence.DictionaryCompetenceTranslations.FirstOrDefault(t => t.LanguageId == languageId);
            return translation != null ? translation.Text : DictionaryCompetence.Name;
        }
    }
}