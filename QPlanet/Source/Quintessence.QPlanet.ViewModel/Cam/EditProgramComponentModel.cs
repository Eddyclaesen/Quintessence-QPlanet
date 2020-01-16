using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Cam
{
    public class EditProgramComponentModel : BaseEntityModel
    {
        [Display(Name = "Candidate")]
        public string CandidateFullName { get; set; }

        [Display(Name = "Contact")]
        public string ContactName { get; set; }

        [Display(Name = "Room")]
        public string AssessmentRoomName { get; set; }

        [Display(Name = "Office")]
        public string AssessmentRoomOfficeFullName { get; set; }

        [Display(Name = "Type")]
        public int SimulationCombinationTypeCode { get; set; }

        [Display(Name = "Simulation")]
        public string SimulationName { get; set; }

        [Display(Name = "Category")]
        public string ProjectCategoryDetailTypeName { get; set; }

        [Display(Name = "Lead assessor")]
        public Guid? LeadAssessorUserId { get; set; }

        [Display(Name = "Co assessor")]
        public Guid? CoAssessorUserId { get; set; }

        public string Description { get; set; }

        [Display(Name = "Start time")]
        public DateTime Start { get; set; }

        [Display(Name = "End time")]
        public DateTime End { get; set; }

        public string EventName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(SimulationName))
                    return string.Format(SimulationCombinationTypeCode == 1 ? "{0} (Preparation)" : "{0} (Execution)", SimulationName);

                if (!string.IsNullOrWhiteSpace(ProjectCategoryDetailTypeName))
                    return ProjectCategoryDetailTypeName;

                return Description;
            }
        }

        public Guid AssessmentRoomId { get; set; }

    }
}