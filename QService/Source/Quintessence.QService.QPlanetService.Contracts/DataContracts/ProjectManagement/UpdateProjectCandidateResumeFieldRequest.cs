using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateResumeFieldRequest : UpdateRequestBase
    {
        [DataMember]
        public string Statement { get; set; }
    }
}