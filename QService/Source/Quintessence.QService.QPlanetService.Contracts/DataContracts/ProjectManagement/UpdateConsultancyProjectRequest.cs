using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateConsultancyProjectRequest : UpdateRequestBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int PricingModelId { get; set; }

        [DataMember]
        public Guid ProjectManagerId { get; set; }

        [DataMember]
        public Guid? CoProjectManagerId { get; set; }

        [DataMember]
        public Guid? CustomerAssistantId { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public Guid? DictionaryId { get; set; }

        [DataMember]
        public Guid? ProjectTypeCategoryId { get; set; }

        [DataMember]
        public string ProjectInformation { get; set; }

        [DataMember]
        public string DepartmentInformation { get; set; }

        [DataMember]
        public string Remarks { get; set; }
    }
}