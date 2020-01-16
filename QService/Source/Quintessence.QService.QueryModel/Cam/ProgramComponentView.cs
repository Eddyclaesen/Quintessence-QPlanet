using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Cam
{
    [DataContract]
    public class ProgramComponentView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public Guid AssessmentRoomId { get; set; }

        [DataMember]
        public string CandidateFirstName { get; set; }

        [DataMember]
        public string CandidateLastName { get; set; }

        public string CandidateFullName
        {
            get { return string.Format("{0} {1}", CandidateFirstName, CandidateLastName); }
        }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string AssessmentRoomName { get; set; }

        [DataMember]
        public int AssessmentRoomOfficeId { get; set; }

        [DataMember]
        public string AssessmentRoomOfficeShortName { get; set; }

        [DataMember]
        public string AssessmentRoomOfficeFullName { get; set; }

        [DataMember]
        public Guid? SimulationCombinationId { get; set; }

        [DataMember]
        public int SimulationCombinationTypeCode { get; set; }

        [DataMember]
        public string SimulationName { get; set; }

        [DataMember]
        public Guid? ProjectCandidateCategoryDetailTypeId { get; set; }

        [DataMember]
        public string ProjectCategoryDetailTypeName { get; set; }

        [DataMember]
        public Guid? LeadAssessorUserId { get; set; }

        [DataMember]
        public Guid? CoAssessorUserId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime End { get; set; }

        [DataMember]
        public string LeadAssessorUserFirstName { get; set; }

        [DataMember]
        public string LeadAssessorUserLastName { get; set; }

        [DataMember]
        public string CoAssessorUserFirstName { get; set; }

        [DataMember]
        public string CoAssessorUserLastName { get; set; }

        public string LeadAssessorUserFullName
        {
            get { return string.Format("{0} {1}", LeadAssessorUserFirstName, LeadAssessorUserLastName); }
        }

        public string CoAssessorUserFullName
        {
            get { return string.Format("{0} {1}", CoAssessorUserFirstName, CoAssessorUserLastName); }
        }

    }
}
