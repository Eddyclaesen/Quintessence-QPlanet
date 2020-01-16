using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateCancelledProjectCandidateRequest : UpdateRequestBase
    {
        [DataMember]
        public string CancelledReason { get; set; }

        [DataMember]
        public decimal InvoiceAmount { get; set; }
    }
}