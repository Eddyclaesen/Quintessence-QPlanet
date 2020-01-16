using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum EvaluationFormEnumType
    {
        [EnumMember]
        [EnumMemberName("Coaching")]
        Coaching = 10,

        [EnumMember]
        [EnumMemberName("Custom projects")]
        CustomProjects = 20,

        [EnumMember]
        [EnumMemberName("ACDC")]
        Acdc = 30
    }
}