using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectComplaint : EntityBase
    {
        public int CrmProjectId { get; set; }
        public string Subject { get; set; }
        public Guid SubmitterId { get; set; }
        public DateTime? ComplaintDate { get; set; }
        public string ComplaintDetails { get; set; }
        public int ComplaintStatusTypeId { get; set; }
        public int ComplaintSeverityTypeId { get; set; }
        public int ComplaintTypeId { get; set; }
        public string FollowUp { get; set; }
    }
}