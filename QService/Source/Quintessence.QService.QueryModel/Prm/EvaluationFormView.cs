using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(EvaluationFormAcdcView))]
    [KnownType(typeof(EvaluationFormCoachingView))]
    [KnownType(typeof(EvaluationFormCustomProjectsView))]
    public class EvaluationFormView : ViewEntityBase
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
        public int MailStatusTypeId { get; set; }

        [DataMember]
        public string MailStatusName { get; set; }

        [DataMember]
        public string VerificationCode { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageCode { get; set; }

        [DataMember]
        public int EvaluationFormTypeId { get; set; }

        [DataMember]
        public string EvaluationFormName { get; set; }

        [DataMember]
        public bool IsCompleted { get; set; }
    }
}