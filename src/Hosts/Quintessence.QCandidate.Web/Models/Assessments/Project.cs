using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QCandidate.Models.Assessments
{
    public class Project
    {
        private Project()
        {
        }

        public Guid ProjectId { get; private set; }

        public string FunctionTitle { get; private set; }

        public string Company { get; private set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime AppointmentDate { get; private set; }
        
        public string AssessmentType { get; set; }
        
        public string Location { get; set; }

        public string Context { get; set; }
        public string LeadAssessor { get; set; }
        public string CoAssessor { get; set; }
        public string ContextUserName { get; set; }
        public string ContextPassword  { get; set; }
        public bool Consent { get; set; }
    }
}