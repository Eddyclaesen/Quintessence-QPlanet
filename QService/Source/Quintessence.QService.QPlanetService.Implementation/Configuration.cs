using System;
using System.Collections.Generic;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.Data.Interfaces.QueryContext;

namespace Quintessence.QService.QPlanetService.Implementation
{
    public class Configuration : IConfiguration
    {
        private readonly Dictionary<Type, string> _connectionStringConfigurations = new Dictionary<Type, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration()
        {
            _connectionStringConfigurations.Add(typeof(IQuintessenceQueryContext), "Quintessence");

            _connectionStringConfigurations.Add(typeof(IQuintessenceCommandContext), "Quintessence");

            _connectionStringConfigurations.Add(typeof(IDomQueryContext), "SharePoint");
            _connectionStringConfigurations.Add(typeof(IDomCommandContext), "SharePoint");

            _connectionStringConfigurations.Add(typeof(IRepQueryContext), "ReportingService");
        }

        /// <summary>
        /// Gets the connection string configuration.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <returns></returns>
        public string GetConnectionStringConfiguration<TContext>()
        {
            try
            {
                return _connectionStringConfigurations[typeof(TContext)];
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return null;
            }
        }
    }
}
