using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QPlanet.ViewModel.Base
{
    public class GuidSelectListModel : SelectListItem
    {
        protected GuidSelectListModel() { }

        protected GuidSelectListModel(IViewEntity entity, Func<dynamic, string> name)
            : this()
        {
            Id = entity.Id;
            Name = name.Invoke(entity);
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
