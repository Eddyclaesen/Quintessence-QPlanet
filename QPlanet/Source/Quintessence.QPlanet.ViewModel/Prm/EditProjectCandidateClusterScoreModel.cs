using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateClusterScoreModel : BaseEntityModel
    {
        public DictionaryClusterView DictionaryCluster { get; set; }

        public decimal? Score { get; set; }

        [AllowHtml]
        public string Statement { get; set; }

        public List<EditProjectCandidateCompetenceScoreModel> ProjectCandidateCompetenceScores { get; set; }

        public string GetDictionaryClusterName(int languageId)
        {
            var translation = DictionaryCluster.DictionaryClusterTranslations.FirstOrDefault(t => t.LanguageId == languageId);
            return translation != null ? translation.Text : DictionaryCluster.Name;
        }
    }
}
