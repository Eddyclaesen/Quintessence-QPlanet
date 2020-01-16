using System.Collections.Generic;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminReport
{
    public class CandidateReportDefinitionsActionModel
    {
        public Dictionary<int, string> Contacts { get; set; }

        public List<CandidateReportDefinitionView> Definitions { get; set; }
    }
}