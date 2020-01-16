using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Crm
{
    [DataContract]
    public class CrmUnregisteredCandidateAppointmentView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime AppointmentDate { get; set; }

        [DataMember]
        public int AssociateId { get; set; }

        [DataMember]
        public Guid? UserId { get; set; }

        [DataMember]
        public int OfficeId { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string AssessorType { get; set; }
    }
}
