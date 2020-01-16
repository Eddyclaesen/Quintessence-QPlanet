using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Web.Mvc;
using Quintessence.Infrastructure.Validation;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Infrastructure.Security;
using Quintessence.QPlanet.Infrastructure.Services;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Infrastructure
{
    public abstract class QPlanetControllerBase : Controller
    {
        public ActionResult HandleError(Exception exception, Func<ActionResult> actionOnError = null, bool isPartial = false)
        {
            if (exception is FaultException<ValidationContainer>)
            {
                var validationContext = ((FaultException<ValidationContainer>)exception).Detail;

                var authenticationFaultEntry =
                    validationContext.FaultDetail.FaultEntries
                        .OfType<AuthenticationFaultEntry>()
                        .FirstOrDefault();

                if (authenticationFaultEntry != null)
                {
                    if (isPartial)
                        return PartialView("ErrorViews/PartialAuthenticationErrorView", (authenticationFaultEntry));

                    TempData[typeof(AuthenticationFaultEntry).Name] = authenticationFaultEntry;
                    return new HttpUnauthorizedResult(authenticationFaultEntry.Message);
                }

                TempData[typeof(FaultDetail).Name] = validationContext.FaultDetail;

                if (actionOnError != null)
                    return actionOnError.Invoke();
            }

            if (isPartial)
                return PartialView("ErrorViews/PartialErrorView", exception);

            return new HttpStatusCodeResult(500);
        }

        public ActionResult HandleStatusCodeError(Exception exception)
        {
            if (exception is FaultException<ValidationContainer>)
            {
                var validationContext = ((FaultException<ValidationContainer>)exception).Detail;

                var statusCode = new HttpStatusCodeResult(400, validationContext.FaultDetail.Reason);
                return statusCode;
            }

            return new HttpStatusCodeResult(400, exception.Message);
        }

        public ActionResult HandleStatusCodeErrorHtml(Exception exception)
        {
            if (exception is FaultException<ValidationContainer>)
            {
                var validationContext = ((FaultException<ValidationContainer>)exception).Detail;

                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;

                return Json(validationContext.FaultDetail.FaultEntries.Where(entry => entry.GetType().BaseType == typeof(FaultEntry)).Select(entry => entry.Message).ToArray(), JsonRequestBehavior.AllowGet);
            }

            return new HttpStatusCodeResult(400, exception.Message);
        }

        public ActionResult HandleStatusCodeErrorHtml(Exception exception, int statusCode)
        {
            if (exception is FaultException<ValidationContainer>)
            {
                var validationContext = ((FaultException<ValidationContainer>)exception).Detail;

                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = statusCode;

                return Json(validationContext.FaultDetail.FaultEntries.Where(entry => entry.GetType().BaseType == typeof(FaultEntry)).Select(entry => entry.Message).ToArray(), JsonRequestBehavior.AllowGet);
            }

            return new HttpStatusCodeResult(statusCode, exception.Message);
        }

        protected AuthenticationTokenView GetAuthenticationToken()
        {
            var token = System.Web.HttpContext.Current.Items["AuthenticationToken"] as AuthenticationTokenView;

            if (token == null)
            {
                var identity = IdentityHelper.RetrieveIdentity(HttpContext.ApplicationInstance.Context);

                if (identity == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var serviceInvoker = new ServiceInvoker<IAuthenticationQueryService>();
                token = serviceInvoker.Execute(HttpContext.ApplicationInstance.Context,
                                               service =>
                                               service.RetrieveAuthenticationTokenDetail(
                                                   new Guid(identity.Ticket.UserData)));

                if (token == null)
                {
                    throw new UnauthorizedAccessException();
                }

                System.Web.HttpContext.Current.Items["AuthenticationToken"] = token;
            }

            return token;
        }

        protected ActionResult DownloadPdfReport(string filename, string reportName, Dictionary<string, string> parameters)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new GenerateReportRequest();
                    request.ReportName = reportName;
                    request.Parameters = parameters;
                    request.OutputFormat = ReportOutputFormat.Pdf;

                    //var response = this.InvokeService<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));
                    //
                    //return File(Convert.FromBase64String(response.Output), response.ContentType, filename + ".pdf");
                    return new EmptyResult();
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return HandleError(exception, isPartial: true);
                }
            }
        }
    }
}