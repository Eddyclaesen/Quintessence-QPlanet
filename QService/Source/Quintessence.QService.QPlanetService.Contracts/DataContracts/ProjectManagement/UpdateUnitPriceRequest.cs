using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateUnitPriceRequest : UpdateRequestBase
    {
        [DataMember]
        public decimal UnitPrice { get; set; }
    }
}