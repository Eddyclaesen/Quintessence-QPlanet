using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment
{
    public class CreateProjectProductActionModel
    {
        public List<ProductTypeView> ProductTypes { get; set; }

        public Guid ProductTypeId { get; set; }

        public Guid ProjectId { get; set; }

        public string Description { get; set; }

        public decimal InvoiceAmount { get; set; }

        public DateTime? Deadline { get; set; }

        public bool NoInvoice { get; set; }

        public string InvoiceRemarks { get; set; }

        public IEnumerable<SelectListItem> CreateProductTypeSelectListItems()
        {
            return ProductTypes.Select(pt => new SelectListItem { Selected = pt.Id == ProductTypeId, Value = pt.Id.ToString(), Text = pt.Name });
        }
    }
}