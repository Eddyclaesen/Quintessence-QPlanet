using System.Collections.Generic;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Rep
{
    public class EditReportParameterModel : BaseEntityModel
    {
        public string Code { get; set; }

        public string Description { get; set; }

        [AllowHtml]
        public string DefaultText { get; set; }

        public List<EditReportParameterValueModel> ReportParameterValues { get; set; }
    }
}