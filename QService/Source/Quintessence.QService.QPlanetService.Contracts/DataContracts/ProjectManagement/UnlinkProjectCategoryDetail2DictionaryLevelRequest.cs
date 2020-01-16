using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UnlinkProjectCategoryDetail2DictionaryLevelRequest
    {
        [DataMember]
        public Guid ProjectCategoryDetailId { get; set; }

        [DataMember]
        public Guid? DictionaryCompetenceId { get; set; }
    }
}
