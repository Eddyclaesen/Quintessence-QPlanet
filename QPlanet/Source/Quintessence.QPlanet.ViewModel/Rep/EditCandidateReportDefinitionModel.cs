using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Rep
{
    public class EditCandidateReportDefinitionModel : BaseEntityModel
    {
        public string ContactName { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public bool IsActive { get; set; }
    }
}