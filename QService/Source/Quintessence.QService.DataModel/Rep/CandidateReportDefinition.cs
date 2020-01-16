using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Rep
{
    public class CandidateReportDefinition : EntityBase
    {
        public int? ContactId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }
}