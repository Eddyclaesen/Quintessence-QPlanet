using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Rep;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminReport
{
    public class ReportDefinitionsActionModel
    {
        public List<EditReportDefinitionModel> Definitions { get; set; }

        public List<ReportTypeView> ReportTypes { get; set; }
    }
}