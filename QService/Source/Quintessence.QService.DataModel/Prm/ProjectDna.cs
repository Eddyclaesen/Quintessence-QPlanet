using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectDna : EntityBase
    {
        public int CrmProjectId { get; set; }
        public string Details { get; set; }
        public bool ApprovedForReferences { get; set; }
    }
}