using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.Infrastructure.Web;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectRoleModel : BaseEntityModel
    {
        public bool IsContactRequired { get; set; }

        [Display(Name="Project role name")]
        [Required]
        public string Name { get; set; }

        public int? ContactId { get; set; }

        [Display(Name = "Contact")]
        [ConditionalRequired("IsContactRequired", typeof(EditProjectRoleModel))]
        public string ContactName { get; set; }

        public List<EditProjectRoleTranslationModel> ProjectRoleTranslations { get; set; }
    }
}
