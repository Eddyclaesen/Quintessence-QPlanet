using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.ServiceModel;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Infrastructure.Web.ActionResults;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;

namespace Quintessence.QPlanet.Webshell.Controllers
{
    public class ReportingController : Controller
    {
        public ActionResult Image(string report, string file, string format, string parameter, string value)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var request = new GenerateReportRequest();

                    request.ReportName = report.Replace("_", "/");
                    request.OutputFormat = ReportOutputFormat.Image;
                    request.Parameters = new Dictionary<string, string> { { parameter, value } };

                    Guid tokenId;
                    using (var factory = new ChannelFactory<IAuthenticationCommandService>(typeof(IAuthenticationCommandService).Name))
                    {
                        var channel = factory.CreateChannel();

                        var negociateAuthenticationTokenResponse = channel.NegociateAuthenticationToken("admin", "$Quint123");
                        tokenId = negociateAuthenticationTokenResponse.AuthenticationTokenId;
                    }

                    GenerateReportResponse response;
                    using (var factory = new ChannelFactory<IReportManagementQueryService>(typeof(IReportManagementQueryService).Name))
                    {
                        var proxy = factory.CreateChannel();
                        using (new OperationContextScope(proxy as IContextChannel))
                        {
                            var typedMessageHeader = new MessageHeader<string>(tokenId.ToString());
                            var messageHeader = typedMessageHeader.GetUntypedHeader("authenticationTokenId", "security");
                            OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader);

                            var action = new Func<IReportManagementQueryService, GenerateReportResponse>(service => service.GenerateReport(request));
                            response = action.Invoke(proxy);
                        }
                    }

                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, Int64.MaxValue);

                    var responseMemomryStream = new MemoryStream(Convert.FromBase64String(response.Output));
                    var bitmap = new Bitmap(responseMemomryStream);

                    switch (format.ToLowerInvariant())
                    {
                        case "png":
                            return new ImageResult(bitmap, ImageFormat.Png);

                        case "jpg":
                            return new ImageResult(bitmap, ImageFormat.Jpeg);

                        case "bmp":
                            return new ImageResult(bitmap, ImageFormat.Bmp);

                        default:
                            throw new ArgumentOutOfRangeException("format", "Illegal image format");
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    return new HttpStatusCodeResult(500);
                }
            }
        }
    }
}