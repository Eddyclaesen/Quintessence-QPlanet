using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectTypeCategoryUnitPriceModel : BaseEntityModel
    {
        [Required]
        public decimal? UnitPrice { get; set; }
    }
}