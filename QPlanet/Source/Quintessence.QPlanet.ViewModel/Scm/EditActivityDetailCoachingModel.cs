using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditActivityDetailCoachingModel : EditActivityDetailModel
    {
        [Display(Name = "Coaching demand")]
        public override string Description { get; set; }

        [Display(Name = "Target group")]
        public string TargetGroup { get; set; }
    }
}