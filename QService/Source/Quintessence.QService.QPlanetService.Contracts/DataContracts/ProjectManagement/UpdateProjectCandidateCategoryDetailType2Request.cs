using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateCategoryDetailType2Request : UpdateProjectCandidateCategoryDetailTypeRequest
    {
        [DataMember]
        public DateTime? Deadline { get; set; }
    }
}