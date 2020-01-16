using System;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateCategoryInvoiceModel : BaseEditInvoiceModel
    {
        public string ProjectCandidateCandidateFullName { get; set; }
        public int CategoryDetailType { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? ProjectCandidateProjectCandidateDetailAssessmentStartDate { get; set; }
    }
}