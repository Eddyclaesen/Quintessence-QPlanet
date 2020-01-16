using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProductsheetEntryRequest : UpdateRequestBase
    {
        [DataMember]
        public int InvoiceStatusCode { get; set; }
    }
}