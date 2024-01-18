using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateTimesheetEntryRequest : UpdateRequestBase
    {
        [DataMember]
        public Guid ProjectPlanPhaseId { get; set; }

        [DataMember]
        public Guid ActivityProfileId { get; set; }

        [DataMember]
        public decimal Duration { get; set; }

        [DataMember]
        public decimal InvoiceAmount { get; set; }

        [DataMember]
        public int InvoiceStatusCode { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string BceEntity { get; set; }
    }
}