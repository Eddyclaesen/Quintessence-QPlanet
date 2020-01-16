using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ActivityDetailTrainingView : ActivityDetailView
    {
        [DataMember]
        public string TargetGroup { get; set; }

        [DataMember]
        public string ChecklistLink { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Duration { get; set; }

        [DataMember]
        public string ExtraInfo { get; set; }

        [DataMember]
        public List<ActivityDetailTrainingLanguageView> ActivityDetailTrainingLanguages { get; set; }

        [DataMember]
        public List<ActivityDetailTrainingTypeView> ActivityDetailTrainingTypes { get; set; }

        [DataMember]
        public List<ActivityDetailTrainingAppointmentView> TrainingAppointments { get; set; }

        [DataMember]
        public List<ActivityDetailTrainingCandidateView> ActivityDetailTrainingCandidates { get; set; }
    }
}
