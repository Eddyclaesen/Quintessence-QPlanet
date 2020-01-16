using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateResumeFieldModel : BaseEntityModel
    {
        [AllowHtml]
        public string Statement { get; set; }

        public string CandidateReportDefinitionFieldName { get; set; }
    }
}