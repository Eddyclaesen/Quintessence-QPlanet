using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Sim
{
    public class SimulationTranslation : EntityBase
    {
        public Guid SimulationId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }
}