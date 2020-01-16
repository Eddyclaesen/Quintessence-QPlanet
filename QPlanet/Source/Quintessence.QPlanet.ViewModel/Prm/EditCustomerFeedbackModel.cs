using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditCustomerFeedbackModel : BaseEntityModel
    {
        [Display(Name="Phone call remarks")]
        [AllowHtml]
        public string PhoneCallRemarks { get; set; }

        [Display(Name = "Report remarks")]
        [AllowHtml]
        public string ReportRemarks { get; set; }

        [Display(Name = "Report deadline step")]
        public int ReportDeadlineStep { get; set; }

        [Display(Name = "Revision by PM")]
        public bool IsRevisionByPmRequired { get; set; }

        [Display(Name = "Send report to participant")]
        public bool SendReportToParticipant { get; set; }

        [Display(Name = "Remarks on report for participant")]
        [AllowHtml]
        public string SendReportToParticipantRemarks { get; set; }
    }
}