using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.Web.Mvc
{
    public static class Extensions
    {
        public static IEnumerable<SelectListItem> AsSelectListItems<TItem>(this IEnumerable<TItem> items, Expression<Func<TItem, string>> valueExpression, Expression<Func<TItem, string>> textExpression, Func<TItem, bool> isSelectedAction = null)
        {
            return items.Select(item => new SelectListItem
                {
                    Value = valueExpression.Compile().Invoke(item),
                    Text = textExpression.Compile().Invoke(item),
                    Selected = isSelectedAction != null && isSelectedAction(item)
                });
        }

        public static JavaScriptHelper Js<TModel>(this HtmlHelper<TModel> page)
        {
            return new JavaScriptHelper();
        }

        public static JavaScriptHelper Js(this HtmlHelper page)
        {
            return new JavaScriptHelper();
        }
    }
}
