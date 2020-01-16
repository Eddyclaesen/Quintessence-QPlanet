using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCandidateCompetenceScore : EntityBase
    {
        public Guid ProjectCandidateId { get; set; }
        public Guid? ProjectCandidateClusterScoreId { get; set; }
        public Guid DictionaryCompetenceId { get; set; }
        public decimal? Score { get; set; }
        public string Statement { get; set; }
    }
}