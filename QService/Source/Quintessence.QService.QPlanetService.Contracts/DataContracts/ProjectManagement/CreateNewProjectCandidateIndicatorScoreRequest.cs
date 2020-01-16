using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectCandidateIndicatorScoreRequest
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public Guid ProjectCandidateCompetenceScoreId { get; set; }

        [DataMember]
        public Guid DictionaryIndicatorId { get; set; }

        [DataMember]
        public string Statement { get; set; }

        [DataMember]
        public decimal? Score { get; set; }
    }
}