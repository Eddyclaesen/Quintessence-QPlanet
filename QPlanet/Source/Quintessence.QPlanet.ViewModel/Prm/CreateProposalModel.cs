using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CreateProposalModel
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

        [Display(Name = "Deadline")]
        public DateTime? Deadline { get; set; }

        [Display(Name = "Written proposal")]
        public bool WrittenProposal { get; set; }
    }
}