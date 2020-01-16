using System;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectPlanPhaseProduct : ProjectPlanPhaseEntry
    {
        public Guid ProductId { get; set; }
        public string Notes { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public bool NoInvoice { get; set; }
        public Guid? ProductsheetEntryId { get; set; }
    }
}
