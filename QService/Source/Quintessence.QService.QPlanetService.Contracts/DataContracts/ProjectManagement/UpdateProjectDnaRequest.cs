using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectDnaRequest : UpdateRequestBase
    {
        [DataMember]
        public int? CrmContactPersonId { get; set; }

        [DataMember]
        public string Details { get; set; }

        [DataMember]
        public bool ApprovedForReferences { get; set; }

        [DataMember]
        public List<Guid> SelectedProjectDnaTypeIds { get; set; }

        [DataMember]
        public List<UpdateProjectDnaCommercialTranslationRequest> ProjectDnaCommercialTranslations { get; set; }

        [DataMember]
        public List<int> SelectedProjectDnaContactPersonIds { get; set; }
    }
}