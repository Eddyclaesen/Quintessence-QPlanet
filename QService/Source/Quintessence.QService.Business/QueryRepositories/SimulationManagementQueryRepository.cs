using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Base;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Sim;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class SimulationManagementQueryRepository : QueryRepositoryBase<ISimQueryContext>, ISimulationManagementQueryRepository
    {
        public SimulationManagementQueryRepository(IUnityContainer container)
            : base(container)
        {
        }

        public List<SimulationSetView> ListSimulationSets(PagingInfo pagingInfo = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulationSets = context.SimulationSets.ToList();

                        if (pagingInfo == null)
                            return simulationSets;

                        var filtered = simulationSets
                            .Where(ss => string.IsNullOrWhiteSpace(pagingInfo.FilterTerm)
                                || pagingInfo.HasMatch(ss.Name)).ToList();

                        pagingInfo.TotalRecords = simulationSets.Count;
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

        public List<SimulationDepartmentView> ListSimulationDepartments(PagingInfo pagingInfo = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulationDepartments = context.SimulationDepartments.ToList();

                        if (pagingInfo == null)
                            return simulationDepartments;

                        var filtered = simulationDepartments
                            .Where(ss => string.IsNullOrWhiteSpace(pagingInfo.FilterTerm)
                                || pagingInfo.HasMatch(ss.Name)).ToList();

                        pagingInfo.TotalRecords = simulationDepartments.Count;
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

        public List<SimulationLevelView> ListSimulationLevels(PagingInfo pagingInfo = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulationlevels = context.SimulationLevels.ToList();

                        if (pagingInfo == null)
                            return simulationlevels;

                        var filtered = simulationlevels
                            .Where(ss => string.IsNullOrWhiteSpace(pagingInfo.FilterTerm)
                                || pagingInfo.HasMatch(ss.Name)).ToList();

                        pagingInfo.TotalRecords = simulationlevels.Count;
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

        public List<SimulationView> ListSimulations(PagingInfo pagingInfo = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulations = context.Simulations.ToList();

                        if (pagingInfo == null)
                            return simulations;

                        var filtered = simulations
                            .Where(ss => string.IsNullOrWhiteSpace(pagingInfo.FilterTerm)
                                || pagingInfo.HasMatch(ss.Name)).ToList();

                        pagingInfo.TotalRecords = simulations.Count;
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

        public List<SimulationMatrixEntryView> ListSimulationMatrixEntries(PagingInfo pagingInfo = null)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulationMatrixEntries = context.SimulationMatrixEntries.ToList();

                        if (pagingInfo == null)
                            return simulationMatrixEntries;

                        var filtered = simulationMatrixEntries
                            .Where(ss => string.IsNullOrWhiteSpace(pagingInfo.FilterTerm)
                                || pagingInfo.HasMatch(ss.SimulationSetName, ss.SimulationDepartmentName, ss.SimulationLevelName, ss.SimulationName)).ToList();

                        pagingInfo.TotalRecords = simulationMatrixEntries.Count;
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

        public List<SimulationCombinationLanguageView> ListSimulationCombinationLanguages(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulationCombinationLanguages = context.ListSimulationCombinationLanguages(id).ToList();
                        return simulationCombinationLanguages;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public SimulationView RetrieveSimulation(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulation = context.Simulations
                            .Include(s => s.SimulationTranslations)
                            .SingleOrDefault(s => s.Id == id);
                        return simulation;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public SimulationCombinationView RetrieveSimulationCombination(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulation = context.SimulationCombinations
                            .SingleOrDefault(s => s.Id == id);
                        return simulation;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<SimulationContextView> ListSimulationContexts()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulationContexts = context.SimulationContexts.ToList();
                        return simulationContexts;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<SimulationContextUserView> ListSimulationContextUsers(Guid simulationContextId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulationContextUsers = context.SimulationContextUsers
                            .Where(scu => scu.SimulationContextId == simulationContextId)
                            .ToList();
                        return simulationContextUsers;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<SimulationTranslationView> ListSimulationTranslations(Guid simulationId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulationTranslations = context.SimulationTranslations
                            .Where(st => st.SimulationId == simulationId)
                            .ToList();
                        return simulationTranslations;
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