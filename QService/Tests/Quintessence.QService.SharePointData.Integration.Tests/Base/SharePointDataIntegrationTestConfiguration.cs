using System;
using System.Collections.Generic;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Data.Interfaces.QueryContext;

namespace Quintessence.QService.SharePointData.Integration.Tests.Base
{
    public class SharePointDataIntegrationTestConfiguration : IConfiguration
    {
        private readonly Dictionary<Type, string> _connectionStringConfigurations = new Dictionary<Type, string>();

        public SharePointDataIntegrationTestConfiguration()
        {
            _connectionStringConfigurations.Add(typeof(IDomQueryContext), "SharePoint");
        }

        public string GetConnectionStringConfiguration<TContext>()
        {
            return _connectionStringConfigurations[typeof(TContext)];
        }
    }
}
