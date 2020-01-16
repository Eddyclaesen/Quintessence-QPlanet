using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditActivityProfileModel : BaseEntityModel
    {
        public decimal DayRate { get; set; }

        public decimal HalfDayRate { get; set; }

        public decimal HourlyRate { get; set; }

        public decimal IsolatedHourlyRate { get; set; }

        public string ProfileName { get; set; }
    }
}