using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Sim
{
    [DataContract(IsReference = true)]
    public class SimulationCombinationLanguageView
    {
        [DataMember, Key]
        public int LanguageId { get; set; }

        [DataMember, Key]
        public Guid? SimulationCombinationId { get; set; }

        [DataMember]
        public string LanguageName { get; set; }
    }
}