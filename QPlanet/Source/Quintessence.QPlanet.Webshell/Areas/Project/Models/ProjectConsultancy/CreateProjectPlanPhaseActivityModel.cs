using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class CreateProjectPlanPhaseActivityModel
    {
        public List<ActivityView> Activities { get; set; }

        public Guid ActivityProfileId { get; set; }

        public Guid ProjectPlanPhaseId { get; set; }

        public string Notes { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [Display(Name = "Duration (hrs)")]
        public decimal Duration { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public decimal Quantity { get; set; }

        public DateTime Deadline { get; set; }
    }
}