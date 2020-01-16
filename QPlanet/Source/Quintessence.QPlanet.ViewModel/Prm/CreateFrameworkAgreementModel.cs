using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CreateFrameworkAgreementModel
    {
        [Required]
        public string Name { get; set; }

        public int? ContactId { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public string ContactName { get; set; }

        [AllowHtml]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }
    }
}