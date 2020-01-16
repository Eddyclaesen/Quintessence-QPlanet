using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectDnaCommercialTranslationRequest : UpdateRequestBase
    {
        [DataMember]
        public string CommercialName { get; set; }

        [DataMember]
        public string CommercialRecap { get; set; }
    }
}