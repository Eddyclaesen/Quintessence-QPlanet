using System;
using System.Collections.Generic;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Data.Interfaces.QueryContext;

namespace Quintessence.QService.VeniceData.Integration.Tests.Base
{
    public class VeniceDataIntegrationTestConfiguration : IConfiguration
    {
        private readonly Dictionary<Type, string> _connectionStringConfigurations = new Dictionary<Type, string>();

        public VeniceDataIntegrationTestConfiguration()
        {
        }

        public string GetConnectionStringConfiguration<TContext>()
        {
            return _connectionStringConfigurations[typeof(TContext)];
        }
    }
}
