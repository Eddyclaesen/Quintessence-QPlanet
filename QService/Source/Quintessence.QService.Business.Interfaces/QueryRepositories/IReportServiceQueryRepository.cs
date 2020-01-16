using System.Collections.Generic;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface IReportServiceQueryRepository : IQueryRepository
    {
        string GenerateReport(string reportName, Dictionary<string, string> parameters, string convertOutputFormat);
    }
}