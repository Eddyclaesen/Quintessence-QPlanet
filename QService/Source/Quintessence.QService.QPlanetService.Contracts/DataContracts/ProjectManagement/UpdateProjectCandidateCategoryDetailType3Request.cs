using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateCategoryDetailType3Request : UpdateProjectCandidateCategoryDetailTypeRequest
    {
        [DataMember]
        public string LoginCode { get; set; }

        [DataMember]
        public DateTime? Deadline { get; set; }
    }
}