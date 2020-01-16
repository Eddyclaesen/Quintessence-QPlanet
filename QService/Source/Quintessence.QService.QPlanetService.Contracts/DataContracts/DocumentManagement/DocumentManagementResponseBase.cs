using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DocumentManagement
{
    [DataContract]
    public abstract class DocumentManagementResponseBase
    {
        [DataMember]
        public string DocumentStoreUrl { get; set; }
    }
}
