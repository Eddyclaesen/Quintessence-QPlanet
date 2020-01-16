using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectRoleTranslationRequest : UpdateRequestBase
    {
        [DataMember]
        public string Text { get; set; }
    }
}