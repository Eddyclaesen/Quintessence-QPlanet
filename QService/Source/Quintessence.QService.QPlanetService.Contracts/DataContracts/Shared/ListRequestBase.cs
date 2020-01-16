using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared
{
    [DataContract]
    public abstract class ListRequestBase
    {
        [DataMember]
        public DataTablePaging DataTablePaging { get; set; }
    }
}
