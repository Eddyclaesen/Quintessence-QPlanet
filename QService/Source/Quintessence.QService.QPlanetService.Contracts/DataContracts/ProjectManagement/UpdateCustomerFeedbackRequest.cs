using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateCustomerFeedbackRequest : UpdateRequestBase
    {
        [DataMember]
        public string PhoneCallRemarks { get; set; }

        [DataMember]
        public string ReportRemarks { get; set; }

        [DataMember]
        public int ReportDeadlineStep { get; set; }

        [DataMember]
        public bool IsRevisionByPmRequired { get; set; }

        [DataMember]
        public bool SendReportToParticipant { get; set; }

        [DataMember]
        public string SendReportToParticipantRemarks { get; set; }
    }
}