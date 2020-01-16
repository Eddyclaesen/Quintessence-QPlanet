using System;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCategoryDetail2Competence2Combination
    {
        public Guid Id { get; set; }

        public Guid ProjectCategoryDetailId { get; set; }

        public Guid DictionaryCompetenceId { get; set; }

        public Guid SimulationCombinationId { get; set; }
        
    }
}