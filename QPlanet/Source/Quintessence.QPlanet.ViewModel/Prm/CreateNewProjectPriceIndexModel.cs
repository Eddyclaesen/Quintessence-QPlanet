using System;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CreateNewProjectPriceIndexModel
    {
        private DateTime? _startDate;
        private decimal? _index;

        public Guid ProjectId { get; set; }

        public decimal Index
        {
            get { return _index.GetValueOrDefault(100); }
            set { _index = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate.GetValueOrDefault(DateTime.Now); }
            set { _startDate = value; }
        }
    }
}
