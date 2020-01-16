using System.Collections.Generic;
using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Rep
{
    public class CreateNewReportParameterModel
    {
        public string Code { get; set; }

        public string Description { get; set; }

        [AllowHtml]
        public string DefaultText { get; set; }

        public List<CreateNewReportParameterValueModel> Values { get; set; }
    }
}