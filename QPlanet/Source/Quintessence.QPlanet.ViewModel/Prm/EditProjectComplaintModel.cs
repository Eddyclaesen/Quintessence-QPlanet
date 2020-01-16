using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectComplaintModel : BaseEntityModel
    {
        public int CrmProjectId { get; set; }

        [AllowHtml]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Display(Name = "Submitter")]
        public string Submitter { get; set; }

        [Display(Name = "Complaint date")]
        public DateTime? ComplaintDate { get; set; }

        [AllowHtml]
        [Display(Name = "Details")]
        public string Details { get; set; }

        [Display(Name = "Complaint status")]
        public int ComplaintStatusTypeId { get; set; }

        [Display(Name = "Complaint severity")]
        public int ComplaintSeverityTypeId { get; set; }

        [Display(Name = "Complaint type")]
        public int ComplaintTypeId { get; set; }

        [AllowHtml]
        [Display(Name = "Follow-up")]
        public string FollowUp { get; set; }

    }
}