using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectReportRecipient : EntityBase
    {
        public Guid ProjectId { get; set; }
        public int CrmEmailId { get; set; }
    }
}