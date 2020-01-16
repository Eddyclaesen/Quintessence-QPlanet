using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Sim
{
    [DataContract(IsReference = true)]
    public class SimulationDepartmentView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }
    }
}