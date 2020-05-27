using AutoMapper;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.DataModel.Cam;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    var graphService = Container.Resolve<IGraphService>();
                    var infrastructureService = Container.Resolve<IInfrastructureQueryService>();
                    var languages = infrastructureService.ListLanguages();
                    var language = languages.SingleOrDefault(l => l.Id == request.LanguageId)?.Code;
                    var password = GenerateNewPassword(3, 3, 2);
                    var qCandidateUserId = graphService.CreateUser(request.FirstName, request.LastName, language, request.Email, entity.Id, password);

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

                if(request.HasQCandidateAccess)
                {
                    var graphService = Container.Resolve<IGraphService>();
                    var infrastructureService = Container.Resolve<IInfrastructureQueryService>();
                    var languages = infrastructureService.ListLanguages();
                    var language = languages.SingleOrDefault(l => l.Id == request.LanguageId)?.Code;

                    if(!candidate.QCandidateUserId.HasValue)
                    {
                        //Create user in Azure AD B2C
                        var password = GenerateNewPassword(3, 3, 2);
                        var qCandidateUserId = graphService.CreateUser(request.FirstName, request.LastName, language, request.Email, candidate.Id, password);
                        candidate.QCandidateUserId = qCandidateUserId;

                        repository.Save(candidate);
                    }
                    else
                    {
                        //Update user in Azure AD B2C
                        graphService.UpdateUser(candidate.QCandidateUserId.Value, request.FirstName, request.LastName, language, request.Email);
                    }
                }
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
        
        private static string GenerateNewPassword(int lowercase, int uppercase, int numerics)
        {
            var lowers = "abcdefghijklmnopqrstuvwxyz";
            var uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var number = "0123456789";

            var random = new Random();
            var generated = "!";

            for(var i = 0; i < lowercase; ++i)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for(var i = 0; i < uppercase; ++i)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for(var i = 0; i < numerics; ++i)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );

            return generated.Replace("!", string.Empty);
        }
    }
}
