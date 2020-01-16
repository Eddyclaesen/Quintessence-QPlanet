using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class RetrieveProjectCandidateReportingResponse
    {
        [DataMember]
        public ProjectCandidateView ProjectCandidate { get; set; }

        [DataMember]
        public AssessmentDevelopmentProjectView Project { get; set; }

        [DataMember]
        public CandidateReportDefinitionView CandidateReportDefinition { get; set; }

        [DataMember]
        public List<ReportDefinitionView> ReportDefinitions { get; set; }
    }
}