using System.Web.Mvc;

namespace Quintessence.QPlanet.Webshell.Areas.Dictionary
{
    public class DictionaryAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Dictionary";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("DictionaryHomeCustomDictionaryContacts",
                             "Dictionary/{controller}/CustomDictionaryContacts/{term}",
                             new { contoller = "DictionaryHome", action = "CustomDictionaryContacts" });

            context.MapRoute("DictionaryClusterDetail",
                             "Dictionary/{controller}/{action}/{id}/{languageId}",
                             new { contoller = "DictionaryDetail", action = "DictionaryDetail" });

            context.MapRoute(
                "Dictionary_default",
                "Dictionary/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
