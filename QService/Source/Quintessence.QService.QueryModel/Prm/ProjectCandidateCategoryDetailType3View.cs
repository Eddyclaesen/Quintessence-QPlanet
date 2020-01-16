using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateCategoryDetailType3View : ProjectCandidateCategoryDetailTypeView
    {
        [DataMember]
        public string LoginCode { get; set; }

        [DataMember]
        public DateTime? Deadline { get; set; }
    }
}