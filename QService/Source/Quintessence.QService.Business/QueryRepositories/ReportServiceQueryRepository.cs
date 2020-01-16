using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.QueryContext;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class ReportServiceQueryRepository : QueryRepositoryBase<IResQueryContext>, IReportServiceQueryRepository
    {
        public ReportServiceQueryRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public string GenerateReport(string reportName, Dictionary<string, string> parameters, string outputformat)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var report = context.GenerateReport(reportName, parameters, outputformat);
                        return report;
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
