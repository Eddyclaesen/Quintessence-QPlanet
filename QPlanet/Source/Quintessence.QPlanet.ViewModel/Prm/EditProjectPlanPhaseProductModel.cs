using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectPlanPhaseProductModel : EditProjectPlanPhaseEntryModel
    {
        private Guid _productId;
        public Guid ProductId { get; set; }

        public Guid ProductsheetEntryId { get; set; }

        public bool NoInvoice { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; }

        [Display(Name = "Product name")]
        public string ProductTypeName { get; set; }

        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "TotalPrice")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Price")]
        public override decimal Price
        {
            get { return TotalPrice; }
        }
    }
}