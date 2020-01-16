using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class AddActivityTypeModel
    {
        [Required]
        public string Name { get; set; }
    }
}