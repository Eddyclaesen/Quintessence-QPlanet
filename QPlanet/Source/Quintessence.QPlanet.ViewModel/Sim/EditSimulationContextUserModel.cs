using System;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class EditSimulationContextUserModel : BaseEntityModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public Guid SimulationContextId { get; set; }
    }
}