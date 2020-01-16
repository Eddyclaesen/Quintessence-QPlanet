using System;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CreateNewProjectFixedPriceModel
    {
        private DateTime? _deadline;

        public Guid ProjectId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Deadline
        {
            get { return _deadline.GetValueOrDefault(DateTime.Now); }
            set { _deadline = value; }
        }

        public string InvoiceRemarks { get; set; }
    }
}