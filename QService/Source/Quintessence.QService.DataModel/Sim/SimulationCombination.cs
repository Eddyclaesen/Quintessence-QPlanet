using System;
using System.Runtime.Serialization;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Sim
{
    public class SimulationCombination : EntityBase
    {
        public Guid SimulationSetId { get; set; }

        public Guid? SimulationDepartmentId { get; set; }

        public Guid? SimulationLevelId { get; set; }

        public Guid SimulationId { get; set; }

        public int Preparation { get; set; }

        public int Execution { get; set; }

        public int QCandidateLayoutId { get; set; }
        

        
    }
}