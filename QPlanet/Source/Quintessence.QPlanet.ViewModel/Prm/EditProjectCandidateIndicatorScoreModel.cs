using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateIndicatorScoreModel : BaseEntityModel
    {
        public DictionaryIndicatorView DictionaryIndicator { get; set; }

        [DisplayFormat(DataFormatString = "{0:G29}", ApplyFormatInEditMode = true)]
        public decimal? Score { get; set; }

        [DataMember]
        public bool? IsStandard { get; set; }

        [DataMember]
        public bool? IsDistinctive { get; set; }

        public string GetDictionaryIndicatorName(int languageId)
        {
            var translation = DictionaryIndicator.DictionaryIndicatorTranslations.FirstOrDefault(t => t.LanguageId == languageId);
            return translation != null ? translation.Text : DictionaryIndicator.Name;
        }
    }
}