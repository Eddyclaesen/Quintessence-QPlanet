using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Microsoft.Reporting.WebForms;

namespace Quintessence.QPlanet.Reportshell
{
    public partial class ReportViewerWebForm : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StandardReportViewer.ProcessingMode = ProcessingMode.Remote;

            var report = Request.QueryString["report"];

            StandardReportViewer.AsyncRendering = false;
            StandardReportViewer.SizeToReportContent = false;

            StandardReportViewer.ServerReport.ReportServerUrl = new Uri("http://qshare/_vti_bin/ReportServer");
            StandardReportViewer.ServerReport.ReportPath = string.Format("http://qshare/ITWeb/Reports/{0}.rdl", report);

            StandardReportViewer.ServerReport.ReportServerCredentials = new ReportServerCredentials("ceddy", "1975Koffiekoek", "QUINTDOMAIN");

            StandardReportViewer.Visible = true;

            //StandardReportViewer.ShowPageNavigationControls = false;
            //StandardReportViewer.ShowBackButton = false;
            //StandardReportViewer.ShowCredentialPrompts = false;
            //StandardReportViewer.ShowDocumentMapButton = false;
            //StandardReportViewer.ShowExportControls = false;
            //StandardReportViewer.ShowFindControls = false;
            //StandardReportViewer.ShowPageNavigationControls = false;
            //StandardReportViewer.ShowParameterPrompts = false;
            //StandardReportViewer.ShowPrintButton = false;
            //StandardReportViewer.ShowPromptAreaButton = false;
            //StandardReportViewer.ShowRefreshButton = false;
            //StandardReportViewer.ShowReportBody = true; //the report
            //StandardReportViewer.ShowToolBar = false;
            //StandardReportViewer.ShowWaitControlCancelLink = false;
            //StandardReportViewer.ShowZoomControl = false;

            foreach (var key in Request.QueryString.AllKeys.Except(new List<string> { "report" }))
            {
                var parameter = new ReportParameter
                    {
                        Name = key, 
                        Visible = false
                    };
                parameter.Values.Add(Request.QueryString[key]);
                StandardReportViewer.ServerReport.SetParameters(parameter);
            }

            StandardReportViewer.ServerReport.Refresh();
        }
    }
}