using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QCandidate.Models.Assessments
{
    public class SubCategories
    {
        private SubCategories()
        {
        }

        public string Name { get; private set; }
        public string LoginCode { get; private set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime? EndDate { get; private set; }
        public string Code { get; private set; }
        public string LoginLink { get; private set; }
        public bool Completed { get; private set; }
    }
}