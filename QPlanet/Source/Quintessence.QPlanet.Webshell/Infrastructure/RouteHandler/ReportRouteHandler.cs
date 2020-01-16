using System;
using System.Configuration;
using System.Web;
using System.Web.Routing;
using Quintessence.QPlanet.Infrastructure.Logging;

namespace Quintessence.QPlanet.Webshell.Infrastructure.RouteHandler
{
    public class ReportRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var reportName = requestContext.RouteData.Values["reportname"] as string;

            return new ReportHttpHandler(reportName);
        }
    }

    public class ReportHttpHandler : IHttpHandler
    {
        private readonly string _reportName;

        public ReportHttpHandler(string reportName)
        {
            _reportName = reportName;
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Write(string.Format(ConfigurationManager.AppSettings["ReportshellUrl"] + "ReportViewerWebPart.aspx?report={0}", _reportName));
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
            }
        }

        public bool IsReusable { get { return true; } }
    }
}