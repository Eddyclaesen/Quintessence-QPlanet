using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Sim;

namespace Quintessence.QService.Business.CommandRepositories
{
    public class SimulationManagementCommandRepository : CommandRepositoryBase<ISimCommandContext>, ISimulationManagementCommandRepository
    {
        public SimulationManagementCommandRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public List<SimulationCombination2Language> ListSimulationCombinationLanguages(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulationLanguages = context.SimulationCombinationLanguages.Where(sl => sl.SimulationCombinationId == id);
                        return simulationLanguages.ToList();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public void UnlinkSimulationCombinationLanguage(Guid simulationId, int languageId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulationLanguage = context.SimulationCombinationLanguages.SingleOrDefault(sl => sl.SimulationCombinationId == simulationId && sl.LanguageId == languageId);
                        if (simulationLanguage != null)
                        {
                            context.SimulationCombinationLanguages.Remove(simulationLanguage);
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

        public void LinkSimulationCombinationLanguage(Guid id, int languageId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var simulationLanguage = context.SimulationCombinationLanguages.Add(context.SimulationCombinationLanguages.Create());

                        simulationLanguage.SimulationCombinationId = id;
                        simulationLanguage.LanguageId = languageId;

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
    }
}
