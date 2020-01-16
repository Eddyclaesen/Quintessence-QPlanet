using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectDnaCommercialTranslationsRequest
    {
        [DataMember]
        public Guid ProjectDnaId { get; set; }

        [DataMember]
        public List<int> LanguageIds { get; set; }
    }
}