using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class RetrieveProjectTypeCategoryRequest
    {
        [DataMember]
        public Guid? Id { get; set; }

        [DataMember]
        public Guid? ProjectCandidateCategoryDetailTypeId { get; set; }
    }
}