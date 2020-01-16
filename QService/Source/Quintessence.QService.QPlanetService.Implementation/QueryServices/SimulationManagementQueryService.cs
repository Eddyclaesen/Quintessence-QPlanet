using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement;
using Quintessence.QService.QPlanetService.Contracts.SecurityContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QueryModel.Sim;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class SimulationManagementQueryService : SecuredUnityServiceBase, ISimulationManagementQueryService
    {
        public ListSimulationSetsResponse ListSimulationSets(ListSimulationSetsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.SIMLISTSET);

                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                var pagingInfo = Mapper.DynamicMap<PagingInfo>(request.DataTablePaging);

                var response = new ListSimulationSetsResponse
                    {
                        SimulationSets = repository.ListSimulationSets(pagingInfo),
                        DataTablePaging = Mapper.DynamicMap<DataTablePaging>(pagingInfo)
                    };

                return response;
            });
        }

        public ListSimulationDepartmentsResponse ListSimulationDepartments(ListSimulationDepartmentsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.SIMLISTDEP);

                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                var pagingInfo = Mapper.DynamicMap<PagingInfo>(request.DataTablePaging);

                var response = new ListSimulationDepartmentsResponse
                {
                    SimulationDepartments = repository.ListSimulationDepartments(pagingInfo),
                    DataTablePaging = Mapper.DynamicMap<DataTablePaging>(pagingInfo)
                };

                return response;
            });
        }

        public ListSimulationLevelsResponse ListSimulationLevels(ListSimulationLevelsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.SIMLISTLEV);

                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                var pagingInfo = Mapper.DynamicMap<PagingInfo>(request.DataTablePaging);

                var response = new ListSimulationLevelsResponse
                {
                    SimulationLevels = repository.ListSimulationLevels(pagingInfo),
                    DataTablePaging = Mapper.DynamicMap<DataTablePaging>(pagingInfo)
                };

                return response;
            });
        }

        public ListSimulationsResponse ListSimulations(ListSimulationsRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.SIMLISTULA);

                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                var pagingInfo = Mapper.DynamicMap<PagingInfo>(request.DataTablePaging);

                var response = new ListSimulationsResponse
                {
                    Simulations = repository.ListSimulations(pagingInfo),
                    DataTablePaging = Mapper.DynamicMap<DataTablePaging>(pagingInfo)
                };

                return response;
            });
        }

        public ListSimulationMatrixEntriesResponse ListSimulationMatrixEntries(ListSimulationMatrixEntriesRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.SIMLISTCOM);

                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                var pagingInfo = Mapper.DynamicMap<PagingInfo>(request.DataTablePaging);

                var response = new ListSimulationMatrixEntriesResponse
                {
                    SimulationMatrixEntries = repository.ListSimulationMatrixEntries(pagingInfo),
                    DataTablePaging = Mapper.DynamicMap<DataTablePaging>(pagingInfo)
                };

                return response;
            });
        }

        public SimulationSetView RetrieveSimulationSet(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.SIMRETRSET);

                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                return repository.Retrieve<SimulationSetView>(id);
            });
        }

        public SimulationDepartmentView RetrieveSimulationDepartment(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.SIMRETRDEP);

                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                return repository.Retrieve<SimulationDepartmentView>(id);
            });
        }

        public SimulationLevelView RetrieveSimulationLevel(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.SIMRETRLEV);

                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                return repository.Retrieve<SimulationLevelView>(id);
            });
        }

        public SimulationView RetrieveSimulation(Guid id)
        {
            LogTrace();

            return Execute(() =>
                {
                    //Make sure the translations are in place
                    var commandService = Container.Resolve<ISimulationManagementCommandService>();
                    commandService.EnsureSimulationTranslations(id);

                    var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                    return repository.RetrieveSimulation(id);
                });
        }

        public SimulationMatrixEntryView RetrieveSimulationMatrixEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.SIMRETRCOM);

                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                return repository.Retrieve<SimulationMatrixEntryView>(id);
            });
        }

        public List<SimulationCombinationLanguageView> ListSimulationCombinationLangauges(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                //ValidateAuthorization(OperationType.SIMRETRCOM);

                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                return repository.ListSimulationCombinationLanguages(id);
            });
        }

        public SimulationCombinationView RetrieveSimulationCombination(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                return repository.RetrieveSimulationCombination(id);
            });
        }

        public List<SimulationContextView> ListSimulationContexts()
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                return repository.ListSimulationContexts();
            });
        }

        public SimulationContextView RetrieveSimulationContext(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                return repository.Retrieve<SimulationContextView>(id);
            });
        }

        public List<SimulationContextUserView> ListSimulationContextUsers(Guid simulationContextId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                return repository.ListSimulationContextUsers(simulationContextId);
            });
        }

        public List<SimulationTranslationView> ListSimulationTranslations(Guid simulationId)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<ISimulationManagementQueryRepository>();

                return repository.ListSimulationTranslations(simulationId);
            });
        }
    }
}
