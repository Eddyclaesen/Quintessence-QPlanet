using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateRoiScoreView
    {
        [DataMember]
        public Guid ProjectCandidateRoiScoreId { get; set; }

        [DataMember]
        public Guid ProjectCandidateId { get; set; }              

        [DataMember]
        public Guid RoiId { get; set; }

        [DataMember]
        public string RoiQuestion { get; set; }

        [DataMember]
        public int? Score { get; set; }
    }
}
