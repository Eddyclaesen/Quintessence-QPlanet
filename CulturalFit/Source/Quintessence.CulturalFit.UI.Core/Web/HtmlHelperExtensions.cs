using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace System.Web.Mvc
{   
    public static class HtmlHelperExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(this T enumeration, string selected)
        {
            var source = Enum.GetValues(typeof(T));

            var items = new Dictionary<object, string>();

            var displayAttributeType = typeof(DisplayAttribute);

            foreach (var value in source)
            {
                var field = value.GetType().GetField(value.ToString());

                var attrs = (DisplayAttribute)field
                    .GetCustomAttributes(displayAttributeType, false)
                    .FirstOrDefault();

                items.Add((int)value, attrs == null ? field.Name : attrs.GetName());
            }

            return items.AsSelectListItems(i => i.Key.ToString(), i => i.Value);
            //return new SelectList(items, "Key", "Value", selected);
        }

        public static IEnumerable<SelectListItem> AsSelectListItems<TItem>(this IEnumerable<TItem> items, Expression<Func<TItem, string>> valueExpression, Expression<Func<TItem, string>> textExpression)
        {
            return items.Select(item => new SelectListItem { Value = valueExpression.Compile().Invoke(item), Text = textExpression.Compile().Invoke(item) });
        }
    }
}
