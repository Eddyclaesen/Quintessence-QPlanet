using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.DataModel.Sim;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class SimulationManagementCommandService : SecuredUnityServiceBase, ISimulationManagementCommandService
    {
        public void DeleteSimulationSet(Guid id)
        {
            LogTrace("Delete simulation set (id: {0}).", id);

            ExecuteTransaction(() =>
                {
                    var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                    repository.Delete<SimulationSet>(id);
                });
        }

        public void DeleteSimulationDepartment(Guid id)
        {
            LogTrace("Delete simulation department (id: {0}).", id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                repository.Delete<SimulationDepartment>(id);
            });
        }

        public void DeleteSimulationLevel(Guid id)
        {
            LogTrace("Delete simulation level (id: {0}).", id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                repository.Delete<SimulationLevel>(id);
            });
        }

        public void DeleteSimulation(Guid id)
        {
            LogTrace("Delete simulation (id: {0}).", id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                repository.Delete<Simulation>(id);
            });
        }

        public void DeleteSimulationCombination(Guid id)
        {
            LogTrace("Delete simulation (id: {0}).", id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                repository.Delete<SimulationCombination>(id);
            });
        }

        public void UpdateSimulationSet(UpdateSimulationSetRequest request)
        {
            LogTrace("Update simulation set (id: {0}).", request.Id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var entity = repository.Retrieve<SimulationSet>(request.Id);

                Mapper.DynamicMap(request, entity, typeof(UpdateSimulationSetRequest), typeof(SimulationSet));

                repository.Save(entity);
            });
        }

        public void UpdateSimulationDepartment(UpdateSimulationDepartmentRequest request)
        {
            LogTrace("Update simulation department (id: {0}).", request.Id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var entity = repository.Retrieve<SimulationDepartment>(request.Id);

                Mapper.DynamicMap(request, entity, typeof(UpdateSimulationDepartmentRequest), typeof(SimulationDepartment));

                repository.Save(entity);
            });
        }

        public void UpdateSimulationLevel(UpdateSimulationLevelRequest request)
        {
            LogTrace("Update simulation level (id: {0}).", request.Id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var entity = repository.Retrieve<SimulationLevel>(request.Id);

                Mapper.DynamicMap(request, entity, typeof(UpdateSimulationLevelRequest), typeof(SimulationLevel));

                repository.Save(entity);
            });
        }

        public void UpdateSimulation(UpdateSimulationRequest request)
        {
            LogTrace("Update simulation (id: {0}).", request.Id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var simulation = repository.Retrieve<Simulation>(request.Id);
                Mapper.DynamicMap(request, simulation);
                repository.Save(simulation);

                var translations = new List<SimulationTranslation>(request.SimulationTranslations.Count);
                foreach (var translationRequest in request.SimulationTranslations)
                {
                    var translation = repository.Retrieve<SimulationTranslation>(translationRequest.Id);
                    Mapper.DynamicMap(translationRequest, translation);
                    translations.Add(translation);
                }
                repository.SaveList(translations);
            });
        }

        public void UpdateSimulationContext(UpdateSimulationContextRequest request)
        {
            LogTrace("Update simulation context (id: {0}).", request.Id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var entity = repository.Retrieve<SimulationContext>(request.Id);

                Mapper.DynamicMap(request, entity);

                if (ValidationContainer.ValidateObject(entity))
                    repository.Save(entity);
            });
        }

        public void UpdateSimulationContextUsers(List<UpdateSimulationContextUserRequest> requests)
        {
            LogTrace("Update simulation context users.");

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();
                foreach (var userRequest in requests)
                {
                    var simulationContextUser = repository.Retrieve<SimulationContextUser>(userRequest.Id);

                    Mapper.DynamicMap(userRequest, simulationContextUser);

                    if (ValidationContainer.ValidateObject(simulationContextUser))
                        repository.Save(simulationContextUser);
                }
            });
        }

        public Guid CreateNewSimulationContextUser(CreateNewSimulationContextUserRequest request)
        {
            LogTrace("Create simulation context user.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var simulationContextUser = repository.Prepare<SimulationContextUser>();

                Mapper.DynamicMap(request, simulationContextUser);

                if (ValidationContainer.ValidateObject(simulationContextUser))
                    repository.Save(simulationContextUser);

                return simulationContextUser.Id;
            });
        }

        public void DeleteSimulationContextUser(Guid id)
        {
            LogTrace("Delete simulation context user {0}.", id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                repository.Delete<SimulationContextUser>(id);
            });
        }

        public void GenerateNewYear(Guid id)
        {
            LogTrace("Generate new year for simulation context {0}.", id);

            ExecuteTransaction(() =>
                {
                    var repository = Container.Resolve<ISimulationManagementCommandRepository>();
                    var queryService = Container.Resolve<ISimulationManagementQueryService>();
                    var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();

                    var allowedChars = infrastructureQueryService.RetrieveAllowedPasswordChars();

                    var simulationContext = queryService.RetrieveSimulationContext(id);

                    var random = new Random();

                    for (int i = 1; i <= 12; i++)
                    {
                        var simulationContextUser = repository.Prepare<SimulationContextUser>();
                        simulationContextUser.SimulationContextId = id;
                        simulationContextUser.ValidFrom = new DateTime(DateTime.Now.Year + 1, i, 1);
                        simulationContextUser.ValidTo = simulationContextUser.ValidFrom.AddMonths(1).AddSeconds(-1);
                        simulationContextUser.UserName = string.Format(simulationContext.UserNameBase, i.ToString("00"));

                        var chars = new char[simulationContext.PasswordLength];

                        for (int x = 0; x < chars.Length; x++)
                            chars[x] = allowedChars[random.Next(0, allowedChars.Length)];

                        simulationContextUser.Password = new string(chars);

                        if (ValidationContainer.ValidateObject(simulationContextUser))
                            repository.Save(simulationContextUser);
                    }
                });
        }

        public Guid CreateNewSimulationContext(CreateNewSimulationContextRequest request)
        {
            LogTrace("Create simulation context.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var simulationContext = repository.Prepare<SimulationContext>();

                Mapper.DynamicMap(request, simulationContext);

                if (ValidationContainer.ValidateObject(simulationContext))
                    repository.Save(simulationContext);

                return simulationContext.Id;
            });
        }

        public void UpdateSimulationCombination(UpdateSimulationCombinationRequest request)
        {
            LogTrace("Update simulation combination (id: {0}).", request.Id);

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var entity = repository.Retrieve<SimulationCombination>(request.Id);

                Mapper.DynamicMap(request, entity, typeof(UpdateSimulationCombinationRequest), typeof(SimulationCombination));

                repository.Save(entity);

                var simulationCombinationLanguages = repository.ListSimulationCombinationLanguages(request.Id);
                foreach (var simulationCombinationLanguage in simulationCombinationLanguages.Where(sl => !request.AvailableLanguageIds.Contains(sl.LanguageId)))
                    repository.UnlinkSimulationCombinationLanguage(simulationCombinationLanguage.SimulationCombinationId, simulationCombinationLanguage.LanguageId);

                foreach (var languageId in request.AvailableLanguageIds.Where(id => !simulationCombinationLanguages.Select(sl => sl.LanguageId).Contains(id)))
                    repository.LinkSimulationCombinationLanguage(request.Id, languageId);
            });
        }

        public Guid CreateSimulationSet(CreateSimulationSetRequest request)
        {
            LogTrace("Create simulation set.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var entity = repository.Prepare<SimulationSet>();

                Mapper.DynamicMap(request, entity, typeof(CreateSimulationSetRequest), typeof(SimulationSet));

                repository.Save(entity);

                return entity.Id;
            });
        }

        public Guid CreateSimulationDepartment(CreateSimulationDepartmentRequest request)
        {
            LogTrace("Create simulation department.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var entity = repository.Prepare<SimulationDepartment>();

                Mapper.DynamicMap(request, entity, typeof(CreateSimulationDepartmentRequest), typeof(SimulationDepartment));

                repository.Save(entity);

                return entity.Id;
            });
        }

        public Guid CreateSimulationLevel(CreateSimulationLevelRequest request)
        {
            LogTrace("Create simulation level.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var entity = repository.Prepare<SimulationLevel>();

                Mapper.DynamicMap(request, entity, typeof(CreateSimulationLevelRequest), typeof(SimulationLevel));

                repository.Save(entity);

                return entity.Id;
            });
        }

        public Guid CreateSimulation(CreateSimulationRequest request)
        {
            LogTrace("Create simulation.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var simulation = repository.Prepare<Simulation>();

                Mapper.DynamicMap(request, simulation, typeof(CreateSimulationRequest), typeof(Simulation));

                repository.Save(simulation);

                EnsureSimulationTranslations(simulation.Id);

                return simulation.Id;
            });
        }

        public void EnsureSimulationTranslations(Guid simulationId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();
                var infrastructureQueryService = Container.Resolve<IInfrastructureQueryService>();
                var queryService = Container.Resolve<ISimulationManagementQueryService>();

                var simulationTranslations = queryService.ListSimulationTranslations(simulationId);

                //Save translations
                var languages = infrastructureQueryService.ListLanguages();
                var translations = new List<SimulationTranslation>(languages.Count());
                foreach (var language in languages.Where(l => !simulationTranslations.Select(st => st.LanguageId).Contains(l.Id)))
                {
                    var translation = repository.Prepare<SimulationTranslation>();
                    translation.SimulationId = simulationId;
                    translation.LanguageId = language.Id;
                    translation.Name = "[Translation needed]";
                    translations.Add(translation);
                }
                repository.SaveList(translations);
            });
        }

        public Guid CreateSimulationMatrixEntry(CreateSimulationMatrixEntryRequest request)
        {
            LogTrace("Create simulation combination.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ISimulationManagementCommandRepository>();

                var entity = repository.Prepare<SimulationCombination>();

                Mapper.DynamicMap(request, entity, typeof(CreateSimulationMatrixEntryRequest), typeof(SimulationCombination));

                repository.Save(entity);

                return entity.Id;
            });
        }
    }
}
