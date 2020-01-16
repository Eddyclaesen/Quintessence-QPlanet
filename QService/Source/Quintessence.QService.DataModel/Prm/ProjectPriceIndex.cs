using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectPriceIndex : EntityBase
    {
        public Guid ProjectId { get; set; }

        public decimal Index { get; set; }

        public DateTime StartDate { get; set; }
    }
}
