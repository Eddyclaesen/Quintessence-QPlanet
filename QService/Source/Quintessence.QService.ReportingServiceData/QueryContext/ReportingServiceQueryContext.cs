using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.ServiceModel;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.ReportingServiceData.ReportExecutionService;

namespace Quintessence.QService.ReportingServiceData.QueryContext
{
    public class ReportingServiceQueryContext : IResQueryContext
    {
        private string _url;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _domain;

        public ReportingServiceQueryContext(IConfiguration configuration)
        {
            var settings = ParseConnectionString(configuration.GetConnectionStringConfiguration<IRepQueryContext>());
            _url = settings["url"];
            _userName = settings["username"];
            _password = settings["password"];
            _domain = settings["domain"];
        }

        /// <summary>
        /// Parses the connection string.
        /// </summary>
        /// <param name="connectionStringConfiguration">The connection string.</param>
        /// <returns></returns>
        private static Dictionary<string, string> ParseConnectionString(string connectionStringConfiguration)
        {
            var connectionstring = ConfigurationManager.ConnectionStrings[connectionStringConfiguration].ConnectionString;
            return
                connectionstring.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(part => part.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(keyValue => keyValue[0].ToLowerInvariant(), keyValue => keyValue[1]);
        }

        public void Dispose()
        {
        }

        public string GenerateReport(string reportName, Dictionary<string, string> parameters, string outputformat)
        {
            var reportExecution = new ReportExecutionServiceSoapClient();
            reportExecution.ChannelFactory.Endpoint.Address = new EndpointAddress(_url);
            reportExecution.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Delegation;
            reportExecution.ClientCredentials.Windows.ClientCredential = new NetworkCredential(_userName, _password, _domain);

            //Initialize variables
            TrustedUserHeader trustedUserHeader = null;
            var reportPath = reportName;

            string historyID = null;
            ServerInfoHeader serverInfoHeader = null;
            ExecutionInfo executionInfo = null;
            string deviceInfo = CreateDeviceInfo(outputformat);
            byte[] result = null;
            string extension = null;
            string mimeType = null;
            string encoding = null;
            Warning[] warnings = null;
            string[] streamIds = null;

            //Load the report information
            var executionHeader = reportExecution.LoadReport(
                trustedUserHeader,
                reportPath,
                historyID,
                out serverInfoHeader,
                out executionInfo);

            //Set report parameters

            //var reportParameters = executionInfo.Parameters;
            //
            //foreach (var reportParameter in reportParameters)
            //{
            //    if (parameters.ContainsKey(reportParameter.Name))
            //        continue;
            //
            //    switch (reportParameter.Type)
            //    {
            //        case ParameterTypeEnum.Boolean:
            //            parameters[reportParameter.Name] = default(bool).ToString();
            //            break;
            //        case ParameterTypeEnum.DateTime:
            //            parameters[reportParameter.Name] = default(DateTime).ToString();
            //            break;
            //        case ParameterTypeEnum.Integer:
            //            parameters[reportParameter.Name] = default(int).ToString();
            //            break;
            //        case ParameterTypeEnum.Float:
            //            parameters[reportParameter.Name] = default(float).ToString();
            //            break;
            //        case ParameterTypeEnum.String:
            //            parameters[reportParameter.Name] = default(string);
            //            break;
            //        default:
            //            break;
            //    }
            //
            //}

            var parameterValues = parameters.Select(parameter => new ParameterValue { Name = parameter.Key, Value = parameter.Value }).ToArray();

            reportExecution.SetExecutionParameters(executionHeader,
                trustedUserHeader,
                parameterValues,
                "nl-be",
                out executionInfo);

            //Render report
            reportExecution.Render(executionHeader, trustedUserHeader, outputformat, deviceInfo,
                                                      out result, out extension, out mimeType, out encoding,
                                                      out warnings, out streamIds);

            return Convert.ToBase64String(result);
        }

        private string CreateDeviceInfo(string outputformat)
        {
            switch (outputformat.ToLowerInvariant())
            {
                case "image":
                    return "<DeviceInfo><OutputFormat>TIFF</OutputFormat><ColorDepth>12</ColorDepth><DpiX>100</DpiX><DpiY>100</DpiY></DeviceInfo>";

                default:
                    return null;
            }
        }
    }
}
