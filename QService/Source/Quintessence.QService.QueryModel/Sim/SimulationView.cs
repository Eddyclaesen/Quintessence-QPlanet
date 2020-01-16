using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Sim
{
    [DataContract(IsReference = true)]
    public class SimulationView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<SimulationTranslationView> SimulationTranslations { get; set; }
    }
}
