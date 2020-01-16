using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectPriceIndexModel : BaseEntityModel
    {
        private DateTime? _startDate;

        public Guid ProjectId { get; set; }

        public DateTime StartDate
        {
            get { return _startDate.GetValueOrDefault(DateTime.Now); }
            set { _startDate = value; }
        }

        public decimal Index { get; set; }
    }
}
