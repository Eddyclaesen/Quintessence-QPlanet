using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(AssessmentDevelopmentProjectView))]
    [KnownType(typeof(ConsultancyProjectView))]
    public class ProjectView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid ProjectTypeId { get; set; }

        [DataMember]
        public Guid? ProjectManagerId { get; set; }

        [DataMember]
        public Guid? CoProjectManagerId { get; set; }

        [DataMember]
        public Guid? CustomerAssistantId { get; set; }

        [DataMember]
        public int PricingModelId { get; set; }

        public PricingModelType PricingModelType
        {
            get { return (PricingModelType)PricingModelId; }
            set { PricingModelId = (int)value; }
        }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string DepartmentInformation { get; set; }

        [DataMember]
        public ProjectTypeView ProjectType { get; set; }

        [DataMember]
        public CrmContactView Contact { get; set; }

        [DataMember]
        public UserView ProjectManager { get; set; }

        [DataMember]
        public UserView CoProjectManager { get; set; }

        [DataMember]
        public UserView CustomerAssistant { get; set; }

        [DataMember]
        public List<CrmProjectView> CrmProjects { get; set; }

        [DataMember]
        public bool Confidential { get; set; }

        [DataMember]
        public List<ProjectCategoryDetailView> ProjectCategoryDetails { get; set; }

        [DataMember]
        public List<ProjectRevenueDistributionView> ProjectRevenueDistributions { get; set; }

        public bool HasSubProjectCategoryDetails
        {
            get { return ProjectCategoryDetails != null && ProjectCategoryDetails.Any(pcd => !pcd.ProjectTypeCategory.IsMain); }
        }

        public string ProjectManagerFullName
        {
            get { return ProjectManager == null ? string.Empty : ProjectManager.FullName; }
        }

        public string CoProjectManagerFullName
        {
            get { return CoProjectManager == null ? string.Empty : CoProjectManager.FullName; }
        }

        public string CustomerAssistantFullName
        {
            get { return CustomerAssistant == null ? string.Empty : CustomerAssistant.FullName; }
        }

        public override bool Equals(object obj)
        {
            ProjectView projectObj = obj as ProjectView;
            if (projectObj == null)
                return false;
            else
                return Id.Equals(projectObj.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
