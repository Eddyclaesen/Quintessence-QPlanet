using System;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectFixedPriceModel : BaseEntityModel
    {
        private DateTime? _deadline;

        public Guid ProjectId { get; set; }

        public DateTime Deadline
        {
            get { return _deadline.GetValueOrDefault(DateTime.Now); }
            set { _deadline = value; }
        }

        public decimal Amount { get; set; }

        public string InvoiceRemarks { get; set; }

        public string BceEntity { get; set; }
    }
}