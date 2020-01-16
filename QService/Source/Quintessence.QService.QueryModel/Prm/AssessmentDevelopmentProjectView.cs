using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class AssessmentDevelopmentProjectView : ProjectView
    {
        [DataMember]
        public string FunctionTitle { get; set; }

        [DataMember]
        public string FunctionTitleEN { get; set; }

        [DataMember]
        public string FunctionTitleFR { get; set; }

        [DataMember]
        public string FunctionInformation { get; set; }

        [DataMember]
        public Guid? DictionaryId { get; set; }

        [DataMember]
        public Guid? CandidateReportDefinitionId { get; set; }

        [DataMember]
        public int CandidateScoreReportTypeId { get; set; }

        [DataMember]
        public int ReportDeadlineStep { get; set; }

        public ProjectCategoryDetailView MainProjectCategoryDetail
        {
            get { return ProjectCategoryDetails.FirstOrDefault(pcd => pcd.ProjectTypeCategory.IsMain); }
        }

        [DataMember]
        public string PhoneCallRemarks { get; set; }

        [DataMember]
        public string ReportRemarks { get; set; }

        [DataMember]
        public bool IsRevisionByPmRequired { get; set; }

        [DataMember]
        public bool SendReportToParticipant { get; set; }

        [DataMember]
        public string SendReportToParticipantRemarks { get; set; }

        [DataMember]
        public string ProjectTypeCategoryCode { get; set; }
    }
}
