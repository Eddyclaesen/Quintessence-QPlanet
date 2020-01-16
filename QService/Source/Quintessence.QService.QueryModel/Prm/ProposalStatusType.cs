using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum ProposalStatusType
    {
        [EnumMember]
        [EnumMemberName("To evaluate")]
        ToEvaluate = 10,

        [EnumMember]
        [EnumMemberName("To propose")]
        ToPropose = 20,

        [EnumMember]
        [EnumMemberName("Turned down")]
        TurnedDown = 100,

        [EnumMember]
        [EnumMemberName("Proposed")]
        Proposed = 30,

        [EnumMember]
        [EnumMemberName("Stopped")]
        Stopped = 110,

        [EnumMember]
        [EnumMemberName("Not proposed")]
        NotProposed = 120,

        [EnumMember]
        [EnumMemberName("Won")]
        Won = 40,

        [EnumMember]
        [EnumMemberName("Lost")]
        Lost = 50,

        [EnumMember]
        [EnumMemberName("Informative")]
        Informative = 150,
    }
}