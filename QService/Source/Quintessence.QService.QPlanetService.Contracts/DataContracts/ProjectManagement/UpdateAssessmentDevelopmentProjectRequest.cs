using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateAssessmentDevelopmentProjectRequest : UpdateRequestBase
    {
        private List<Guid> _selectedProjectTypeCategoryIds;

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid ProjectManagerId { get; set; }

        [DataMember]
        public Guid? CoProjectManagerId { get; set; }

        [DataMember]
        public Guid? CustomerAssistantId { get; set; }

        [DataMember]
        public int PricingModelId { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public Guid? DictionaryId { get; set; }

        [DataMember]
        public Guid? ProjectTypeCategoryId { get; set; }

        [DataMember]
        public string FunctionTitle { get; set; }

        [DataMember]
        public string FunctionTitleEN { get; set; }

        [DataMember]
        public string FunctionTitleFR { get; set; }

        [DataMember]
        public string FunctionInformation { get; set; }

        [DataMember]
        public string DepartmentInformation { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public bool Confidential { get; set; }        

        [DataMember]
        public bool Roi { get; set; }

        [DataMember]
        public bool Executive { get; set; }

        [DataMember]
        public bool Lock { get; set; }

        [DataMember]
        public List<Guid> SelectedProjectTypeCategoryIds
        {
            get { return _selectedProjectTypeCategoryIds ?? (_selectedProjectTypeCategoryIds = new List<Guid>()); }
            set { _selectedProjectTypeCategoryIds = value; }
        }
    }
}
