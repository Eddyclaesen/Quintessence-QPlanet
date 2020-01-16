using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.DataModel.Rep;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;


namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class ReportManagementCommandService : SecuredUnityServiceBase, IReportManagementCommandService
    {
        public Guid CreateNewCandidateReportDefinition(CreateNewCandidateReportDefinitionRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                var candidateReportDefinition = repository.Prepare<CandidateReportDefinition>();
                candidateReportDefinition.IsActive = true;
                Mapper.DynamicMap(request, candidateReportDefinition);

                repository.Save(candidateReportDefinition);

                return candidateReportDefinition.Id;
            });
        }

        public void UpdateCandidateReportDefinition(UpdateCandidateReportDefinitionRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                var candidateReportDefinition = repository.Retrieve<CandidateReportDefinition>(request.Id);
                Mapper.DynamicMap(request, candidateReportDefinition);

                repository.Save(candidateReportDefinition);
            });
        }

        public void DeleteCandidateReportDefinition(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                repository.Delete<CandidateReportDefinition>(id);
            });
        }

        public Guid CreateNewCandidateReportDefinitionField(CreateNewCandidateReportDefinitionFieldRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                var candidateReportDefinitionField = repository.Prepare<CandidateReportDefinitionField>();
                candidateReportDefinitionField.IsActive = true;
                Mapper.DynamicMap(request, candidateReportDefinitionField);

                repository.Save(candidateReportDefinitionField);

                return candidateReportDefinitionField.Id;
            });
        }

        public void UpdateCandidateReportDefinitionField(UpdateCandidateReportDefinitionFieldRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                var candidateReportDefinitionField = repository.Retrieve<CandidateReportDefinitionField>(request.Id);
                Mapper.DynamicMap(request, candidateReportDefinitionField);

                repository.Save(candidateReportDefinitionField);
            });
        }

        public void DeleteCandidateReportDefinitionField(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                repository.Delete<CandidateReportDefinitionField>(id);
            });
        }

        public Guid CreateNewReportDefinition(CreateNewReportDefinitionRequest request)
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                var reportDefinition = repository.Prepare<ReportDefinition>();

                Mapper.DynamicMap(request, reportDefinition);

                reportDefinition.IsActive = true;

                repository.Save(reportDefinition);

                return reportDefinition.Id;
            });
        }

        public void UpdateReportDefinition(UpdateReportDefinitionRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                var reportDefinition = repository.Retrieve<ReportDefinition>(request.Id);
                Mapper.DynamicMap(request, reportDefinition);

                repository.Save(reportDefinition);
            });
        }

        public void DeleteReportDefinition(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                repository.Delete<ReportDefinition>(id);
            });
        }

        public void UpdateReportDefinitions(List<UpdateReportDefinitionRequest> requests)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                foreach (var request in requests)
                {
                    var reportDefinition = repository.Retrieve<ReportDefinition>(request.Id);
                    Mapper.DynamicMap(request, reportDefinition);

                    repository.Save(reportDefinition);
                }
            });
        }

        public Guid CreateNewReportParameter(CreateNewReportParameterRequest request)
        {
            LogTrace();

            var reportParameterId = ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                var reportParameter = repository.Prepare<ReportParameter>();

                Mapper.DynamicMap(request, reportParameter);

                if (ValidationContainer.ValidateObject(reportParameter))
                    repository.Save(reportParameter);

                return reportParameter.Id;
            });

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                foreach (var value in request.Values)
                {
                    var reportParameterValue = repository.Prepare<ReportParameterValue>();

                    Mapper.DynamicMap(value, reportParameterValue);
                    reportParameterValue.ReportParameterId = reportParameterId;

                    if (ValidationContainer.ValidateObject(reportParameterValue))
                        repository.Save(reportParameterValue);
                }
            });

            return reportParameterId;
        }

        public void DeleteReportParameterValue(Guid id)
        {
            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                repository.Delete<ReportParameterValue>(id);
            });
        }

        public Guid CreateNewReportParameterValue(CreateNewReportParameterValueRequest request)
        {
            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                if (!request.ReportParameterId.HasValue)
                    throw new ArgumentNullException("request", "Missing report parameter id.");

                var reportParameterValue = repository.Prepare<ReportParameterValue>();
                Mapper.DynamicMap(request, reportParameterValue);

                repository.Save(reportParameterValue);

                return reportParameterValue.Id;
            });
        }

        public void UpdateReportParameter(UpdateReportParameterRequest request)
        {
            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();

                var reportParameter = repository.Retrieve<ReportParameter>(request.Id);

                Mapper.DynamicMap(request, reportParameter);

                repository.Save(reportParameter);

                foreach (var valueRequest in request.ReportParameterValues)
                {
                    var reportParameterValue = repository.Retrieve<ReportParameterValue>(valueRequest.Id);

                    Mapper.DynamicMap(valueRequest, reportParameterValue);

                    repository.Save(reportParameterValue);
                }
            });
        }

        public void DeleteReportParameter(Guid id)
        {
            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IReportManagementCommandRepository>();
                var queryService = Container.Resolve<IReportManagementQueryService>();

                var reportParameter = queryService.RetrieveReportParameter(id);

                foreach (var reportParameterValue in reportParameter.ReportParameterValues)
                    repository.Delete<ReportParameterValue>(reportParameterValue.Id);

                repository.Delete<ReportParameter>(id);
            });
        }
    }
}
