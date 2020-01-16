using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Inf
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(ProjectSearchResultView))]
    [KnownType(typeof(CandidateSearchResultView))]
    public class SearchResultView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}