using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCandidateIndicatorSimulationScore : EntityBase
    {
        public Guid ProjectCandidateId { get; set; }
        public Guid DictionaryIndicatorId { get; set; }
        public Guid SimulationCombinationId { get; set; }
        public decimal? Score { get; set; }
    }
}
