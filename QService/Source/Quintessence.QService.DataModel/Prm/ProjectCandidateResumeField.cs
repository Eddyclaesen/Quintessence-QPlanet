using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCandidateResumeField : EntityBase
    {
        public Guid ProjectCandidateResumeId { get; set; }
        public Guid CandidateReportDefinitionFieldId { get; set; }
        public string Statement { get; set; }
    }
}