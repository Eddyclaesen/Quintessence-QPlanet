using Quintessence.QService.QueryModel.Sec;
using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CreateNewTimesheetEntryModel
    {
        public Guid UserId { get; set; }
        public UserView User { get; set; }

        public Guid ProjectId { get; set; }

        public Guid? ProjectPlanPhaseId { get; set; }

        public Guid? ActivityProfileId { get; set; }

        public int AppointmentId { get; set; }

        public string ActivityName { get; set; }

        public string ProfileName { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal Duration { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal InvoiceAmount { get; set; }

        public DateTime Date { get; set; }

        public int InvoiceStatusCode { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }
    }
}