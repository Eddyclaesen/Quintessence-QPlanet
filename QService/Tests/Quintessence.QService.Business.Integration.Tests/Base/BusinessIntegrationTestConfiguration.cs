using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.Data.Interfaces.QueryContext;

namespace Quintessence.QService.Business.Integration.Tests.Base
{
    public class BusinessIntegrationTestConfiguration : IConfiguration
    {
        private readonly Dictionary<Type, string> _connectionStringConfigurations = new Dictionary<Type, string>();

        public BusinessIntegrationTestConfiguration()
        {
            var qDataTestContext =
                (ConfigurationManager.ConnectionStrings[string.Format("{0}.Quintessence", Environment.MachineName)]
                ?? ConfigurationManager.ConnectionStrings[string.Format("Quintessence")]).Name;

            Trace.WriteLine(string.Format("Register '{0}' as connection string configuration name.", qDataTestContext));
            _connectionStringConfigurations.Add(typeof(IQuintessenceQueryContext), qDataTestContext);
            _connectionStringConfigurations.Add(typeof(IQuintessenceCommandContext), qDataTestContext);
        }

        public string GetConnectionStringConfiguration<TContext>()
        {
            return _connectionStringConfigurations[typeof(TContext)];
        }
    }
}
