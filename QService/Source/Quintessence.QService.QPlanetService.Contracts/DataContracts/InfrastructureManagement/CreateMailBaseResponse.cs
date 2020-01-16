using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement
{
    [KnownType(typeof(CreateCulturalFitInvitationMailResponse))]
    [KnownType(typeof(CreateProjectCandidateInvitationMailResponse))]
    [KnownType(typeof(CreateEvaluationFormMailResponse))]
    [DataContract]
    public class CreateMailBaseResponse
    {
        [DataMember]
        public string To { get; set; }

        [DataMember]
        public string Bcc { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public string Subject { get; set; }
    }
}