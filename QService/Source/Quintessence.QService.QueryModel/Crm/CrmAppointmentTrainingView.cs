using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Crm
{
    [DataContract]
    public class CrmAppointmentTrainingView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}