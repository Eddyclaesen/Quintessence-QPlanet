using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditProductTypeModel : BaseEntityModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name="Unit price")]
        public decimal UnitPrice { get; set; }
    }
}
