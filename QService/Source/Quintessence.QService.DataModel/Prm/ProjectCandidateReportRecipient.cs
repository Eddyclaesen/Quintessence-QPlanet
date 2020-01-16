using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCandidateReportRecipient : EntityBase
    {
        public Guid ProjectCandidateId { get; set; }
        public int CrmEmailId { get; set; }
    }
}
