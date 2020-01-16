using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public enum ProjectCandidateOverviewEntryType
    {
        [EnumMember]
        ProjectCandidate,
        [EnumMember]
        ProjectCandidateCategory
    }
}