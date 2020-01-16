using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Sim
{
    [DataContract(IsReference = true)]
    public class SimulationMatrixEntryView : SimulationCombinationView
    {
        [DataMember]
        public string SimulationSetName { get; set; }

        [DataMember]
        public string SimulationDepartmentName { get; set; }

        [DataMember]
        public string SimulationLevelName { get; set; }

        [DataMember]
        public string SimulationName { get; set; }
    }
}
