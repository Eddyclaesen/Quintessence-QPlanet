using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ListProjectRoleDictionaryIndicatorsRequest
    {
        public ListProjectRoleDictionaryIndicatorsRequest() { }

        public ListProjectRoleDictionaryIndicatorsRequest(Guid projectRoleId, int? languageId = null)
            : this()
        {
            ProjectRoleId = projectRoleId;
            LanguageId = languageId;
        }

        [DataMember]
        public Guid ProjectRoleId { get; set; }

        [DataMember]
        public int? LanguageId { get; set; }
    }
}