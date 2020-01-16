using System.Web.Mvc;

namespace Quintessence.QPlanet.Webshell.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("AdminUserUsers",
                             "Admin/{controller}/SearchUsers/{term}",
                             new { contoller = "AdminUser", action = "SearchUsers" });

            context.MapRoute(
                name: "Admin_AdminProject_ProjectRole2DictionaryLevel",
                url: "Admin/AdminProject/UnlinkProjectRoleDictionaryLevel/{projectRoleId}/{dictionaryLevelId}",
                defaults: new { controller = "AdminProject", action = "UnlinkProjectRoleDictionaryLevel" }
            );

            context.MapRoute(
                name: "Admin_AdminProject_MarkDictionaryIndicatorAsStandard",
                url: "Admin/AdminProject/MarkDictionaryIndicatorAsStandard/{projectRoleId}/{dictionaryIndicatorId}",
                defaults: new { controller = "AdminProject", action = "MarkDictionaryIndicatorAsStandard" }
            );

            context.MapRoute(
                name: "Admin_AdminProject_MarkDictionaryIndicatorAsDistinctive",
                url: "Admin/AdminProject/MarkDictionaryIndicatorAsDistinctive/{projectRoleId}/{dictionaryIndicatorId}",
                defaults: new { controller = "AdminProject", action = "MarkDictionaryIndicatorAsDistinctive" }
            );

            context.MapRoute(
                name: "Admin_AdminProject_DeleteProjectRoleDictionaryIndicator",
                url: "Admin/AdminProject/DeleteProjectRoleDictionaryIndicator/{projectRoleId}/{dictionaryIndicatorId}",
                defaults: new { controller = "AdminProject", action = "DeleteProjectRoleDictionaryIndicator" }
            );

            context.MapRoute(
                name: "Admin_AdminActivity_ApplyPriceIndex",
                url: "Admin/AdminActivity/ApplyPriceIndex/{index}",
                defaults: new { controller = "AdminActivity", action = "ApplyPriceIndex" }
            );

            context.MapRoute(
                name: "Admin_AdminDictionary_EditDictionaryLanguage",
                url: "Admin/AdminDictionary/EditDictionaryLanguage/{id}/{languageId}",
                defaults: new { controller = "AdminDictionary", action = "EditDictionaryLanguage" }
            );

            context.MapRoute(
                name: "Admin_AdminDictionary_ImportDictionary",
                url: "Admin/AdminDictionary/ImportDictionary/{name}",
                defaults: new { controller = "AdminDictionary", action = "ImportDictionary" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
