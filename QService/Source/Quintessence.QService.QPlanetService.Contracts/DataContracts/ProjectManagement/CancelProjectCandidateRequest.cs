using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CancelProjectCandidateRequest : UpdateRequestBase
    {
        [DataMember]
        public DateTime CancelledDate { get; set; }

        [DataMember]
        public string CancelledReason { get; set; }

        [DataMember]
        public decimal CancelledInvoiceAmount { get; set; }
    }
}
