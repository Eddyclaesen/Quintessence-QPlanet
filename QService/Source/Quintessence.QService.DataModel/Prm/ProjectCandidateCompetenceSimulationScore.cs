using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCandidateCompetenceSimulationScore : EntityBase
    {
        public Guid ProjectCandidateId { get; set; }
        public Guid DictionaryCompetenceId { get; set; }
        public Guid SimulationCombinationId { get; set; }
        public decimal? Score { get; set; }
        public string Remarks { get; set; }
    }
}