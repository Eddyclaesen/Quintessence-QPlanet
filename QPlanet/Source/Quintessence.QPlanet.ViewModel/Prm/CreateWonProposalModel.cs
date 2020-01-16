using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CreateWonProposalModel
    {
        [Required]
        [Display(Name = "Proposal Name")]
        public string Name { get; set; }

        public int? ContactId { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public string ContactName { get; set; }

        [AllowHtml]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Received on")]
        public DateTime? DateReceived { get; set; }

        [Display(Name = "Final budget")]
        public decimal? FinalBudget { get; set; }

        [Display(Name = "Date won")]
        public DateTime? DateWon { get; set; }

        [Display(Name = "Written proposal")]
        public bool WrittenProposal { get; set; }
    }
}