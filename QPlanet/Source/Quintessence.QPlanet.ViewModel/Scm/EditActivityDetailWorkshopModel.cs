using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditActivityDetailWorkshopModel : EditActivityDetailModel
    {
        private List<EditActivityDetailWorkshopLanguageModel> _activityDetailWorkshopLanguages;

        [Display(Name = "Workshop demand")]
        public override string Description { get; set; }

        [Display(Name = "Target group")]
        public string TargetGroup { get; set; }

        public List<EditActivityDetailWorkshopLanguageModel> ActivityDetailWorkshopLanguages
        {
            get { return _activityDetailWorkshopLanguages ?? (_activityDetailWorkshopLanguages = new List<EditActivityDetailWorkshopLanguageModel>()); }
            set { _activityDetailWorkshopLanguages = value; }
        }
    }
}