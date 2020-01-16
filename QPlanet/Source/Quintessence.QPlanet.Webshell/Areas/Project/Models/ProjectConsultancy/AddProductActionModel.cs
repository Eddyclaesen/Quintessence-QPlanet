using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Scm;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class AddProductActionModel
    {
        public List<ProductTypeSelectListItemModel> ProductTypeSelectListItems { get; set; }

        public List<ProductTypeView> ProductTypes { get; set; }

        public Guid ProductTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public decimal UnitPrice { get; set; }
    }
}