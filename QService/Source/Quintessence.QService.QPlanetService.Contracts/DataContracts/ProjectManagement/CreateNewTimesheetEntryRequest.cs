using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewTimesheetEntryRequest
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public Guid? ProjectPlanPhaseId { get; set; }

        [DataMember]
        public Guid? ActivityProfileId { get; set; }

        [DataMember]
        public int AppointmentId { get; set; }

        [DataMember]
        public decimal Duration { get; set; }

        [DataMember]
        public decimal InvoiceAmount { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public int InvoiceStatusCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}