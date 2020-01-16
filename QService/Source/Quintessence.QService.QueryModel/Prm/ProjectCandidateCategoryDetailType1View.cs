using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateCategoryDetailType1View : ProjectCandidateCategoryDetailTypeView, IProjectCandidateCategoryDetailTypeScheduledDateView
    {
        [DataMember]
        public DateTime? ScheduledDate { get; set; }

        [DataMember]
        public int OfficeId { get; set; }
    }
}