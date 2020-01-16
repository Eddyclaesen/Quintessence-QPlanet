using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditTimesheetEntryModel : BaseEntityModel
    {
        public Guid UserId { get; set; }
        public UserView User { get; set; }

        public Guid ProjectId { get; set; }

        public Guid ProjectPlanPhaseId { get; set; }

        public string ProjectPlanPhaseName { get; set; }

        public Guid ActivityProfileId { get; set; }

        public string ActivityProfileName
        {
            get { return string.Format("{0} - {1}", ActivityName, ProfileName); }
        }

        public int AppointmentId { get; set; }

        public string ActivityName { get; set; }

        public string ProfileName { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal Duration { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal InvoiceAmount { get; set; }

        public DateTime Date { get; set; }

        public int InvoiceStatusCode { get; set; }

        public InvoiceStatusType Status
        {
            get { return (InvoiceStatusType)InvoiceStatusCode; }
            set { InvoiceStatusCode = (int)value; }
        }

        public string Description { get; set; }
        public string Category { get; set; }
    }
}