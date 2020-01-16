using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class UpdateActivityDetailTrainingRequest : UpdateRequestBase
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string TargetGroup { get; set; }

        [DataMember]
        public string Duration { get; set; }

        [DataMember]
        public string ExtraInfo { get; set; }

        [DataMember]
        public string ChecklistLink { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public List<UpdateActivityDetailTrainingLanguageRequest> ActivityDetailTrainingLanguages { get; set; }

        [DataMember]
        public List<Guid> SelectedTrainingTypeIds { get; set; }
    }
}