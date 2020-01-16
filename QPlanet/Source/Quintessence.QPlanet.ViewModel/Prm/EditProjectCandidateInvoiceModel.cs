using System;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateInvoiceModel : BaseEditInvoiceModel
    {
        public string CandidateFullName { get; set; }
        public DateTime? ProjectCandidateDetailAssessmentStartDate { get; set; }
    }
}