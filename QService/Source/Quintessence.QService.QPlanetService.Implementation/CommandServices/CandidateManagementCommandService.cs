using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using AutoMapper;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.DataModel.Cam;
using Quintessence.QService.DataModel.Prm;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Microsoft.Practices.Unity;
using Quintessence.QService.QueryModel.Prm;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class CandidateManagementCommandService : SecuredUnityServiceBase, ICandidateManagementCommandService
    {
        #region Create methods
        public Guid CreateCandidate(CreateCandidateRequest request)
        {
            LogTrace("Create candidate.");

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICandidateManagementCommandRepository>();

                var entity = repository.Prepare<Candidate>();

                Mapper.DynamicMap(request, entity, typeof(CreateCandidateRequest), typeof(Candidate));

                repository.Save(entity);

                if(entity.HasQCandidateAccess
                   && !entity.QCandidateUserId.HasValue)
                {
                    //Create user in Azure AD B2C
                    var settings = new AzureAdB2CSettings
                    {
                        ApplicationId = "cb70571a-aa65-4e68-96dc-dfc27d8a94a0",
                        TenantId = "kenzequintessenceb2cdev.onmicrosoft.com",
                        ClientSecret = "53H1~0j_wvPj7B}}FdL1dv3,",
                        B2cExtensionApplicationId = "b6df2c72-70d1-4d04-bbe4-c9711bc1293e"
                    };
                    var graphService = new GraphService(settings);
                    var infrastructureService = Container.Resolve<IInfrastructureQueryService>();
                    var languages = infrastructureService.ListLanguages();
                    var language = languages.FirstOrDefault(l => l.Id == request.LanguageId)?.Code;
                    var users = graphService.GetAllUsers();
                    var qCandidateUserId = graphService.CreateUser(request.FirstName, request.LastName, language, request.Email, entity.Id);

                    entity.QCandidateUserId = qCandidateUserId;

                    repository.Save(entity);
                }

                return entity.Id;
            });
        }

        #endregion

        #region Update methods
        public void UpdateCandidate(UpdateCandidateRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICandidateManagementCommandRepository>();

                var candidate = repository.Retrieve<Candidate>(request.Id);

                Mapper.DynamicMap(request, candidate);

                repository.Save(candidate);
            });
        }
        #endregion

        #region Delete methods
        public void DeleteCandidate(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICandidateManagementCommandRepository>();

                repository.Delete<Candidate>(id);
            });
        }

        public void ChangeCandidateLanguage(int languageId, Guid candidateId)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICandidateManagementCommandRepository>();

                var candidate = repository.Retrieve<Candidate>(candidateId);

                candidate.LanguageId = languageId;

                repository.Save(candidate);
            });
        }

        public void CreateProgramComponent(CreateProgramComponentRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICandidateManagementCommandRepository>();
                var queryService = Container.Resolve<ICandidateManagementQueryService>();
                var simulationManagementQueryService = Container.Resolve<ISimulationManagementQueryService>();
                var projectManagementQueryService = Container.Resolve<IProjectManagementQueryService>();

                var programComponents = queryService.ListProgramComponentsByCandidate(request.ProjectCandidateId);
                var projectCandidate = projectManagementQueryService.RetrieveProjectCandidateDetail(request.ProjectCandidateId);

                if (request.SimulationCombinationId.HasValue)
                {
                    var simulationCombination = simulationManagementQueryService.RetrieveSimulationCombination(request.SimulationCombinationId.Value);

                    if (programComponents.All(pc => pc.SimulationCombinationId != request.SimulationCombinationId))
                    {
                        var startDate = programComponents.Count > 0
                                ? programComponents.Max(pc => pc.End)
                                : projectCandidate.ProjectCandidateDetail.AssessmentStartDate.GetValueOrDefault();

                        if (simulationCombination.Preparation > 0)
                        {
                            var preparationProgramComponent = repository.Prepare<ProgramComponent>();
                            preparationProgramComponent.ProjectCandidateId = request.ProjectCandidateId;
                            preparationProgramComponent.AssessmentRoomId = request.AssessmentRoomId;
                            preparationProgramComponent.SimulationCombinationId = request.SimulationCombinationId;
                            preparationProgramComponent.SimulationCombinationTypeCode = 1;
                            preparationProgramComponent.Start = startDate;
                            preparationProgramComponent.End = preparationProgramComponent.Start.AddMinutes(simulationCombination.Preparation);
                            repository.Save(preparationProgramComponent);

                            startDate = preparationProgramComponent.End;
                        }

                        var executionProgramComponent = repository.Prepare<ProgramComponent>();
                        executionProgramComponent.ProjectCandidateId = request.ProjectCandidateId;
                        executionProgramComponent.AssessmentRoomId = request.AssessmentRoomId;
                        executionProgramComponent.SimulationCombinationId = request.SimulationCombinationId;
                        executionProgramComponent.SimulationCombinationTypeCode = 2;
                        executionProgramComponent.Start = startDate;
                        executionProgramComponent.End = executionProgramComponent.Start.AddMinutes(simulationCombination.Execution != 0 ? simulationCombination.Execution : 15);
                        executionProgramComponent.LeadAssessorUserId = projectCandidate.ProjectCandidateDetail.LeadAssessorUserId;
                        repository.Save(executionProgramComponent);
                    }
                }
                else if(request.ProjectCandidateCategoryDetailTypeId.HasValue)
                {
                    var projectTypeCategory = projectManagementQueryService.RetrieveProjectTypeCategory(new RetrieveProjectTypeCategoryRequest { ProjectCandidateCategoryDetailTypeId = request.ProjectCandidateCategoryDetailTypeId.Value });

                    var startDate = programComponents.Count > 0
                            ? programComponents.Max(pc => pc.End)
                            : projectCandidate.ProjectCandidateDetail.AssessmentStartDate.GetValueOrDefault();

                    var programComponent = repository.Prepare<ProgramComponent>();
                    programComponent.ProjectCandidateId = request.ProjectCandidateId;
                    programComponent.AssessmentRoomId = request.AssessmentRoomId;
                    programComponent.ProjectCandidateCategoryDetailTypeId = request.ProjectCandidateCategoryDetailTypeId;
                    programComponent.Start = startDate;
                    programComponent.End = startDate.AddMinutes(projectTypeCategory == null ? 15 : projectTypeCategory.Execution.GetValueOrDefault(15));
                    programComponent.LeadAssessorUserId = projectCandidate.ProjectCandidateDetail.LeadAssessorUserId;
                    repository.Save(programComponent);
                }
                else if (request.ProgramComponentSpecialId.HasValue)
                {
                    var startDate = programComponents.Count > 0
                            ? programComponents.Max(pc => pc.End)
                            : projectCandidate.ProjectCandidateDetail.AssessmentStartDate.GetValueOrDefault();
                    var programComponentSpecial = repository.Retrieve<ProgramComponentSpecial>(request.ProgramComponentSpecialId.Value);

                    var programComponent = repository.Prepare<ProgramComponent>();
                    programComponent.ProjectCandidateId = request.ProjectCandidateId;
                    programComponent.AssessmentRoomId = request.AssessmentRoomId;
                    programComponent.Description = programComponentSpecial.Name;
                    programComponent.Start = startDate;
                    programComponent.End = startDate.AddMinutes(programComponentSpecial.Execution);
                    if (programComponentSpecial.IsWithLeadAssessor)
                        programComponent.LeadAssessorUserId = projectCandidate.ProjectCandidateDetail.LeadAssessorUserId;
                    repository.Save(programComponent);
                }
            });
        }

        public void UpdateProgramComponentEnd(UpdateProgramComponentEndRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICandidateManagementCommandRepository>();

                var programComponent = repository.Retrieve<ProgramComponent>(request.Id);

                Mapper.DynamicMap(request, programComponent);

                programComponent.End = programComponent.End.AddMinutes(request.MinuteDelta);

                if (programComponent.End < programComponent.Start)
                    programComponent.Start = programComponent.End.Subtract(TimeSpan.FromMinutes(15));

                repository.Save(programComponent);
            });
        }

        public void UpdateProgramComponentStart(UpdateProgramComponentStartRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICandidateManagementCommandRepository>();

                var programComponent = repository.Retrieve<ProgramComponent>(request.Id);

                Mapper.DynamicMap(request, programComponent);

                programComponent.Start = programComponent.Start.AddMinutes(request.MinuteDelta);
                programComponent.End = programComponent.End.AddMinutes(request.MinuteDelta);

                if (programComponent.End < programComponent.Start)
                    programComponent.Start = programComponent.End.Subtract(TimeSpan.FromMinutes(15));

                repository.Save(programComponent);
            });
        }

        public void UpdateProgramComponent(UpdateProgramComponentRequest request)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICandidateManagementCommandRepository>();

                var programComponent = repository.Retrieve<ProgramComponent>(request.Id);

                Mapper.DynamicMap(request, programComponent);

                repository.Save(programComponent);
            });
        }

        public void DeleteProgramComponent(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICandidateManagementCommandRepository>();
                var queryService = Container.Resolve<ICandidateManagementQueryService>();

                var programComponents = new List<ProgramComponent>();
                var programComponent = repository.Retrieve<ProgramComponent>(id);

                programComponents.Add(programComponent);
                
                //If a simulation is deleted, delete also the preparation or execution event for that simulation
                if (programComponent.SimulationCombinationId.HasValue)
                {
                    var linkedProgramComponent = queryService.RetrieveLinkedProgramComponent(programComponent.Id);

                    if (linkedProgramComponent != null)
                        programComponents.Add(repository.Retrieve<ProgramComponent>(linkedProgramComponent.Id));
                }

                foreach (var component in programComponents.Where(pc => pc != null))
                    repository.Delete(component);
            });
        }

        public void DeleteProjectCandidateProgramComponents(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICandidateManagementCommandRepository>();

                repository.DeleteProjectCandidateProgramComponents(id);
            });
        }

        public void DeleteRoomProgramComponents(Guid id)
        {
            LogTrace();

            ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<ICandidateManagementCommandRepository>();

                repository.DeleteRoomProgramComponents(id);
            });
        }

        #endregion



    }
}
