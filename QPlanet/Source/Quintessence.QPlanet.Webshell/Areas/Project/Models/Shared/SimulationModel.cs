using System;
using System.Collections.Generic;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.Shared
{
    public class SimulationModel
    {
        public Guid SimulationSetId { get; set; }

        public string SimulationSetName { get; set; }

        public Guid? SimulationDepartmentId { get; set; }

        public string SimulationDepartmentName { get; set; }

        public Guid? SimulationLevelId { get; set; }

        public string SimulationLevelName { get; set; }

        public Guid SimulationId { get; set; }

        public string SimulationName { get; set; }
    }

    public class SimulationModelComparer : IEqualityComparer<SimulationModel>
    {
        public bool Equals(SimulationModel x, SimulationModel y)
        {
            return x.SimulationSetId == y.SimulationSetId
                   && x.SimulationDepartmentId == y.SimulationDepartmentId
                   && x.SimulationLevelId == y.SimulationLevelId
                   && x.SimulationId == y.SimulationId;
        }

        public int GetHashCode(SimulationModel obj)
        {
            return obj.SimulationSetId.GetHashCode() 
                ^ obj.SimulationDepartmentId.GetHashCode() 
                ^ obj.SimulationLevelId.GetHashCode() 
                ^ obj.SimulationId.GetHashCode();
        }
    }
}