using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCandidateResume : EntityBase
    {
        public Guid ProjectCandidateId { get; set; }
        public int AdviceId { get; set; }
        public string Reasoning { get; set; }
    }
}
