using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class EvaluationForm : EntityBase
    {
        public int CrmProjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int MailStatusTypeId { get; set; }
        public string VerificationCode { get; set; }
        public int LanguageId { get; set; }
        public int EvaluationFormTypeId { get; set; }
        public bool IsCompleted { get; set; }
    }
}