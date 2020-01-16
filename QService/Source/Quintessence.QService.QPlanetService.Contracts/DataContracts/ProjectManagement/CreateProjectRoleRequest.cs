using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateProjectRoleRequest
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int? ContactId { get; set; }
    }
}
