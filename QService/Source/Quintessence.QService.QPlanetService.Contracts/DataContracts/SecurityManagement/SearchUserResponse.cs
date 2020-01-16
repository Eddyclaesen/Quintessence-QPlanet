using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement
{
    [DataContract]
    public class SearchUserResponse
    {
        [DataMember]
        public List<UserView> Users { get; set; }
    }
}