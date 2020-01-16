using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QPlanet.ViewModel.Base
{
    public class NullableGuidSelectListModel : SelectListItem
    {
        protected NullableGuidSelectListModel()
        { }

        protected NullableGuidSelectListModel(IViewEntity viewEntity, Func<dynamic, string> name)
            : this()
        {
            Id = viewEntity.Id;
            Name = name.Invoke(viewEntity);
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
