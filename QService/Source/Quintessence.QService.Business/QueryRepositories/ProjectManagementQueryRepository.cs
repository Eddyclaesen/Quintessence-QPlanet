using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.DataModel.Prm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class ProjectManagementQueryRepository : QueryRepositoryBase<IPrmQueryContext>, IProjectManagementQueryRepository
    {
        public ProjectManagementQueryRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public List<ProjectTypeView> ListProjectTypes()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectTypes = context.ProjectTypes.ToList();

                        return projectTypes;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectView RetrieveProject(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var project = context.Projects

                            .Include(p => p.ProjectType)

                            .SingleOrDefault(p => p.Id == id);

                        return project;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectView RetrieveProjectDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var project = context.Projects

                            .Include(p => p.ProjectType)
                            .Include(p => p.Contact)
                            .Include(p => p.CustomerAssistant)
                            .Include(p => p.ProjectManager)
                            .Include(p => p.ProjectCategoryDetails.Select(pcd => pcd.ProjectTypeCategory))

                            .SingleOrDefault(p => p.Id == id);

                        return project;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public AssessmentDevelopmentProjectView RetrieveAssessmentDevelopmentProjectDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var project = context.Projects
                            .OfType<AssessmentDevelopmentProjectView>()
                            .Include(p => p.ProjectType)
                            .Include(p => p.Contact)
                            .Include(p => p.CustomerAssistant)
                            .Include(p => p.ProjectManager)
                            .Include(p => p.CoProjectManager)
                            .Include(p => p.ProjectCategoryDetails.Select(pcd => pcd.ProjectTypeCategory))

                            .SingleOrDefault(p => p.Id == id);

                        return project;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ConsultancyProjectView RetrieveConsultancyProjectDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var project = context.Projects
                            .OfType<ConsultancyProjectView>()
                            .Include(p => p.ProjectType)
                            .Include(p => p.Contact)
                            .Include(p => p.CustomerAssistant)
                            .Include(p => p.ProjectManager)
                            .Include(p => p.CoProjectManager)
                            .Include(p => p.ProjectCategoryDetails.Select(pcd => pcd.ProjectTypeCategory))

                            .SingleOrDefault(p => p.Id == id);

                        return project;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectTypeCategoryView> ListAvailableProjectCategories(Guid projectTypeId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCategories = context
                            .ProjectTypeCategories
                            .Where(pc => pc.ProjectTypeId == projectTypeId)
                            .ToList();

                        return projectCategories;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCategoryDetailView RetrieveMainProjectCategoryDetail(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectCategoryDetails
                            .Include(pcd => pcd.ProjectTypeCategory)
                            .SingleOrDefault(pcd => pcd.ProjectTypeCategory.IsMain && pcd.ProjectId == projectId);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCategoryDetailView RetrieveMainProjectCategoryDetailDetail(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    var projectCategoryDetail = RetrieveMainProjectCategoryDetail(projectId);
                    using (var context = CreateContext())
                    {
                        ProjectView project;
                        switch (projectCategoryDetail.ProjectTypeCategory.Code)
                        {
                            case "AC":
                            case "DC":
                            case "EA":
                            case "PS":
                            case "SO":
                            case "CA":
                            case "CU":
                                project = context.Projects
                                    .Include(p => p.ProjectCategoryDetails.Select(pcd => pcd.ProjectTypeCategory))
                                    .SingleOrDefault(p => p.Id == projectId);

                                if (project != null)
                                    return project.ProjectCategoryDetails.SingleOrDefault(pcd => pcd.ProjectTypeCategory.IsMain);

                                break;

                            case "CO":
                                project = context.Projects
                                    .Include(p => p.ProjectCategoryDetails.Select(pcd => pcd.ProjectTypeCategory))
                                    .SingleOrDefault(p => p.Id == projectId);

                                if (project != null)
                                    return project.ProjectCategoryDetails.SingleOrDefault(pcd => pcd.ProjectTypeCategory.IsMain);

                                break;

                            case "FA":
                                project = context.Projects
                                    .Include(p => p.ProjectCategoryDetails.Select(pcd => pcd.ProjectTypeCategory))
                                    .SingleOrDefault(p => p.Id == projectId);

                                if (project != null)
                                {
                                    var projectCategoryFaDetail = project.ProjectCategoryDetails.OfType<ProjectCategoryFaDetailView>().SingleOrDefault(pcd => pcd.ProjectTypeCategory.IsMain);
                                    if (projectCategoryFaDetail.ProjectRoleId.HasValue)
                                        projectCategoryFaDetail.ProjectRole = context.ProjectRoles.SingleOrDefault(pr => pr.Id == projectCategoryFaDetail.ProjectRoleId.Value);
                                    return projectCategoryFaDetail;
                                }

                                break;

                            case "FD":
                                project = context.Projects
                                    .Include(p => p.ProjectCategoryDetails.Select(pcd => pcd.ProjectTypeCategory))
                                    .SingleOrDefault(p => p.Id == projectId);

                                if (project != null)
                                {
                                    var projectCategoryFdDetail = project.ProjectCategoryDetails.OfType<ProjectCategoryFdDetailView>().SingleOrDefault(pcd => pcd.ProjectTypeCategory.IsMain);
                                    if (projectCategoryFdDetail.ProjectRoleId.HasValue)
                                        projectCategoryFdDetail.ProjectRole = context.ProjectRoles.SingleOrDefault(pr => pr.Id == projectCategoryFdDetail.ProjectRoleId.Value);
                                    return projectCategoryFdDetail;
                                }

                                break;
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

        public List<ProjectView> ListProjects(PagingInfo pagingInfo = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projects = context.Projects
                            .Include(p => p.ProjectType)
                            .Include(p => p.Contact)
                            .Include(p => p.ProjectManager)
                            .Include(p => p.CustomerAssistant)
                            .OrderByDescending(p => p.Audit.CreatedOn)
                            .ToList();

                        if (pagingInfo == null)
                            return projects;

                        //Orriginele versie
                        var filtered = projects
                            .Where(p => string.IsNullOrWhiteSpace(pagingInfo.FilterTerm)
                                         || pagingInfo.HasMatch(p.Name, p.Contact != null ? p.Contact.FullName : "N/A", p.ProjectType.Name, p.ProjectManagerFullName, p.CustomerAssistantFullName)).ToList();

                        //Versie met verloren functionaliteit.
                        //var filtered = projects
                        //    .Where(p => string.IsNullOrWhiteSpace(pagingInfo.FilterTerm)
                        //                 || pagingInfo.HasMatch(p.Name, p.ProjectManagerFullName, p.CustomerAssistantFullName) || (p.Contact != null && pagingInfo.HasMatch(p.Contact.FullName)) || p.ProjectType != null && pagingInfo.HasMatch(p.ProjectType.Name)).ToList();

                        pagingInfo.TotalRecords = projects.Count;
                        pagingInfo.TotalDisplayRecords = filtered.Count;

                        return filtered.SkipTake(pagingInfo).ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCategoryDetailDictionaryIndicatorView> ListProjectCategoryDetailDictionaryIndicators(Guid projectCategoryDetailId, int? languageId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var indicators = context.ListProjectCategoryDetailDictionaryIndicators(projectCategoryDetailId, languageId).ToList();

                        return indicators;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCategoryDetailSimulationCombinationView> ListProjectCategoryDetailSimulationCombinations(Guid projectCategoryDetailId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var indicators = context.ProjectCategoryDetailDictionaryCombinations
                            .Where(pdcdi => pdcdi.ProjectCategoryDetailId == projectCategoryDetailId)
                            .ToList();

                        return indicators;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectRoleDictionaryLevelView> ListProjectRoleDictionaryLevels(Guid projectRoleId, int? languageId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectRoleDictionaryLevels = context.ListProjectRoleDictionaryLevels(projectRoleId, languageId).ToList();
                            //context.ProjectRoleDictionaryLevels
                            //.Where(prdl => prdl.ProjectRoleId == projectRoleId)
                            //.ToList();

                        return projectRoleDictionaryLevels;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCategoryDetailCompetenceSimulationView> ListProjectCategoryDetailCompetenceSimulations(Guid projectCategoryDetailId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var indicators = context.ProjectCategoryDetailCompetenceSimulations
                            .Where(pdcdi => pdcdi.ProjectCategoryDetailId == projectCategoryDetailId)
                            .ToList();

                        return indicators;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectRoleView> ListProjectRolesForQuintessenceAndContact(int contactId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectRoles = context.ProjectRoles
                            .Where(pr => pr.ContactId == contactId || pr.ContactId == null).ToList();

                        return projectRoles;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectTypeCategoryView RetrieveProjectTypeCategory(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectTypeCategory = context.ProjectTypeCategories
                            .Include(ptc => ptc.ProjectTypeCategoryDefaultValues)
                            .SingleOrDefault(ptc => ptc.Id == id);

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

        public List<ProjectCandidateView> ListProjectCandidates(Guid? projectId = null, Guid? candidateId = null, DateTime? date = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var startWindow = date;
                        var endWindow = date.HasValue ? date.Value.AddDays(1).AddSeconds(-1) : (DateTime?)null;


                        var projectCandidates = context.ProjectCandidates
                            //KC.Include(pc => pc.ProjectCandidateCategoryDetailTypes)
                            //KC.Include(pc => pc.ProjectCandidateProjects)
                            //.Include(pc => pc.ProjectCandidateAssessors.Select(pca => pca.User))
                            .Where(pc => projectId == null || pc.ProjectId == projectId)
                            .Where(pc => candidateId == null || pc.CandidateId == candidateId)
                            .Where(pc => date == null || (pc.ProjectCandidateDetail.AssessmentStartDate > startWindow && pc.ProjectCandidateDetail.AssessmentStartDate < endWindow))
                            .ToList();

                        foreach(var item in projectCandidates)
                        {
                            item.ProjectCandidateCategoryDetailTypes = context.ProjectCandidateCategoryDetailTypes.Where(i => i.ProjectCandidateId == item.Id).ToList();
                            item.ProjectCandidateProjects = context.ProjectCandidateProjects.Where(i => i.ProjectCandidateId == item.Id).ToList();
                        }

                        return projectCandidates;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectTypeCategoryView> ListSubcategories()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var subcategories = context.ProjectTypeCategories
                            .Where(ptc => ptc.SubCategoryType != null)
                            .ToList();

                        return subcategories;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public MainProjectView RetrieveMainProject(Guid subProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var mainProject = context.MainProjects
                            .SingleOrDefault(pp => pp.SubProjectId == subProjectId);

                        return mainProject;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateView> ListUserProjectCandidates(DateTime startDate, DateTime endDate, int? associateId = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidates = context.ProjectCandidates
                            .Include(pc => pc.Project)
                            .Include(pc => pc.Candidate)
                            .Include(pc => pc.ProjectCandidateDetail)
                            .Where(pc => (associateId == null || pc.ProjectCandidateDetail.AssociateId == associateId)
                                && (pc.ProjectCandidateDetail.AssessmentStartDate >= startDate && pc.ProjectCandidateDetail.AssessmentStartDate <= endDate))
                            .ToList();

                        return projectCandidates;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateIndicatorSimulationScoreView> ListProjectCandidateIndicatorSimulationScores(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var scores = context.ProjectCandidateIndicatorSimulationScores
                            .Where(pp => pp.ProjectCandidateId == projectCandidateId)
                            .ToList();

                        return scores;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateCompetenceSimulationScoreView> ListProjectCandidateCompetenceSimulationScores(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var scores = context.ProjectCandidateCompetenceSimulationScores
                            .Where(pp => pp.ProjectCandidateId == projectCandidateId)
                            .ToList();

                        return scores;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateIndicatorSimulationFocusedScoreView> ListProjectCandidateIndicatorSimulationFocusedScores(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var scores = context.ProjectCandidateIndicatorSimulationFocusedScores
                            .Where(pp => pp.ProjectCandidateId == projectCandidateId)
                            .ToList();

                        return scores;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateIndicatorScoreView> ListProjectCandidateIndicatorScores(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var scores = context.ProjectCandidateIndicatorScores
                            .Where(pp => pp.ProjectCandidateId == projectCandidateId)
                            .ToList();

                        return scores;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidateView RetrieveProjectCandidate(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidate = context.ProjectCandidates
                            .Include(pc => pc.ProjectCandidateDetail)
                            .SingleOrDefault(pp => pp.Id == id);

                        return projectCandidate;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidateView RetrieveProjectCandidateDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidate = context.ProjectCandidates
                            .Include(pc => pc.Candidate)
                            .Include(pc => pc.Project)
                            .Include(pc => pc.ProjectCandidateDetail)
                            .SingleOrDefault(pp => pp.Id == id);

                        return projectCandidate;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidateView RetrieveProjectCandidateDetailWithTypes(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidate = context.ProjectCandidates
                            .Include(pc => pc.ProjectCandidateCategoryDetailTypes)
                            .Include(pc => pc.Candidate)
                            .Include(pc => pc.Project)
                            .Include(pc => pc.ProjectCandidateDetail)
                            .SingleOrDefault(pp => pp.Id == id);

                        return projectCandidate;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateClusterScoreView> ListProjectCandidateClusterScores(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 180;
                        //var test = context.ProjectCandidateClusterScores
                        //    .Include(pccls => pccls.DictionaryCluster.DictionaryClusterTranslations)
                        //    .Where(pccls => pccls.ProjectCandidateId == projectCandidateId)
                        //    .ToList();

                        //var test2 = context.ProjectCandidateClusterScores
                        //    .Include(pccls => pccls.DictionaryCluster.DictionaryClusterTranslations)
                        //    .Include(pccls => pccls.ProjectCandidateCompetenceScores.Select(pccos => pccos.DictionaryCompetence.DictionaryCompetenceTranslations))
                        //    .Where(pccls => pccls.ProjectCandidateId == projectCandidateId)
                        //    .ToList();

                        //var test3 = context.ProjectCandidateClusterScores
                        //    .Include(pccls => pccls.DictionaryCluster.DictionaryClusterTranslations)
                        //    .Include(pccls => pccls.ProjectCandidateCompetenceScores.Select(pccos => pccos.DictionaryCompetence.DictionaryCompetenceTranslations))
                        //    .Include(pccls => pccls.ProjectCandidateCompetenceScores.Select(pccos => pccos.ProjectCandidateIndicatorScores))
                        //    .Where(pccls => pccls.ProjectCandidateId == projectCandidateId)
                        //    .ToList();

                        var projectCandidateClusterScores = context.ProjectCandidateClusterScores
                            .Include(pccls => pccls.DictionaryCluster.DictionaryClusterTranslations)
                            .Include(pccls => pccls.ProjectCandidateCompetenceScores.Select(pccos => pccos.DictionaryCompetence.DictionaryCompetenceTranslations))
                            .Include(pccls => pccls.ProjectCandidateCompetenceScores.Select(pccos => pccos.ProjectCandidateIndicatorScores.Select(pcis => pcis.DictionaryIndicator.DictionaryIndicatorTranslations)))
                            .Where(pccls => pccls.ProjectCandidateId == projectCandidateId)
                            .ToList();

                        return projectCandidateClusterScores;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateCompetenceScoreView> ListProjectCandidateCompetenceScores(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidateCompetenceScores = context.ProjectCandidateCompetenceScores
                            .Include(pccos => pccos.DictionaryCompetence.DictionaryCompetenceTranslations)
                            .Include(pccos => pccos.DictionaryCompetence.DictionaryCluster)
                            .Include(pccos => pccos.ProjectCandidateIndicatorScores.Select(pcis => pcis.DictionaryIndicator.DictionaryIndicatorTranslations))
                            .Where(pccls => pccls.ProjectCandidateId == projectCandidateId)
                            .ToList();

                        return projectCandidateCompetenceScores;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidateResumeView RetrieveProjectCandidateResume(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidateResume = context.ProjectCandidateResumes
                            .Include(pcr => pcr.Advice)
                            .Include(pcr => pcr.ProjectCandidateResumeFields)
                            .OrderBy(p => p.Audit.CreatedOn)
                            .SingleOrDefault(pp => pp.ProjectCandidateId == projectCandidateId);

                        return projectCandidateResume;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<AdviceView> ListAdvices()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var advices = context.Advices.ToList();

                        return advices;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateView> ListProjectCandidateDetails(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidateDetails = context.ProjectCandidates
                            .Include(pc => pc.ProjectCandidateCategoryDetailTypes)
                            .Include(pc => pc.ProjectCandidateDetail)
                            .Include(pc => pc.ProjectCandidateProjects)
                            .Where(pcd => pcd.ProjectId == projectId)
                            .ToList();

                        return projectCandidateDetails;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProposalView> ListProposalsByYear(int year)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var proposals = context.Proposals
                            .Where(p => (p.DateReceived.HasValue && p.DateReceived.Value.Year == year)
                                        || (p.DateSent.HasValue && p.DateSent.Value.Year == year))
                            .ToList();

                        return proposals;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<int> ListProposalYears()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var proposals = context.ListProposalYears()
                            .ToList();

                        return proposals;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProposalView RetrieveProposal(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var proposal = context.Proposals
                            .SingleOrDefault(pp => pp.Id == id);

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

        public List<ProjectCandidateResumeView> ListProjectCandidateResumes(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var resumes = context.ProjectCandidateResumes
                            .Include(pcr => pcr.ProjectCandidateResumeFields)
                            .Where(pcr => pcr.ProjectCandidateId == projectCandidateId)
                            .ToList();

                        return resumes;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public FrameworkAgreementView RetrieveFrameworkAgreement(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var frameworkAgreement = context.FrameworkAgreements
                            .SingleOrDefault(fwa => fwa.Id == id);

                        return frameworkAgreement;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<FrameworkAgreementView> ListFrameworkAgreementsByYear(int year)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var frameworkAgreements = context.FrameworkAgreements
                            .Where(fwa => fwa.StartDate.Year == year
                                       || fwa.EndDate.Year == year)
                            .ToList();

                        return frameworkAgreements;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<int> ListFrameworkAgreementYears()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var years = context.ListFrameworkAgreementYears()
                            .ToList();

                        return years;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidateCategoryDetailTypeView RetrieveProjectCandidateCategoryDetailType(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var link = context.ProjectCandidateCategoryDetailTypes
                            .FirstOrDefault(pccdt => pccdt.Id == id);
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

        public List<ProjectCandidateReportRecipientView> ListProjectCandidateReportRecipientsByProjectCandidateId(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var reportRecipients = context.ListProjectCandidateReportRecipients(projectCandidateId)
                            .ToList();

                        return reportRecipients;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ReportStatusView RetrieveReportStatusByCode(string code)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var reportStatus = context.ReportStatuses
                            .SingleOrDefault(rs => rs.Code == code);

                        return reportStatus;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectDocumentMetadataView> ListProjectDocumentMetadatas(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectDocumentMetadatas = context.ProjectDocumentMetadatas
                            .Where(rr => rr.ProjectId == projectId)
                            .ToList();

                        return projectDocumentMetadatas;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateCategoryDetailTypeView> ListProjectCandidateCategoryDetailTypes(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidateCategoryDetailTypes = context.ProjectCandidateCategoryDetailTypes
                            .Where(pccdt => pccdt.ProjectCandidateId == projectCandidateId)
                            .ToList();

                        return projectCandidateCategoryDetailTypes;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateView> ListProjectCandidateDetailsForPlanning(int officeId, DateTime date)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var startWindow = date;
                        var endWindow = date.AddDays(1).AddSeconds(-1);

                        var projectCandidates = context.ProjectCandidates
                            .Include(pc => pc.ProjectCandidateDetail)
                            .Where(pc =>
                                pc.OfficeId == officeId
                                && pc.ProjectCandidateDetail.AssessmentStartDate > startWindow
                                && pc.ProjectCandidateDetail.AssessmentStartDate < endWindow)
                            .ToList();

                        return projectCandidates;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectDnaView RetrieveProjectDna(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectPlan = context.ProjectDnas
                            .Include(pd => pd.ProjectDnaCommercialTranslations)
                            .FirstOrDefault(pp => pp.CrmProjectId == crmProjectId);

                        return projectPlan;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectDnaSelectedTypeView> ListDnaTypes(Guid projectDnaId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var query = context.ListProjectDnaTypes(projectDnaId);
                        var projectDnaTypes = query.ToList();

                        return projectDnaTypes;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectDnaSelectedContactPersonView> ListProjectDnaContactPersons(Guid projectDnaId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var query = context.ListProjectDnaContactPersons(projectDnaId);
                        var projectDnaContactPersons = query.ToList();

                        return projectDnaContactPersons;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectProductView> ListProjectProducts(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectProducts = context.ProjectProducts
                            .Where(pp => pp.ProjectId == projectId)
                            .ToList();

                        return projectProducts;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectEvaluationView RetrieveProjectEvaluationByCrmProject(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectEvaluation = context.ProjectEvaluations.SingleOrDefault(pe => pe.CrmProjectId == crmProjectId);

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

        public string CreateEvaluationFormVerificationCode()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var verificationCode = Guid.NewGuid().ToString().ToUpperInvariant().Substring(0, 6);

                        while (context.EvaluationForms.FirstOrDefault(tl => tl.VerificationCode == verificationCode) != null)
                            verificationCode = Guid.NewGuid().ToString().ToUpperInvariant().Substring(0, 6);

                        return verificationCode;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<EvaluationFormView> ListEvaluationFormsByCrmProject(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var evaluationForms = context.EvaluationForms
                            .Where(er => er.CrmProjectId == crmProjectId)
                            .ToList();

                        return evaluationForms;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectComplaintView> ListProjectComplaintByCrmProject(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectComplaint = context.ProjectComplaints
                            .Where(pc => pc.CrmProjectId == crmProjectId)
                            .ToList();

                        return projectComplaint;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<EvaluationFormTypeView> ListEvaluationFormTypes()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var evaluationFormTypes = context.EvaluationFormTypes
                            .ToList();

                        return evaluationFormTypes;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<MailStatusTypeView> ListMailStatusTypes()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var mailStatusTypes = context.MailStatusTypes
                            .ToList();

                        return mailStatusTypes;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectTypeCategoryUnitPriceView RetrieveProjectTypeCategoryUnitPrice(Guid projectTypeCategoryId, Guid projectTypeCategoryLevelId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectTypeCategoryUnitPrice = context.ProjectTypeCategoryUnitPrices
                            .SingleOrDefault(ptcup => ptcup.ProjectTypeCategoryId == projectTypeCategoryId
                                        && ptcup.ProjectTypeCategoryLevelId == projectTypeCategoryLevelId);

                        return projectTypeCategoryUnitPrice;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ComplaintTypeView> ListComplaintTypes()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var complaintTypes = context.ComplaintTypes
                            .ToList();

                        return complaintTypes;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectComplaintView RetrieveProjectComplaintByCrmProject(int crmProjectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectComplaint = context.ProjectComplaints
                            .SingleOrDefault(pc => pc.CrmProjectId == crmProjectId);

                        return projectComplaint;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<NeopirScoreView> ListNeopirScores(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var neopirScores = context.ListNeopirScores(projectCandidateId).ToList();

                        return neopirScores;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<LeaderScoreView> ListLeiderschapScores(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var neopirScores = context.ListLeiderschapScores(projectCandidateId).ToList();

                        return neopirScores;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCategoryDetailView> ListProjectCategoryDetails(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCategoryDetails = context.ProjectCategoryDetails
                            .Include(pcd => pcd.ProjectTypeCategory)
                            .Where(pcd => pcd.ProjectId == projectId)
                            .ToList();

                        return projectCategoryDetails;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectReportRecipientView> ListProjectReportRecipientsByProjectId(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectReportRecipients = context.ProjectReportRecipients
                            .Where(prr => prr.ProjectId == projectId)
                            .ToList();

                        return projectReportRecipients;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateOverviewEntryView> ListProjectCandidateOverviewEntries(DateTime? startDate = null, DateTime? endDate = null, Guid? customerAssistantId = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidateOverviewEntries = context.RetrieveProjectCandidateAssistantOverview(startDate, endDate, null, customerAssistantId)
                            .ToList();

                        return projectCandidateOverviewEntries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidateOverviewEntryView RetrieveProjectCandidateOverviewProjectCandidateEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.RetrieveProjectCandidateAssistantOverview(null, null, projectCandidateId: id, customerAssistantId: null).ToList();
                        var projectCandidateOverviewEntry = entries.FirstOrDefault(entry => entry.ProjectCandidateCategoryDetailTypeId == null && entry.Id == id);

                        return projectCandidateOverviewEntry;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidateOverviewEntryView RetrieveProjectCandidateOverviewProjectCandidateCategoryEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidateCategoryDetailType = context.ProjectCandidateCategoryDetailTypes.FirstOrDefault(pccdt => pccdt.Id == id);

                        if (projectCandidateCategoryDetailType == null)
                            throw new ArgumentOutOfRangeException("id", string.Format("Unable to retrieve a project candidate category detail type with id {0}", id));

                        List<ProjectCandidateOverviewEntryView> entries;

                        const string type1 = "ProjectCandidateCategoryDetailType1View";
                        const string type2 = "ProjectCandidateCategoryDetailType2View";
                        const string type3 = "ProjectCandidateCategoryDetailType3View";

                        switch (projectCandidateCategoryDetailType.GetType().Name)
                        {
                            case type1:
                                entries = context.RetrieveProjectCandidateCategoryType1AssistantOverview(null, null, projectCandidateCategoryDetailTypeId: id, customerAssistantId: null).ToList();
                                break;

                            case type2:
                                entries = context.RetrieveProjectCandidateCategoryType2AssistantOverview(null, null, projectCandidateCategoryDetailTypeId: id, customerAssistantId: null).ToList();
                                break;

                            case type3:
                                entries = context.RetrieveProjectCandidateCategoryType3AssistantOverview(null, null, projectCandidateCategoryDetailTypeId: id, customerAssistantId: null).ToList();
                                break;

                            default:
                                entries = new List<ProjectCandidateOverviewEntryView>(0);
                                break;
                        }
                        
                        //var projectCandidateOverviewEntry = entries.FirstOrDefault(entry => entry.ProjectCandidateCategoryDetailTypeId != null);
                        var projectCandidateOverviewEntry = entries.FirstOrDefault(entry => entry.ProjectCandidateCategoryDetailTypeId != null && entry.Id == id);

                        return projectCandidateOverviewEntry;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateOverviewEntryView> ListProjectCandidateCategoryOverviewEntries(DateTime? startDate = null, DateTime? endDate = null, Guid? customerAssistantId = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.RetrieveProjectCandidateCategoryType1AssistantOverview(startDate, endDate, null, customerAssistantId)
                            .Concat(context.RetrieveProjectCandidateCategoryType2AssistantOverview(startDate, endDate, null, customerAssistantId))
                            .Concat(context.RetrieveProjectCandidateCategoryType3AssistantOverview(startDate, endDate, null, customerAssistantId))
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateOverviewEntryView> ListProjectCandidateReservedOverviewEntries(DateTime? startDate = null, DateTime? endDate = null, Guid? customerAssistantId = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.RetrieveProjectCandidateReservedAssistantOverview(startDate, endDate, customerAssistantId)
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateOverviewEntryView> ListProjectCandidateCancelledOverviewEntries(DateTime? startDate, DateTime? endDate = null, Guid? customerAssistantId = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.RetrieveProjectCandidateCancelledAssistantOverview(startDate, endDate, customerAssistantId)
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public EvaluationFormView RetrieveEvaluationFormByCode(string verificationCode)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var evaluationForm =
                            context.EvaluationForms.FirstOrDefault(ef => ef.VerificationCode == verificationCode);

                        return evaluationForm;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public bool ValidateQCareVerificationCode(string verificationCode)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var exists =
                            context.EvaluationForms.Any(ef => ef.VerificationCode == verificationCode);

                        return exists;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<TheoremListRequestView> ListCulturalFitContactRequests(int contactId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var requests = context.TheoremListRequests
                            .Where(tlr => tlr.ContactId == contactId && tlr.CandidateId == null)
                            .OrderByDescending(tlr => tlr.Audit.CreatedOn)
                            .ToList();

                        return requests;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateReportingOverviewEntryView> ListProjectCandidateReportingOverviewEntries(DateTime? startDate, Guid? customerAssistantId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.RetrieveProjectCandidateReportingAssistantOverview(startDate, null, customerAssistantId).ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectCandidateReportingOverviewEntryView RetrieveProjectCandidateReportingOverviewProjectCandidateEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.RetrieveProjectCandidateReportingAssistantOverview(null, projectCandidateId: id, customerAssistantId: null).ToList();
                        var projectCandidateOverviewEntry = entries.FirstOrDefault();

                        return projectCandidateOverviewEntry;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<TheoremListRequestView> ListCulturalFitCandidateRequests(Guid candidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var theoremListRequests = context.TheoremListRequests
                            .Where(tlr => tlr.CandidateId == candidateId)
                            .ToList();

                        return theoremListRequests;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectRoleView RetrieveProjectRole(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectRole = context.ProjectRoles
                            .Include(pr => pr.ProjectRoleTranslations)
                            .SingleOrDefault(pr => pr.Id == id);

                        return projectRole;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public bool HasCandidates(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectCandidates.Any(pc => pc.ProjectId == projectId);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public bool HasProjectProducts(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectProducts.Any(pc => pc.ProjectId == projectId);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public bool HasProjectFixedPrices(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.ProjectFixedPrices.Any(pc => pc.ProjectId == projectId);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public TheoremListRequestView RetrieveTheoremListRequestByProjectAndCandidate(Guid projectId, Guid candidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        return context.TheoremListRequests.FirstOrDefault(tlr => tlr.ProjectId == projectId && tlr.CandidateId == candidateId);
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectInvoiceAmountOverviewEntryView> ListProjectInvoiceAmountOverviewEntries(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListProjectInvoiceAmountOverviewEntries(id)
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<SimulationContextLoginView> ListSimulationContextLogins(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListSimulationContextLogins(id)
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateView> ListProjectCandidatesWithCandidateDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ProjectCandidates
                            .Include(pc => pc.Candidate)
                            .Include(pc => pc.ProjectCandidateDetail)
                            .Where(pc => pc.ProjectId == id)
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectTypeCategoryView> ListProjectTypeCategories()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectTypeCategories = context.ProjectTypeCategories
                            .ToList();

                        return projectTypeCategories;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectTypeCategoryLevelView> ListProjectTypeCategoryLevels()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectTypeCategoryLevels = context.ProjectTypeCategoryLevels
                            .ToList();

                        return projectTypeCategoryLevels;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectTypeCategoryUnitPriceView> ListProjectTypeCategoryUnitPrices()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectTypeCategoryUnitPrices = context.ProjectTypeCategoryUnitPrices
                            .ToList();

                        return projectTypeCategoryUnitPrices;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectCandidateView> ListProjectCandidatesWithCategoryDetailTypes(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectCandidates = context.ProjectCandidates
                            .Include(pc => pc.ProjectCandidateCategoryDetailTypes)
                            .Where(pc => pc.ProjectId == projectId)
                            .ToList();

                        return projectCandidates;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectTypeCategoryUnitPriceView> ListProjectTypeCategoryUnitPricesByCategory(Guid projectTypeCategoryId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectTypeCategoryUnitPrices = context.ProjectTypeCategoryUnitPrices
                            .Include(ptc => ptc.ProjectTypeCategoryLevel)
                            .Where(ptc => ptc.ProjectTypeCategoryId == projectTypeCategoryId)
                            .ToList();

                        return projectTypeCategoryUnitPrices;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectPlanView RetrieveProjectPlanDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectPlan = context.ProjectPlans
                            .Include(pp => pp.ProjectPlanPhases.Select(ppf => ppf.ProjectPlanPhaseEntries))
                            .SingleOrDefault(pp => pp.Id == id);

                        return projectPlan;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectPlanPhaseView RetrieveProjectPlanPhaseDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectPlan = context.ProjectPlanPhases
                            .Include(ppf => ppf.ProjectPlanPhaseEntries)
                            .SingleOrDefault(ppf => ppf.Id == id);

                        return projectPlan;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectPlanPhaseActivityView> ListRelatedProjectPlanPhaseActivities(Guid projectPlanPhaseId, Guid activityId, Guid profileId, decimal duration)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ProjectPlanPhaseEntries
                            .OfType<ProjectPlanPhaseActivityView>()
                            .Where(e => e.ProjectPlanPhaseId == projectPlanPhaseId
                                        && e.ActivityId == activityId
                                        && e.ProfileId == profileId
                                        && e.Duration == duration).ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectPlanPhaseProductView> ListRelatedProjectPlanPhaseProducts(Guid projectPlanPhaseId, Guid productId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ProjectPlanPhaseEntries
                            .OfType<ProjectPlanPhaseProductView>()
                            .Where(e => e.ProjectPlanPhaseId == projectPlanPhaseId
                                        && e.ProductId == productId).ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectPlanPhaseEntryView RetrieveProjectPlanPhaseEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entry = context.ProjectPlanPhaseEntries
                            .SingleOrDefault(e => e.Id == id);

                        return entry;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public TEntryType RetrieveProjectPlanPhaseEntry<TEntryType>(Guid id)
            where TEntryType : ProjectPlanPhaseEntryView
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entry = context.ProjectPlanPhaseEntries
                            .OfType<TEntryType>()
                            .SingleOrDefault(e => e.Id == id);

                        return entry;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectPriceIndexView> ListProjectPriceIndices(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var indices = context.ProjectPriceIndices
                            .Where(ppi => ppi.ProjectId == projectId)
                            .ToList();

                        return indices;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<TimesheetEntryView> ListProjectTimesheets(Guid? projectId = null, Guid? userId = null, DateTime? dateFrom = null, DateTime? dateTo = null, bool isProjectManager = false)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.TimesheetEntries
                            .Include(entry => entry.User)
                            .Include(entry => entry.Project)
                            .Include(entry => entry.Project.Contact)
                            .Include(entry => entry.ActivityProfile)
                            .Include(entry => entry.ProjectPlanPhase)
                            .Where(entry =>
                                (projectId == null || entry.ProjectId == projectId)
                                && (userId == null || (entry.UserId == userId && !isProjectManager) || (entry.Project.ProjectManagerId == userId && isProjectManager))
                                && (dateFrom == null || entry.Date >= dateFrom)
                                && (dateTo == null || entry.Date < dateTo))
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectView RetrieveProjectByProjectPlan(Guid projectPlanId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var project = context.Projects
                            .OfType<ConsultancyProjectView>()
                            .SingleOrDefault(p => p.ProjectPlanId == projectPlanId);
                        return project;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectFixedPriceView> ListProjectFixedPrices(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectFixedPrices = context.ProjectFixedPrices
                            .Where(pfp => pfp.ProjectId == id)
                            .ToList();
                        return projectFixedPrices;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProductsheetEntryView> ListProjectProductsheets(Guid? projectId = null, Guid? userId = null, int? year = null, int? month = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ProductsheetEntries
                            .Include(entry => entry.User)
                            .Include(entry => entry.Project)
                            .Include(entry => entry.ProjectPlanPhase)
                            .Where(entry =>
                                (projectId == null || entry.ProjectId == projectId)
                                && (userId == null || entry.UserId == userId)
                                && (year == null || entry.Date.Year == year)
                                && (month == null || entry.Date.Month == month))
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectPlanPhaseView> ListActiveProjectPlanPhases(Guid projectPlanId, int? year, int? month)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var greaterThanStartDate = new DateTime(year.GetValueOrDefault(), month.GetValueOrDefault(), 1).AddMonths(1).AddDays(-1);
                        var smallerThanEndDate = new DateTime(year.GetValueOrDefault(), month.GetValueOrDefault(), 1);

                        var entries = context.ProjectPlanPhases
                            .Where(phase => phase.ProjectPlanId == projectPlanId
                                && phase.StartDate < greaterThanStartDate
                                && phase.EndDate > smallerThanEndDate)
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectPlanPhaseProductView> ListProjectPlanPhaseProducts(Guid projectPlanPhaseId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ProjectPlanPhaseEntries
                            .OfType<ProjectPlanPhaseProductView>()
                            .Where(entry => entry.ProjectPlanPhaseId == projectPlanPhaseId)
                            .ToList();

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<SubProjectView> ListSubProjects(Guid projectId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var subProjects = context.SubProjects
                            .Where(sp => sp.MainProjectId == projectId)
                            .ToList();

                        return subProjects;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectView> ListRecentProjects(Guid userId, PagingInfo pagingInfo = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projects = context.Projects
                            .Include(p => p.ProjectType)
                            .Include(p => p.Contact)
                            .Where(p => p.ProjectManagerId == userId)
                            .OrderByDescending(p => p.Audit.CreatedOn)
                            .ToList();

                        if (pagingInfo == null)
                            return projects;

                        var filtered = projects
                            .Where(ss => string.IsNullOrWhiteSpace(pagingInfo.FilterTerm)
                                         || pagingInfo.HasMatch(ss.Name, ss.Contact.FullName, ss.ProjectType.Name)).ToList();

                        pagingInfo.TotalRecords = projects.Count;
                        pagingInfo.TotalDisplayRecords = filtered.Count;

                        return filtered.SkipTake(pagingInfo).ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProjectView RetrieveProjectWithCrmProjects(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var project = context.Projects

                            .Include(p => p.ProjectType)
                            .Include(p => p.CrmProjects.Select(cp => cp.ProjectStatus))
                            .Include(p => p.ProjectRevenueDistributions)

                            .SingleOrDefault(p => p.Id == id);

                        return project;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<SearchCrmProjectResultItemView> SearchCrmProjects(Guid? projectId, string projectName = null, bool withPlannedStatus = false, bool withRunningStatus = false, bool withDoneStatus = false, bool withStoppedStatus = false)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var result = context
                            .SearchCrmProjects(projectId, projectName, withPlannedStatus, withRunningStatus, withDoneStatus, withStoppedStatus)
                            .ToList();
                        return result;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectRoleView> ListProjectRolesForQuintessence()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectRoles = context.ProjectRoles.Where(pr => pr.ContactId == null).ToList();

                        return projectRoles;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProjectRoleView> ListProjectRolesForContacts()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var projectRoles = context.ProjectRoles
                            .Where(pr => pr.ContactId != null).ToList();

                        return projectRoles;
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
