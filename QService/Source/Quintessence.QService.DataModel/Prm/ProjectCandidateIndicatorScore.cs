using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCandidateIndicatorScore : EntityBase
    {
        public Guid ProjectCandidateId { get; set; }
        public Guid ProjectCandidateCompetenceScoreId { get; set; }
        public Guid DictionaryIndicatorId { get; set; }
        public decimal? Score { get; set; }
    }
}
