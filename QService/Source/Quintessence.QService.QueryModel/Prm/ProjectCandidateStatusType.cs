using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum ProjectCandidateStatusType
    {
        [EnumMember]
        [EnumMemberName("In progress")]
        InProgress = 10,

        [EnumMember]
        [EnumMemberName("Done")]
        Done = 100
    }
}