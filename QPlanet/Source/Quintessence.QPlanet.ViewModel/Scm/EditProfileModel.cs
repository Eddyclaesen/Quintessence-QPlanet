using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditProfileModel : BaseEntityModel
    {
        [Required]
        public string Name { get; set; }
    }
}
