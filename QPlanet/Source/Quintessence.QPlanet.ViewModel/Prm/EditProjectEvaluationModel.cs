using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectEvaluationModel : BaseEntityModel
    {
        public int CrmProjectId { get; set; }

        [AllowHtml]
        [Display(Name = "Lessons learned")]
        public string LessonsLearned { get; set; }

        [AllowHtml]
        [Display(Name = "Evaluation")]
        public string Evaluation { get; set; }
    }
}