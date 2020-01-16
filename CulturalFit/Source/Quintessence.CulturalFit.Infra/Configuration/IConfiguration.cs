namespace Quintessence.CulturalFit.Infra.Configuration
{
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the connection string configuration.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <returns></returns>
        string GetConnectionStringConfiguration<TContext>();

        /// <summary>
        /// Gets the name of the reporting service user.
        /// </summary>
        /// <value>
        /// The name of the reporting service user.
        /// </value>
        string ReportingServiceUserName { get; }

        /// <summary>
        /// Gets the reporting service password.
        /// </summary>
        /// <value>
        /// The reporting service password.
        /// </value>
        string ReportingServicePassword { get; }

        /// <summary>
        /// Gets the reporting service domain.
        /// </summary>
        /// <value>
        /// The reporting service domain.
        /// </value>
        string ReportingServiceDomain { get; }

        /// <summary>
        /// Gets or sets the reporting service report path.
        /// </summary>
        /// <value>
        /// The reporting service report path.
        /// </value>
        string ReportingServiceReportPath { get; }

        /// <summary>
        /// Gets the name theorem list template for company.
        /// </summary>
        /// <value>
        /// The name theorem list template for company.
        /// </value>
        string NameTheoremListTemplateForCompany { get; }

        /// <summary>
        /// Gets the name theorem list template for employees.
        /// </summary>
        /// <value>
        /// The name theorem list template for employees.
        /// </value>
        string NameTheoremListTemplateForEmployees { get; }
 
    }
}
