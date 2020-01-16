using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateEvaluationFormRequest
    {
        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public int EvaluationFormTypeId { get; set; }
    }
}