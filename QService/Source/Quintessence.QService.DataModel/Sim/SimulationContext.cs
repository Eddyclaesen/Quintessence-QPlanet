using System.ComponentModel.DataAnnotations;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Sim
{
    public class SimulationContext : EntityBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserNameBase { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int PasswordLength { get; set; }
    }
}