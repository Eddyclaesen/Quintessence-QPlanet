using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class DayPlanAssessorView
    {
        [DataMember]
        public Guid AssessorId { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}