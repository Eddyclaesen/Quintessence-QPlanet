using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditActivityTypeModel : BaseEntityModel
    {
        [Required]
        public string Name { get; set; }
    }
}