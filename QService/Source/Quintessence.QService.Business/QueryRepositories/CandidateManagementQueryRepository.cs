using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Cam;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class CandidateManagementQueryRepository : QueryRepositoryBase<ICamQueryContext>, ICandidateManagementQueryRepository
    {
        public CandidateManagementQueryRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public List<CandidateView> ListCandidates()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var candidates = context.Candidates.OrderByDescending(i => i.Audit.CreatedOn).Take(1000)
                            .ToList();

                        return candidates;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<CandidateView> ListCandidatesByFullName(string firstName, string lastName)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var candidates = context.Candidates
                            .Include(cand => cand.ProjectCandidates)
                            .Include(cand => cand.ProjectCandidates.Select(pc => pc.Project))
                            .Where(c => c.FirstName.ToLower() == firstName.ToLower()
                                     && c.LastName.ToLower() == lastName.ToLower()
                                  )
                            .ToList();


                        return candidates;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public CandidateView RetrieveCandidateDetail(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var candidate = context.Candidates
                            .Include(c => c.ProjectCandidates.Select(pc => pc.Project))
                            .Include(c => c.ProjectCandidates.Select(pc => pc.ProjectCandidateDetail))
                            .SingleOrDefault(c => c.Id == id);


                        return candidate;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProgramComponentView> ListProgramComponents(DateTime date)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var startWindow = date;
                        var endWindow = date.AddDays(1).AddSeconds(-1);

                        var programComponents = context.ProgramComponents
                            .Where(pc => pc.Start > startWindow && pc.Start < endWindow)
                            .ToList();

                        return programComponents;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProgramComponentView> ListProgramComponentsByAssessmentRoom(Guid assessmentRoomId, DateTime date)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var startWindow = date;
                        var endWindow = date.AddDays(1).AddSeconds(-1);

                        var programComponents = context.ProgramComponents
                            .Where(pc =>
                                pc.Start > startWindow
                                && pc.Start < endWindow
                                && pc.AssessmentRoomId == assessmentRoomId)
                            .ToList();

                        return programComponents;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProgramComponentView> ListProgramComponentsByCandidate(Guid projectCandidateId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var programComponents = context.ProgramComponents
                            .Where(pc => pc.ProjectCandidateId == projectCandidateId)
                            .ToList();

                        return programComponents;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProgramComponentView> ListProgramComponentsByOfficeId(int officeId, DateTime dateTime)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var programComponents = context.ProgramComponents
                            .Where(pc => pc.AssessmentRoomOfficeId == officeId)
                            .ToList();

                        return programComponents;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public bool CheckForCollisions(ProgramComponentView programComponent)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var hasCollisions = context.ProgramComponents
                            .Any(pc => pc.Id != programComponent.Id
                                && ((pc.LeadAssessorUserId.HasValue && programComponent.LeadAssessorUserId.HasValue && pc.LeadAssessorUserId == programComponent.LeadAssessorUserId)
                                    || (pc.LeadAssessorUserId.HasValue && programComponent.CoAssessorUserId.HasValue && pc.LeadAssessorUserId == programComponent.CoAssessorUserId)
                                    || (pc.CoAssessorUserId.HasValue && programComponent.CoAssessorUserId.HasValue && pc.CoAssessorUserId == programComponent.CoAssessorUserId)
                                    || (pc.CoAssessorUserId.HasValue && programComponent.LeadAssessorUserId.HasValue && pc.CoAssessorUserId == programComponent.LeadAssessorUserId))
                                && (pc.Start < programComponent.End || pc.End > programComponent.Start));

                        return hasCollisions;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProgramComponentView> ListProgramComponentsByUser(Guid userId, DateTime start, DateTime end)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var programComponents = context.ProgramComponents
                            .Where(pc => (pc.LeadAssessorUserId == userId || pc.CoAssessorUserId == userId) && (pc.Start >= start && pc.End <= end))
                            .ToList();

                        return programComponents;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProgramComponentView> ListProgramComponentsByDate(DateTime start, DateTime end)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var programComponents = context.ProgramComponents
                                    .Where(pc => (pc.Start > start && pc.Start < end))
                                    .ToList();

                        return programComponents;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<ProgramComponentView> ListProgramComponentsByRoom(Guid roomId, DateTime start, DateTime end)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var programComponents = context.ProgramComponents
                                    .Where(pc => pc.AssessmentRoomId == roomId && (pc.Start > start && pc.Start < end))
                                    .ToList();

                        return programComponents;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public ProgramComponentView RetrieveLinkedProgramComponent(Guid projectCandidateId, Guid? simulationCombinationId, int simulationCombinationTypeCode)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var programComponent = context.ProgramComponents
                                                       .SingleOrDefault(
                                                           pc =>
                                                           pc.ProjectCandidateId == projectCandidateId &&
                                                           pc.SimulationCombinationId == simulationCombinationId &&
                                                           pc.SimulationCombinationTypeCode !=
                                                           simulationCombinationTypeCode);

                        return programComponent;
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
