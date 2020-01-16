using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.DataModel.Sec;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement
{
    [DataContract]
    public class UserCollection
    {
        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

        [DataMember]
        public List<User> Users { get; set; }
    }
}
