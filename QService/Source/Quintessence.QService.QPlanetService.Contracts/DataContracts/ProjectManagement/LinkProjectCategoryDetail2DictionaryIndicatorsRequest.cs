using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class LinkProjectCategoryDetail2DictionaryIndicatorsRequest
    {
        [DataMember(IsRequired = true)]
        public Guid ProjectCategoryDetailId { get; set; }

        [DataMember]
        public List<Guid> DictionaryIndicatorIds { get; set; }

        [DataMember]
        public List<Guid> DictionaryLevelIds { get; set; }

        [DataMember]
        public bool IsDefinedByRole { get; set; }
    }
}