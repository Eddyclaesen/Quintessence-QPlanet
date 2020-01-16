using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateCategoryDetailType2View : ProjectCandidateCategoryDetailTypeView
    {
        [DataMember]
        public DateTime? Deadline { get; set; }
    }
}