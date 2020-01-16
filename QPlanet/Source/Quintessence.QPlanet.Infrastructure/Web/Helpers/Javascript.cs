using System.Globalization;
using System.Text;

namespace System.Web.Mvc
{
    public class JavaScriptHelper
    {
        public HtmlString InitializeDialog(string selector, string title, int width, int height, bool autoOpen = false, bool modal = true, bool resizable = false)
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Format("$('{0}').dialog({{", selector));
            builder.AppendLine(string.Format("\ttitle: '{0}',", title));
            builder.AppendLine(string.Format("\tautoOpen: {0},", autoOpen.ToString().ToLowerInvariant()));
            builder.AppendLine(string.Format("\tmodal: {0},", modal.ToString().ToLowerInvariant()));
            builder.AppendLine(string.Format("\tresizable: {0},", resizable.ToString().ToLowerInvariant()));
            builder.AppendLine(string.Format("\twidth: {0},", width.ToString(CultureInfo.InvariantCulture).ToLowerInvariant()));
            builder.AppendLine(string.Format("\theight: {0}", height.ToString(CultureInfo.InvariantCulture).ToLowerInvariant()));
            builder.AppendLine("});");

            return new HtmlString(builder.ToString());
        }
    }
}
