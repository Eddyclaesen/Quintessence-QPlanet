using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectPlanPhaseEntryModel : BaseEntityModel
    {
        public Guid ProjectPlanPhaseId { get; set; }

        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }

        private decimal _price;
        [Display(Name = "Price")]
        public virtual decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }
    }
}