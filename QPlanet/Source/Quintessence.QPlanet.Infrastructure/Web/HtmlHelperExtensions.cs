using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Quintessence.QPlanet.Infrastructure.Web
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString GroupDropDownList(this HtmlHelper helper, string name, IEnumerable<DropDownListGroupItem> data, string selectedValue, object htmlAttributes)
        {
            var select = new TagBuilder("select");

            if (htmlAttributes != null)
                select.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            select.GenerateId(name);
            select.MergeAttribute("name", name);

            var optgroupHtml = new StringBuilder();
            foreach (var group in data)
            {
                var groupTag = new TagBuilder("optgroup");
                groupTag.Attributes.Add("label", helper.Encode(group.Name));
                var optHtml = new StringBuilder();
                foreach (var item in group.Items)
                {
                    var option = new TagBuilder("option");
                    option.Attributes.Add("value", helper.Encode(item.Value));
                    if (selectedValue != null && item.Value == selectedValue)
                        option.Attributes.Add("selected", "selected");
                    option.InnerHtml = helper.Encode(item.Text);
                    optHtml.AppendLine(option.ToString(TagRenderMode.Normal));
                }
                groupTag.InnerHtml = optHtml.ToString();
                optgroupHtml.AppendLine(groupTag.ToString(TagRenderMode.Normal));
            }
            select.InnerHtml = optgroupHtml.ToString();
            return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
        }
    }
}
