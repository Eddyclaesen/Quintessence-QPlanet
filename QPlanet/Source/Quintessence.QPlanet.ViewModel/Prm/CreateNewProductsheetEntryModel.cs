using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CreateNewProductsheetEntryModel
    {
        private string _name;

        public string Name
        {
            get { return _name ?? string.Format("{0} ({1})", ProductName, ProductTypeName); }
            set { _name = value; }
        }

        public Guid ProductTypeId { get; set; }

        public Guid UserId { get; set; }

        public Guid ProjectId { get; set; }

        public Guid? ProjectPlanPhaseId { get; set; }

        public decimal Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal InvoiceAmount { get; set; }

        public int InvoiceStatusCode { get; set; }

        public string InvoiceRemarks { get; set; }

        public string Description { get; set; }

        public Guid ProductId { get; set; }

        public string ProductTypeName { get; set; }

        public string ProductName { get; set; }

        public string ProjectPlanPhaseName { get; set; }

        public decimal Total { get; set; }

        public decimal Remaining { get; set; }

        public decimal UnitPrice { get; set; }

        public bool IsChecked { get; set; }

        public DateTime Date { get; set; }
    }
}