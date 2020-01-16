using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.WorkspaceManagement
{
    [DataContract]
    public class UpdateUserProfileRequest : UpdateRequestBase
    {
        [DataMember]
        public int LanguageId { get; set; }
    }
}