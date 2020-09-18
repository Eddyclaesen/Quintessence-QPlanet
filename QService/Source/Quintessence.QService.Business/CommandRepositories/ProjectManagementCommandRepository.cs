using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Core.Security;
using Quintessence.QService.Data.Extensions;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Prm;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QService.Business.CommandRepositories
{
    public class ProjectManagementCommandRepository : CommandRepositoryBase<IPrmCommandContext>, IProjectManagementCommandRepository
    {
        public ProjectManagementCommandRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public Project PrepareProject(Guid projectTypeId, string projectName)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectType = context.ProjectTypes.SingleOrDefault(pt => pt.Id == projectTypeId);

                        switch (projectType.Code)
                        {
                            case "ACDC":
                                var assessmentDevelopmentProject = context.Create<AssessmentDevelopmentProject>();
                                assessmentDevelopmentProject.ProjectTypeId = projectTypeId;
                                assessmentDevelopmentProject.StatusCode = (int)ProjectStatusCodeType.Draft;
                                assessmentDevelopmentProject.Name = projectName;
                                assessmentDevelopmentProject.PricingModelId = 1;
                                return assessmentDevelopmentProject;

                            case "CONS":
                                var consultancyProject = context.Create<ConsultancyProject>();
                                consultancyProject.ProjectTypeId = projectTypeId;
                                consultancyProject.StatusCode = (int)ProjectStatusCodeType.Draft;
                                consultancyProject.Name = projectName;
                                consultancyProject.PricingModelId = 1;
                                return consultancyProject;
                        }

                        return null;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public Guid Save(Project project)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        context.CreateOrUpdate(c => c.Projects, project, typeof(AssessmentDevelopmentProject), typeof(ConsultancyProject));
                        context.SaveChanges();
                        return project.Id;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void LinkProject2CrmProject(Guid projectId, int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        if (context.Project2CrmProjects.Any(link => link.CrmProjectId == crmProjectId && link.ProjectId == projectId))
                            return;

                        var revenueDistribution = context.ProjectRevenueDistributions.FirstOrDefault(prd => prd.ProjectId == projectId && prd.CrmProjectId == crmProjectId);

                        if (revenueDistribution == null)
                        {
                            context.Project2CrmProjects.Add(new Project2CrmProject { CrmProjectId = crmProjectId, ProjectId = projectId });

                            revenueDistribution = Prepare<ProjectRevenueDistribution>();
                            revenueDistribution.ProjectId = projectId;
                            revenueDistribution.CrmProjectId = crmProjectId;
                            context.ProjectRevenueDistributions.Add(revenueDistribution);
                        }
                        else
                        {
                            context.Undelete(revenueDistribution);
                        }

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void UnlinkProject2CrmProject(Guid projectId, int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.Project2CrmProjects.SingleOrDefault(p2Cp => p2Cp.ProjectId == projectId && p2Cp.CrmProjectId == crmProjectId);
                        context.Project2CrmProjects.Remove(link);

                        var revenueDistribution = context.ProjectRevenueDistributions.FirstOrDefault(prd => prd.ProjectId == projectId && prd.CrmProjectId == crmProjectId);

                        if (revenueDistribution != null)
                            context.Delete(revenueDistribution);

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public Project RetrieveProject(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.Projects.SingleOrDefault(p => p.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<Project2CrmProject> ListProject2CrmProject(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.Project2CrmProjects.Where(p => p.ProjectId == projectId).ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCategoryDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCategoryDetail = context.ProjectCategoryDetails.SingleOrDefault(pcd => pcd.Id == id);
                        context.Delete(projectCategoryDetail);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCategoryDetail PrepareProjectCategoryDetail(Guid projectTypeCategoryId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectTypeCategory = RetrieveProjectTypeCategory(projectTypeCategoryId);

                        ProjectCategoryDetail projectCategoryDetail = null;
                        switch (projectTypeCategory.Code)
                        {
                            case "AC":
                                projectCategoryDetail = context.Create<ProjectCategoryAcDetail>();
                                projectCategoryDetail.ProjectTypeCategoryId = projectTypeCategoryId;
                                ((ProjectCategoryAcDetail)projectCategoryDetail).ScoringTypeCode = 10; //With indicators
                                break;

                            case "FA":
                                projectCategoryDetail = context.Create<ProjectCategoryFaDetail>();
                                projectCategoryDetail.ProjectTypeCategoryId = projectTypeCategoryId;
                                ((ProjectCategoryFaDetail)projectCategoryDetail).ScoringTypeCode = 20; //Without indicators
                                break;

                            case "DC":
                                projectCategoryDetail = context.Create<ProjectCategoryDcDetail>();
                                projectCategoryDetail.ProjectTypeCategoryId = projectTypeCategoryId;
                                ((ProjectCategoryDcDetail)projectCategoryDetail).ScoringTypeCode = 10; //With indicators
                                break;

                            case "FD":
                                projectCategoryDetail = context.Create<ProjectCategoryFdDetail>();
                                projectCategoryDetail.ProjectTypeCategoryId = projectTypeCategoryId;
                                ((ProjectCategoryFdDetail)projectCategoryDetail).ScoringTypeCode = 20; //Without indicators
                                break;

                            case "EA":
                                projectCategoryDetail = context.Create<ProjectCategoryEaDetail>();
                                projectCategoryDetail.ProjectTypeCategoryId = projectTypeCategoryId;
                                ((ProjectCategoryEaDetail)projectCategoryDetail).ScoringTypeCode = 10; //With indicators
                                break;

                            case "PS":
                                projectCategoryDetail = context.Create<ProjectCategoryPsDetail>();
                                projectCategoryDetail.ProjectTypeCategoryId = projectTypeCategoryId;
                                ((ProjectCategoryPsDetail)projectCategoryDetail).ScoringTypeCode = 10; //With indicators
                                break;

                            case "SO":
                                projectCategoryDetail = context.Create<ProjectCategorySoDetail>();
                                projectCategoryDetail.ProjectTypeCategoryId = projectTypeCategoryId;
                                ((ProjectCategorySoDetail)projectCategoryDetail).ScoringTypeCode = 10; //With indicators
                                break;

                            case "CA":
                                projectCategoryDetail = context.Create<ProjectCategoryCaDetail>();
                                projectCategoryDetail.ProjectTypeCategoryId = projectTypeCategoryId;
                                break;

                            case "CU":
                                projectCategoryDetail = context.Create<ProjectCategoryCuDetail>();
                                projectCategoryDetail.ProjectTypeCategoryId = projectTypeCategoryId;
                                break;

                            case "CO":
                                projectCategoryDetail = context.Create<ProjectCategoryCuDetail>();
                                projectCategoryDetail.ProjectTypeCategoryId = projectTypeCategoryId;
                                break;

                            default:
                                var queryService = Container.Resolve<IProjectManagementQueryService>();
                                var subCategoryType = queryService.RetrieveProjectTypeCategory(new RetrieveProjectTypeCategoryRequest { Id = projectTypeCategoryId });
                                var defaultValues = context.ProjectTypeCategoryDefaultValues.Where(ptcdt => ptcdt.ProjectTypeCategoryId == projectTypeCategoryId);

                                switch (subCategoryType.SubCategoryType)
                                {
                                    case 1:
                                        projectCategoryDetail = context.Create<ProjectCategoryDetailType1>();
                                        ((ProjectCategoryDetailType1)projectCategoryDetail).Name = subCategoryType.Name;
                                        ((ProjectCategoryDetailType1)projectCategoryDetail).SurveyPlanningId = int.Parse(defaultValues.SingleOrDefault(text => text.Code.ToUpper() == "SURVEYPLANNINGID").Value);
                                        ((ProjectCategoryDetailType1)projectCategoryDetail).Description = defaultValues.SingleOrDefault(text => text.Code.ToUpper() == "DESCRIPTION").Value;
                                        ((ProjectCategoryDetailType1)projectCategoryDetail).MailTextStandalone = defaultValues.SingleOrDefault(text => text.Code.ToUpper() == "MAILTEXTSTANDALONE").Value;
                                        ((ProjectCategoryDetailType1)projectCategoryDetail).MailTextIntegrated = defaultValues.SingleOrDefault(text => text.Code.ToUpper() == "MAILTEXTINTEGRATED").Value;
                                        break;
                                    case 2:
                                        projectCategoryDetail = context.Create<ProjectCategoryDetailType2>();
                                        ((ProjectCategoryDetailType2)projectCategoryDetail).Name = subCategoryType.Name;
                                        ((ProjectCategoryDetailType2)projectCategoryDetail).SurveyPlanningId = int.Parse(defaultValues.SingleOrDefault(text => text.Code.ToUpper() == "SURVEYPLANNINGID").Value);
                                        ((ProjectCategoryDetailType2)projectCategoryDetail).Description = defaultValues.SingleOrDefault(text => text.Code.ToUpper() == "DESCRIPTION").Value;
                                        break;
                                    case 3:
                                        projectCategoryDetail = context.Create<ProjectCategoryDetailType3>();
                                        ((ProjectCategoryDetailType3)projectCategoryDetail).Name = subCategoryType.Name;
                                        ((ProjectCategoryDetailType3)projectCategoryDetail).SurveyPlanningId = int.Parse(defaultValues.SingleOrDefault(text => text.Code.ToUpper() == "SURVEYPLANNINGID").Value);
                                        ((ProjectCategoryDetailType3)projectCategoryDetail).Description = defaultValues.SingleOrDefault(text => text.Code.ToUpper() == "DESCRIPTION").Value;
                                        if (((ProjectCategoryDetailType3)projectCategoryDetail).Name == "NEO-pir")
                                        {
                                            ((ProjectCategoryDetailType3)projectCategoryDetail).IncludeInCandidateReport = true; 
                                        }
                                        else
                                        {
                                            ((ProjectCategoryDetailType3)projectCategoryDetail).IncludeInCandidateReport = false; //Default false
                                        }                                                                              
                                        ((ProjectCategoryDetailType3)projectCategoryDetail).MailTextStandalone = defaultValues.SingleOrDefault(text => text.Code.ToUpper() == "MAILTEXTSTANDALONE").Value;
                                        ((ProjectCategoryDetailType3)projectCategoryDetail).MailTextIntegrated = defaultValues.SingleOrDefault(text => text.Code.ToUpper() == "MAILTEXTINTEGRATED").Value;
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException("projectTypeCategoryId", "Unable to find project type category.");
                                }
                                projectCategoryDetail.ProjectTypeCategoryId = projectTypeCategoryId;
                                break;
                        }

                        return projectCategoryDetail;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectTypeCategory RetrieveProjectTypeCategory(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectTypeCategory = context.ProjectTypeCategories.SingleOrDefault(ptc => ptc.Id == id);

                        return projectTypeCategory;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }
        public ProjectCategoryDetail2DictionaryIndicator RetrieveProjectCategoryDetail2DictionaryIndicator(Guid projectCategoryDetailId, Guid dictionaryIndicatorId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCategoryDetail2DictionaryIndicators
                            .SingleOrDefault(e => e.ProjectCategoryDetailId == projectCategoryDetailId && e.DictionaryIndicatorId == dictionaryIndicatorId);

                        return link;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCategoryDetail2DictionaryIndicator RetrieveProjectCategoryDetail2DictionaryIndicator(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCategoryDetail2DictionaryIndicators
                            .SingleOrDefault(e => e.Id == id);

                        return link;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void LinkProjectCategoryDetail2DictionaryIndicator(Guid projectCategoryDetailId, Guid dictionaryIndicatorId, bool isDefinedByRole = false, bool isStandard = false, bool isDistinctive = false)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCategoryDetail2DictionaryIndicators.Create();
                        context.ProjectCategoryDetail2DictionaryIndicators.Add(link);
                        link.Id = Guid.NewGuid();
                        link.ProjectCategoryDetailId = projectCategoryDetailId;
                        link.DictionaryIndicatorId = dictionaryIndicatorId;
                        link.IsDefinedByRole = isDefinedByRole;
                        link.IsStandard = isStandard;
                        link.IsDistinctive = isDistinctive;

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCategoryDetail2DictionaryIndicator(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCategoryDetail2DictionaryIndicators.SingleOrDefault(e => e.Id == id);
                        context.ProjectCategoryDetail2DictionaryIndicators.Remove(link);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCategoryDetail2DictionaryIndicator(Guid projectCategoryDetailId, Guid dictionaryIndicatorId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCategoryDetail2DictionaryIndicators
                            .SingleOrDefault(e => e.ProjectCategoryDetailId == projectCategoryDetailId && e.DictionaryIndicatorId == dictionaryIndicatorId);

                        //If link not found, return
                        if (link == null)
                            return;

                        context.ProjectCategoryDetail2DictionaryIndicators.Remove(link);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCategoryDetail2SimulationCombination RetrieveProjectCategoryDetail2SimulationCombination(Guid projectCategoryDetailId, Guid simulationCombinationId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var combination = context.ProjectCategoryDetail2SimulationCombinations.SingleOrDefault(e => e.ProjectCategoryDetailId == projectCategoryDetailId && e.SimulationCombinationId == simulationCombinationId);

                        return combination;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void LinkProjectCategoryDetail2SimulationCombination(Guid projectCategoryDetailId, Guid simulationCombinationId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCategoryDetail2SimulationCombinations.Create();
                        context.ProjectCategoryDetail2SimulationCombinations.Add(link);
                        link.Id = Guid.NewGuid();
                        link.ProjectCategoryDetailId = projectCategoryDetailId;
                        link.SimulationCombinationId = simulationCombinationId;

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCategoryDetail2SimulationCombination(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCategoryDetail2SimulationCombinations.SingleOrDefault(e => e.Id == id);
                        context.ProjectCategoryDetail2SimulationCombinations.Remove(link);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCategoryDetail2Competence2Combination RetrieveProjectCategoryDetail2Competence2Combination(Guid projectCategoryDetailId, Guid dictionaryCompetenceId, Guid simulationCombinationId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entity = context
                            .ProjectCategoryDetail2Competence2Combinations
                            .FirstOrDefault(link => link.ProjectCategoryDetailId == projectCategoryDetailId
                                                    && link.DictionaryCompetenceId == dictionaryCompetenceId
                                                    && link.SimulationCombinationId == simulationCombinationId);
                        return entity;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCategoryDetail2Competence2Combination PrepareProjectCategoryDetail2Competence2Combination(Guid projectCategoryDetailId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return new ProjectCategoryDetail2Competence2Combination { Id = Guid.NewGuid(), ProjectCategoryDetailId = projectCategoryDetailId };
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void Save(ProjectCategoryDetail2Competence2Combination projectCategoryDetail2Competence2Combination)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        context.ProjectCategoryDetail2Competence2Combinations.Add(projectCategoryDetail2Competence2Combination);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void Delete(ProjectCategoryDetail2Competence2Combination projectCategoryDetail2Competence2Combination)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        context.ProjectCategoryDetail2Competence2Combinations.Attach(projectCategoryDetail2Competence2Combination);
                        context.ProjectCategoryDetail2Competence2Combinations.Remove(projectCategoryDetail2Competence2Combination);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void UnlinkProjectRole2DictionaryLevel(Guid projectRoleId, Guid dictionaryLevelId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link =
                            context.ProjectRole2DictionaryLevels.SingleOrDefault(
                                prdl =>
                                prdl.ProjectRoleId == projectRoleId && prdl.DictionaryLevelId == dictionaryLevelId);
                        if (link != null)
                        {
                            context.ProjectRole2DictionaryLevels.Remove(link);
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void LinkProjectRole2DictionaryLevel(Guid projectRoleId, Guid selectedLevelId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link =
                            context.ProjectRole2DictionaryLevels.SingleOrDefault(
                                prdl =>
                                prdl.ProjectRoleId == projectRoleId && prdl.DictionaryLevelId == selectedLevelId);
                        if (link != null)
                            return;

                        link = context.ProjectRole2DictionaryLevels.Add(context.ProjectRole2DictionaryLevels.Create());
                        link.ProjectRoleId = projectRoleId;
                        link.DictionaryLevelId = selectedLevelId;
                        context.SaveChanges();

                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void LinkProjectRole2DictionaryIndicator(Guid projectRoleId, Guid dictionaryIndicatorId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectRole2DictionaryIndicators
                            .SingleOrDefault(prdl => prdl.ProjectRoleId == projectRoleId && prdl.DictionaryIndicatorId == dictionaryIndicatorId);

                        if (link != null)
                            return;

                        link = context.ProjectRole2DictionaryIndicators.Add(context.ProjectRole2DictionaryIndicators.Create());
                        link.ProjectRoleId = projectRoleId;
                        link.DictionaryIndicatorId = dictionaryIndicatorId;
                        link.Norm = 10;
                        context.SaveChanges();

                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void UpdateProjectRoleDictionaryIndicatorNorm(Guid projectRoleId, Guid dictionaryIndicatorId, int norm)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectRole2DictionaryIndicators
                            .SingleOrDefault(l => l.ProjectRoleId == projectRoleId && l.DictionaryIndicatorId == dictionaryIndicatorId);

                        if (link == null)
                        {
                            link = context.ProjectRole2DictionaryIndicators.Add(context.ProjectRole2DictionaryIndicators.Create());
                            link.DictionaryIndicatorId = dictionaryIndicatorId;
                            link.ProjectRoleId = projectRoleId;
                            link.Norm = norm;
                        }
                        else
                        {
                            link.Norm = norm;
                        }

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectRoleDictionaryIndicator(Guid projectRoleId, Guid dictionaryIndicatorId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectRole2DictionaryIndicators
                            .SingleOrDefault(l => l.ProjectRoleId == projectRoleId && l.DictionaryIndicatorId == dictionaryIndicatorId);

                        if (link != null)
                            context.ProjectRole2DictionaryIndicators.Remove(link);

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCandidateScores(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var competenceScores = context.ProjectCandidateCompetenceSimulationScores
                            .Where(pccss => pccss.ProjectCandidateId == projectCandidateId);

                        competenceScores.ForEach(score => context.ProjectCandidateCompetenceSimulationScores.Remove(score));

                        var indicatorScores = context.ProjectCandidateIndicatorSimulationScores
                            .Where(pciss => pciss.ProjectCandidateId == projectCandidateId);

                        indicatorScores.ForEach(score => context.ProjectCandidateIndicatorSimulationScores.Remove(score));
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCategoryDetailDictionaryIndicators(Guid projectCategoryDetailId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var links = context.ProjectCategoryDetail2DictionaryIndicators
                            .Where(link => link.ProjectCategoryDetailId == projectCategoryDetailId);

                        links.ForEach(link => context.ProjectCategoryDetail2DictionaryIndicators.Remove(link));
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCategoryDetailCompetenceSimulations(Guid projectCategoryDetailId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var links = context.ProjectCategoryDetail2Competence2Combinations
                            .Where(link => link.ProjectCategoryDetailId == projectCategoryDetailId);

                        links.ForEach(link => context.ProjectCategoryDetail2Competence2Combinations.Remove(link));
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCategoryDetailSimulationCombinations(Guid projectCategoryDetailId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var links = context.ProjectCategoryDetail2Competence2Combinations
                            .Where(link => link.ProjectCategoryDetailId == projectCategoryDetailId);

                        links.ForEach(link => context.ProjectCategoryDetail2Competence2Combinations.Remove(link));
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public Proposal PrepareProposal()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var proposal = this.Prepare<Proposal>();

                        proposal.StatusCode = 10;

                        return proposal;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public string GenerateType3Code(int crmParticipantId, string firstName, string lastName, string languageCode, string categoryCode)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.GenerateType3Code(crmParticipantId, firstName, lastName, languageCode, categoryCode);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void MarkDocumentAsImportant(Guid projectId, Guid uniqueId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectDocumentMetadatas.Add(context.ProjectDocumentMetadatas.Create());
                        link.ProjectId = projectId;
                        link.DocumentUniqueId = uniqueId;
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void UnmarkDocumentAsImportant(Guid projectId, Guid uniqueId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectDocumentMetadatas.FirstOrDefault(pdm => pdm.ProjectId == projectId && pdm.DocumentUniqueId == uniqueId);

                        if (link != null)
                            context.ProjectDocumentMetadatas.Remove(link);

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectDna2ProjectDnaType> ListProjectDnaProjectTypes(Guid projectDnaId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var links = context.ProjectDna2ProjectDnaTypes
                            .Where(link => link.ProjectDnaId == projectDnaId)
                            .ToList();
                        return links;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectDna2CrmPerson> ListProjectDnaProjectContactPersons(Guid projectDnaId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var links = context.ProjectDna2CrmPersons
                            .Where(link => link.ProjectDnaId == projectDnaId)
                            .ToList();
                        return links;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectDna2CrmContactPerson(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectDna2CrmPersons.SingleOrDefault(l => l.Id == id);
                        context.ProjectDna2CrmPersons.Remove(link);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectDna2ProjectDnaType(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectDna2ProjectDnaTypes.SingleOrDefault(l => l.Id == id);
                        context.ProjectDna2ProjectDnaTypes.Remove(link);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectTypeCategoryUnitPrice RetrieveProjectTypeCategoryUnitPrice(Guid projectTypeCategoryId, Guid projectTypeCategoryLevelId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var unitPrice = context.ProjectTypeCategoryUnitPrices.SingleOrDefault(ptcup => ptcup.ProjectTypeCategoryId == projectTypeCategoryId
                                                                    && ptcup.ProjectTypeCategoryLevelId == projectTypeCategoryLevelId);

                        return unitPrice;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectTypeCategoryUnitPrice PrepareProjectTypeCategoryUnitPrice(Guid projectTypeCategoryId, Guid projectTypeCategoryLevelId, decimal unitPrice)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectTypeCategoryUnitPrice = Prepare<ProjectTypeCategoryUnitPrice>();
                    projectTypeCategoryUnitPrice.ProjectTypeCategoryId = projectTypeCategoryId;
                    projectTypeCategoryUnitPrice.ProjectTypeCategoryLevelId = projectTypeCategoryLevelId;
                    projectTypeCategoryUnitPrice.UnitPrice = unitPrice;
                    return projectTypeCategoryUnitPrice;
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectEvaluation RetrieveProjectEvaluationByCrmProject(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectEvaluation = context.ProjectEvaluations.FirstOrDefault(pe => pe.CrmProjectId == crmProjectId);

                        return projectEvaluation;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public Guid CreateTheoremListRequestCandidate(int contactId, Guid projectId, Guid candidateId, DateTime deadline)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var existingRequest =
                            context.TheoremListRequests.FirstOrDefault(
                                tlr =>
                                tlr.ContactId == contactId && tlr.ProjectId == projectId &&
                                tlr.CandidateId == candidateId);

                        //If there is no existing request, create one.
                        if (existingRequest == null)
                        {
                            var theoremListRequest = Prepare<TheoremListRequest>();
                            theoremListRequest.ContactId = contactId;
                            theoremListRequest.ProjectId = projectId;
                            theoremListRequest.Deadline = deadline;
                            theoremListRequest.TheoremListRequestTypeId = (int)TheoremListRequestType.AsIs;
                            theoremListRequest.CandidateId = candidateId;
                            theoremListRequest.IsMailSent = false;
                            theoremListRequest.RequestDate = DateTime.Now;

                            //Create the verification code for the theorem list request.
                            var verificationCode = Guid.NewGuid().ToString().ToUpperInvariant().Substring(0, 6);

                            //Check if code is unique throughout all existings requests.
                            while (context.TheoremListRequests.Any(tl => tl.VerificationCode == verificationCode))
                                verificationCode = Guid.NewGuid().ToString().ToUpperInvariant().Substring(0, 6);

                            theoremListRequest.VerificationCode = verificationCode;
                            context.TheoremListRequests.Add(theoremListRequest);

                            var theoremListTemplate = context.TheoremListTemplates.FirstOrDefault(tlt => tlt.Code == "EMPLOYEE");
                            var theoremTemplates = context.TheoremTemplates.Where(tt => tt.TheoremListTemplateId == theoremListTemplate.Id).ToList();

                            CopyTheoremListFromTemplate(theoremListRequest, 1, context, theoremTemplates);

                            context.SaveChanges();

                            return theoremListRequest.Id;
                        }

                        return existingRequest.Id;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCandidateCategoryDetail(List<Guid> candidateIds, Guid projectCategoryDetailId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        foreach (var detail in context.ProjectCandidateCategoryDetailTypes1.Where(pccdt => candidateIds.Contains(pccdt.ProjectCandidateId) && pccdt.ProjectCategoryDetailType1Id == projectCategoryDetailId))
                            context.Delete(detail);

                        foreach (var detail in context.ProjectCandidateCategoryDetailTypes2.Where(pccdt => candidateIds.Contains(pccdt.ProjectCandidateId) && pccdt.ProjectCategoryDetailType2Id == projectCategoryDetailId))
                            context.Delete(detail);

                        foreach (var detail in context.ProjectCandidateCategoryDetailTypes3.Where(pccdt => candidateIds.Contains(pccdt.ProjectCandidateId) && pccdt.ProjectCategoryDetailType3Id == projectCategoryDetailId))
                            context.Delete(detail);

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCandidatesCompetenceScoring(IEnumerable<Guid> projectCandidateIds)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var securityContext = context.Container.Resolve<SecurityContext>();
                        foreach (var projectCandidateId in projectCandidateIds)
                        {
                            context.DeleteProjectCandidateCompetenceScores(projectCandidateId, securityContext.UserName);
                            context.DeleteProjectCandidateIndicatorScores(projectCandidateId, securityContext.UserName);
                        }
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCandidatesClusterScore(IEnumerable<Guid> projectCandidateIds)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var securityContext = context.Container.Resolve<SecurityContext>();
                        foreach (var projectCandidateId in projectCandidateIds)
                        {
                            context.DeleteProjectCandidateClusteScores(projectCandidateId, securityContext.UserName);
                            context.DeleteProjectCandidateCompetenceScores(projectCandidateId, securityContext.UserName);
                            context.DeleteProjectCandidateIndicatorScores(projectCandidateId, securityContext.UserName);
                        }
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCandidateCompetenceSimulationScores(IEnumerable<Guid> ids)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        foreach (var id in ids)
                            context.DeleteProjectCandidateCompetenceSimulationScore(id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCandidateFocusedIndicatorSimulationScores(IEnumerable<Guid> ids)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        foreach (var id in ids)
                            context.DeleteProjectCandidateFocusedIndicatorSimulationScores(id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCandidateStandardIndicatorSimulationScores(IEnumerable<Guid> ids)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        foreach (var id in ids)
                            context.DeleteProjectCandidateStandardIndicatorSimulationScores(id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void CreateProjectRenevueDistribution(Guid projectId, int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectRevenueDistribution = context.ProjectRevenueDistributions.FirstOrDefault(prd => prd.ProjectId == projectId && prd.CrmProjectId == crmProjectId);

                        if (projectRevenueDistribution != null && projectRevenueDistribution.Audit.IsDeleted)
                        {
                            context.Undelete(projectRevenueDistribution);
                        }
                        else
                        {
                            var prd = context.ProjectRevenueDistributions.Add(Prepare<ProjectRevenueDistribution>());
                            prd.ProjectId = projectId;
                            prd.CrmProjectId = crmProjectId;
                        }

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectRevenueDistribution(Guid projectId, int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectRevenueDistribution = context.ProjectRevenueDistributions.FirstOrDefault(prd => prd.ProjectId == projectId && prd.CrmProjectId == crmProjectId);

                        if (projectRevenueDistribution != null && !projectRevenueDistribution.Audit.IsDeleted)
                            context.Delete(projectRevenueDistribution);

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public Guid CreateTheoremListRequestContact(int contactId, Guid projectId, int crmEmailId, DateTime deadline, int theoremListRequestTypeId, string description)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var theoremListRequest = Prepare<TheoremListRequest>();
                        theoremListRequest.ContactId = contactId;
                        theoremListRequest.ProjectId = projectId;
                        theoremListRequest.CrmEmailId = crmEmailId;
                        theoremListRequest.Deadline = deadline;
                        theoremListRequest.TheoremListRequestTypeId = theoremListRequestTypeId;
                        theoremListRequest.CandidateId = null;
                        theoremListRequest.IsMailSent = false;
                        theoremListRequest.RequestDate = DateTime.Now;
                        theoremListRequest.Description = description;

                        //Create the verification code for the theorem list request.
                        var verificationCode = Guid.NewGuid().ToString().ToUpperInvariant().Substring(0, 6);

                        //Check if code is unique throughout all existings requests.
                        while (context.TheoremListRequests.Any(tl => tl.VerificationCode == verificationCode))
                            verificationCode = Guid.NewGuid().ToString().ToUpperInvariant().Substring(0, 6);

                        theoremListRequest.VerificationCode = verificationCode;
                        context.TheoremListRequests.Add(theoremListRequest);

                        var theoremListTemplate = context.TheoremListTemplates.FirstOrDefault(tlt => tlt.Code == "COMPANY");
                        var theoremTemplates = context.TheoremTemplates.Where(tt => tt.TheoremListTemplateId == theoremListTemplate.Id).ToList();

                        switch (theoremListRequestTypeId)
                        {
                            case 1://As is.
                                CopyTheoremListFromTemplate(theoremListRequest, 1, context, theoremTemplates);
                                break;
                            case 2://As is & to be.
                                CopyTheoremListFromTemplate(theoremListRequest, 1, context, theoremTemplates);
                                CopyTheoremListFromTemplate(theoremListRequest, 2, context, theoremTemplates);
                                break;
                            default:
                                throw new NullReferenceException("Theorem list request has no type.");
                        }


                        context.SaveChanges();

                        return theoremListRequest.Id;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<TheoremList> RetrieveTheoremListsByTheoremListRequestId(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var theoremLists = context.TheoremLists.Where(tl => tl.TheoremListRequestId == id).ToList();
                        return theoremLists;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        private void CopyTheoremListFromTemplate(TheoremListRequest theoremListRequest, int theoremListTypeId, IPrmCommandContext context,
                                                 IEnumerable<TheoremTemplate> theoremTemplates)
        {
            var theoremList = PrepareTheoremList(theoremListRequest.Id, theoremListTypeId);
            theoremList.TheoremListRequest = theoremListRequest;
            context.TheoremLists.Add(theoremList);

            foreach (var theoremTemplate in theoremTemplates)
            {
                var templateTranslations =
                    context.TheoremTemplateTranslations.Where(
                        ttt => ttt.TheoremTemplateId == theoremTemplate.Id).ToList();

                var theorem = Prepare<Theorem>();
                theorem.IsLeastApplicable = false;
                theorem.IsMostApplicable = false;
                theorem.TheoremListId = theoremList.Id;
                theorem.TheoremList = theoremList;
                context.Theorems.Add(theorem);

                foreach (var templateTranslation in templateTranslations)
                {
                    var theoremTranslation = Prepare<TheoremTranslation>();
                    theoremTranslation.Quote = templateTranslation.Text;
                    theoremTranslation.TheoremId = theorem.Id;
                    theoremTranslation.LanguageId = templateTranslation.LanguageId;
                    theoremTranslation.Theorem = theorem;
                    context.TheoremTranslations.Add(theoremTranslation);
                }
            }
        }

        private TheoremList PrepareTheoremList(Guid theoremListRequestId, int theoremListTypeId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var theoremList = Prepare<TheoremList>();
                        theoremList.IsCompleted = false;
                        theoremList.TheoremListRequestId = theoremListRequestId;
                        theoremList.TheoremListTypeId = theoremListTypeId;

                        //Create the verification code for the theorem list.
                        var verificationCode = Guid.NewGuid().ToString().ToUpperInvariant().Substring(0, 6);

                        //Check if code is unique throughout all existings lists.
                        while (context.TheoremLists.Any(tl => tl.VerificationCode == verificationCode))
                            verificationCode = Guid.NewGuid().ToString().ToUpperInvariant().Substring(0, 6);
                        theoremList.VerificationCode = verificationCode;

                        return theoremList;
                    }

                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCategoryDetail2DictionaryIndicator> ListProjectCategoryDetailDictionaryIndicators(Guid projectCategoryDetailId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var links = context.ProjectCategoryDetail2DictionaryIndicators
                            .Where(pcddi => pcddi.ProjectCategoryDetailId == projectCategoryDetailId)
                            .ToList();

                        return links;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void MarkProjectCategoryDetail2DictionaryIndicatorAsStandard(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCategoryDetail2DictionaryIndicators.SingleOrDefault(l => l.Id == id);

                        if (link == null || link.IsDefinedByRole)
                            return;

                        link.IsStandard = true;
                        link.IsDistinctive = false;

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void MarkProjectCategoryDetail2DictionaryIndicatorAsDistinctive(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCategoryDetail2DictionaryIndicators.SingleOrDefault(l => l.Id == id);

                        if (link == null || link.IsDefinedByRole)
                            return;

                        link.IsStandard = false;
                        link.IsDistinctive = true;

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void UnMarkProjectCategoryDetail2DictionaryIndicator(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCategoryDetail2DictionaryIndicators.SingleOrDefault(l => l.Id == id);

                        if (link == null || link.IsDefinedByRole)
                            return;

                        link.IsStandard = false;
                        link.IsDistinctive = false;

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCategoryDetail2Competence2Combination> ListProjectCategoryDetail2Competence2CombinationByCompetence(Guid projectCategoryDetailId, Guid? dictionaryCompetenceId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var links = context.ProjectCategoryDetail2Competence2Combinations
                            .Where(link => link.ProjectCategoryDetailId == projectCategoryDetailId
                                && link.DictionaryCompetenceId == dictionaryCompetenceId)
                            .ToList();

                        return links;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void DeleteProjectCategoryDetail2DictionaryLevel(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProjectCategoryDetail RetrieveProjectCategoryDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectCategoryDetails.SingleOrDefault(p => p.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public Guid Save(ProjectCategoryDetail projectCategoryDetail)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        context.CreateOrUpdate(c => c.ProjectCategoryDetails, projectCategoryDetail, typeof(ProjectCategoryAcDetail), typeof(ProjectCategoryDcDetail), typeof(ProjectCategoryEaDetail), typeof(ProjectCategoryFaDetail), typeof(ProjectCategoryFdDetail), typeof(ProjectCategoryPsDetail), typeof(ProjectCategorySoDetail), typeof(ProjectCategoryCaDetail), typeof(ProjectCategoryDetailType1), typeof(ProjectCategoryDetailType2), typeof(ProjectCategoryDetailType3));
                        context.SaveChanges();
                        return projectCategoryDetail.Id;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCategoryDetail> ListProjectCategoryDetails(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectCategoryDetails
                            .Where(p => p.ProjectId == projectId && !p.Audit.IsDeleted)
                            .ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidate RetrieveProjectCandidate(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectCandidates.SingleOrDefault(pc => pc.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidateCategoryDetailType1 ProjectCandidateCategoryType1(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectCandidateCategoryDetailTypes1.SingleOrDefault(pc => pc.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidateCategoryDetailType2 ProjectCandidateCategoryType2(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectCandidateCategoryDetailTypes2.SingleOrDefault(x => x.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidateCategoryDetailType3 ProjectCandidateCategoryType3(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectCandidateCategoryDetailTypes3.SingleOrDefault(x => x.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectProduct RetrieveProjectProduct(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectProducts.SingleOrDefault(x => x.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public TimesheetEntry RetrieveTimeSheetEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.TimesheetEntries.SingleOrDefault(x => x.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProductSheetEntry RetrieveProductSheetEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProductSheetEntries.SingleOrDefault(x => x.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectFixedPrice RetrieveProjectFixedPrice(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectFixedPrices.SingleOrDefault(x => x.Id == id);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }                            

        public ProjectCandidate RetrieveProject2Candidate(Guid projectId, Guid candidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectCandidates.SingleOrDefault(pc => pc.Id == candidateId && pc.ProjectId == projectId);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void LinkProject2Candidate(Guid projectId, Guid candidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidate = context.Create<ProjectCandidate>();
                        projectCandidate.CandidateId = candidateId;
                        projectCandidate.ProjectId = projectId;
                        context.ProjectCandidates.Add(projectCandidate);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void UnlinkProject2Candidate(Guid projectId, Guid candidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCandidates.SingleOrDefault(pc => pc.ProjectId == projectId && pc.CandidateId == candidateId);
                        context.ProjectCandidates.Remove(link);
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void LinkSubProject(Guid projectId, Guid subProjectId, Guid? projectCandidateId = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.SubProjects.Add(context.SubProjects.Create());
                        link.ProjectId = projectId;
                        link.SubProjectId = subProjectId;
                        link.ProjectCandidateId = projectCandidateId;
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void LinkProjectType2ProjectTypeCategory(Guid projectTypeId, Guid projectTypeCategoryId, bool isMain)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectType2ProjectTypeCategories.Add(context.ProjectType2ProjectTypeCategories.Create());
                        link.ProjectTypeId = projectTypeId;
                        link.ProjectTypeCategoryId = projectTypeCategoryId;
                        link.IsMain = isMain;
                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void CreateSubcategoryDefaultValues(Guid id, int subcategoryType)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {

                        var defaultValueSurveyPlanning = context.ProjectTypeCategoryDefaultValues.Add(context.Create<ProjectTypeCategoryDefaultValue>());
                        defaultValueSurveyPlanning.Code = "SURVEYPLANNINGID";
                        defaultValueSurveyPlanning.ProjectTypeCategoryId = id;
                        defaultValueSurveyPlanning.Value = "1";

                        var defaultValueDescription = context.ProjectTypeCategoryDefaultValues.Add(context.Create<ProjectTypeCategoryDefaultValue>());
                        defaultValueDescription.Code = "DESCRIPTION";
                        defaultValueDescription.ProjectTypeCategoryId = id;
                        defaultValueDescription.Value = "default description";

                        if (subcategoryType == 1 || subcategoryType == 3)
                        {
                            var defaultValueMailTextStandalone = context.ProjectTypeCategoryDefaultValues.Add(context.Create<ProjectTypeCategoryDefaultValue>());
                            defaultValueMailTextStandalone.Code = "MAILTEXTSTANDALONE";
                            defaultValueMailTextStandalone.ProjectTypeCategoryId = id;
                            defaultValueMailTextStandalone.Value = "default text for standalone mail";

                            var defaultValueMailTextIntegrated = context.ProjectTypeCategoryDefaultValues.Add(context.Create<ProjectTypeCategoryDefaultValue>());
                            defaultValueMailTextIntegrated.Code = "MAILTEXTINTEGRATED";
                            defaultValueMailTextIntegrated.ProjectTypeCategoryId = id;
                            defaultValueMailTextIntegrated.Value = "default text for integrated mail";
                        }

                        context.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public TProjectCandidateCategoryDetailType RetrieveProjectCandidateCategoryDetailType<TProjectCandidateCategoryDetailType>(Guid Id)
            where TProjectCandidateCategoryDetailType : class, IProjectCandidateCategoryDetailType
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        if (typeof(TProjectCandidateCategoryDetailType) == typeof(ProjectCandidateCategoryDetailType1))
                            return context.ProjectCandidateCategoryDetailTypes1.SingleOrDefault(detail => detail.Id == Id) as TProjectCandidateCategoryDetailType;

                        if (typeof(TProjectCandidateCategoryDetailType) == typeof(ProjectCandidateCategoryDetailType2))
                            return context.ProjectCandidateCategoryDetailTypes2.SingleOrDefault(detail => detail.Id == Id) as TProjectCandidateCategoryDetailType;

                        if (typeof(TProjectCandidateCategoryDetailType) == typeof(ProjectCandidateCategoryDetailType3))
                            return context.ProjectCandidateCategoryDetailTypes3.SingleOrDefault(detail => detail.Id == Id) as TProjectCandidateCategoryDetailType;

                        throw new ArgumentOutOfRangeException();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void Save(IProjectCandidateCategoryDetailType projectCandidateCategoryDetailType)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var securityContext = context.Container.Resolve<SecurityContext>();

                        if (projectCandidateCategoryDetailType is ProjectCandidateCategoryDetailType1)
                        {
                            var detail = (ProjectCandidateCategoryDetailType1)projectCandidateCategoryDetailType;
                            detail.Audit.ModifiedOn = DateTime.Now;
                            detail.Audit.ModifiedBy = securityContext.UserName;
                            detail.Audit.VersionId = Guid.NewGuid();
                            context.ProjectCandidateCategoryDetailTypes1.Attach(detail);
                            context.Entry(detail).State = EntityState.Modified;
                            if (Container.Resolve<ValidationRuleEngine>().Validate(Container, detail))
                                context.SaveChanges();
                        }
                        else if (projectCandidateCategoryDetailType is ProjectCandidateCategoryDetailType2)
                        {
                            var detail = (ProjectCandidateCategoryDetailType2)projectCandidateCategoryDetailType;

                            detail.Audit.ModifiedOn = DateTime.Now;
                            detail.Audit.ModifiedBy = securityContext.UserName;
                            detail.Audit.VersionId = Guid.NewGuid();
                            context.CreateOrUpdate(c => c.ProjectCandidateCategoryDetailTypes2, detail, typeof(ProjectCandidateCategoryDetailType2));
                            //context.ProjectCandidateCategoryDetailTypes2.AddOrUpdate(detail);
                            if (Container.Resolve<ValidationRuleEngine>().Validate(Container, detail))
                                context.SaveChanges();
                        }
                        else if (projectCandidateCategoryDetailType is ProjectCandidateCategoryDetailType3)
                        {
                            var detail = (ProjectCandidateCategoryDetailType3)projectCandidateCategoryDetailType;
                            detail.Audit.ModifiedOn = DateTime.Now;
                            detail.Audit.ModifiedBy = securityContext.UserName;
                            detail.Audit.VersionId = Guid.NewGuid();
                            context.ProjectCandidateCategoryDetailTypes3.Attach(detail);
                            context.Entry(detail).State = EntityState.Modified;
                            if (Container.Resolve<ValidationRuleEngine>().Validate(Container, detail))
                                context.SaveChanges();
                        }
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public Guid LinkProjectCandidateCategoryDetailType1(Guid projectCandidateId, Guid projectId, Guid projectCategoryDetailTypeId, decimal invoiceAmount, DateTime? scheduledDate = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var project = context.Projects.FirstOrDefault(p => p.Id == projectId);
                        var link = Prepare<ProjectCandidateCategoryDetailType1>();
                        context.ProjectCandidateCategoryDetailTypes1.Add(link);
                        link.Id = Guid.NewGuid();
                        link.ProjectCandidateId = projectCandidateId;
                        link.ProjectCategoryDetailType1Id = projectCategoryDetailTypeId;
                        switch (project.PricingModelId)
                        {
                            case 1: //Time & Material
                                link.InvoiceStatusCode = (int)InvoiceStatusType.Planned;
                                break;
                            case 2: //Fixed price
                                link.InvoiceStatusCode = (int)InvoiceStatusType.FixedPrice;
                                break;
                            default: throw new ArgumentOutOfRangeException("projectId", "Invalid pricing model.");
                        }
                        link.InvoiceAmount = invoiceAmount;
                        context.SaveChanges();
                        return link.Id;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public Guid LinkProjectCandidateCategoryDetailType2(Guid projectCandidateId, Guid projectId, Guid projectCategoryDetailTypeId, decimal invoiceAmount)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var project = context.Projects.FirstOrDefault(p => p.Id == projectId);
                        var link = Prepare<ProjectCandidateCategoryDetailType2>();
                        context.ProjectCandidateCategoryDetailTypes2.Add(link);
                        link.Id = Guid.NewGuid();
                        link.ProjectCandidateId = projectCandidateId;
                        link.ProjectCategoryDetailType2Id = projectCategoryDetailTypeId;
                        switch (project.PricingModelId)
                        {
                            case 1: //Time & Material
                                link.InvoiceStatusCode = (int)InvoiceStatusType.Planned;
                                break;
                            case 2: //Fixed price
                                link.InvoiceStatusCode = (int)InvoiceStatusType.FixedPrice;
                                break;
                            default: throw new ArgumentOutOfRangeException("projectId", "Invalid pricing model.");
                        }
                        link.InvoiceAmount = invoiceAmount;
                        context.SaveChanges();
                        return link.Id;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public Guid LinkProjectCandidateCategoryDetailType3(Guid projectCandidateId, Guid projectId, Guid projectCategoryDetailTypeId, string loginCode, DateTime? scheduledDate, decimal invoiceAmount)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var project = context.Projects.FirstOrDefault(p => p.Id == projectId);
                        var link = Prepare<ProjectCandidateCategoryDetailType3>();
                        context.ProjectCandidateCategoryDetailTypes3.Add(link);
                        link.Id = Guid.NewGuid();
                        link.ProjectCandidateId = projectCandidateId;
                        link.ProjectCategoryDetailType3Id = projectCategoryDetailTypeId;
                        link.LoginCode = loginCode;
                        switch (project.PricingModelId)
                        {
                            case 1: //Time & Material
                                link.InvoiceStatusCode = (int)InvoiceStatusType.Planned;
                                break;
                            case 2: //Fixed price
                                link.InvoiceStatusCode = (int)InvoiceStatusType.FixedPrice;
                                break;
                            default: throw new ArgumentOutOfRangeException("projectId", "Invalid pricing model.");
                        }
                        link.InvoiceAmount = invoiceAmount;
                        context.SaveChanges();
                        return link.Id;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }
    }
}
