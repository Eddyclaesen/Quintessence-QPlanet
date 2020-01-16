using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QService.DataModel.Prm
{
    public class AssessmentDevelopmentProject : Project
    {
        public string FunctionTitle { get; set; }
        public string FunctionInformation { get; set; }
        public Guid? DictionaryId { get; set; }
        public Guid? CandidateReportDefinitionId { get; set; }
        public int CandidateScoreReportTypeId { get; set; }
        public int ReportDeadlineStep { get; set; }
        public string PhoneCallRemarks { get; set; }
        public string ReportRemarks { get; set; }
        public bool IsRevisionByPmRequired { get; set; }
        public bool SendReportToParticipant { get; set; }
        public string SendReportToParticipantRemarks { get; set; }
        public string FunctionTitleEN { get; set; }
        public string FunctionTitleFR { get; set; }
    }
}
