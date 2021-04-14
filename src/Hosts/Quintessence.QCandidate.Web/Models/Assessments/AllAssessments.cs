using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QCandidate.Models.Assessments
{
    public class AllAssessments
    {
        private AllAssessments()
        {
        }

        public Guid ProjectId { get; private set; }

        public string FunctionTitle { get; private set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime AppointmentDate { get; private set; }

        public bool IsCancelled { get; private set; }
    }
}