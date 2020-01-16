using System;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class ProductTypeSelectListItemModel : SelectListItem
    {
        public ProductTypeSelectListItemModel(ProductTypeView productType)
        {
            Id = productType.Id;
            Name = productType.Name;
        }

        public Guid? Id
        {
            get { return Value != null ? new Guid(Value) : (Guid?)null; }
            set { Value = value != null ? value.ToString() : null; }
        }

        public string Name
        {
            get { return Text; }
            set { Text = value; }
        }
    }
}