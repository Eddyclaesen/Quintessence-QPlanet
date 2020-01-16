using System;
using System.Collections.Generic;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Data.Interfaces.QueryContext;

namespace Quintessence.QService.Data.IntegrationTests.Base
{
    public class DataIntegrationTestConfiguration : IConfiguration
    {
        private readonly Dictionary<Type, string> _connectionStringConfigurations = new Dictionary<Type, string>();

        public DataIntegrationTestConfiguration()
        {
            _connectionStringConfigurations.Add(typeof(IQuintessenceQueryContext), "Quintessence");
            Console.WriteLine("Connection string: {0}", GetConnectionStringConfiguration<IQuintessenceQueryContext>());
            _connectionStringConfigurations.Add(typeof(IDomQueryContext), "SharePoint");
        }

        public string GetConnectionStringConfiguration<TContext>()
        {
            return _connectionStringConfigurations[typeof(TContext)];
        }
    }
}
