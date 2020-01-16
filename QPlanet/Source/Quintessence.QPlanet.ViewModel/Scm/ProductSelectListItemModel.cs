using System;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class ProductSelectListItemModel : SelectListItem
    {
        public ProductSelectListItemModel(ProductView product)
        {
            Id = product.Id;
            Name = product.Name;
        }

        public Guid Id
        {
            get { return new Guid(Value); }
            set { Value = value.ToString(); }
        }

        public string Name
        {
            get { return Text; }
            set { Text = value; }
        }
    }
}