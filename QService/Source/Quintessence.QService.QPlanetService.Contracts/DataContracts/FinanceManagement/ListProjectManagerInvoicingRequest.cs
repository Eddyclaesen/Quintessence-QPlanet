using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement
{
    [DataContract]
    public class ListProjectManagerInvoicingRequest : ListInvoicingBaseRequest
    {
        [DataMember]
        public Guid ProjectManagerId { get; set; }
    }
}