using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateRemarksModel : BaseEntityModel
    {
        [AllowHtml]
        public string Remarks { get; set; }
    }
}