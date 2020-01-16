using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCandidateClusterScore : EntityBase
    {
        public Guid ProjectCandidateId { get; set; }
        public Guid DictionaryClusterId { get; set; }
        public decimal? Score { get; set; }
        public string Statement { get; set; }
    }
}