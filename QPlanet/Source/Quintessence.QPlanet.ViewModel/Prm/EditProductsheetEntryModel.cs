using System;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProductsheetEntryModel : BaseEntityModel
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }

        public Guid ProjectId { get; set; }

        public Guid ProjectPlanPhaseId { get; set; }

        public string ProjectPlanPhaseName { get; set; }

        public Guid ProductTypeId { get; set; }

        public int Quantity { get; set; }

        public decimal InvoiceAmount { get; set; }

        public DateTime Date { get; set; }

        public int InvoiceStatusCode { get; set; }
        
        public DateTime? InvoicedDate { get; set; }

        public string Description { get; set; }

        public InvoiceStatusType Status
        {
            get { return (InvoiceStatusType)InvoiceStatusCode; }
            set { InvoiceStatusCode = (int)value; }
        }
    }
}