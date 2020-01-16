using System.Collections.Generic;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    public interface IResQueryContext : IQuintessenceQueryContext
    {
        string GenerateReport(string reportName, Dictionary<string, string> parameters, string outputformat);
    }
}