using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditProductModel : BaseEntityModel
    {
        public Guid ProductTypeId { get; set; }

        [Display(Name="Product type")]
        public string ProductTypeName { get; set; }

        [Display(Name = "Product name")]
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}