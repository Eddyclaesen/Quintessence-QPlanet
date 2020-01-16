using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Scm;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class CreateProjectPlanPhaseProductModel
    {
        private List<SelectListItem> _productSelectListItems;

        public List<ProductView> Products { get; set; }

        [Display(Name = "Product")]
        public Guid ProductId { get; set; }

        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }

        [Display(Name = "NoInvoice")]
        public bool NoInvoice { get; set; }

        public Guid ProjectPlanPhaseId { get; set; }

        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Display(Name = "UnitPrice")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "TotalPrice")]
        public decimal TotalPrice { get; set; }

        public List<SelectListItem> ProductSelectListItems
        {
            get
            {
                return _productSelectListItems 
                    ?? (_productSelectListItems = new List<SelectListItem>(Products.Select(p => new ProductSelectListItemModel(p))));
            }
        }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public Guid ProjectId { get; set; }

        public string ProductName { get; set; }
    }
}