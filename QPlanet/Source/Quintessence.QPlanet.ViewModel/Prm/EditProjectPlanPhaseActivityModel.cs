using System;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectPlanPhaseActivityModel : EditProjectPlanPhaseEntryModel
    {
        public Guid ActivityId { get; set; }

        public string ActivityName { get; set; }

        public Guid ProfileId { get; set; }

        public string ProfileName { get; set; }

        public decimal Duration { get; set; }

        public decimal DayRate { get; set; }

        public decimal HalfDayRate { get; set; }

        public decimal HourlyRate { get; set; }

        public decimal IsolatedHourlyRate { get; set; }

        public decimal Days { get { return Math.Round(Quantity * Duration / 8, 2); } }

        public string Notes { get; set; }

        public override decimal Price
        {
            get
            {
                if (Duration == 4 && HalfDayRate > 0)
                    return Quantity * HalfDayRate;

                if (Duration == 8 && DayRate > 0)
                    return Quantity * DayRate;

                if (Duration == 1 && IsolatedHourlyRate > 0)
                    return Quantity * IsolatedHourlyRate;

                return Quantity * Duration * HourlyRate;
            }
        }

    }
}