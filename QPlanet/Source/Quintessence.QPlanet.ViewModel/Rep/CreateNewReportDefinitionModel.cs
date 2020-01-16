using System.Collections.Generic;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QPlanet.ViewModel.Rep
{
    public class CreateNewReportDefinitionModel
    {
        public List<ReportTypeView> ReportTypes { get; set; }

        public int ReportTypeId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }
    }
}