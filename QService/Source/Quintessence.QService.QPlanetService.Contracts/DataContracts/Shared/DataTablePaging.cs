using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared
{
    [DataContract]
    public class DataTablePaging
    {
        [DataMember]
        public string FilterTerm { get; set; }

        [DataMember]
        public int Page { get; set; }

        [DataMember]
        public int PageLength { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

        [DataMember]
        public int TotalDisplayRecords { get; set; }
    }
}
