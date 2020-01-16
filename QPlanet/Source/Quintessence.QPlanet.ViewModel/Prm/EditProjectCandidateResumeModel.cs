using System.Collections.Generic;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateResumeModel : BaseEntityModel
    {
        public int AdviceId { get; set; }

        [AllowHtml]
        public string Reasoning { get; set; }

        public List<EditProjectCandidateResumeFieldModel> ProjectCandidateResumeFields { get; set; }
    }
}
