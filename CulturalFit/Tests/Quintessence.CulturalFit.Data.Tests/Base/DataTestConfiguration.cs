using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Quintessence.CulturalFit.Data.Interfaces;
using Quintessence.CulturalFit.Infra.Configuration;

namespace Quintessence.CulturalFit.Data.Tests.Base
{
    public class DataTestConfiguration : IConfiguration
    {
        private readonly Dictionary<Type, string> _connectionStringConfigurations = new Dictionary<Type, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTestConfiguration" /> class.
        /// </summary>
        public DataTestConfiguration()
        {
            var qDataTestContext =
                (ConfigurationManager.ConnectionStrings[string.Format("{0}.CulturalFit", Environment.MachineName)] 
                ?? ConfigurationManager.ConnectionStrings[string.Format("CulturalFit")]).Name;

            Trace.WriteLine(string.Format("Register '{0}' as connection string configuration name.", qDataTestContext));
            _connectionStringConfigurations.Add(typeof(IQContext), qDataTestContext);
        }


        /// <summary>
        /// Gets the connection string configuration.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <returns></returns>
        public string GetConnectionStringConfiguration<TContext>()
        {
            return _connectionStringConfigurations[typeof (TContext)];
        }

        /// <summary>
        /// Gets the name of the reporting service user.
        /// </summary>
        /// <value>
        /// The name of the reporting service user.
        /// </value>
        public string ReportingServiceUserName
        {
            get { return ConfigurationManager.AppSettings["ReportingServiceUserName"]; }
        }

        /// <summary>
        /// Gets the reporting service password.
        /// </summary>
        /// <value>
        /// The reporting service password.
        /// </value>
        public string ReportingServicePassword
        {
            get { return ConfigurationManager.AppSettings["ReportingServicePassword"]; }
        }

        /// <summary>
        /// Gets the reporting service domain.
        /// </summary>
        /// <value>
        /// The reporting service domain.
        /// </value>
        public string ReportingServiceDomain
        {
            get { return ConfigurationManager.AppSettings["ReportingServiceDomain"]; }
        }

        /// <summary>
        /// Gets or sets the reporting service report path.
        /// </summary>
        /// <value>
        /// The reporting service report path.
        /// </value>
        public string ReportingServiceReportPath
        {
            get { return ConfigurationManager.AppSettings["ReportingServiceReportPath"]; }
        }

        /// <summary>
        /// Gets the name theorem list template for company.
        /// </summary>
        /// <value>
        /// The name theorem list template for company.
        /// </value>
        public string NameTheoremListTemplateForCompany
        {
            get { return ConfigurationManager.AppSettings["NameTheoremListTemplateForCompany"]; }
        }

        /// <summary>
        /// Gets the name theorem list template for employees.
        /// </summary>
        /// <value>
        /// The name theorem list template for employees.
        /// </value>
        public string NameTheoremListTemplateForEmployees
        {
            get { return ConfigurationManager.AppSettings["NameTheoremListTemplateForEmployees"]; }
        }
    }
}
