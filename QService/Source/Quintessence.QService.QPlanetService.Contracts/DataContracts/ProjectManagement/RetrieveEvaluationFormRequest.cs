using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class RetrieveEvaluationFormRequest
    {
        [DataMember]
        public Guid? Id { get; set; }

        [DataMember]
        public string VerificationCode { get; set; }

    }
}