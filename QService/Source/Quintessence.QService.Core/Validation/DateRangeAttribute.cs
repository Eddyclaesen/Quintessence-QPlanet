using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QService.Core.Validation
{
    public class DateRangeAttribute : ValidationAttribute
    {
        private DateTime? _minimum;
        private DateTime? _maximum;

        public DateTime Minimum
        {
            get { return _minimum.GetValueOrDefault(new DateTime(1975, 1, 1)); }
            set { _minimum = value; }
        }

        public DateTime Maximum
        {
            get { return _maximum.GetValueOrDefault(new DateTime(9999, 12, 31)); }
            set { _maximum = value; }
        }

        public override bool IsValid(object value)
        {
            var date = value as DateTime?;

            return date.HasValue 
                && date.Value >= Minimum 
                && date.Value <= Maximum;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name, Minimum.ToShortDateString(), Maximum.ToShortDateString());
        }
    }
}
