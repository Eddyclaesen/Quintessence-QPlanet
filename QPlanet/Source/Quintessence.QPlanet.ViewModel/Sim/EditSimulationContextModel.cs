using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class EditSimulationContextModel : BaseEntityModel
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Username base")]
        [Required]
        public string UserNameBase { get; set; }

        [Display(Name = "Password length")]
        [Required]
        public int PasswordLength { get; set; }
    }
}