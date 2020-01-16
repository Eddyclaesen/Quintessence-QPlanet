using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CancelProjectCandidateModel : BaseEntityModel
    {
        [Required]
        [Display(Name = "Cancel date")]
        public DateTime CancelledDate { get; set; }

        [Required]
        [Display(Name="Reason for cancel")]
        public string CancelledReason { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public decimal CancelledInvoiceAmount { get; set; }

        public string CandidateFullName { get; set; }
    }
}