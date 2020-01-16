using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum SurveyPlanningType
    {
        [EnumMember]
        [EnumMemberName("During measuring moment")]
        During = 1,

        [EnumMember]
        [EnumMemberName("Before measuring moment")]
        Before = 2,

        [EnumMember]
        [EnumMemberName("After measuring moment")]
        After = 3,

        [EnumMember]
        [EnumMemberName("Not applicable")]
        NotApplicable = 4
    }
}
