using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class TheoremListRequest : EntityBase
    {
        public int ContactId { get; set; }
        public Guid ProjectId { get; set; }					
        public int? CrmEmailId { get; set; }				
        public Guid? CandidateId { get; set; }				
        public DateTime RequestDate { get; set; }
        public DateTime Deadline { get; set; }					
        public int TheoremListRequestTypeId { get; set; }	
        public string VerificationCode { get; set; }
        public bool IsMailSent { get; set; }
        public string Description { get; set; }
    }
}