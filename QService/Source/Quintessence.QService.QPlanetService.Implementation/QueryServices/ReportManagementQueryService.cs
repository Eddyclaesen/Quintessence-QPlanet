using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class ReportManagementQueryService : SecuredUnityServiceBase, IReportManagementQueryService
    {
        public GenerateReportResponse GenerateReport(GenerateReportRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IReportServiceQueryRepository>();

                var response = new GenerateReportResponse();
                response.Output = repository.GenerateReport(request.ReportName, request.Parameters, ConvertOutputFormat(request.OutputFormat));
                response.ContentType = ConvertOutputFormatToContentType(request.OutputFormat);
                return response;
            });
        }

        private string ConvertOutputFormat(ReportOutputFormat outputFormat)
        {
            switch (outputFormat)
            {
                case ReportOutputFormat.Xml: return "XML";
                case ReportOutputFormat.Csv: return "CSV";
                case ReportOutputFormat.Image: return "IMAGE";
                case ReportOutputFormat.Pdf: return "PDF";
                case ReportOutputFormat.MHtml: return "MHTML";
                case ReportOutputFormat.Html4: return "HTML4.0";
                case ReportOutputFormat.Html32: return "HTML3.2";
                case ReportOutputFormat.Excel: return "EXCEL";
                case ReportOutputFormat.Word: return "WORDOPENXML";

                default:
                    return "PDF";
            }
        }

        private string ConvertOutputFormatToContentType(ReportOutputFormat outputFormat)
        {
            switch (outputFormat)
            {
                case ReportOutputFormat.Xml: return "XML";
                case ReportOutputFormat.Csv: return "CSV";
                case ReportOutputFormat.Image: return "IMAGE";
                case ReportOutputFormat.Pdf: return "application/pdf";
                case ReportOutputFormat.MHtml: return "MHTML";
                case ReportOutputFormat.Html4: return "text/html";
                case ReportOutputFormat.Html32: return "text/html";
                case ReportOutputFormat.Excel: return "application/vnd.xls";
                case ReportOutputFormat.Word: return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                default:
                    return string.Empty;
            }

        }

        public List<CandidateReportDefinitionView> ListCandidateReportDefinitions()
        {
            LogTrace();

            return Execute(() =>
                {
                    var repository = Container.Resolve<IReportManagementQueryRepository>();

                    return repository.ListCandidateReportDefinitions();
                });
        }

        public CandidateReportDefinitionView RetrieveCandidateReportDefinition(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IReportManagementQueryRepository>();

                return repository.RetrieveCandidateReportDefinition(id);
            });
        }

        public CandidateReportDefinitionFieldView RetrieveCandidateReportDefinitionField(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IReportManagementQueryRepository>();

                return repository.Retrieve<CandidateReportDefinitionFieldView>(id);
            });
        }

        public List<CandidateReportDefinitionView> ListCandidateReportDefinitionsForCustomer(int contactId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IReportManagementQueryRepository>();

                return repository.ListCandidateReportDefinitionsForCustomer(contactId);
            });
        }

        public List<CandidateScoreReportTypeView> ListCandidateScoreReportTypes()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IReportManagementQueryRepository>();

                return repository.ListCandidateScoreReportTypes();
            });
        }

        public List<ReportTypeView> ListReportTypes()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IReportManagementQueryRepository>();

                return repository.ListReportTypes();
            });
        }

        public List<ReportDefinitionView> ListReportDefinitions(ListReportDefinitionsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IReportManagementQueryRepository>();

                if (!string.IsNullOrWhiteSpace(request.Code) && !request.ReportTypeId.HasValue)
                {
                    var reportTypes = repository.ListReportTypes();
                    var reportType = reportTypes.FirstOrDefault(rt => rt.Code.Equals(request.Code, StringComparison.InvariantCultureIgnoreCase));

                    if (reportType != null)
                        request.ReportTypeId = reportType.Id;
                }

                return request.ReportTypeId.HasValue ?
                    repository.ListReportDefinitionsByReportType(request.ReportTypeId.Value) :
                    repository.ListReportDefinitions();
            });
        }

        public ReportDefinitionView RetrieveReportDefinition(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IReportManagementQueryRepository>();

                return repository.Retrieve<ReportDefinitionView>(id);
            });
        }

        public List<ReportParameterView> ListReportParameters()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IReportManagementQueryRepository>();

                return repository.List<ReportParameterView>();
            });
        }

        public ReportParameterView RetrieveReportParameter(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IReportManagementQueryRepository>();
                var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();

                var reportParameter = repository.RetrieveReportParameter(id);

                bool reloadReportParameter = false;

                var languageIds = infrastructureQueryService.ListLanguages().Select(l => l.Id).ToList();
                var commandService = Container.Resolve<IReportManagementCommandService>();

                if (reportParameter.ReportParameterValues != null && reportParameter.ReportParameterValues.Count > 0)
                {
                    for (var i = reportParameter.ReportParameterValues.Count - 1; i >= 0; i--)
                        if (!languageIds.Contains(reportParameter.ReportParameterValues[i].LanguageId))
                        {
                            commandService.DeleteReportParameterValue(reportParameter.ReportParameterValues[i].Id);
                            reportParameter.ReportParameterValues.Remove(reportParameter.ReportParameterValues[i]);
                        }
                }

                foreach (var languageId in languageIds.Where(lid => !reportParameter.ReportParameterValues.Select(rpv => rpv.LanguageId).Contains(lid)))
                {
                    var request = new CreateNewReportParameterValueRequest
                        {
                            LanguageId = languageId,
                            ReportParameterId = id
                        };
                    commandService.CreateNewReportParameterValue(request);
                    reloadReportParameter = true;
                }

                if (reloadReportParameter)
                    reportParameter = repository.RetrieveReportParameter(id);

                return reportParameter;
            });
        }
    }
}
