using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Sim
{
    [DataContract(IsReference = true)]
    public class SimulationTranslationView : ViewEntityBase
    {
        [DataMember]
        public Guid SimulationId { get; set; }

        [DataMember]
        public SimulationView Simulation { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string SimulationName { get; set; }
    }
}