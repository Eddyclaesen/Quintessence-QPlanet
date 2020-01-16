using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Rep
{
    public class CandidateReportDefinitionField : EntityBase
    {
        public Guid CandidateReportDefinitionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}