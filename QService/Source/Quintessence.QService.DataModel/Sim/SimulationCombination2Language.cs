using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QService.DataModel.Sim
{
    public class SimulationCombination2Language
    {
        [Key]
        public Guid SimulationCombinationId { get; set; }
        
        [Key]
        public int LanguageId { get; set; }
    }
}