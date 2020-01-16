using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Crm;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference=true)]
    public class ProjectCandidateDetailView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public bool IsSuperofficeAppointmentDeleted { get; set; }

        [DataMember]
        public DateTime? AssessmentStartDate { get; set; }

        [DataMember]
        public DateTime? AssessmentEndDate { get; set; }

        [DataMember]
        public int? AssociateId { get; set; }

        [DataMember]
        public Guid? LeadAssessorUserId { get; set; }

        [DataMember]
        public string LeadAssessorFirstName { get; set; }

        [DataMember]
        public string LeadAssessorLastName { get; set; }

        [DataMember]
        public List<CrmAssessorAppointmentView> CoAssessors { get; set; }

        public string LeadAssessorFullName
        {
            get { return string.Format("{0} {1}", LeadAssessorFirstName, LeadAssessorLastName); }
        }
    }
}