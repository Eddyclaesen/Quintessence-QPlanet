using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectSubCategoryDetailType3Model : EditProjectSubCategoryDetailModelBase
    {
        [Display(Name="Include in report")]
        public bool IncludeInCandidateReport { get; set; }
    }
}
