using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DocumentManagement
{
    [DataContract]
    public class ListProjectDocumentsRequest
    {
        [DataMember]
        public Guid ProjectId { get; set; }
    }
}
