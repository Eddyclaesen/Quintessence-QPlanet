using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectTypeCategoryDefaultValueRequest : UpdateRequestBase
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}