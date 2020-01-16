using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateRemarksRequest : UpdateRequestBase
    {
        [DataMember]
        public string Remarks { get; set; }
    }
}