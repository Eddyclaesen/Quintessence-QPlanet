using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.UI.Core.Service;
using Quintessence.CulturalFit.UI.Core.Unity;

namespace Quintessence.CulturalFit.UI.Webshell
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                name: "Login",
                url: "{lang}/Login/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", lang = "en", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LoginDefault",
                url: "Login/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LoginForm",
                url: "{lang}/Login/ChangeLanguage",
                defaults: new { controller = "Login", action = "ChangeLanguage" });

            //routes.MapRoute(
            //    name: "LoginForm",
            //    url: "{lang}/Login/LoginForm",
            //    defaults: new { controller = "Login", action = "LoginForm" });

            routes.MapRoute(
                name: "Welcome",
                url: "{lang}/Welcome/{action}/{requestcode}",
                defaults: new { controller = "Welcome", action = "Index" }
            );

            routes.MapRoute(
                name: "Submit",
                url: "{lang}/Questionnaire/Submit/{requestId}",
                defaults: new { controller = "Questionnaire", action = "Submit" }
            );

            routes.MapRoute(
                name: "Questionnaire",
                url: "{lang}/Questionnaire/{action}/{requestcode}/{listcode}",
                defaults: new { controller = "Questionnaire", action = "Index" }
            );

            routes.MapRoute(
                name: "TheoremCheck",
                url: "Questionnaire/RegisterTheoremCheck/{theoremId}/{isLeast}/{isMost}",
                defaults: new { controller = "Questionnaire", action = "RegisterTheoremCheck" }
            );

            routes.MapRoute(
                name: "GenerateReport",
                url: "Questionnaire/GenerateReport/{requestId}/{languageId}",
                defaults: new { controller = "Questionnaire", action = "GenerateReport" }
            );

            routes.MapRoute(
                name: "Default",
                url: string.Empty,
                defaults: new { controller = "Login", action = "Index" }
        );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            IUnityContainer container = new UnityContainer();
            container.RegisterInstance<IUnityContainer>(container);
            container.RegisterInstance(new ServiceFactory());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // Use LocalDB for Entity Framework by default
            //Database.DefaultConnectionFactory = new SqlConnectionFactory("Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.RegisterTemplateBundles();
        }
    }
}