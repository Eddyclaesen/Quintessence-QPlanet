using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [KnownType(typeof(UpdateProjectCandidateInvoicingRequest))]
    [KnownType(typeof(UpdateProjectCandidateCategoryInvoicingRequest))]
    [KnownType(typeof(UpdateProductInvoicingRequest))]
    [DataContract]
    public class UpdateBaseInvoicingRequest : UpdateRequestBase
    {
        [DataMember]
        public int InvoiceStatusCode { get; set; }

        [DataMember]
        public decimal InvoiceAmount { get; set; }

        [DataMember]
        public string InvoiceRemarks { get; set; }
    }
}