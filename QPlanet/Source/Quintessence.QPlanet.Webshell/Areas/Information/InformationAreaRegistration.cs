using System.Web.Mvc;

namespace Quintessence.QPlanet.Webshell.Areas.Information
{
    public class InformationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Information";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Information_default",
                "Information/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
