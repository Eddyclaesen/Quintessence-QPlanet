using System;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Rep
{
    public class EditCandidateReportDefinitionFieldModel : BaseEntityModel
    {
        public Guid CandidateReportDefinitionId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; }
    }
}