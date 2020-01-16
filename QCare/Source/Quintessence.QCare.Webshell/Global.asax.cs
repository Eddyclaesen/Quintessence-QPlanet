using System.Globalization;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Quintessence.QCare.Webshell
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default_EvaluationCoaching",
                url: "EvaluationCoaching/{action}/{verificationCode}",
                defaults: new { controller = "EvaluationCoaching", action = "Intro" }
            );

            routes.MapRoute(
                name: "Default_EvaluationAcdc",
                url: "EvaluationAcdc/{action}/{verificationCode}",
                defaults: new { controller = "EvaluationAcdc", action = "Intro" }
            );

            routes.MapRoute(
                name: "Default_EvaluationCustomProjects",
                url: "EvaluationCustomProjects/{action}/{verificationCode}",
                defaults: new { controller = "EvaluationCustomProjects", action = "Intro" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Login", action = "Login" }
            );
        }

        protected void Application_Start()
        {
            HtmlHelper.ClientValidationEnabled = true;

            AreaRegistration.RegisterAllAreas();

            InitializeAutoMapper();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private void InitializeAutoMapper()
        {
            //Mapper.CreateMap(typeof(ClassToMap), typeof(ClassToMap));
        }
    }
}